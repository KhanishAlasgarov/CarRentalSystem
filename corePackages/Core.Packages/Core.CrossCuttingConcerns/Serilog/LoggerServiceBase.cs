﻿using Serilog;

namespace Core.CrossCuttingConcerns.Serilog;

public class LoggerServiceBase
{
    public LoggerServiceBase(ILogger logger)
    {
        Logger = logger;
    }
    public LoggerServiceBase()
    {
        Logger = null;
    }

    protected ILogger Logger { get; set; }

    public void Verbose(string message) => Logger.Verbose(message);

    public void Fatal(string message) => Logger.Fatal(message);

    public void Info(string message) => Logger.Information(message);

    public void Warn(string message) => Logger.Warning(message);

    public void Debug(string message) => Logger.Debug(message);

    public void Error(string message) => Logger.Error(message);
}
