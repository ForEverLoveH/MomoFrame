using MomoFrameServer.Net;
using MomoFrameServer.Scripts;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

public partial class Program
{
    /// <summary>
    /// 应用程序的主入口点。
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        AssemblyResolver.Hook("./Assets");

        FrameworkRoot.Init_Start();

        while (Console.ReadLine() != "exited") { }
        if (LogSys.Instance != null)
        {
            NetServerSvc.Instance?.socket?.Dispose();
            Debug.LogWarn("The program has exited");
        }
    }

    public static class AssemblyResolver
    {
        internal static void Hook(params string[] folders)
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
                if (loadedAssembly != null)
                    return loadedAssembly;

                AssemblyName n = new AssemblyName(args.Name);

                if (n.Name == null)
                {
                    return null;
                }

                if (n.Name.EndsWith(".xmlserializers", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                if (n.Name.EndsWith(".resources", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                string assy = null;

                foreach (var dir in folders)
                {
                    assy = new[] { "*.dll", "*.exe" }.SelectMany(g => Directory.EnumerateFiles(dir, g)).FirstOrDefault(f =>
                    {
                        try { return n.Name.Equals(AssemblyName.GetAssemblyName(f).Name, StringComparison.OrdinalIgnoreCase); }
                        catch (BadImageFormatException) { return false; /* Bypass assembly is not a .net exe */ }
                        catch (Exception ex) { throw new ApplicationException("Error loading assembly " + f, ex); }
                    });

                    if (assy != null)
                        return Assembly.LoadFrom(assy);
                }

                throw new ApplicationException("Assembly " + args.Name + " not found");
            };
        }
    }
}