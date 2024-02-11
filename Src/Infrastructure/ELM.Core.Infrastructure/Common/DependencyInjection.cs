using ELM.Core.Application.Books.Search;
using ELM.Core.Application.Common;
using ELM.Core.Application.Common.Behaviours;
using ELM.Core.Application.Common.Contracts;
using ELM.Core.Application.Common.Interfaces;
using ELM.Core.Infrastructure.Common.Caching;
using ELM.Core.Infrastructure.Common.Logging;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ELM.Core.Infrastructure.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddTransient<ILoggerService, LoggerService>();

            services.AddTransient<ICacheService, CacheService>();

            services.RegisterMediator();

            return services;
        }

        private static void RegisterMediator(this IServiceCollection services)
        {
            var applicationAssembly = Application.Common.ApplicationAssemblyReference.Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            services.AddTransient<IELMMediator, ELMMediator>();
        }
    }
}