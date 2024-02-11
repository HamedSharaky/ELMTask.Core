using Ardalis.Specification.EntityFrameworkCore;
using ELM.Core.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELM.Core.Persistence.Common;

public abstract class Repository<TEntity, TKey>
    : RepositoryBase<TEntity>, IRepository<TEntity, TKey> where TEntity : AggregateRoot<TKey> where TKey : IComparable<TKey>
{
    protected readonly DbSet<TEntity> EntityDbSet;

    public Repository(DbContext context) : base(context)
    {
        EntityDbSet = context.Set<TEntity>();
    }

    public Task AddAsync(TEntity entity)
    {
        EntityDbSet.Add(entity);

        return Task.CompletedTask;
    }

    public Task AttachAsync(TEntity entity)
    {
        EntityDbSet.Attach(entity);

        return Task.CompletedTask;
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        return EntityDbSet.AddRangeAsync(entities);
    }

    public Task UpdateAsync(TEntity entity)
    {
        EntityDbSet.Update(entity);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(TEntity entity)
    {
        EntityDbSet.Remove(entity);

        return Task.CompletedTask;
    }
}
