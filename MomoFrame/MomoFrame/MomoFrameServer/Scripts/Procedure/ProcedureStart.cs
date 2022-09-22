using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using StarForce;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

}


