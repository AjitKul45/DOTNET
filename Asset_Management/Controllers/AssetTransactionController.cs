using Asset_Management.Models;
using Asset_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Asset_Management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class AssetTransactionController : ControllerBase
    {
        IService<AssetTransaction, int> assetService;
        IAssetTransactionService<AssetTransaction,string> assetTransactionService;
        public AssetTransactionController(IService<AssetTransaction, int> assetService, IAssetTransactionService<AssetTransaction, string> assetTransactionService)
        {
            this.assetService = assetService;
            this.assetTransactionService = assetTransactionService;
        }

        [HttpGet]
        public async Task<IActionResult> ListOfAssetTransactions()
        {
           var result = await assetService.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetTransaction(int? id) 
        {
            if(id == null || id == 0)
            {
                return BadRequest();
            }
            var record = await assetService.GetAsync((int)id);
            return Ok(record);
        }
        [HttpPost]
        public async Task<IActionResult> AssignAsset( AssetTransaction assetTransaction)
        {
            if(ModelState.IsValid)
            {
                var result = await assetService.CreateAsync(assetTransaction);
                return Ok(result);
            }
            throw new Exception("Please provide correct information");
           
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssetTransaction([FromBody] AssetTransaction assetTransaction, int id)
        {
            if(ModelState.IsValid) 
            {
                var result = await assetService.UpdateAsync(id, assetTransaction);
                if (result == null)
                {
                    return NotFound($"Record not found with Id {id}");
                }
                return Ok(result);
            }
            throw new Exception("Please provide correct information");
        }
        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteAssetTransaction(int id)
        {
            var result = await assetService.DeleteAsync(id);
            if(result == null)
            {
                return NotFound($"Record not found with Id {id}");
            }
            return Ok(await assetService.GetAsync());
        }
        [HttpGet("get_by_email/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var record = await assetTransactionService.getAssetTransactionByEmail(email);
            if(record != null)
                 return Ok(record);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetByDept([FromQuery]string dept)
        {
            if (dept != null)
            {
                return Ok(await assetTransactionService.GetByDeptAsync(dept));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByLocation([FromQuery] string location)
        {
            if (location != null)
            {
                return Ok(await assetTransactionService.GetByDeptAsync(location));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery]string search)
        {
            if (search != null)
            {
                var record = await assetTransactionService.Search(search);
                return Ok(record);
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }

    }
}
