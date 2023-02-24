namespace Asset_Management.Services
{
    public interface IAssetDetailService<TEntity, in Tpk> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetByTypeAsync(Tpk type);
        Task<IEnumerable<TEntity>> GetByVendorAsync(Tpk vendor);
    }
}
