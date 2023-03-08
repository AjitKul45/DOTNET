using CS_Assessment0._1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CS_Assessment0._1.Controllers
{
    [Authorize(Roles ="admin,manager,staff,accountant")]
    public class InvestmentController : Controller
    {
        A01Context context;

        public InvestmentController(A01Context context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            Investment investment = new Investment();
            investment.UserId = context.
                              UserInfos.
                              ToList().
                              Where(u => u.Id == (this.User.FindFirstValue(ClaimTypes.NameIdentifier))).
                              Select(u => u.UserId).
                              FirstOrDefault();

            return View(investment);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Investment investment)
        {
            await context.Investments.AddAsync(investment);
            await context.SaveChangesAsync();
            return RedirectToAction("Index","User");
        }
    }
}
