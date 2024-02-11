using System.Threading;
using System.Threading.Tasks;

namespace ELM.Core.Application.Common.Interfaces
{
    public interface IELMDbContext
    {
        void MigrateDatabase();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #region DbSets


        #endregion
    }
}