using CS_Assessment0._1.Models;
using CS_Assessment0._1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace CS_Assessment0._1.Controllers
{
    [Authorize(Roles = "admin,manager,staff,accountant")]

    public class LeaveController : Controller
    {
        A01Context context;
        IUserService<UserInfo,int> userService;
        ILeaveService<Leave, int> leaveService;
        public LeaveController(A01Context context, IUserService<UserInfo, int> userService, ILeaveService<Leave, int> leaveService)
        {
            this.context = context;
            this.userService = userService;
            this.leaveService = leaveService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            Leave leave = new Leave();
            leave.UserId = (await userService.GetUsersAsync()).
                              ToList().
                              Where(u => u.Id == (this.User.FindFirstValue(ClaimTypes.NameIdentifier))).
                              Select(u => u.UserId).
                              FirstOrDefault();

            return View(leave);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Leave leave)
        {
            await leaveService.CreateAsync(leave);
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> LeavesToApprove()
        {
            var list = (await leaveService.GetLeavesAsync()).Where(l => l.Status == null).ToList();
            return View(list);
        }

        public async Task<IActionResult> ApproveLeave(int id)
        {
            await leaveService.ApproveLeaveAsync(id);
            return RedirectToAction("Index", "User");
        }

        public async Task<IActionResult> DeclineLeave(int id)
        {
            await leaveService.DeclineLeaveAsync(id);
            return RedirectToAction("Index", "User");
        }
    }
}
