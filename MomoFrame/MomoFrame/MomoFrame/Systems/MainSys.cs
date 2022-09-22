using MomoFrame;
using MomoFrame.Windows.WindowView;
using StarForce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MainSys
{

    public static MainSys Instance;

    private MainWindow mainWindow;

    public void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        mainWindow = MainWindow.Instance;

        LocalNetClientSvc.Instance.Init();
        LocalNetServerSvc.Instance.Init();
        NetClientSvc.Instance.Start();
        SqliteDB.Instance.Init();

        GameEntry.Instance.Start();
        MonoBehaviour.Instance.Start();
    }
}
