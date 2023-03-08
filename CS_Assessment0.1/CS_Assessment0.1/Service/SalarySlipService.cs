using CS_Assessment0._1.Models;
using Microsoft.EntityFrameworkCore;

namespace CS_Assessment0._1.Service
{
    public class SalarySlipService : ISalarySlipService<SalarySlip, int>
    {
        A01Context context;

        public SalarySlipService(A01Context context)
        {
            this.context = context;
        }

        async Task<SalarySlip> ISalarySlipService<SalarySlip, int>.Add(SalarySlip entity)
        {
            await context.AddAsync(entity);
             await context.SaveChangesAsync();
            return entity;
        }


        async Task<object> ISalarySlipService<SalarySlip, int>.CalculateSalary(DateTime? from, DateTime? to)
        {
            var sals = (await context.Salaries.ToListAsync());

            foreach (var item in sals)
            {
                var investment = await context.Investments.FindAsync(item.UserId);
                var slip = new SalarySlip();
                slip.UserId = item.UserId;
                slip.TotalEarnings = item.BasicPay + item.Ta + item.Hra + item.Da;
                if(investment.PfAmount != 0)
                    slip.Contribution = investment.PfAmount;
                slip.Deduction = item.Tds + slip.Contribution;
                slip.NetAmount = slip.TotalEarnings - slip.Deduction;
                slip.DateFrom = from;
                slip.DateTo = to;
                await context.SalarySlips.AddAsync(slip);
                await context.SaveChangesAsync();
            }
            return 1;
        }

        async Task<IEnumerable<SalarySlip>> ISalarySlipService<SalarySlip, int>.GetListAsync()
        {
            return await context.SalarySlips.ToListAsync();
        }


    }
}
