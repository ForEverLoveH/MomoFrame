using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class LogSys
{
    public static LogSys Instance;

    public void Awake()
    {
        Instance = this;
        Init();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        Debug.InitDebug();
    }

}
