using CS_Assessment0._1.Models;
using Microsoft.EntityFrameworkCore;

namespace CS_Assessment0._1.Service
{
    public class UserService : IUserService<UserInfo, int>
    {
        A01Context context;
        public UserService(A01Context context) 
        {
            this.context = context;
        }
        async Task<UserInfo> IUserService<UserInfo, int>.Create(UserInfo user)
        {
            await context.UserInfos.AddAsync(user);
            await context.SaveChangesAsync();
            return user;
        }

        Task<UserInfo> IUserService<UserInfo, int>.GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        async Task<IEnumerable<UserInfo>> IUserService<UserInfo, int>.GetUsersAsync()
        {
           return await context.UserInfos.ToListAsync();
        }

        async Task<UserInfo> IUserService<UserInfo, int>.Update(int id, UserInfo user)
        {
            var new_user_info = await context.UserInfos.FindAsync(id);
            if (new_user_info == null)
                throw new Exception("User not found!");

            new_user_info.FirstName = user.FirstName;
            new_user_info.LastName = user.LastName;
            new_user_info.Dob = user.Dob;

            await context.SaveChangesAsync();
            return new_user_info;
        }
    }
}
