using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Microsoft.Extensions.Configuration;
using System.IO;
using Serilog.Exceptions;
using System.Collections.Generic;
using System;
using ELM.Core.Common.Configurations;

namespace ELM.Core.Infrastructure.Common.Logging;

public static class LoggerExtensions
{
    public static IHostBuilder UseSerilogLogger(this IHostBuilder builder)
    {
        return builder.UseSerilog((hostContext, loggerConfiguration) => BuildLoggerConfiguration(hostContext.Configuration, loggerConfiguration));
    }

    private static void BuildLoggerConfiguration(IConfiguration configuration,
        LoggerConfiguration loggerConfiguration)
    {
        string serviceName = configuration.GetServiceName();

        IDictionary<string, LogEventLevel> minLevelConfigurations = CreateMinLevelConfigurations();

        foreach (var minLevelConfig in minLevelConfigurations)
        {
            loggerConfiguration.MinimumLevel.Override(minLevelConfig.Key, minLevelConfig.Value);
        }

        loggerConfiguration
            .MinimumLevel.Verbose()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithExceptionDetails()
            .Enrich.WithThreadId()
            .Enrich.FromLogContext()
            .Enrich.WithProperty("ServiceName", serviceName, true);

        var logType = configuration.GetConfiguration<LoggerConfigurations, string>(k => k.LoggerType);

        if (logType == "File")
        {
            var logFolderPath = configuration.GetConfiguration<LoggerConfigurations, string>(k => k.LoggerFolderPath);

            var retainedFileTimeLimitInDays = configuration.GetConfiguration<LoggerConfigurations, byte>(k => k.LoggerRetainedFileTimeLimitInDays);

            loggerConfiguration.WriteTo.Async(a => a.File(
                new JsonFormatter(renderMessage: true),
                path: Path.Combine(logFolderPath ?? "logs/", "log.json"),
                fileSizeLimitBytes: 1048576,//1 MB
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: null,
                retainedFileTimeLimit: TimeSpan.FromDays(retainedFileTimeLimitInDays),
                rollOnFileSizeLimit: true,
                shared: true), blockWhenFull: true);
        }
        else
        {
            loggerConfiguration.WriteTo.Console();
        }
    }

    private static IDictionary<string, LogEventLevel> CreateMinLevelConfigurations()
    {
        return new Dictionary<string, LogEventLevel>
        {
            { "Microsoft", LogEventLevel.Information },
            { "Microsoft.AspNetCore", LogEventLevel.Warning },
            { "Microsoft.EntityFrameworkCore", LogEventLevel.Warning },
            { "System.Net.Http.HttpClient", LogEventLevel.Information },
            { "DevExpress.AspNetCore.Reporting.Logger", LogEventLevel.Information },
            { "Quartz.Core.QuartzSchedulerThread", LogEventLevel.Information },
            { "Quartz.Simpl.TaskSchedulingThreadPool", LogEventLevel.Information }
        };
    }

    private static string GetServiceName(this IConfiguration configuration)
    {
        return configuration.GetSection("ServiceName")?.Value;
    }
}
