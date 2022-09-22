using GameFramework.Fsm;
using GameFramework.Procedure;
using MomoFrame.Windows.WindowView;
using StarForce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProcedureMain : ProcedureBase
{

    public static ProcedureMain Instance;

    protected override void OnInit(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnInit(procedureOwner);

        Instance = this;

        Debug.Log("ProcedureMain流程初始化成功");
    }

    protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnEnter(procedureOwner);

        Debug.Log("进入ProcedureMain流程");
    }

    protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        if (GameEntry.Procedure.CurrentProcedureTime == 0f)
        {
            return;
        }

        //Debug.Log($"当前流程:{GameEntry.Procedure.CurrentProcedure} 流程运行时间:{GameEntry.Procedure.CurrentProcedureTime}   帧运行时间:{elapseSeconds}m   运行时间:{realElapseSeconds}m");
    }

    protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
    {
        base.OnLeave(procedureOwner, isShutdown);
        Debug.Log("离开ProcedureMain流程");
    }

    protected override void OnDestroy(IFsm<IProcedureManager> procedureOwner)
    {
        base.OnDestroy(procedureOwner);
        Debug.Log("销毁ProcedureMain流程");
    }
}
