using Asset_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Asset_Management.Services
{
    public class AuthService : IAuthService<User, int>
    {
        asset_managementContext ctx;
        public AuthService(asset_managementContext con)
        {
            ctx = con;
        }

        public async Task<User> Login(User login)
        {
            var user = (await ctx.Users.ToListAsync()).Where(u => u.Email == login.Email && u.Password == login.Password).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("User Not Found");
            }
        }

    }
}
