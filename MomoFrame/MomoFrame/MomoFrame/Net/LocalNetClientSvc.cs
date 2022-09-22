//功能：网络服务


using PEProtocol;
using PEUtils;
using System;
using System.Collections.Generic;

public class LocalNetClientSvc
{
    public static LocalNetClientSvc Instance;

    private LocalNetServerSvc localNetServerSvc;

    public void Awake()
    {
        Instance = this;
    } 

    private Queue<GameMsg> msgQue = null;

    public void Init()
    {
        msgQue = new Queue<GameMsg>();

        localNetServerSvc = LocalNetServerSvc.Instance;

    }

    public void AddMsgQue(GameMsg msg)
    {
        lock (msgQue)
        {
            msgQue.Enqueue(msg);
        }
    }

    public void Update()
    {
        while (msgQue.Count > 0)
        {
            lock (msgQue)
            {
                GameMsg msg = msgQue.Dequeue();
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
        localNetServerSvc.OnReciveMsg(msg);
    }

    /// <summary>
    /// 接受消息
    /// </summary>
    /// <param name="msg"></param>
    public void OnReciveMsg(GameMsg msg)
    {
        Debug.Log($"LocalClientMsg CMD:{msg.cmd.ToString()}");
        Instance.AddMsgQue(msg);
    }
}
