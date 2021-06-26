using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadTimer
{
    /// <summary>
    /// 毫秒级精确的定时器
    /// </summary>
    public class TickTimer : ThreadTimerBase
    {
        private readonly DateTime startDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        private readonly ConcurrentDictionary<int, TickTask> taskDic;

        private readonly Thread timerThread;

        public TickTimer(int interval = 0)
        {
            taskDic = new ConcurrentDictionary<int, TickTask>();
            
            if (interval != 0)
            {
                void StartTick()
                {
                    try
                    {
                        while (true)
                        {
                            UpdateTask();
                            Thread.Sleep(interval);
                        }
                    }
                    catch (ThreadAbortException e)
                    {
                        LogWarningAction?.Invoke($"Tick线程中止:{e}");
                    }
                }
                timerThread = new Thread(new ThreadStart(StartTick));
                timerThread.Start();
            }
        }

        public override int AddTask(uint delayTime, Action<int> onDo, Action<int> onCancel, int count = 1)
        {
            int tid = GenerateTaskId();
            double startTime = GetUtcMilliseconds();
            TickTask task = new TickTask(tid, delayTime, count, startTime, onDo, onCancel, startTime);
            if (taskDic.TryAdd(tid, task))
            {
                return tid;
            }
            else
            {
                LogWarningAction?.Invoke($"key:{tid} 已经存在.");
                return -1;
            }
        }

        public override bool DeleteTask(int taskId)
        {
            if (taskDic.TryRemove(taskId, out TickTask task))
            {
                task.OnCancel?.Invoke(taskId);
                return true;
            }
            else
            {
                LogWarningAction?.Invoke($"tid:{taskId} 移除失败.");
                return false;
            }
        }

        public override void Reset()
        {
            taskDic.Clear();
            if (timerThread != null) // 在外面驱动就会为空
            {
                timerThread.Abort();
            }
        }

        public void UpdateTask()
        {

        }

        private double GetUtcMilliseconds()
        {
            TimeSpan timeSpan = DateTime.UtcNow - startDateTime;
            return timeSpan.TotalMilliseconds;
        }

        protected override int GenerateTaskId()
        {
            
        }

        class TickTask
        {
            public int TaskId;
            public uint DelayTime;
            public int Count;
            public double DestinationTime;
            public Action<int> OnDo;
            public Action<int> OnCancel;

            public double StartTime;

            public TickTask(int taskId, uint delayTime, int count, double destinationTime, 
                Action<int> onDo, Action<int> onCancel, double startTime)
            {
                TaskId = taskId;
                DelayTime = delayTime;
                Count = count;
                DestinationTime = destinationTime;
                OnDo = onDo;
                OnCancel = onCancel;
                StartTime = startTime;
            }
        }
    }
}
