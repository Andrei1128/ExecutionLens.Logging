﻿namespace ExecutionLens.Logging.APPLICATION.Utilities;

internal class LogManager
{
    public bool IsLogging { get; private set; } = false;
    public void StartLogging() => IsLogging = true;
}
