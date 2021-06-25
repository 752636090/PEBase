using System;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Utils
{
    public static class CommonLog
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
            msg = DecorateLog(string.Format(msg, args));
            logger.Log(msg);
        }

        private static string DecorateLog(string msg, bool isTrace = false)
        {
            StringBuilder sb = new StringBuilder(LogConfig.LogPrefix, 100);
            if (LogConfig.EnableTime)
            {
                //sb.Append($" {DateTime.Now.ToString("hh:mm:ss--fff")}");
                sb.Append($" {DateTime.Now:hh:mm:ss--fff}");
            }
            if (LogConfig.EnableThreadId)
            {
                sb.Append($" {GetThreadId()}");
            }
            sb.Append($" {LogConfig.LogSeperate} {msg}");
            if (isTrace)
            {
                sb.Append($" \nStackTrace:{GetLogTrace()}");
            }
            return sb.ToString();
        }

        private static string GetThreadId()
        {
            return $" ThreadId:{Thread.CurrentThread.ManagedThreadId}";
        }

        private static string GetLogTrace()
        {
            StackTrace trace = new StackTrace(3, true);

            string traceInfo = "";
            for (int i = 0; i < trace.FrameCount; i++)
            {
                StackFrame frame = trace.GetFrame(i);
                traceInfo += $"\n\t{frame.GetFileName()}::{frame.GetMethod()} line:{frame.GetFileLineNumber()}";
            }

            return traceInfo;
        }
    }
}
