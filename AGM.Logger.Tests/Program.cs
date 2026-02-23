using System;
using AGM;
using AGM.Logging;

namespace AGMLoggerTests
{
    internal class Program
    {
        private class DummyLogger : ILogger
        {
            public bool InfoCalled;
            public bool WarningCalled;
            public bool ErrorCalled;
            public bool DebugCalled;

            public void Info(string message) { InfoCalled = true; Console.WriteLine("Info: " + message); }
            public void Warning(string message) { WarningCalled = true; Console.WriteLine("Warning: " + message); }
            public void Error(string message) { ErrorCalled = true; Console.WriteLine("Error: " + message); }
            public void Debug(string message) { DebugCalled = true; Console.WriteLine("Debug: " + message); }
        }

        public static int Main(string[] args)
        {
            var dummy = new DummyLogger();
            Logger.Initialize(dummy);

            Logger.Info("test-info");
            Logger.Warning("test-warning");
            Logger.Error("test-error");
            Logger.Debug("test-debug");

            bool ok = dummy.InfoCalled && dummy.WarningCalled && dummy.ErrorCalled && dummy.DebugCalled;
            Console.WriteLine("Logger facade test: " + (ok ? "PASS" : "FAIL"));
            return ok ? 0 : 1;
        }
    }
}
