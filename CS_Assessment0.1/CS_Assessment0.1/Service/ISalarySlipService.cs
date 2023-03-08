using CS_Assessment0._1.Models;

namespace CS_Assessment0._1.Service
{
    public interface ISalarySlipService<TEntity,in TPk> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetListAsync();
        Task<SalarySlip> Add(SalarySlip entity);
        Task<object> CalculateSalary(DateTime? from, DateTime? to);
        
    }
}
