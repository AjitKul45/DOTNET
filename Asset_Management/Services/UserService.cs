using Asset_Management.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Diagnostics.Metrics;

namespace Asset_Management.Services
{
    public class UserService : IService<User, int>
    {
        asset_managementContext ctx;
        public UserService(asset_managementContext con) 
        {
            ctx = con;
        }

        public Task<IEnumerable> GetStatus()
        {
            throw new NotImplementedException();
        }

        async Task<User> IService<User, int>.CreateAsync(User entity)
        {
            var user = await ctx.Users.AddAsync(entity);
            await ctx.SaveChangesAsync();
            return user.Entity;
        }

        async Task<User> IService<User, int>.DeleteAsync(int id)
        {
            var user = await ctx.Users.FindAsync(id);
            if (user != null)
            {
                ctx.Users.Remove(user);
                await ctx.SaveChangesAsync();
                return user;
            }
            else
            {
                throw new Exception("User Not Found");
            }
        }

        async Task<IEnumerable<User>> IService<User, int>.GetAsync()
        {
            var users = await ctx.Users.ToListAsync();
            return users;
        }

        async Task<User> IService<User, int>.GetAsync(int id)
        {
            var user = await ctx.Users.FindAsync(id);
            if (user != null)
            {
                return user;
            }
            else
            {
                throw new Exception("User Not Found");
            }
        }

        async Task<User> IService<User, int>.UpdateAsync(int id, User entity)
        {
            var user = await ctx.Users.FindAsync(id);
            if (user != null)
            {
                user.Email = entity.Email;
                user.Password = entity.Password;
                
                
                await ctx.SaveChangesAsync();
                return user;
            }
            else
            {
                throw new Exception("User Not Found");
            }
        }
    }
}
