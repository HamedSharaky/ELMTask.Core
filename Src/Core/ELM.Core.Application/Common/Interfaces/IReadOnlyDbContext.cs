using System;
using System.Collections.Generic;
using System.Linq;

namespace ELM.Core.Application.Common.Interfaces;

public interface IReadOnlyDbContext : IDisposable
{
    IQueryable<TEntity> Query<TEntity>() where TEntity : class;
}
