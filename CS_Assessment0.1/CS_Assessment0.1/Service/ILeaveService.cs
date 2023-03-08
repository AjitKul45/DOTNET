namespace CS_Assessment0._1.Service
{
    public interface ILeaveService<TEntity,in TPk> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity user);
        Task<IEnumerable<TEntity>> GetLeavesAsync();
        Task<TEntity> ApproveLeaveAsync(int id);
        Task<TEntity> DeclineLeaveAsync(int id);
    }
}
