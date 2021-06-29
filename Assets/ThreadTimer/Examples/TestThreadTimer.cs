using System;
using System.Collections;
using System.Collections.Generic;
using ThreadTimer;
using UnityEngine;
using Utils;

public class TestThreadTimer : MonoBehaviour
{
    private TickTimer timer;

    private void Start()
    {
        Example1Start();
    }

    private Action onUpdate;
    private void Update()
    {
        onUpdate?.Invoke();
    }

    private void Example1Start()
    {
        LogConfig logConfig = new LogConfig { LogType = Utils.LogType.Unity };
        CommonLog.InitSettings(logConfig);

        uint interval = 66;
        int count = 50;
        int sum = 0;
        int taskId = 0;

        //TickTimer timer = new TickTimer(0, true)
        //TickTimer timer = new TickTimer(0, false)
        //TickTimer timer = new TickTimer(10, false)
        TickTimer timer = new TickTimer(10, true)
        {
            LogAction = CommonLog.Log,
            LogWarningAction = CommonLog.LogWarning,
            LogErrorAction = CommonLog.LogError
        };

        onUpdate = () =>
        {
            //timer.UpdateTask(); // new TickTimer(0, xxx)
            timer.HandleTask(); // new TickTimer(xxx, true)

            if (Input.GetKeyDown(KeyCode.A))
            {
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
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                timer.DeleteTask(taskId);
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                CommonLog.ColorLog(ConsoleColor.DarkRed, $"平均间隔={sum * 1.0f / count}");
            }
        };
    }
}
