using ELM.Core.Application.Common;
using ELM.Core.Infrastructure.Common;
using ELM.Core.Persistence.Common;
using Microsoft.AspNetCore.Mvc;
using ELM.Core.Application.Common.Interfaces;
using ELM.Core.Presentation.Configuration.Middlewares;
using ELM.Core.Presentation.Configuration.Extensions;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using ELM.Core.Application.Common.Exceptions;
using ELM.Core.Common.Configurations;
using FluentValidation;
using ELM.Core.Application.Books.Search;
using ValidationException = ELM.Core.Application.Common.Exceptions.ValidationException;

namespace ELM.Core.Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddControllers();

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));

            services.AddResponseCompression();

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddHttpContextAccessor();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddApplication();
            services.AddInfrastructure();
            services.AddPersistence(Configuration);

            services.AddMemoryCache();
            services.AddSwaggerDocumentation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IELMDbContext dbContext, ILoggerService logger)
        {
            MigrateDatabase(dbContext, logger);

            if (env.IsDevelopment())
            {
                app.UseSwaggerDocumentation();

                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionHandler();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseResponseCompression();

            app.UseSerilogRequestLogging();

            app.UseCustomExceptionHandler();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void MigrateDatabase(IELMDbContext dbContext, ILoggerService logger)
        {
            var allowAutoDatabaseMigration = Configuration.GetConfiguration<CommonKeys, bool>(k => k.AllowAutoDatabaseMigration);

            if (allowAutoDatabaseMigration == false) return;

            try
            {
                logger.Debug("Start Schema Migration");

                dbContext.MigrateDatabase();

                logger.Debug("End Schema Migration");
            }
            catch (System.Exception ex)
            {
                logger.Error(ex.Message);
                throw;
            }
        }
    }
}
