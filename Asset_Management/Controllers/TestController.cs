using Asset_Management.Models;
using Asset_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Asset_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        IService<User,int> _userService;
        public TestController(IService<User,int> userService) 
        {
            this._userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _userService.GetAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(User u)
        {
            return Ok(await _userService.CreateAsync(u));
        }
    }
}
