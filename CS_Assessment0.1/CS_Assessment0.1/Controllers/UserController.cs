using CS_Assessment0._1.Data;
using CS_Assessment0._1.Models;
using CS_Assessment0._1.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CS_Assessment0._1.Controllers
{
    public class UserController : Controller
    {
        public A01Context context;
        public ApplicationDbContext appContext;
        public IUserService<UserInfo,int>  userService;
        IConsultantService<Consultant,int> consultantService;
        ISalarySlipService<SalarySlip,int> salarySlipService;
        IPaySlipService paySlipService;
        RoleManager<IdentityRole> roleManager;
        UserManager<IdentityUser> userManager;
        public UserController(A01Context context,
            ApplicationDbContext appContext,
            IUserService<UserInfo, int> userService,
            IConsultantService<Consultant, int> consultantService,
            ISalarySlipService<SalarySlip, int> salarySlipService,
            IPaySlipService paySlipService,
            RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.appContext = appContext;
            this.userService = userService;
            this.consultantService = consultantService;
            this. salarySlipService =  salarySlipService;
            this.paySlipService = paySlipService;
            this.roleManager = roleManager;
            this.userManager = userManager;
            
        }
        [Authorize(Roles = "admin")]
        async public Task<IActionResult> Admin()
        {
            ViewBag.id = (await userService.GetUsersAsync()).Where(u => u.Id == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Select(u => u.UserId).FirstOrDefault();

            return View();
        }

        [Authorize(Roles ="manager")]
        async public Task<IActionResult> Manager()
        {
            ViewBag.id = (await userService.GetUsersAsync()).Where(u => u.Id == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Select(u => u.UserId).FirstOrDefault();

            return View();
        }

        [Authorize(Roles ="accountant")]
        async public Task<IActionResult> Accountant()
        {
            ViewBag.id = (await userService.GetUsersAsync()).Where(u => u.Id == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Select(u => u.UserId).FirstOrDefault();

            return View();
        }

        [Authorize(Roles = "staff")]
        async public Task<IActionResult> Staff()
        {
            ViewBag.id = (await userService.GetUsersAsync()).Where(u => u.Id == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Select(u => u.UserId).FirstOrDefault();

            return View();
        }

        [Authorize(Roles = "consultant")]
        async public Task<IActionResult> Consultant()
        {
            ViewBag.id = (await userService.GetUsersAsync()).Where(u => u.Id == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Select(u => u.UserId).FirstOrDefault();

            return View();
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.id = (await userService.GetUsersAsync()).Where(u=>u.Id == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).Select(u => u.UserId).FirstOrDefault();
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);
            var role = (await userManager.GetRolesAsync(user)).FirstOrDefault();

            if (role == "admin")
                return RedirectToAction("Admin");
            else if (role == "manager")
                return RedirectToAction("Manager");
            else if (role == "staff")
                return RedirectToAction("Staff");
            else if (role == "accountant")
                return RedirectToAction("Accountant");
            else if (role == "consultant")
                return RedirectToAction("Consultant");

            return View();
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            UserInfo? user = new UserInfo();
            var usertype = new List<SelectListItem>();
            usertype.Add(new SelectListItem("Full Time", "Full Time"));
            usertype.Add(new SelectListItem("Part Time", "Part Time"));
            ViewBag.usertype = usertype;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserInfo user)
        {
            user.Id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var usertype = new List<SelectListItem>();
            //Full time for staff
            usertype.Add(new SelectListItem("Full Time", "Full Time"));
            //Part time for consultant
            usertype.Add(new SelectListItem("Part Time", "Part Time"));
            ViewBag.usertype = usertype;

            if ((await userService.GetUsersAsync()).Select(u => u.Id).ToList().Contains(user.Id))
            {
                throw new Exception("Profile already created!");
            }

            await userService.Create(user);

            return RedirectToAction("Index","User");
        }

        [Authorize(Roles ="admin,manager,staff,consultant")]
        public async Task<IActionResult> Edit(int id)
        {
            var user =  await context.UserInfos.FindAsync(id);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserInfo user)
        {
            await userService.Update(id, user);
            return RedirectToPage("Index");
        }

        [Authorize(Roles = "manager")]
        public async Task<IActionResult> ConsultantDetailsToApprove()
        {
            var consultants = (await consultantService.GetConsultantDetailsAsync()).Where(c => c.Status == null).ToList();
            return View(consultants);
        }

        [Authorize(Roles = "manager")]
        public async Task<IActionResult> ApproveConsultantDetail(int id)
        {
            await consultantService.ApproveDetailAsync(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "manager")]
        public async Task<IActionResult> DeclineConsultantDetail(int id)
        {
            await consultantService.DeclineDetailAsync(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "manager,admin,staff")]
        public async Task<IActionResult> ViewSalarySlip(int id)
        {
            var slips = (await salarySlipService.GetListAsync()).Where(s=>s.UserId== id).ToList(); 
            return View(slips);
        }

        [Authorize(Roles ="manager,admin,staff")]
        public async Task<IActionResult> DetailedPaySlip(int id)
        {
            var slip = await paySlipService.DetailPaySlip(id);
            return View(slip);
        }


    }
}
