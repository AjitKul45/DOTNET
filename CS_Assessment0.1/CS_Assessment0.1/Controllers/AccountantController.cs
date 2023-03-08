using CS_Assessment0._1.Models;
using CS_Assessment0._1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CS_Assessment0._1.Controllers
{
    [Authorize(Roles ="accountant")]
    public class AccountantController : Controller
    {
        A01Context context;
        IUserService<UserInfo,int> userService;
        ISalarySlipService<SalarySlip, int> service;
        IConsultantService<Consultant, int> consultantService;

        public AccountantController(A01Context context, ISalarySlipService<SalarySlip, int> service, IUserService<UserInfo, int> userService)
        {
            this.context = context;
            this.service = service;
            this.userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddSalaryDetails()
        {
            var users = new List<SelectListItem>();

            foreach (var item in context.UserInfos.Where(u=>u.Type.Equals("Full Time")).ToList())
            {
                var fullname = item.FirstName + " " + item.LastName;
                users.Add(new SelectListItem(fullname, item.UserId.ToString()));
            };

            ViewBag.user = users;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddSalaryDetails(Salary salary)
        {
            if (salary.BasicPay < 250000)
                salary.Tds = 0;
            else if (salary.BasicPay < 500000)
                salary.Tds = (int)(0.05 * salary.BasicPay);
            else if (salary.BasicPay < 100000)
                salary.Tds = (int)(0.2 * salary.BasicPay);
            else
                salary.Tds = (int)(0.3 * salary.BasicPay);
                
            await context.Salaries.AddAsync(salary);
            await context.SaveChangesAsync();
            return RedirectToAction("Index", "User");
        }

        public IActionResult CalculateSal()
        {
            return View(new calSal());
        }

        [HttpPost]
        public async Task<IActionResult> CalculateSal(calSal dates)
        {
            await service.CalculateSalary(dates.From,dates.To);
            return RedirectToAction("Index", "User");
        }

        public IActionResult ConsultantBill()
        {
            var consultants = new List<SelectListItem>();

            foreach (var item in context.UserInfos.Where(u => u.Type.Equals("Part Time")).ToList())
            {
                var fullname = item.FirstName + " " + item.LastName;
                consultants.Add(new SelectListItem(fullname, item.UserId.ToString()));
            };

            ViewBag.user = consultants;

            return View(new CalBill());
        }
        [HttpPost]
        public async Task<IActionResult> ConsultantBill(CalBill? bill)
        {
            var con = (await userService.GetAsync(bill.Cid));
            var details = await consultantService.GetConsultantDetailsAsync(bill.Cid);
            return View();

        }




    }
}
