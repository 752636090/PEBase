using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace ThreadTimer
{
    /// <summary>
    /// 毫秒级精确的定时器
    /// </summary>
    public class TickTimer : ThreadTimerBase
    {
        private readonly DateTime startDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        private readonly ConcurrentDictionary<int, TickTask> taskDic;

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
                return -1;
            }
        }

        public override bool DeleteTask(int taskId)
        {
            
        }

        public override void Reset()
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
