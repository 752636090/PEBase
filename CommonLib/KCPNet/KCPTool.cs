using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace KCPNet
{
    public class KCPTool
    {
        public static Action<string> LogFunc;
        public static Action<ConsoleColor, string> ColorLogFunc;
        public static Action<string> WarningFunc;
        public static Action<string> ErrorFunc;

        public static void Log(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            if (LogFunc != null)
            {
                LogFunc(msg);
            }
            else
            {
                ConsoleLog(msg);
            }
        }

        public static void ColorLog(ConsoleColor color, string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            if (ColorLogFunc != null)
            {
                ColorLogFunc(color, msg);
            }
            else
            {
                ConsoleLog(msg, color);
            }
        }

        public static void Warning(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            if (WarningFunc != null)
            {
                WarningFunc(msg);
            }
            else
            {
                ConsoleLog(msg, ConsoleColor.DarkYellow);
            }
        }

        public static void Error(string msg, params object[] args)
        {
            msg = string.Format(msg, args);
            if (ErrorFunc != null)
            {
                ErrorFunc(msg);
            }
            else
            {
                ConsoleLog(msg, ConsoleColor.DarkRed);
            }
        }

        private static void ConsoleLog(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            msg = $"Thread:{threadId} {msg}";

            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
