using CS_Assessment0._1.Models;

namespace CS_Assessment0._1.Service
{
    public interface IConsultantService<TEntity,in TPk> where TEntity : class
    {
        Task<Consultant> CreateAsync(Consultant consultant);
        Task<IEnumerable<Consultant>> GetConsultantDetailsAsync();
        Task<Consultant> GetConsultantDetailsAsync(int id);
        Task<TEntity> ApproveDetailAsync(int id);
        Task<TEntity> DeclineDetailAsync(int id);
        
    }
}
