namespace ELM.Core.Application.Common.Interfaces;

public interface ILoggerService
{
    void Debug(string message);
    void Debug(string message, object context);
    void Information(string message);
    void Information(string message, object context);
    void Warning(string message);
    void Warning(string message, object context);
    void Error(string message);
    void Error(string message, object context);
    void Fatal(string message);
    void Fatal(string message, object context);
    void ApiCall(string message, object request, object response);
}
