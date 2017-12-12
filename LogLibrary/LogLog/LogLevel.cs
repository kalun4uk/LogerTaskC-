using System;

[Flags]
enum LogLevel
{
    All = 1,
    Debug = 2,
    Info = 4,
    Warning = 8,
    Error = 16
}