using System.Data;

namespace ELM.Core.Application.Common.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
