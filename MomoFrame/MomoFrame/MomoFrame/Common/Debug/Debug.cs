using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ahpily;
using PEUtils;
using HandyControl.Controls;



public class Debug
{
    private static LogSys logSys;

    public static void InitDebug()
    {
        LogConfig logConfig = new LogConfig() { enableCover = false, saveName = $"{DateTime.Now.ToString("yyyy-MM-dd HH")}@Log.txt" };
        PELog.InitSettings(logConfig);
        logSys = LogSys.Instance;
    }

    public enum LogShowPosition
    {
        MainGrowlMsg
    }

    public enum LogType
    {
        Info,
        Success,
        InfoGlobal,
        SuccessGlobal
    }

    public enum LogWarnType
    {
        Warning,
        WarningGlobal
    }

    public enum LogErrorType
    {
        Error,
        Fatal,
        ErrorGlobal,
        FatalGlobal
    }

#if NET6_0 || NET5_0
    public static void Log(object msg, bool IsDetailedMsg = true, bool IsGrowl = false, LogType logType = LogType.InfoGlobal, LogShowPosition logShowPosition = LogShowPosition.MainGrowlMsg)
    {
        SingleExecute.Instance.Execute(() =>
        {
            if (msg == null)
            {
                return;
            }
            string LogMsg = PELog.DLog(msg).ToString();
            if (logShowPosition == LogShowPosition.MainGrowlMsg)
            {
                if (IsGrowl)
                {
                    switch (logType)
                    {
                        case LogType.Info:
                            if (IsDetailedMsg)
                            {
                                Growl.Info(LogMsg, "MainGrowlMsg");
                            }
                            else
                            {
                                Growl.Info(msg.ToString(), "MainGrowlMsg");
                            }
                            break;
                        case LogType.Success:
                            if (IsDetailedMsg)
                            {
                                Growl.Success(LogMsg, "MainGrowlMsg");
                            }
                            else
                            {
                                Growl.Success(msg.ToString(), "MainGrowlMsg");
                            }
                            break;
                        case LogType.InfoGlobal:
                            if (IsDetailedMsg)
                            {
                                Growl.InfoGlobal(LogMsg);
                            }
                            else
                            {
                                Growl.InfoGlobal(msg.ToString());
                            }
                            break;
                        case LogType.SuccessGlobal:
                            if (IsDetailedMsg)
                            {
                                Growl.SuccessGlobal(LogMsg);
                            }
                            else
                            {
                                Growl.SuccessGlobal(msg.ToString());
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (MomoFrame.Windows.WindowView.MainWindow.Instance != null)
                {
                    MomoFrame.Windows.WindowView.MainWindow.Instance.MainLogDataList.AddLog(MomoFrame.Windows.UserControlView.Log.LogFsm.Log, LogMsg);
                }
            }

        });
    }

    public static void LogWarn(object msg, bool IsDetailedMsg = true, bool IsGrowl = true, LogWarnType logWarnType = LogWarnType.WarningGlobal, LogShowPosition logShowPosition = LogShowPosition.MainGrowlMsg)
    {
        SingleExecute.Instance.Execute(() =>
        {
            if (msg == null)
            {
                return;
            }
            string LogWarnMsg = PELog.DWarn(msg).ToString();
            if (logShowPosition == LogShowPosition.MainGrowlMsg)
            {
                if (IsGrowl)
                {
                    switch (logWarnType)
                    {
                        case LogWarnType.Warning:
                            if (IsDetailedMsg)
                            {
                                Growl.Warning(LogWarnMsg, "MainGrowlMsg");
                            }
                            else
                            {
                                Growl.Warning(msg.ToString(), "MainGrowlMsg");
                            }
                            break;
                        case LogWarnType.WarningGlobal:
                            if (IsDetailedMsg)
                            {
                                Growl.WarningGlobal(LogWarnMsg);
                            }
                            else
                            {
                                Growl.WarningGlobal(msg.ToString());
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (MomoFrame.Windows.WindowView.MainWindow.Instance != null)
                {
                    MomoFrame.Windows.WindowView.MainWindow.Instance.MainLogDataList.AddLog(MomoFrame.Windows.UserControlView.Log.LogFsm.LogWarn, LogWarnMsg);
                }
            }

        });
    }

    public static void LogError(object msg, bool IsDetailedMsg = true, bool IsGrowl = true, LogErrorType logErrorType = LogErrorType.ErrorGlobal, LogShowPosition logShowPosition = LogShowPosition.MainGrowlMsg)
    {
        SingleExecute.Instance.Execute(() =>
        {
            if (msg == null)
            {
                return;
            }
            string LogErrorMsg = PELog.DError(msg).ToString();
            if (logShowPosition == LogShowPosition.MainGrowlMsg)
            {
                if (IsGrowl)
                {
                    switch (logErrorType)
                    {
                        case LogErrorType.Error:
                            if (IsDetailedMsg)
                            {
                                Growl.Error(LogErrorMsg, "MainGrowlMsg");
                            }
                            else
                            {
                                Growl.Error(msg.ToString(), "MainGrowlMsg");
                            }
                            break;
                        case LogErrorType.Fatal:
                            if (IsDetailedMsg)
                            {
                                Growl.Fatal(LogErrorMsg, "MainGrowlMsg");
                            }
                            else
                            {
                                Growl.Fatal(msg.ToString(), "MainGrowlMsg");
                            }
                            break;
                        case LogErrorType.ErrorGlobal:
                            if (IsDetailedMsg)
                            {
                                Growl.ErrorGlobal(LogErrorMsg);
                            }
                            else
                            {
                                Growl.ErrorGlobal(msg.ToString());
                            }
                            break;
                        case LogErrorType.FatalGlobal:
                            if (IsDetailedMsg)
                            {
                                Growl.FatalGlobal(LogErrorMsg);
                            }
                            else
                            {
                                Growl.FatalGlobal(msg.ToString());
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (MomoFrame.Windows.WindowView.MainWindow.Instance != null)
                {
                    MomoFrame.Windows.WindowView.MainWindow.Instance.MainLogDataList.AddLog(MomoFrame.Windows.UserControlView.Log.LogFsm.LogError, LogErrorMsg);
                }
            }

        });

    }
#else

    public static void Log(object msg, bool IsDetailedMsg = true, bool IsGrowl = false, LogType logType = LogType.Info, LogShowPosition logShowPosition = LogShowPosition.MainGrowlMsg)
    {
        SingleExecute.Instance.Execute(() => {
            string LogMsg = PELog.DLog(msg).ToString();
            if (logShowPosition == LogShowPosition.MainGrowlMsg)
            {
                if (IsGrowl)
                {
                    switch (logType)
                    {
                        case LogType.Info:
                            if (IsDetailedMsg)
                            {
                                Growl.Info(LogMsg, "MainGrowlMsg");
                            }
                            else
                            {
                                Growl.Info(msg.ToString(), "MainGrowlMsg");
                            }
                            break;
                        case LogType.Success:
                            if (IsDetailedMsg)
                            {
                                Growl.Success(LogMsg, "MainGrowlMsg");
                            }
                            else
                            {
                                Growl.Success(msg.ToString(), "MainGrowlMsg");
                            }
                            break;
                        case LogType.InfoGlobal:
                            if (IsDetailedMsg)
                            {
                                Growl.InfoGlobal(LogMsg);
                            }
                            else
                            {
                                Growl.InfoGlobal(msg.ToString());
                            }
                            break;
                        case LogType.SuccessGlobal:
                            if (IsDetailedMsg)
                            {
                                Growl.SuccessGlobal(LogMsg);
                            }
                            else
                            {
                                Growl.SuccessGlobal(msg.ToString());
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (MomoFrame.Windows.WindowView.MainWindow.Instance != null)
                {
                    MomoFrame.Windows.WindowView.MainWindow.Instance.MainLogDataList.AddLog(MomoFrame.Windows.UserControlView.Log.LogFsm.Log, LogMsg);
                }
            }

        });
    }

    public static void LogWarn(object msg, bool IsDetailedMsg = true, bool IsGrowl = true, LogWarnType logWarnType = LogWarnType.Warning, LogShowPosition logShowPosition = LogShowPosition.MainGrowlMsg)
    {
        SingleExecute.Instance.Execute(() => {
            string LogWarnMsg = PELog.DWarn(msg).ToString();
            if (logShowPosition == LogShowPosition.MainGrowlMsg)
            {
                if (IsGrowl)
                {
                    switch (logWarnType)
                    {
                        case LogWarnType.Warning:
                            if (IsDetailedMsg)
                            {
                                Growl.Warning(LogWarnMsg, "MainGrowlMsg");
                            }
                            else
                            {
                                Growl.Warning(msg.ToString(), "MainGrowlMsg");
                            }
                            break;
                        case LogWarnType.WarningGlobal:
                            if (IsDetailedMsg)
                            {
                                Growl.WarningGlobal(LogWarnMsg);
                            }
                            else
                            {
                                Growl.WarningGlobal(msg.ToString());
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (MomoFrame.Windows.WindowView.MainWindow.Instance != null)
                {
                    MomoFrame.Windows.WindowView.MainWindow.Instance.MainLogDataList.AddLog(MomoFrame.Windows.UserControlView.Log.LogFsm.LogWarn, LogWarnMsg);
                }
            }

        });
    }

    public static void LogError(object msg, bool IsDetailedMsg = true, bool IsGrowl = true, LogErrorType logErrorType = LogErrorType.Error, LogShowPosition logShowPosition = LogShowPosition.MainGrowlMsg)
    {
        SingleExecute.Instance.Execute(() => {
            string LogErrorMsg = PELog.DError(msg).ToString();
            if (logShowPosition == LogShowPosition.MainGrowlMsg)
            {
                if (IsGrowl)
                {
                    switch (logErrorType)
                    {
                        case LogErrorType.Error:
                            if (IsDetailedMsg)
                            {
                                Growl.Error(LogErrorMsg, "MainGrowlMsg");
                            }
                            else
                            {
                                Growl.Error(msg.ToString(), "MainGrowlMsg");
                            }
                            break;
                        case LogErrorType.Fatal:
                            if (IsDetailedMsg)
                            {
                                Growl.Fatal(LogErrorMsg, "MainGrowlMsg");
                            }
                            else
                            {
                                Growl.Fatal(msg.ToString(), "MainGrowlMsg");
                            }
                            break;
                        case LogErrorType.ErrorGlobal:
                            if (IsDetailedMsg)
                            {
                                Growl.ErrorGlobal(LogErrorMsg);
                            }
                            else
                            {
                                Growl.ErrorGlobal(msg.ToString());
                            }
                            break;
                        case LogErrorType.FatalGlobal:
                            if (IsDetailedMsg)
                            {
                                Growl.FatalGlobal(LogErrorMsg);
                            }
                            else
                            {
                                Growl.FatalGlobal(msg.ToString());
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (MomoFrame.Windows.WindowView.MainWindow.Instance != null)
                {
                    MomoFrame.Windows.WindowView.MainWindow.Instance.MainLogDataList.AddLog(MomoFrame.Windows.UserControlView.Log.LogFsm.LogError, LogErrorMsg);
                }
            }

        });

    }

#endif

}
