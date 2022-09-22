using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using MomoFrame.Windows.WindowView;
using MProtocol;
using StarForce;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouchSocket.Sockets;

public class ProcedureStart : ProcedureBase
{
    public static ProcedureStart Instance;

    public bool IsChangeProcedure = false;

    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);

        Instance = this;
        Debug.Log("ProcedureStart流程初始化成功");
    }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);

        Debug.Log("进入ProcedureStart流程");
    }

    protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        
        if (IsChangeProcedure)
        {
            ChangeState<ProcedureMain>(procedureOwner);
        }
        if (GameEntry.Procedure.CurrentProcedureTime == 0f)
        {
            return;
        }
        //Debug.Log($"当前流程:{GameEntry.Procedure.CurrentProcedure} 流程运行时间:{GameEntry.Procedure.CurrentProcedureTime}   帧运行时间:{elapseSeconds}m   运行时间:{realElapseSeconds}m");
        //NetClientSvc.Instance.SendMsg($"当前流程:{GameEntry.Procedure.CurrentProcedure} 流程运行时间:{GameEntry.Procedure.CurrentProcedureTime}   帧运行时间:{elapseSeconds}m   运行时间:{realElapseSeconds}m");

        //M_Message m_Message = new M_Message() { cmd = CMD.Test };
        //NetClientSvc.Instance.SendMsg(m_Message);
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);

        Debug.Log("离开ProcedureStart流程");
    }

    protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnDestroy(procedureOwner);

        Debug.Log("销毁ProcedureStart流程");
    }


    //private string ConfigPath = Application.StartupPath + Constants.ConfigPath;
    //private string SystemConfigPath = Application.StartupPath + Constants.SystemConfigPath;

    /// <summary>
    /// 初始化配置文件
    /// </summary>
    //public void InitConfig()
    //{
    //    if (!Directory.Exists(ConfigPath))
    //    {
    //        Directory.CreateDirectory(ConfigPath);
    //        Debug.LogError($"Config文件夹已丢失,已重新创建 Config文件夹路径:{ConfigPath}");
    //    }

    //    if (!Directory.Exists(SystemConfigPath))
    //    {
    //        Directory.CreateDirectory(SystemConfigPath);
    //        Debug.LogError($"SystemConfig文件夹已丢失,已重新创建 SystemConfig文件夹路径:{SystemConfigPath}");
    //    }
    //}


    //private string AgiletConfigPath = Application.StartupPath + Constants.AgiletConfigPath;

    ///// <summary>
    ///// 初始化Agilet配置文件
    ///// </summary>
    //private void InitAgiletConfig()
    //{
    //    IniFile iniFile = new IniFile(AgiletConfigPath);

    //    if (!iniFile.ExistIniFile())
    //    {
    //        //创建该文件
    //        using (FileStream myFs = new FileStream(AgiletConfigPath, FileMode.Create)) { }
    //        iniFile.WriteString("AgiletConfig", "COM", "COM1");
    //        iniFile.WriteInt("AgiletConfig", "Baud_rate", 9600);
    //        iniFile.WriteInt("AgiletConfig", "Data_bit", 8);
    //        iniFile.WriteInt("AgiletConfig", "Stop_bit", 1);
    //        iniFile.WriteString("AgiletConfig", "Check_bit", "None");
    //        iniFile.WriteInt("AgiletConfig", "overtime", 1000);
    //        iniFile.WriteInt("AgiletConfig", "Communication_delay", 100);
    //        Debug.LogError($"AgiletConfig.ini文件已丢失,已重新创建 AgiletConfig.ini文件路径:{AgiletConfigPath}");
    //    }
    //    else
    //    {
    //        Debug.Log("AgiletConfig.ini文件存在");
    //    }
    //}

}


