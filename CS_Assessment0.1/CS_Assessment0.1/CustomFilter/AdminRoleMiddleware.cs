using Microsoft.AspNetCore.Identity;

namespace CS_Assessment0._1.CustomFilter
{
    public class AdminRoleMiddleware
    {
        RequestDelegate _next;

        public AdminRoleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider service)
        {
            var roleManager = service
                .GetRequiredService<RoleManager<IdentityRole>>();
            //var role = service.GetRequiredService<RoleStore<IdentityRole>>();
            var roleName = "admin";

            IdentityResult result;
            //var roleExist = ;
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                result = await roleManager
                    .CreateAsync(new IdentityRole(roleName));
                if (result.Succeeded)
                {
                    var userManager = service
                        .GetRequiredService<UserManager<IdentityUser>>();
                    //var config = service
                    //    .GetRequiredService<IConfiguration>();
                    var admin = await userManager
                        .FindByEmailAsync("admin@app.com");

                    if (admin == null)
                    {
                        admin = new IdentityUser()
                        {
                            UserName = "admin@app.com",
                            Email = "admin@app.com",
                            EmailConfirmed = true
                        };
                        result = await userManager.CreateAsync(admin, "Admin@123");

                        if (result.Succeeded)
                        {
                            result = await userManager.AddToRoleAsync(admin, roleName);
                            await _next(context);
                        }
                    }
                }
            }
            else
            {
                await _next(context);
            }
        }
    }
    public static class AdminMiddleware
    {
        public static void UseAdminMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<AdminRoleMiddleware>();
        }
    }
}
