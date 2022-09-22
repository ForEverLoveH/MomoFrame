using MProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchSocket.Sockets;

namespace MomoFrameServer.Net
{
    public class NetServerSvc
    {
        public static NetServerSvc Instance;

        public ServerSocket socket;

        public void Awake()
        {
            Instance = this;
        }

        private Queue<M_Message> msgQueSetver = null;

        public void Start()
        {
            socket = new ServerSocket();
            socket.InitTcpSocket(new IPHost[] { new IPHost("127.0.0.1:6666"), new IPHost(9999) });
            Init();
        }

        public void Init()
        {
            msgQueSetver = new Queue<M_Message>();
        }

        public void AddMsgQue(M_Message msg)
        {
            if (msgQueSetver != null)
            {
                lock (msgQueSetver)
                {
                    msgQueSetver.Enqueue(msg);
                }
            }
        }

        public void Update()
        {
            if (msgQueSetver != null)
            {
                while (msgQueSetver.Count > 0)
                {
                    lock (msgQueSetver)
                    {
                        M_Message msg = msgQueSetver.Dequeue();
                        HandleMsg(msg);
                    }
                }
            }
        }

        private void HandleMsg(M_Message msg)
        {
            switch (msg.cmd)
            {
                case CMD.None:

                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(M_Message msg)
        {

        }

        /// <summary>
        /// 接受消息
        /// </summary>
        /// <param name="msg"></param>
        public void OnReciveMsg(M_Message msg)
        {
            Debug.Log($"ServerMsg CMD:{msg.cmd.ToString()}");
            if (Instance != null)
            {
                Instance.AddMsgQue(msg);
            }
        }
    }
}
