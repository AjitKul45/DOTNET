namespace Asset_Management.Services
{
    public interface IService<TEntity, in Tpk> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity> GetAsync(Tpk pk);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(Tpk id, TEntity entity);
        Task<TEntity> DeleteAsync(Tpk id);
    }
}
