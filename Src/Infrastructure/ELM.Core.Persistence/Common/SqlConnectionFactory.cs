using ELM.Core.Application.Common.Interfaces;
using ELM.Core.Common.Configurations;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ELM.Core.Persistence.Common
{
    public sealed class SqlConnectionFactory : ISqlConnectionFactory
    {
        private readonly string _connectionString;

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConfiguration<CommonKeys, string>(k => k.DatabaseConnectionString);
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
