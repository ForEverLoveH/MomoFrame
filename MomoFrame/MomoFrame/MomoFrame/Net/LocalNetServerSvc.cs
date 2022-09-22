//功能：网络服务


using PEProtocol;
using System;
using System.Collections.Generic;

public class LocalNetServerSvc
{
    public static LocalNetServerSvc Instance;

    private LocalNetClientSvc localNetClientSvc;

    public void Awake()
    {
        Instance = this;
    }   

    private Queue<GameMsg> msgQueSetver = null;

    public void Init()
    {
        msgQueSetver = new Queue<GameMsg>();


        localNetClientSvc = LocalNetClientSvc.Instance;
    }

    public void AddMsgQue(GameMsg msg)
    {
        lock (msgQueSetver)
        {
            msgQueSetver.Enqueue(msg);
        }
    }

    public void Update()
    {
        while (msgQueSetver.Count > 0)
        {
            lock (msgQueSetver)
            {
                GameMsg msg = msgQueSetver.Dequeue();
                HandleMsg(msg);
            }
        }
    }

    private void HandleMsg(GameMsg msg)
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
    public void SendMsg(GameMsg msg)
    {
        localNetClientSvc.OnReciveMsg(msg);
    }

    /// <summary>
    /// 接受消息
    /// </summary>
    /// <param name="msg"></param>
    public void OnReciveMsg(GameMsg msg)
    {
        Debug.Log($"LocalServerMsg CMD:{msg.cmd.ToString()}");
        Instance.AddMsgQue(msg);
    }
}
