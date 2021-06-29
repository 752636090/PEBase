using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    interface ILogger
    {
        void Log(string msg, ConsoleColor color = ConsoleColor.Gray);
        void LogWaring(string msg);
        void LogError(string msg);
    }
}
