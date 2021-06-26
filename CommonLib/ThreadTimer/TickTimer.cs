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
        private readonly ConcurrentDictionary<int, TickTask> taskDic; // 线程安全，并且可在遍历中移除元素

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
            double destTime = startTime + delayTime;
            TickTask task = new TickTask(tid, delayTime, count, destTime, onDo, onCancel, startTime);
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
            double nowTime = GetUtcMilliseconds();
            foreach (KeyValuePair<int, TickTask> item in taskDic)
            {
                TickTask task = item.Value;
                if (nowTime < task.DestinationTime)
                {
                    continue;
                }

                ++task.CurrLoopIndex;
                if (task.Count-- > 0)
                {
                    if (task.Count == 0)
                    {
                        FinishTask(task.TaskId);
                    }
                    else
                    {
                        task.DestinationTime = task.StartTime + task.DelayTime * (task.CurrLoopIndex + 1);
                        CallDo(task.TaskId, task.OnDo);
                    }
                }
                else
                {
                    task.DestinationTime = task.StartTime + task.DelayTime * (task.CurrLoopIndex + 1);
                    CallDo(task.TaskId, task.OnDo);
                }
            }
        }

        private void FinishTask(int taskId)
        {
            // 线程安全字典，遍历过程中删除无影响
            if (taskDic.TryRemove(taskId, out TickTask task))
            {
                CallDo(taskId, task.OnDo);
                task.OnDo = null;
            }
        }

        private void CallDo(int taskId, Action<int> onDo)
        {
            onDo.Invoke(taskId);
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
            public ulong CurrLoopIndex; // 代替在循环中加时间，防止累积偏差

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

                CurrLoopIndex = 0;
            }
        }
    }
}
