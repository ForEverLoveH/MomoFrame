

using GameFramework;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 基础组件。
    /// </summary>
    public sealed class BaseComponent : GameFrameworkComponent
    {

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        public override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// Updata循环类型
        /// </summary>
        private enum UpdateType
        {
            Timer,
            Task
        }

        public enum FPS
        {
            FPS_30,
            FPS_60,
            FPS_120,
        }

        private UpdateType updateType = UpdateType.Task;
        public FPS GameFps = FPS.FPS_30;

        /// <summary>
        /// Update循环延迟毫秒数
        /// </summary>
        public static int UpdateDelayMilliseconds;

        /// <summary>
        /// 实现定时器的主要功能就是这个Timer类
        /// </summary>
        private System.Timers.Timer timer;

        public static bool IsUpdate = true;

        public void Start()
        {
            SetFPS(GameFps);
            switch (updateType)
            {
                case UpdateType.Timer:
                    timer = new System.Timers.Timer(UpdateDelayMilliseconds);
                    timer.AutoReset = true;
                    timer.Enabled = IsUpdate;
                    timer.Elapsed += Update;
                    break;
                case UpdateType.Task:
                    Task.Run(() =>
                    {
                        while (IsUpdate)
                        {
                            System.Threading.Thread.Sleep(UpdateDelayMilliseconds);
                            Update(null, null);
                        }
                    });
                    break;
                default:
                    break;
            }            
        }

        public void SetFPS(FPS fps)
        {
            switch (fps)
            {
                case FPS.FPS_30:
                    UpdateDelayMilliseconds = 1000 / 30;
                    break;
                case FPS.FPS_60:
                    UpdateDelayMilliseconds = 1000 / 60;
                    break;
                case FPS.FPS_120:
                    UpdateDelayMilliseconds = 1000 / 120;
                    break;
                default:
                    break;
            }
            if (timer != null)
            {
                timer.Interval = UpdateDelayMilliseconds;
            }
        }

        public void SetFPS(int fps)
        {
            UpdateDelayMilliseconds = fps;
            if (timer != null)
            {
                timer.Interval = UpdateDelayMilliseconds;
            }
        }


        private static readonly object UpdataLock = new object();

        long dataTime = 0;
        long unscaledDeltaTime = 0;

        private void Update(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (UpdataLock)
            {

                if (unscaledDeltaTime == 0)
                {
                    unscaledDeltaTime = DateTime.Now.Ticks;
                }
                if (dataTime != 0)
                {
                    GameFrameworkEntry.Update((float)(DateTime.Now.Ticks - dataTime) / (10000f * 1000f), (float)(DateTime.Now.Ticks - unscaledDeltaTime) / (10000 * 1000));
                }
                dataTime = DateTime.Now.Ticks;

            }
        }
    }
}
