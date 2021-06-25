using System;
using Utils;

namespace ServerLogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //CommonLog.InitSettings();
            CommonLog.Log("{0} START...", "ServerCommonLog");
            CommonLog.ColorLog(ConsoleColor.Red, "Color Log:{0}", ConsoleColor.Red.ToString());
            CommonLog.ColorLog(ConsoleColor.DarkGreen, "Color Log:{0}", ConsoleColor.DarkGreen.ToString());
            CommonLog.ColorLog(ConsoleColor.Blue, "Color Log:{0}", ConsoleColor.Blue.ToString());
            CommonLog.ColorLog(ConsoleColor.Cyan, "Color Log:{0}", ConsoleColor.Cyan.ToString());
            CommonLog.ColorLog(ConsoleColor.Magenta, "Color Log:{0}", ConsoleColor.Magenta.ToString());
            CommonLog.ColorLog(ConsoleColor.DarkYellow, "Color Log:" + ConsoleColor.DarkYellow.ToString());

            Root root = new Root();
            root.Init();

            Console.ReadKey();
        }
    }
}
