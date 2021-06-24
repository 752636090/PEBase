using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    class ConsoleLogger : ILogger
    {
        public void Error(string msg)
        {
            WriteConsoleLog(msg, ConsoleColor.DarkRed);
        }

        public void Log(string msg, ConsoleColor color = ConsoleColor.Gray)
        {
            WriteConsoleLog(msg, color);
        }

        public void Waring(string msg)
        {
            WriteConsoleLog(msg, ConsoleColor.DarkYellow);
        }

        private void WriteConsoleLog(string msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
