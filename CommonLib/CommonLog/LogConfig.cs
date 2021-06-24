using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    public enum LogType
    {
        Unity,
        Console
    }

    public class LogConfig
    {
        public bool EnableLog = true;
        public string LogPrefix = "#";
        public bool EnableTime = true;
        public string LogSeperate = ">>";
        public bool EnableThreadId = true;
        public bool EnableTrace = true;
        public bool EnableSave = true;
        /// <summary>
        /// 新日志文件是否覆盖旧的
        /// </summary>
        public bool EnableCover = true;
        public string SavePath = $@"{AppDomain.CurrentDomain.BaseDirectory}Logs\";
        public string SaveName = "ConsoleCommonLog.txt";
        public LogType LogType = LogType.Console;
    }
}
