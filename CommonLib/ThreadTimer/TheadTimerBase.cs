using System;

namespace ThreadTimer
{
    public abstract class TheadTimerBase
    {
        /// <summary>
        /// 创建定时任务
        /// </summary>
        /// <param name="delayTime">定时任务时间</param>
        /// <param name="onDo">定时任务回调</param>
        /// <param name="onCancel">取消任务回调</param>
        /// <param name="count">任务重复计数</param>
        /// <returns>当前计时器唯一任务ID</returns>
        public abstract int AddTask(uint delayTime, Action<int> onDo, Action<int> onCancel, int count = 1);

        public abstract bool DeleteTask(int taskId);

        public abstract void Reset();

        protected int taskId = 0;
        protected abstract int GenerateTaskId();
    }
}
