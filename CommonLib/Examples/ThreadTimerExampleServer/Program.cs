using System;
using System.Threading.Tasks;
using ThreadTimer;
using Utils;

namespace ThreadTimerExampleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            //CommonLog.InitSettings();


            CommonLog.ColorLog(ConsoleColor.DarkGreen, 1);
            TickTimerExample4();

            Console.ReadKey();
        }

        private static void TickTimerExample1()
        {
            TickTimer timer = new(10, false)
            {
                LogAction = CommonLog.Log,
                LogWarningAction = CommonLog.Warning,
                LogErrorAction = CommonLog.Error
            };

            uint interval = 66;
            int count = 50;
            int sum = 0;
            int taskId = 0;
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                DateTime historyTime = DateTime.UtcNow;
                taskId = timer.AddTask(
                    interval,
                    (int taskId) =>
                    {
                        DateTime nowTime = DateTime.UtcNow;
                        TimeSpan span = nowTime - historyTime;
                        historyTime = nowTime;
                        int delta = (int)(span.TotalMilliseconds - interval);
                        CommonLog.ColorLog(ConsoleColor.DarkYellow, $"间隔差:{delta}");

                        sum += Math.Abs(delta);
                        CommonLog.ColorLog(ConsoleColor.Magenta, $"TaskId:{taskId} 执行");
                    },
                    (int taskId) =>
                    {
                        CommonLog.ColorLog(ConsoleColor.Magenta, $"TaskId:{taskId} 取消");
                    },
                    count);
            });

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "calc")
                {
                    CommonLog.ColorLog(ConsoleColor.DarkRed, $"平均间隔={sum * 1.0f / count}");
                }
                else if (input == "del")
                {
                    timer.DeleteTask(taskId);
                }
            }
        }

        private static void TickTimerExample2()
        {
            TickTimer timer = new(10, true)
            {
                LogAction = CommonLog.Log,
                LogWarningAction = CommonLog.Warning,
                LogErrorAction = CommonLog.Error
            };

            uint interval = 66;
            int count = 50;
            int sum = 0;
            int taskId = 0;
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                DateTime historyTime = DateTime.UtcNow;
                taskId = timer.AddTask(
                    interval,
                    (int taskId) =>
                    {
                        DateTime nowTime = DateTime.UtcNow;
                        TimeSpan span = nowTime - historyTime;
                        historyTime = nowTime;
                        int delta = (int)(span.TotalMilliseconds - interval);
                        CommonLog.ColorLog(ConsoleColor.DarkYellow, $"间隔差:{delta}");

                        sum += Math.Abs(delta);
                        CommonLog.ColorLog(ConsoleColor.Magenta, $"TaskId:{taskId} 执行");
                    },
                    (int taskId) =>
                    {
                        CommonLog.ColorLog(ConsoleColor.Magenta, $"TaskId:{taskId} 取消");
                    },
                    count);
            });

            Task.Run(async () =>
            {
                CommonLog.Log("Handle 启动.");
                while (true)
                {
                    timer.HandleTask();
                    await Task.Delay(2);
                }
            });

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "calc")
                {
                    CommonLog.ColorLog(ConsoleColor.DarkRed, $"平均间隔={sum * 1.0f / count}");
                }
                else if (input == "del")
                {
                    timer.DeleteTask(taskId);
                }
            }
        }

        private static void TickTimerExample3()
        {
            TickTimer timer = new(0, false)
            {
                LogAction = CommonLog.Log,
                LogWarningAction = CommonLog.Warning,
                LogErrorAction = CommonLog.Error
            };

            uint interval = 66;
            int count = 50;
            int sum = 0;
            int taskId = 0;
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                DateTime historyTime = DateTime.UtcNow;
                taskId = timer.AddTask(
                    interval,
                    (int taskId) =>
                    {
                        DateTime nowTime = DateTime.UtcNow;
                        TimeSpan span = nowTime - historyTime;
                        historyTime = nowTime;
                        int delta = (int)(span.TotalMilliseconds - interval);
                        CommonLog.ColorLog(ConsoleColor.DarkYellow, $"间隔差:{delta}");

                        sum += Math.Abs(delta);
                        CommonLog.ColorLog(ConsoleColor.Magenta, $"TaskId:{taskId} 执行");
                    },
                    (int taskId) =>
                    {
                        CommonLog.ColorLog(ConsoleColor.Magenta, $"TaskId:{taskId} 取消");
                    },
                    count);
            });

            Task.Run(async () =>
            {
                CommonLog.Log("Handle 启动.");
                while (true)
                {
                    timer.UpdateTask();
                    await Task.Delay(2);
                }
            });

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "calc")
                {
                    CommonLog.ColorLog(ConsoleColor.DarkRed, $"平均间隔={sum * 1.0f / count}");
                }
                else if (input == "del")
                {
                    timer.DeleteTask(taskId);
                }
            }
        }

        private static void TickTimerExample4()
        {
            TickTimer timer = new(0, false)
            {
                LogAction = CommonLog.Log,
                LogWarningAction = CommonLog.Warning,
                LogErrorAction = CommonLog.Error
            };

            uint interval = 66;
            int count = 50;
            int sum = 0;
            int taskId = 0;
            Task.Run(async () =>
            {
                await Task.Delay(2000);
                DateTime historyTime = DateTime.UtcNow;
                taskId = timer.AddTask(
                    interval,
                    (int taskId) =>
                    {
                        DateTime nowTime = DateTime.UtcNow;
                        TimeSpan span = nowTime - historyTime;
                        historyTime = nowTime;
                        int delta = (int)(span.TotalMilliseconds - interval);
                        CommonLog.ColorLog(ConsoleColor.DarkYellow, $"间隔差:{delta}");

                        sum += Math.Abs(delta);
                        CommonLog.ColorLog(ConsoleColor.Magenta, $"TaskId:{taskId} 执行");
                    },
                    (int taskId) =>
                    {
                        CommonLog.ColorLog(ConsoleColor.Magenta, $"TaskId:{taskId} 取消");
                    },
                    count);
            });

            Task.Run(async () =>
            {
                CommonLog.Log("Handle 启动.");
                while (true)
                {
                    timer.UpdateTask();
                }
            });

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "calc")
                {
                    CommonLog.ColorLog(ConsoleColor.DarkRed, $"平均间隔={sum * 1.0f / count}");
                }
                else if (input == "del")
                {
                    timer.DeleteTask(taskId);
                }
            }
        }
    }
}
