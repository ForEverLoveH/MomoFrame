
namespace UnityGameFramework.Runtime
{
    /// <summary>
    /// 游戏框架组件抽象类。
    /// </summary>
    public abstract class GameFrameworkComponent
    {
        BaseComponent baseComponent;
        EventComponent eventComponent;
        FsmComponent fsmComponent;
        NetworkComponent networkComponent;
        ObjectPoolComponent objectPoolComponent;
        ProcedureComponent procedureComponent;


        public virtual void InitAwake()
        {
            baseComponent = new BaseComponent();
            baseComponent.Awake();

            eventComponent = new EventComponent();
            eventComponent.Awake();

            fsmComponent = new FsmComponent();
            fsmComponent.Awake();

            networkComponent = new NetworkComponent();
            networkComponent.Awake();

            objectPoolComponent = new ObjectPoolComponent();
            objectPoolComponent.Awake();

            procedureComponent = new ProcedureComponent();
            procedureComponent.Awake();



        }

        public virtual void InitStart()
        {
            baseComponent.Start();
            networkComponent.Start();
            procedureComponent.Start();
        }

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        public virtual void Awake()
        {
            GameEntry.RegisterComponent(this);
        }
    }
}
