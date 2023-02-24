using Asset_Management.Models;
using Asset_Management.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace Asset_Management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        IService<User, int> _userService;
        IAuthService<User, int> _authService;
        public UserController(IService<User, int> userService, IAuthService<User, int> auth)
        {
            this._userService = userService;
            this._authService = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var result = await _userService.GetAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            try
            {
                var result = await _userService.GetAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _userService.CreateAsync(user);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var record = await _userService.UpdateAsync(id, user);
                    return Ok(record);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var record = await _userService.DeleteAsync(id);
                return Ok(record);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]   
        
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _authService.Login(user);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
