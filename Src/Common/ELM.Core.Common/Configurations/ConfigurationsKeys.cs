namespace ELM.Core.Common.Configurations;

public sealed class CommonKeys
{
    public string DatabaseConnectionString { get; set; }
    public string AllowAutoDatabaseMigration { get; set; }
    public string DatabaseMigrationTimeoutDurationInSeconds { get; set; }
    public string BookSearchCacheDurationInMinutes { get; set; }
}

public sealed class LoggerConfigurations
{
    public string LoggerType { get; set; }
    public string LoggerFolderPath { get; set; }
    public string LoggerRetainedFileTimeLimitInDays { get; set; }
    public string LoggerOffLevels { get; set; }
}