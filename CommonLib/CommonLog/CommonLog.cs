using System;

namespace Utils
{
    public class CommonLog
    {
        public static LogConfig LogConfig { get; private set; }
        private static ILogger logger;

        public static void InitSettings(LogConfig config = null)
        {
            config = config ?? new LogConfig();

            if (LogConfig.LogType == LogType.Console)
            {
                logger = new ConsoleLogger();
            }
            else
            {
                logger = new UnityLogger();
            }


        }
    }
}
