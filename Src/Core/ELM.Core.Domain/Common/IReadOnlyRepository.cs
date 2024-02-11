using Ardalis.Specification;

namespace ELM.Core.Domain.Common
{
    /// <inheritdoc/>
    public interface IReadOnlyRepository<TEntity, TKey> : IReadRepositoryBase<TEntity> where TEntity : ReadOnlyEntity<TKey>
    {
    }
}
