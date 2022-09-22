using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace MomoFrame.Windows.UserControlViewModel
{

    public class LogViewModel
    {
        private ObservableCollection<LogData> _logDataList = new ObservableCollection<LogData>();

        public ObservableCollection<LogData> LogDataList
        {
            get { return _logDataList; }
            set { this._logDataList = value;}
        }
    }

    public class LogData
    {
        public System.Windows.Media.Brush MsgTypeImgForeground { get; set; }

        public string MsgTypeImg { get; set; }

        public string MsgType { get; set; }

        public string Msg { get; set; }

        public int MsgRepeatNumber { get; set; }
    }
}


