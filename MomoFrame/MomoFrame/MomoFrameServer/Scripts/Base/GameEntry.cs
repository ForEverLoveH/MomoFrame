namespace StarForce
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry
    {

        public static GameEntry Instance;
        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            InitBuiltinComponents();
            InitCustomComponents();
        }
    }
}
