using Ahpily.Concurrent;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ahpily.Timer
{
    /// <summary>
    /// 定时任务（计时器）管理类
    /// </summary>
    public class TimerManager
    {
        private static TimerManager instance = null;

        private static object o = 0;

        public static TimerManager Instance
        {
            get 
            {
                lock (o)
                {
                    if (instance == null)
                    {
                        instance = new TimerManager();
                    }
                    return instance;
                }

            }

        }

        /// <summary>
        /// 实现定时器的主要功能就是这个Timer类
        /// </summary>
        private System.Timers.Timer timer;

        /// <summary>
        /// 这个字典存储：任务id 和 任务模型 的映射
        /// </summary>
        private ConcurrentDictionary<int, TimerModel> idModelDict = new ConcurrentDictionary<int, TimerModel>();

        /// <summary>
        /// 要移除的任务ID列表
        /// </summary>
        private List<int> removeList = new List<int>();

        /// <summary>
        /// 用来表示ID
        /// </summary>
        private ConcurrentInt id = new ConcurrentInt(-1);


        public TimerManager()
        {
            timer = new System.Timers.Timer(100);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        /// <summary>
        /// 到达时间间隔时候触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (removeList)
            {
                TimerModel tmpmodel = null;
                foreach (var id in removeList)
                {
                    idModelDict.TryRemove(id, out tmpmodel);
                }
                removeList.Clear();

                foreach (var model in idModelDict.Values)
                {
                    if (model.Time <= DateTime.Now.Ticks)
                    {
                        model.Run();
                        removeList.Add(model.Id);
                    }
                }
            }
        }


        /// <summary>
        /// 添加定时任务 指定触发的时间 2021年5月31日20:22:22
        /// </summary>
        public void AddTimerEvent(DateTime datetime, TimerDelegate timerDelegate)
        {
            long delayTime = (datetime.Ticks - DateTime.Now.Ticks) / 10000;
            if(delayTime <= 0)
            {
                return;
            }
            AddTimerEvent(delayTime, timerDelegate);
        }

        /// <summary>
        /// 添加定时任务 指定延迟的事件时间 40s
        /// </summary>
        /// <param name="delayTime">毫秒！！</param>
        /// <param name="timerDelegate"></param>
        public void AddTimerEvent(long delayTime, TimerDelegate timerDelegate)
        {
            TimerModel model = new TimerModel(id.Add_Get(), DateTime.Now.Ticks + delayTime * 10000, timerDelegate);
            idModelDict.TryAdd(model.Id, model);

        }

    }
}
