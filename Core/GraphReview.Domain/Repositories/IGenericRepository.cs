namespace GraphReview.Domain.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
