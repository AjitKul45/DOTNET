
using Asset_Management.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Asset_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using System.Data;
using Asset_Management.CustomMiddleware;

namespace Asset_Management.Controllers
{
    [Route("api/[controller]/[action]")]
    /// USed to Map the Received JSON Data from Http POST and PUT Request to CLR
    /// Object
    [ApiController]
    //[TypeFilter(typeof(LogFilterAttribute))]
    //[Authorize(Roles = "Admin,Member")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class VendorController : Controller
    {

        IService<Vendor, int> vendorService;
        public VendorController(IService<Vendor, int> vendorService)
        {

            this.vendorService = vendorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVendors()
        {
            var record = await vendorService.GetAsync();
            if (record == null) { return NotFound("Record not found"); }
            return Ok(record);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVendor(int id)
        {
            var record = await vendorService.GetAsync(id);
            return Ok(record);
        }
        [HttpPost]
        public async Task<IActionResult> AddVendor(Vendor data)
        {
            if (ModelState.IsValid)
            {


                var record = await vendorService.CreateAsync(data);

                return Ok(record);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(int id, Vendor data)
        {
            if (ModelState.IsValid)
            {
                var record = await vendorService.UpdateAsync(id, data);

                return Ok(record);

            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var record = await vendorService.DeleteAsync(id);

            return Ok(await vendorService.GetAsync());


        }

        [HttpGet]
        public async Task<IActionResult> GetVendorStatus()
        {
            return Ok(await vendorService.GetStatus());
        }

    }
}
