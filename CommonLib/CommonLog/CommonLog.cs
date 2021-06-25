using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Utils
{
    public static class CommonLog
    {
        public static LogConfig LogConfig { get; private set; }
        private static ILogger logger;
        private static StreamWriter LogFileWriter = null;

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

            if (!LogConfig.EnableSave)
            {
                return;
            }
            if (LogConfig.EnableCover)
            {
                string path = LogConfig.SavePath + LogConfig.SaveName;
                try
                {
                    if (Directory.Exists(LogConfig.SavePath))
                    {
                        if (File.Exists(path))
                        {
                            FileInfo file = new FileInfo(path);
                            file.CopyTo($"{LogConfig.SavePath}_Old_{file.LastWriteTime.ToString("yyyyMMdd@HH-mm")}_{LogConfig.SaveName}");
                            File.Delete(path);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(LogConfig.SavePath);
                    }
                    LogFileWriter = File.AppendText(path);
                    LogFileWriter.AutoFlush = true;
                }
                catch
                {
                    LogFileWriter = null;
                }
            }
            else
            {
                string prefix = DateTime.Now.ToString("yyyyMMdd@HH-mm-ss");
                string path = LogConfig.SavePath + prefix + config.SaveName;
                try
                {
                    if (Directory.Exists(LogConfig.SavePath) == false)
                    {
                        Directory.CreateDirectory(LogConfig.SavePath);
                    }
                    LogFileWriter = File.AppendText(path);
                    LogFileWriter.AutoFlush = true;
                }
                catch
                {
                    LogFileWriter = null;
                }
            }
        }

        public static void Log(string msg, params object[] args)
        {
            if (!LogConfig.EnableLog)
            {
                return;
            }
            msg = DecorateLog(string.Format(msg, args));
            logger.Log(msg);
            if (LogConfig.EnableSave)
            {
                WriteToFile($"[L]{msg}");
            }
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

        private static void WriteToFile(string msg)
        {
            if (LogConfig.EnableSave && LogFileWriter != null)
            {
                try
                {
                    LogFileWriter.WriteLine(msg);
                }
                catch
                {
                    LogFileWriter = null;
                }
            }
        }
    }
}
