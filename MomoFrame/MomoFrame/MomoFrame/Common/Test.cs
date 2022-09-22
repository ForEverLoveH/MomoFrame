using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ahpily;
using Ahpily.Timer;
using GameFramework.Event;
using Microsoft.CSharp;
using PEProtocol;
using StarForce;

public static class Test
{
    public static void test01()
    {
        

        SingleExecute.Instance.Execute(() => {

            for (int i = 0; i < 2; i++)
            {
                Debug.LogWarn("单线程池任务触发");
            }
         });
        //Task.Run(() =>
        //{
        //    while (true)
        //    {
        //        Thread.Sleep(500);
        //        Debug.Log(GameEntry.Procedure.CurrentProcedureTime);

        //    }

        //});

        

        //String Path = Application.StartupPath + @"\Cofing.ini";
        //IniFile iniFile = new IniFile(Path);
        //if (!iniFile.ExistIniFile())
        //{
        //    //创建该文件
        //    using (FileStream myFs = new FileStream(Path, FileMode.Create)) { }
        //    iniFile.WriteString("账号缓存数据", "User", "账号");
        //    iniFile.IniWriteValue("账号缓存数据", "N", "昵称");
        //    iniFile.WriteInt("账号缓存数据", "Psw", 123456);
        //    Debug.LogError($"AgiletConfig.ini文件已丢失,已重新创建 AgiletConfig.ini文件路径:{Path}");
        //}
        //Debug.Log(iniFile.ReadString("账号缓存数据", "User", "Null"));
        //Debug.Log(iniFile.ReadInt("账号缓存数据", "Psw", -1));
        //Debug.Log(iniFile.IniReadValue("账号缓存数据", "N"));


        DateTime dateTime = new DateTime(2022, 4, 7, 17, 25, 0);
        TimerManager.Instance.AddTimerEvent(dateTime, () => { Debug.Log("已到设定的时间,任务触发"); });


        TimerManager.Instance.AddTimerEvent(3 * 1000, () =>
        {
            ProcedureStart.Instance.IsChangeProcedure = true;
            Debug.Log("切换流程......................................");
        });
        TimerManager.Instance.AddTimerEvent(4 * 1000, () => { Debug.LogWarn("已过4秒,任务触发"); });
        TimerManager.Instance.AddTimerEvent(5 * 1000, () => { Debug.LogError("已过5秒,任务触发"); });

        TimerManager.Instance.AddTimerEvent(6 * 1000, () => { GameEntry.Base.SetFPS(UnityGameFramework.Runtime.BaseComponent.FPS.FPS_120); ; });

        TimerManager.Instance.AddTimerEvent(7 * 1000, () => { GameMsg msg = new GameMsg() { cmd = CMD.None }; LocalNetClientSvc.Instance.SendMsg(msg); });
        TimerManager.Instance.AddTimerEvent(8 * 1000, () => { GameMsg msg = new GameMsg() { cmd = CMD.None }; LocalNetServerSvc.Instance.SendMsg(msg); });
        //TimerManager.Instance.AddTimerEvent(10 * 1000, () =>
        //{
        //    UnityGameFramework.Runtime.GameEntry.Shutdown(UnityGameFramework.Runtime.ShutdownType.Quit);
        //});
    }


    /// <summary>
    /// 执行动态代码
    /// </summary>
    /// <param name="Code"></param>
    /// <param name="Namespace_Class"></param>
    /// <param name="Method_Name"></param>
    public static void Dynamic_code(string Code, string Namespace_Class, string Method_Name)
    {
        CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();

        CompilerParameters objCompilerParameters = new CompilerParameters();

        objCompilerParameters.ReferencedAssemblies.Add("System.dll");
        objCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");

        objCompilerParameters.GenerateExecutable = false;
        objCompilerParameters.GenerateInMemory = true;

        CompilerResults cresult = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, Code);

        if (cresult.Errors.HasErrors)
        {
            foreach (CompilerError err in cresult.Errors)
            {
                Debug.LogError(err.ErrorText);
            }
        }
        else
        {
            // 通过反射，执行代码
            Assembly objAssembly = cresult.CompiledAssembly;
            object obj = objAssembly.CreateInstance(Namespace_Class);
            MethodInfo objMI = obj.GetType().GetMethod(Method_Name);
            objMI.Invoke(obj, new object[] { });
        }
    }

}
