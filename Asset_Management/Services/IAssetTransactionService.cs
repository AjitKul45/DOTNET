using Asset_Management.Models;

namespace Asset_Management.Services
{
    public interface IAssetTransactionService<TEntity, in Tpk> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByDeptAsync(Tpk dept);
        Task<IEnumerable<TEntity>> GetByLocationtAsync(Tpk location);

        Task<TEntity> Search(Tpk search);

        Task<TEntity> getAssetTransactionByEmail(Tpk email);
    }
}
