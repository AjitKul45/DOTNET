using CS_Assessment0._1.Models;

namespace CS_Assessment0._1.Service
{
    public interface IUserService<TEntity, in TPk> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetUsersAsync();
        Task<TEntity> GetAsync(TPk id);
        Task<TEntity> Create(TEntity user);
        Task<TEntity> Update(int id,TEntity user);
    }

}
