using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ahpily;
using PEUtils;



public class Debug
{
    private static LogSys logSys;

    public static void InitDebug()
    {
        LogConfig logConfig = new LogConfig() { enableCover = false, saveName = $"{DateTime.Now.ToString("yyyy-MM-dd HH")}@Log.txt"};
        PELog.InitSettings(logConfig);
        logSys = LogSys.Instance;
    }

    public static void ColorLog(LogColor logColor, object msg)
    {
        SingleExecute.Instance.Execute(() =>
        {
            if (msg == null)
            {
                return;
            }
            PELog.ColorLog(logColor, msg);
        });
    }

    public static void Log(object msg)
    {
        SingleExecute.Instance.Execute(() =>
        {
            if (msg == null)
            {
                return;
            }
            PELog.Log(msg);
        });
    }

    public static void LogWarn(object msg)
    {
        SingleExecute.Instance.Execute(() =>
        {
            if (msg == null)
            {
                return;
            }
            PELog.Warn(msg);
        });
    }

    public static void LogError(object msg)
    {
        SingleExecute.Instance.Execute(() =>
        {
            if (msg == null)
            {
                return;
            }
            PELog.Error(msg);
        });

    }
   
}
