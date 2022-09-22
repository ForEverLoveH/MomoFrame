using MomoFrame.Windows.UserControlViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MomoFrame.Windows.UserControlView
{
    /// <summary>
    /// Log.xaml 的交互逻辑
    /// </summary>
    public partial class Log : UserControl
    {
        public static Log Instance = null;

        public LogViewModel logViewModel;

        public Log()
        {
            InitializeComponent();

            Instance = this;

            LogDataGrid.Items.Clear();

            logViewModel = new LogViewModel();
            this.DataContext = logViewModel;
        }

        public enum LogFsm
        {
            Log,
            LogWarn,
            LogError
        }

        string RepeatMsg = null;

        public void AddLog(LogFsm logFsm, object msg)
        {
            if (msg == null)
            {
                return;
            }
            if (RepeatMsg == null)
            {
                RepeatMsg = msg.ToString();
                LogDataGrid?.Dispatcher.InvokeAsync(new Action(() =>
                {
                    DelegateSetValue(logFsm, msg);
                }));
            }
            else
            {
                if (RepeatMsg == msg.ToString())
                {
                    logViewModel.LogDataList[logViewModel.LogDataList.Count - 1].MsgRepeatNumber += 1;
                }
                else
                {
                    LogDataGrid?.Dispatcher.InvokeAsync(new Action(() =>
                    {
                        DelegateSetValue(logFsm, msg);
                    }));
                }
            }
        }


        private void DelegateSetValue(LogFsm logFsm, object msg)
        {
            switch (logFsm)
            {
                case LogFsm.Log:
                    LogData logData = new LogData()
                    {
                        MsgTypeImgForeground = new SolidColorBrush(Color.FromRgb(26, 250, 41)),
                        MsgTypeImg = "\xe680",
                        MsgType = "消息",
                        Msg = msg.ToString(),
                        MsgRepeatNumber = 1
                    };
                    logViewModel.LogDataList.Add(logData);
                    //SetScroll(LogDataGrid, LogDataGrid.Items.Count - 1);
                    break;
                case LogFsm.LogWarn:
                    LogData logWarnData = new LogData()
                    {
                        MsgTypeImgForeground = new SolidColorBrush(Color.FromRgb(244, 234, 42)),
                        MsgTypeImg = "\xe605",
                        MsgType = "警告",
                        Msg = msg.ToString(),
                        MsgRepeatNumber = 1
                    };
                    logViewModel.LogDataList.Add(logWarnData);
                    SetScroll(LogDataGrid, LogDataGrid.Items.Count - 1);
                    break;
                case LogFsm.LogError:
                    LogData logErrorData = new LogData()
                    {
                        MsgTypeImgForeground = new SolidColorBrush(Color.FromRgb(216, 30, 6)),
                        MsgTypeImg = "\xe63e",
                        MsgType = "错误",
                        Msg = msg.ToString(),
                        MsgRepeatNumber = 1
                    };
                    logViewModel.LogDataList.Add(logErrorData);
                    SetScroll(LogDataGrid, LogDataGrid.Items.Count - 1);
                    break;
                default:
                    break;
            }
        }

        public void SetScroll(DataGrid dataGrid, int SelectedIndex)
        {
            if (SelectedIndex <= dataGrid.Items.Count - 1)
            {
                dataGrid.SelectedIndex = SelectedIndex;
                dataGrid.UpdateLayout();
                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
            }
        }
    }
}
