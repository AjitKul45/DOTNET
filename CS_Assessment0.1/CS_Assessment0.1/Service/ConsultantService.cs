using CS_Assessment0._1.Models;
using Microsoft.EntityFrameworkCore;

namespace CS_Assessment0._1.Service
{
    public class ConsultantService : IConsultantService<Consultant, int>
    {
        A01Context context;
        
        public ConsultantService(A01Context context) 
        {
            this.context = context;
            
        }
        async Task<Consultant> IConsultantService<Consultant, int>.ApproveDetailAsync(int id)
        {
            var consultant = await context.Consultants.FindAsync(id);
            if (consultant == null)
                throw new Exception("Consultant not found");
            consultant.Status = true;
            await context.SaveChangesAsync();
            return consultant;
        }

        async Task<Consultant> IConsultantService<Consultant, int>.CreateAsync(Consultant consultant)
        {
            await context.Consultants.AddAsync(consultant);
            await context.SaveChangesAsync();
            return consultant;
        }

        async Task<Consultant> IConsultantService<Consultant, int>.DeclineDetailAsync(int id)
        {
            var consultant = await context.Consultants.FindAsync(id);
            if (consultant == null)
                throw new Exception("Consultant not found");
            consultant.Status = false;
            await context.SaveChangesAsync();
            return consultant;
        }

        async Task<IEnumerable<Consultant>> IConsultantService<Consultant, int>.GetConsultantDetailsAsync()
        {
            return await context.Consultants.ToListAsync();
        }

        async Task<Consultant> IConsultantService<Consultant, int>.GetConsultantDetailsAsync(int id)
        {
            return await context.Consultants.FindAsync(id);
        }
    }
}
