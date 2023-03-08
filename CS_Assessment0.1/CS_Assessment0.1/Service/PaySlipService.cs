using CS_Assessment0._1.Models;

namespace CS_Assessment0._1.Service
{
    public class PaySlipService : IPaySlipService
    {
        A01Context context;
        public PaySlipService(A01Context context)
        {
            this.context = context;
        }
        async Task<object> IPaySlipService.DetailPaySlip(int id)
        {
            PaySlip? payslip = new PaySlip();
            payslip.user = await context.UserInfos.FindAsync(id);
            payslip.leave = await context.Leaves.FindAsync(id);
            payslip.investment = await context.Investments.FindAsync(id);
            payslip.salary = await context.Salaries.FindAsync(id);
            payslip.slip = await context.SalarySlips.FindAsync(id);
            return payslip;
        }
    }
}
