﻿namespace ExecutionLens.Logging.DOMAIN.Models;

public class MethodExit
{
    public DateTime Time { get; init; } = DateTime.Now;
    public bool HasException { get; init; } = false;
    public Property? Output { get; init; } = null;
}