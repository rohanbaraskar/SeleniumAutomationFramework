using System;
using Nephila.Toolkit.Serilog;
using Serilog;
using Serilog.Debugging;
using ILogger = Nephila.Toolkit.Logging.ILogger;

namespace Automation.Selenium.Tests.Common.Logging
{
    public abstract class NephilaLogger
    {
        public static ILogger Log;

        static NephilaLogger()
        {
            Log = ConfigureLogging();
        }

        private static ILogger ConfigureLogging()
        {
            Serilog.Log.Logger = new DoNotUseTheStaticLogger();
            SelfLog.Out = Console.Out;

            var loggerConfiguration = new LoggerConfiguration()
                .ReadFrom.AppSettings()
                .Enrich.WithProperty("AppName", "Insight-BDD-Test")
                .WriteTo.Seq("http://localhost:5341")
                .Enrich.WithMachineName();

            if (Environment.UserInteractive)
            {
                loggerConfiguration.WriteTo.ColoredConsole();
            }

            return loggerConfiguration.CreateLogger()
                .ToNephilaILogger();
        }
    }
}