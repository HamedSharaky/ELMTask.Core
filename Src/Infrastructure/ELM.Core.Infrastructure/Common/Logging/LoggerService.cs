using ELM.Core.Application.Common.Interfaces;
using ELM.Core.Common.Configurations;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Context;
using System;
using System.Linq;

namespace ELM.Core.Infrastructure.Common.Logging;

public class LoggerService : ILoggerService
{
    private readonly string[] _disabledLevels;
    private readonly ILogger _logger;

    public LoggerService(IConfiguration configuration, ILogger logger = null)
    {
        _disabledLevels = GetDisabledLoggerLevels(configuration);
        _logger = logger ?? Log.Logger;
    }

    public void Debug(string message)
    {
        if (_disabledLevels.Contains(nameof(Debug))) { return; }

        _logger.Debug(message);
    }

    public void Debug(string message, object context)
    {
        if (_disabledLevels.Contains(nameof(Debug))) { return; }

        LogContext.PushProperty("context", context, true);

        _logger.Debug(message);
    }

    public void Error(string message)
    {
        if (_disabledLevels.Contains(nameof(Error))) { return; }

        _logger.Error(message);
    }

    public void Error(string message, object context)
    {
        if (_disabledLevels.Contains(nameof(Error))) { return; }

        LogContext.PushProperty("context", context, true);

        _logger.Error(message);
    }

    public void Fatal(string message)
    {
        if (_disabledLevels.Contains(nameof(Fatal))) { return; }

        _logger.Fatal(message);
    }

    public void Fatal(string message, object context)
    {
        if (_disabledLevels.Contains(nameof(Fatal))) { return; }

        LogContext.PushProperty("context", context, true);

        _logger.Fatal(message);
    }

    public void Information(string message)
    {
        if (_disabledLevels.Contains(nameof(Information))) { return; }

        _logger.Information(message);
    }

    public void Information(string message, object context)
    {
        if (_disabledLevels.Contains(nameof(Information))) { return; }

        LogContext.PushProperty("context", context, true);

        _logger.Information(message);
    }

    public void Warning(string message)
    {
        if (_disabledLevels.Contains(nameof(Warning))) { return; }

        _logger.Warning(message);
    }

    public void Warning(string message, object context)
    {
        if (_disabledLevels.Contains(nameof(Warning))) { return; }

        LogContext.PushProperty("context", context, true);

        _logger.Warning(message);
    }

    public void ApiCall(string message, object request, object response)
    {
        if (_disabledLevels.Contains(nameof(ApiCall))) { return; }

        LogContext.PushProperty("context", new { request, response }, true);

        _logger.Information(message);
    }

    private string[] GetDisabledLoggerLevels(IConfiguration configuration)
    {
        var loggerConfigOffLevels = configuration.GetConfiguration<LoggerConfigurations, string>(k => k.LoggerOffLevels);

        return string.IsNullOrWhiteSpace(loggerConfigOffLevels) ? (new string[] { }) : loggerConfigOffLevels.Split(',');
    }
}
