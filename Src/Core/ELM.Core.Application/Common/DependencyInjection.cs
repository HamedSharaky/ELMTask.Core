using System.Reflection;
using ELM.Core.Application.Books.Search;
using ELM.Core.Application.Common.Behaviours;
using ELM.Core.Application.Common.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ELM.Core.Application.Common
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = ApplicationAssemblyReference.Assembly;

            services.AddAutoMapper(applicationAssembly);

            //services.AddValidatorsFromAssembly(applicationAssembly);
            services.AddValidatorsFromAssemblyContaining<SearchBookQueryValidator>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLoggingBehaviour<,>));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}