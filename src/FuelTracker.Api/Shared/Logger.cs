using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Monacs.Core;
using static Monacs.Core.Option;

namespace FuelTracker.Api.Shared
{
    public static class Logger
    {
        private static readonly string _fullPath = Path.Combine("logs", "all.log");

        public static void Trace(string message, Exception exception = null) => Log(Level.Fatal, message, exception);
        public static void Debug(string message, Exception exception = null) => Log(Level.Fatal, message, exception);
        public static void Info(string message, Exception exception = null) => Log(Level.Fatal, message, exception);
        public static void Warn(string message, Exception exception = null) => Log(Level.Fatal, message, exception);
        public static void Error(string message, Exception exception = null) => Log(Level.Fatal, message, exception);
        public static void Fatal(string message, Exception exception = null) => Log(Level.Fatal, message, exception);
        
        private static void Log(Level level, string message, Exception exception = null)
        {
            var dir = Path.GetDirectoryName(_fullPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            File.AppendAllText(_fullPath, $"{DateTime.UtcNow:u};{level};{message};{exception};{Environment.NewLine}");
        }

        private enum Level
        {
            Trace, Debug, Info, Warn, Error, Fatal
        }
    }
}
