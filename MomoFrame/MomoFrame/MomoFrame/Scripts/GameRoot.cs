using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using UnityGameFramework.Runtime;

        
public static class GameRoot
{

    static LogSys logSys = new LogSys();
    static MonoBehaviour monoBehaviour = new MonoBehaviour();
    static StarForce.GameEntry gameEntry = new StarForce.GameEntry();
    static LocalNetClientSvc localNetClientSvc = new LocalNetClientSvc();
    static LocalNetServerSvc localNetServerSvc = new LocalNetServerSvc();
    static NetClientSvc netClientSvc = new NetClientSvc();
    static SqliteDB sqliteDB = new SqliteDB();
    static MainSys mainSys = new MainSys();

    public static void GameRootStart()
    {
        Awake();

        Start();

        Update();

    }
    /// <summary>
    /// 启动执行
    /// </summary>
    private static void Awake()
    {
        logSys.Awake();

        ProcedureComponent.m_AvailableProcedureTypeNames = new string[] { "ProcedureStart", "ProcedureMain" };
        ProcedureComponent.m_EntranceProcedureTypeName = "ProcedureStart";
        monoBehaviour.Awake();
        gameEntry.Awake();

        localNetClientSvc.Awake();
        localNetServerSvc.Awake();
        netClientSvc.Awake();
        sqliteDB.Awake();


        mainSys.Awake();
    }

    /// <summary>
    /// 启动后执行
    /// </summary>
    private static void Start()
    {
        Debug.InitDebug();

        //UI线程未捕获异常处理事件
        MomoFrame.App.Instance.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
        //Task线程内未捕获异常处理事件
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        // 非UI线程未捕获异常处理事件
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);


        string strProcessName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
        //检查进程是否已经启动，已经启动则退出程序。 
        if (System.Diagnostics.Process.GetProcessesByName(strProcessName).Length > 1)
        {
            Debug.LogError("请勿重复启动...", false, true, Debug.LogErrorType.FatalGlobal);
            MomoFrame.Windows.WindowView.MainWindow.Instance.IsGameEntryShutdown = false;
            MomoFrame.Windows.WindowView.MainWindow.Instance.Close();
            Ahpily.Timer.TimerManager.Instance.AddTimerEvent(5 * 1000, () =>
            {
                UnityGameFramework.Runtime.GameEntry.Shutdown(UnityGameFramework.Runtime.ShutdownType.Quit);
            });
        }

        mainSys.Init();
        
    }

    /// <summary>
    /// 每帧执行
    /// </summary>
    private static void Update()
    {
        Task.Run(() => {
            while (BaseComponent.IsUpdate)
            {
                System.Threading.Thread.Sleep(BaseComponent.UpdateDelayMilliseconds);
                localNetClientSvc.Update();
                localNetServerSvc.Update();
            }
        });
    }

    /// <summary>
    /// 主线程异常
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        try
        {
            e.Handled = true; //把 Handled 属性设为true，表示此异常已处理，程序可以继续运行，不会强制退出      
            Debug.LogWarn($"主线程异常:{e.Exception.Message}\r\n异常详细信息:\r\n{e.Exception.StackTrace}");
        }
        catch (Exception ex)
        {
            //此时程序出现严重异常，将强制结束退出
            Debug.LogError("UI线程发生致命错误！" + ex.ToString());
        }

    }

    /// <summary>
    /// 非主线程异常
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        StringBuilder sbEx = new StringBuilder();
        if (e.IsTerminating)
        {
            sbEx.Append("非主线程发生致命错误");
        }
        sbEx.Append("非主线程异常：");
        if (e.ExceptionObject is Exception)
        {
            sbEx.Append(((Exception)e.ExceptionObject).Message);
        }
        else
        {
            sbEx.Append(e.ExceptionObject);
        }
        Debug.LogError(sbEx.ToString());
    }

    /// <summary>
    /// Task线程异常
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        //task线程内未处理捕获
        Debug.LogError($"Task线程异常:{e.Exception.Message}\r\n异常详细信息:\r\n{e.Exception.StackTrace}");
        e.SetObserved();//设置该异常已察觉（这样处理后就不会引起程序崩溃）
    }
}
