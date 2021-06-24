using System;
using Utils;

namespace ServerLogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //CommonLog.InitSettings();
            CommonLog.Log("haha.");

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
