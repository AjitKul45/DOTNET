using Asset_Management.CustomMiddleware;
using Asset_Management.Models;
using Asset_Management.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Asset_Management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class AssetDetailsController : ControllerBase
    {
        IService<AssetDetail, int> assetService;
        IAssetDetailService<AssetDetail, string> assetDetailsService;

        public AssetDetailsController(IService<AssetDetail, int> assetService,IAssetDetailService<AssetDetail,string> assetDetailsService)
        {
            this.assetService = assetService;
            this.assetDetailsService = assetDetailsService;
        }

        [HttpGet]
        //[ServiceFilter(typeof(LogFilterAttribute))]
        //[LogFilter]
        public async Task<IActionResult> GetAllAssetDetails()
        {
            return Ok(await assetService.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetDetails(int id)
        {
            return Ok(await assetService.GetAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsset(AssetDetail assetDetail)
        {
            if (ModelState.IsValid)
            {
                var record = await assetService.CreateAsync(assetDetail);
                return Ok(record);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsset(int id,AssetDetail assetDetail)
        {
            if (ModelState.IsValid)
            {
                
                return Ok(await assetService.UpdateAsync(id, assetDetail));
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsset(int id)
        {
            if (id > 0 )
            {
                await assetService.DeleteAsync(id);
                return Ok(await assetService.GetAsync());
            }
            else
            {
                return BadRequest("Delete faild");
            }
        }

        [HttpGet]
        [ActionName("GetAssetsByType")]
        public async Task<IActionResult> GetAssetsByType([FromQuery]string type)
        {
            if (type != null)
            {
                return Ok(await assetDetailsService.GetByTypeAsync(type));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetsByVendor(string id)
        {
            if (id != null)
            {
                return Ok(await assetDetailsService.GetByVendorAsync(id));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAssetsCount()
        {
            return Ok(await assetDetailsService.GetAssetCount());
        }

        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            return Ok(await assetDetailsService.GetCountOfNotAssignedAssets());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAssetCount()
        {
            return Ok((await assetService.GetAsync()).Count());
        }
    }
}
