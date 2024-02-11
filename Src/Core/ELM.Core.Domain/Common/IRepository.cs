using Ardalis.Specification;

namespace ELM.Core.Domain.Common
{
    /// <inheritdoc/>
    public interface IRepository<TEntity, TKey>
        : IReadRepositoryBase<TEntity> where TEntity : AggregateRoot<TKey> where TKey : IComparable<TKey>
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
        Task AttachAsync(TEntity entity);
    }
}
