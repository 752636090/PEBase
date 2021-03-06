using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Utils
{
    class UnityLogger : ILogger
    {
        private Type type = Type.GetType("UnityEngine.Debug, UnityEngine");
        private MethodInfo logMethod;
        private MethodInfo warningMethod;
        private MethodInfo errorMethod;

        public UnityLogger()
        {
            logMethod = type.GetMethod("Log", new Type[] { typeof(object) });
            warningMethod = type.GetMethod("LogWarning", new Type[] { typeof(object) });
            errorMethod = type.GetMethod("LogError", new Type[] { typeof(object) });
        }

        public void Log(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            if (color != ConsoleColor.Gray)
            {
                msg = ColorUnityLog(msg, color);
            }
            logMethod.Invoke(null, new object[] { msg });
        }

        public void LogWaring(string msg)
        {
            warningMethod.Invoke(null, new object[] { msg });
        }

        public void LogError(string msg)
        {
            errorMethod.Invoke(null, new object[] { msg });
        }

        private string ColorUnityLog(string msg, ConsoleColor color = ConsoleColor.Black)
        {
            switch (color)
            {
                case ConsoleColor.Black:
                    msg = string.Format($"<color=#000000>{msg}</color>");
                    break;
                case ConsoleColor.Blue:
                    msg = string.Format($"<color=#FF0000>{msg}</color>");
                    break;
                case ConsoleColor.Cyan:
                    msg = string.Format($"<color=#00FFFF>{msg}</color>");
                    break;
                case ConsoleColor.DarkBlue:
                    msg = string.Format($"<color=#0000FF>{msg}</color>");
                    break;
                case ConsoleColor.DarkCyan:
                    msg = string.Format($"<color=#00FFFF>{msg}</color>");
                    break;
                case ConsoleColor.DarkGray:
                    msg = string.Format($"<color=#BBBBBB>{msg}</color>");
                    break;
                case ConsoleColor.DarkGreen:
                    msg = string.Format($"<color=#00FF00>{msg}</color>");
                    break;
                case ConsoleColor.DarkMagenta:
                    msg = string.Format($"<color=#FF00FF>{msg}</color>");
                    break;
                case ConsoleColor.DarkRed:
                    msg = string.Format($"<color=#FF0000>{msg}</color>");
                    break;
                case ConsoleColor.DarkYellow:
                    msg = string.Format($"<color=#FFFF00>{msg}</color>");
                    break;
                case ConsoleColor.Green:
                    msg = string.Format($"<color=#00FF00>{msg}</color>");
                    break;
                case ConsoleColor.Magenta:
                    msg = string.Format($"<color=#FF00FF>{msg}</color>");
                    break;
                case ConsoleColor.Red:
                    msg = string.Format($"<color=#FF0000>{msg}</color>");
                    break;
                case ConsoleColor.White:
                    msg = string.Format($"<color=#FFFFFF>{msg}</color>");
                    break;
                case ConsoleColor.Yellow:
                    msg = string.Format($"<color=#FFFF00>{msg}</color>");
                    break;
                case ConsoleColor.Gray:
                default:
                    msg = string.Format($"<color=#999999>{msg}</color>");
                    break;
            }
            return msg;
        }
    }
}
