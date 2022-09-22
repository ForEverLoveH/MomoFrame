using MomoFrameServer.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGameFramework.Runtime;

namespace MomoFrameServer.Scripts
{
    public static class FrameworkRoot
    {
        static LogSys logSys = new LogSys();
        static MonoBehaviour monoBehaviour = new MonoBehaviour();
        static StarForce.GameEntry gameEntry = new StarForce.GameEntry();
        static NetServerSvc netServerSvc = new NetServerSvc();

        public static void Init_Start()
        {
            Awake();

            Start();

            Update();

        }

        public static void Awake()
        {
            logSys.Awake();

            InitGlobalExceptions();

            ProcedureComponent.m_AvailableProcedureTypeNames = new string[] { "ProcedureStart", "ProcedureMain" };
            ProcedureComponent.m_EntranceProcedureTypeName = "ProcedureStart";
            monoBehaviour.Awake();
            gameEntry.Awake();

            netServerSvc.Awake();
        }

        public static void Start()
        {
            monoBehaviour.Start();
            gameEntry.Start();

            netServerSvc.Start();
        }

        public static void Update()
        {
            Task.Run(() => {
                while (BaseComponent.IsUpdate)
                {
                    System.Threading.Thread.Sleep(BaseComponent.UpdateDelayMilliseconds);
                    netServerSvc.Update();

                }
            });
        }

        /// <summary>
        /// 全局异常 只限于主线程
        /// </summary>
        public static void InitGlobalExceptions()
        {
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
        }

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            dynamic err = e.ExceptionObject;
            Debug.LogError($"主线程异常:{err.Message}\r\n详细信息:\r\n{err.StackTrace}");
        }
    }
}
