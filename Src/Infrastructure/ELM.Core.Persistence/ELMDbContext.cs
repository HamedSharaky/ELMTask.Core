using ELM.Core.Application.Common.Interfaces;
using ELM.Core.Persistence.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ELM.Core.Persistence
{
    public class ELMDbContext : DbContext, IELMDbContext, IReadOnlyDbContext
    {
        public ELMDbContext(DbContextOptions<ELMDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ELMDbContext).Assembly);
        }

        public void MigrateDatabase()
        {
            Database.Migrate();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return Set<TEntity>().AsNoTracking();
        }


        #region DbSets


        #endregion
    }
}