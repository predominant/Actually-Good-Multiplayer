using System;
using AGM.Logging;

namespace AGM
{
    /// <summary>
    /// Simple static logging facade exposing the configured AGM.Logging.ILogger implementation.
    /// Call Logger.Initialize(...) early (e.g. at program startup). If not initialized, a ConsoleLogger
    /// will be used as the fallback implementation.
    /// </summary>
    public static class Logger
    {
        private static ILogger _instance;
        private static readonly object _lock = new object();

        public static ILogger Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AGM.Logging.ConsoleLogger();
                    }
                }

                return _instance;
            }
        }

        public static void Initialize(ILogger impl)
        {
            if (impl == null) throw new ArgumentNullException(nameof(impl));
            lock (_lock)
            {
                _instance = impl;
            }
        }

        public static void Info(string message) => Instance?.Info(message);
        public static void Warning(string message) => Instance?.Warning(message);
        public static void Error(string message) => Instance?.Error(message);
        public static void Debug(string message) => Instance?.Debug(message);
    }
}
