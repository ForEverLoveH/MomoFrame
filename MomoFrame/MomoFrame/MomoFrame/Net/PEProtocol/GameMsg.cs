//功能：逻辑业务协议


using System;
using System.Collections.Generic;

namespace PEProtocol
{
    [Serializable]
    public class GameMsg
    {
        public CMD cmd;
        public ErrorCode err;

       
    }


    public enum ErrorCode
    {
        None,

    }

    public enum CMD
    {
        None = 0,

    }
}
