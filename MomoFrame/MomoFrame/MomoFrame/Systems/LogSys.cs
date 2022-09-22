using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class LogSys
{
    public static LogSys Instance;

    //private LogWindow logWindow;

    public void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        //if (logWindow != null)
        //{
        //    logWindow.InitWindow();
        //}
    }


    //public void Log(object msg)
    //{
    //    if (logWindow != null)
    //    {
    //        logWindow.Log(msg);
    //    }
    //}

    //public void LogWarn(object msg)
    //{
    //    if (logWindow != null)
    //    {
    //        logWindow.LogWarn(msg);
    //    }
    //}

    //public void LogError(object msg)
    //{
    //    if (logWindow != null)
    //    {
    //        logWindow.LogError(msg);
    //    }
    //}


}
