//using Core_MVCApp.CustomFilters;
//using Core_MVCApp.Data;
//using Core_MVCApp.models;
//using Core_MVCApp.Services;
using CS_Assessment0._1.CustomFilter;
using CS_Assessment0._1.Data;
using CS_Assessment0._1.Models;
using CS_Assessment0._1.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// WebApplication builder obj responsible for creating collection of all those obj used to design the current asp.net core app
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// This is DI container
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// register entity framework in DI container
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    // create the EF core instance for SQL Server
    options.UseSqlServer(connectionString));

//registr the companyContext in DI container
builder.Services.AddDbContext<A01Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection"));
});

// register session service here
// add cache for storing session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

//register serivce classes in DI container
builder.Services.AddScoped<IUserService<UserInfo, int>, UserService>();
builder.Services.AddScoped<IConsultantService<Consultant, int>, ConsultantService>();
builder.Services.AddScoped<ILeaveService<Leave, int>, LeaveService>();
builder.Services.AddScoped<ISalarySlipService<SalarySlip, int>, SalarySlipService>();
builder.Services.AddScoped < IPaySlipService, PaySlipService>();




// service obj for default error page for database error
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// service obj for registring following classes UserManager<IdentitUser>,signinmanger<identityuser>
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI();

// serivce obj for request processing obj for MVC controller and views
builder.Services.AddControllersWithViews(options =>
{
    //options.Filters.Add(new LogFilterAttribute());
    //// the IModelMetadataProvider and ModelStateDIctionary
    //// will be resolved using MvcOPtions used by FIlters.Add()
    //options.Filters.Add(typeof(CustomExceptionFilterAttribute));
});

// WebApplication builder class to create HTTP pipeline and register middleware in it
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // Exception filter
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // HTTP securitycontext for transport security AKA SSL
    app.UseHsts();
}

// http ---> https
app.UseHttpsRedirection();
// reads file form wwwroot by default
app.UseStaticFiles();
// strict routing by creating raoute table
app.UseRouting();
// cofigure session middleware
app.UseSession();
// identity middlewares
app.UseAuthentication();
app.UseAuthorization();
app.UseAdminMiddleware();
    // map the req with mvc controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// map the req with razor view
app.MapRazorPages();

// run the application
app.Run();
