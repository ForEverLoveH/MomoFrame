using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityGameFramework.Runtime;

public class MonoBehaviour : GameFrameworkComponent
{
    public static MonoBehaviour Instance;

    public new void Awake()
    {
        Instance = this;
        base.InitAwake();
    }

    public void Start()
    {
        base.InitStart();
    }

}
