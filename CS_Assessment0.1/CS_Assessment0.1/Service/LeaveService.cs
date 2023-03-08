using CS_Assessment0._1.Models;
using Microsoft.EntityFrameworkCore;

namespace CS_Assessment0._1.Service
{
    public class LeaveService : ILeaveService<Leave, int>
    {
        A01Context context;
        public LeaveService(A01Context context) 
        {
            this.context = context;
        }
        async Task<Leave> ILeaveService<Leave, int>.ApproveLeaveAsync(int id)
        {
            Leave? leaveReq = await context.Leaves.FindAsync(id);

            leaveReq.Status = true;

            await context.SaveChangesAsync();
            return leaveReq;
        }

        async Task<Leave> ILeaveService<Leave, int>.CreateAsync(Leave leave)
        {
            await context.Leaves.AddAsync(leave);
            await context.SaveChangesAsync();
            return leave;
        }

        async Task<Leave> ILeaveService<Leave, int>.DeclineLeaveAsync(int id)
        {
            Leave? leaveReq = await context.Leaves.FindAsync(id);

            leaveReq.Status = false;

            await context.SaveChangesAsync();
            return leaveReq;
        }

        async Task<IEnumerable<Leave>> ILeaveService<Leave, int>.GetLeavesAsync()
        {
            return await context.Leaves.ToListAsync();
        }
    }
}
