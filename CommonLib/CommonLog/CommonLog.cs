using System;

namespace Utils
{
    public class CommonLog
    {
        public static LogConfig LogConfig { get; private set; }
        private static ILogger logger;

        static CommonLog()
        {
            InitSettings();
        }

        public static void InitSettings(LogConfig config = null)
        {
            LogConfig = config ?? new LogConfig();

            if (LogConfig.LogType == LogType.Console)
            {
                logger = new ConsoleLogger();
            }
            else
            {
                logger = new UnityLogger();
            }
        }

        public static void Log(string msg, params object[] args)
        {
            if (LogConfig.EnableLog == false)
            {
                return;
            }
            msg = string.Format(msg, args);
            logger.Log(msg);
        }
    }
}
