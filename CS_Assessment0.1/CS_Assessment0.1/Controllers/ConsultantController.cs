using CS_Assessment0._1.Models;
using CS_Assessment0._1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CS_Assessment0._1.Controllers
{
    [Authorize(Roles ="consultant")]
    public class ConsultantController : Controller
    {
        A01Context context;
        IUserService<UserInfo, int> userService;
        IConsultantService<Consultant, int> consultantService;
        public ConsultantController(A01Context context,IUserService<UserInfo,int> userService, IConsultantService<Consultant, int> consultantService)
        {
            this.context = context;
            this.userService = userService;
            this.consultantService = consultantService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddDetails()
        {
            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Consultant consultant= new Consultant();

            consultant.UserId = (await userService.GetUsersAsync()).
                                Where(x => x.Id == id).
                                Select(u => u.UserId).FirstOrDefault();
                                
                                
            var user = await userService.GetAsync((int)consultant.UserId);
            consultant.FirstName = user.FirstName;
            consultant.LastName = user.LastName;
            return View(consultant);
        }

        [HttpPost]
        public async Task<IActionResult> AddDetails(Consultant consultant)
        {
            var id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            consultant.UserId = (await userService.GetUsersAsync()).
                                Where(x => x.Id == id).
                                Select(u => u.UserId).FirstOrDefault();
            
            await consultantService.CreateAsync(consultant);
            return RedirectToAction("Index","User");
        }
    }
}
