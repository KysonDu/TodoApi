using Pomelo.AspNetCore.TimedJob;
using System;

namespace TodoApi.TimedJobs
{
    public class Schedule : Job
    {
        /// <summary>
        /// 任务开始时间
        /// </summary>
        private const string _beginTime = "2023-03-07 17:07";

        /// <summary>
        /// 执行间隔，单位：毫秒（以任务开始时间为基准）
        /// </summary>
        private const int _interval = 1000 * 5;

        /// <summary>
        /// 是否需要等待上一次任务执行完毕后才可执行下一次任务
        /// </summary>
        private const bool _skipWhileExecuting = true;

        [Invoke(Begin = _beginTime, Interval = _interval, SkipWhileExecuting = _skipWhileExecuting)]
        public void Run()
        {
            TaskA();
            TaskB();
        }

        private void TaskA()
        {
            Console.WriteLine("任务A");
        }

        private void TaskB()
        {
            Console.WriteLine("任务B");
        }
    }
}



