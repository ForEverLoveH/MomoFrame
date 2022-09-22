using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFramework.Fsm;
using UnityGameFramework.Runtime;

namespace MomoFrame.Scripts.Runtime.Fsm
{
    public class FsmLoginTemp
    {
        public IFsm<FsmLoginTemp> m_Fsm = null;

        public FsmLoginTemp()
        {
            FsmComponent fsmComponent = UnityGameFramework.Runtime.GameEntry.GetComponent<FsmComponent>();

            m_Fsm = fsmComponent.CreateFsm("LoginFsm", this, new IdleState(), new MoveState());

            m_Fsm.Start<IdleState>();
        }

    }

    public class IdleState : FsmState<FsmLoginTemp>
    {
        protected override void OnInit(IFsm<FsmLoginTemp> fsm)
        {
            base.OnInit(fsm);
        }
    }

    public class MoveState : FsmState<FsmLoginTemp>
    {
    }
}
