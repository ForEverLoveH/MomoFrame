using System;

namespace MProtocol
{
    [Serializable]
    public class M_Message
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
        None,
        Test,
    }
}