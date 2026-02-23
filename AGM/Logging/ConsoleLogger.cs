using System;
using AGM.Logging;
using Spectre.Console;

namespace AGM.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Info(string message) => AnsiConsole.Markup($"[green bold][[INFO]][/]: {message}\n");
        public void Warning(string message) => AnsiConsole.Markup($"[yellow bold][[WARNING]][/]: {message}\n");
        public void Error(string message) => AnsiConsole.Markup($"[red bold][[ERROR]][/]: {message}\n");
        public void Debug(string message) => AnsiConsole.Markup($"[blue bold][[DEBUG]][/]: {message}\n");
    }
}