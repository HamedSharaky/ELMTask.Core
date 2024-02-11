using ELM.Core.Application.Common.Interfaces;
using ELM.Core.Common.Configurations;
using ELM.Core.Domain.Books;
using ELM.Core.Persistence.Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static CSharpFunctionalExtensions.Result;

namespace ELM.Core.Persistence.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ELMDbContext>(options => BuildDbContextOptions(options, configuration));

            services.AddScoped<IReadOnlyDbContext>(provider => provider.GetService<ELMDbContext>());

            services.AddScoped<IELMDbContext>(provider => provider.GetService<ELMDbContext>());

            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

            services.AddScoped<IBookRepository, BookRepository>();

            return services;
        }

        private static void BuildDbContextOptions(DbContextOptionsBuilder optionsBuilder, IConfiguration configuration)
        {
            //Only for manual migration
            var connectionString = "Data Source=.;Initial Catalog=ElmTestDb;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=P@ssw0rd;Encrypt=False;";

            //var connectionString = configuration.GetConfiguration<CommonKeys, string>(k => k.DatabaseConnectionString);
            var maxMigrationDuration = configuration.GetConfiguration<CommonKeys, int>(k => k.DatabaseMigrationTimeoutDurationInSeconds);

            optionsBuilder.UseSqlServer(connectionString,
                options => options.CommandTimeout(maxMigrationDuration));
        }
    }
}