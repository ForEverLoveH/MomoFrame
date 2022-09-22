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

public class ClientSocket
{
    TcpClient tcpClient = null;

    public void InitTcpSocket(IPHost iPHost)
    {
        Task.Run(() => {
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connected += tcpClientConnectedEventHandler;
                tcpClient.Disconnected += tcpClientDisconnectedEventHandler;
                tcpClient.Received += tcpClientReceivedEventHandler;

                //声明配置
                TouchSocketConfig config = new TouchSocketConfig();
                config.SetRemoteIPHost(iPHost)
                    .SetDataHandlingAdapter(() => { return new FixedHeaderPackageAdapter(); })
                    .SetBufferLength(1024 * 10);

                //载入配置
                tcpClient.Setup(config);
                tcpClient.Connect(100);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message, false);
            }
        });

    }


    /// <summary>
    /// 连接到服务器
    /// </summary>
    /// <param name="client"></param>
    /// <param name="e"></param>
    private void tcpClientConnectedEventHandler(ITcpClient client, MsgEventArgs e)
    {
        Debug.Log($"服务器连接成功:{client.GetIPPort()}",true,true,Debug.LogType.SuccessGlobal);
    }

    /// <summary>
    /// 从服务器断开连接，当连接不成功时不会触发
    /// </summary>
    /// <param name="client"></param>
    /// <param name="e"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void tcpClientDisconnectedEventHandler(ITcpClientBase client, ClientDisconnectedEventArgs e)
    {
        Debug.LogWarn($"已与服务器断开连接:{client.GetIPPort()}");
    }

    /// <summary>
    /// 从服务器收到信息
    /// </summary>
    /// <param name="client"></param>
    /// <param name="byteBlock"></param>
    /// <param name="requestInfo"></param>
    /// <exception cref="NotImplementedException"></exception>
    private void tcpClientReceivedEventHandler(TcpClient client, ByteBlock byteBlock, IRequestInfo requestInfo)
    {
        string mes = Encoding.GetEncoding("GB2312").GetString(byteBlock.Buffer, 0, byteBlock.Len);
        Debug.Log($"接收到信息：{mes}", true, false);
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="msg"></param>
    public bool Send(string msg)
    {
        return Send(Encoding.GetEncoding("GB2312").GetBytes(msg));
    }

    public bool Send(M_Message msg)
    {
        return Send(Serialize.ToBytes(msg));
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="buffer"></param>
    public bool Send(byte[] buffer)
    {
        if (tcpClient != null)
        {
            return tcpClient.TrySend(buffer);
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="buffer"></param>
    /// <param name="offset"></param>
    /// <param name="length"></param>
    public bool Send(byte[] buffer, int offset, int length)
    {
        if (tcpClient != null)
        {
            return tcpClient.TrySend(buffer, offset, length);
        }
        else
        {
            return false;
        }
    }
}
