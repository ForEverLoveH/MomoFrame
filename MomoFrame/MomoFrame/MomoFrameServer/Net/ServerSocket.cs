using MProtocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchSocket.Core;
using TouchSocket.Core.ByteManager;
using TouchSocket.Core.Config;
using TouchSocket.Sockets;

namespace MomoFrameServer.Net
{
    public class ServerSocket
    {
        TcpService service;

        public void InitTcpSocket(IPHost[] iPHost)
        {
            service = new TcpService();
            service.Connecting += ClientConnectingEventHandler;
            service.Connected += ClientConnectedEventHandler;
            service.Disconnected += ClientDisconnectedEventHandler;
            service.Received += ClientReceivedEventHandler;

            service.Setup(new TouchSocketConfig()//载入配置     
                .SetListenIPHosts(iPHost)//同时监听两个地址
                .SetMaxCount(10000)
                .SetThreadCount(10)
                .SetClearInterval(30 * 60 * 1000)
                .SetDataHandlingAdapter(() => { return new FixedHeaderPackageAdapter(); })
                )
                .Start();//启动
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Debug.Log("TCPSetver Start...");
        }


        //有客户端正在连接
        private void ClientConnectingEventHandler(SocketClient client, ClientOperationEventArgs e)
        {
            Debug.Log($"{client.GetIPPort()} 正在连接");
        }

        //有客户端连接
        private void ClientConnectedEventHandler(SocketClient client, TouchSocketEventArgs e)
        {
            Debug.Log($"{client.GetIPPort()} 连接成功");
        }

        //有客户端断开连接
        private void ClientDisconnectedEventHandler(SocketClient client, ClientDisconnectedEventArgs e)
        {
            Debug.LogWarn($"{client.GetIPPort()} 已断开连接");
        }

        //从客户端收到信息
        private void ClientReceivedEventHandler(SocketClient client, ByteBlock byteBlock, IRequestInfo requestInfo)
        {
            //M_Message m_Message = Serialize.FromBytes<M_Message>(byteBlock.Buffer);
            string msg = Encoding.GetEncoding("GB2312").GetString(byteBlock.Buffer, 0, byteBlock.Len);
            Debug.ColorLog(PEUtils.LogColor.None ,$"已从{client.GetIPPort()}接收到信息：{msg}");

            byte[] buff = Encoding.GetEncoding("GB2312").GetBytes(msg);
            //client.Send(buff);//将收到的信息直接返回给发送方

            //client.Send("id",mes);//将收到的信息返回给特定ID的客户端

            var clients = service?.GetClients();
            if (clients == null)
            {
                return;
            }
            foreach (var targetClient in clients)//将收到的信息返回给在线的所有客户端。
            {
                if (targetClient.ID != client.ID)
                {
                    targetClient.Send(buff);
                }
            }
        }

        public void Dispose()
        {
            if (service != null)
            {
                service.Stop();
                service.Clear();
                service.Dispose();
                service = null;
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void Send(string id ,string msg)
        {
            Send(id, Encoding.GetEncoding("GB2312").GetBytes(msg));
        }

        public void Send(string id, M_Message msg)
        {
            Send(id, Serialize.ToBytes(msg));
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="buffer"></param>
        public void Send(string id, byte[] buffer)
        {
            if (service != null)
            {
                service.Send(id, buffer);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public void Send(string id,byte[] buffer, int offset, int length)
        {
            if (service != null)
            {
                service.Send(id, buffer, offset, length);
            }
    
        }
    }
}
