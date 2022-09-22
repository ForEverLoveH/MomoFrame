using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;

using static System.IO.Path;
using static System.StringComparison;
using static System.Reflection.Assembly;

namespace MomoFrame
{
    public class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            AssemblyResolver.Hook("./Assets");

            SplashScreen splashScreen = new SplashScreen("resources/images/logo.png");
            splashScreen.Show(true);

            App app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }

    public static class SubfolderAssemblyResolver
    {
        public static void Hook(string subfolderKey)
        {
            if (string.IsNullOrWhiteSpace(subfolderKey)) { return; }
            if (!string.IsNullOrWhiteSpace(subfolderPath)) { return; }

            subfolderPath = Combine(
                GetDirectoryName(GetCallingAssembly().Location),
                subfolderKey
            );
            basePath = GetDirectoryName(subfolderPath);

            AppDomain.CurrentDomain.AssemblyResolve += resolver;
        }

        private static bool EndsWithAny(this string s, StringComparison comparisonType, params string[] testStrings) => testStrings.Any(x => s.EndsWith(x, comparisonType));

        private static readonly string[] exclusions = new[] { ".xmlserializers", ".resources" };
        private static readonly string[] patterns = new[] { "*.dll", "*.exe" };

        private static string basePath;
        private static string subfolderPath;

        private static Assembly resolver(object sender, ResolveEventArgs e)
        {
            var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == e.Name);
            if (loadedAssembly is { }) { return loadedAssembly; }

            var n = new AssemblyName(e.Name);
            if (n.Name.EndsWithAny(OrdinalIgnoreCase, exclusions)) { return null; }

            // 首先在basePath中搜索 因为它可能是更好的依赖关系
            var assemblyPath =
                resolveFromFolder(basePath!) ??
                resolveFromFolder(subfolderPath!) ??
                null;

            if (assemblyPath is null) { return null; }

            return LoadFrom(assemblyPath);

            string resolveFromFolder(string folder) =>
                patterns
                    .SelectMany(pattern => Directory.EnumerateFiles(folder, pattern))
                    .FirstOrDefault(filePath =>
                    {
                        try
                        {
                            return n.Name.Equals(AssemblyName.GetAssemblyName(filePath).Name, OrdinalIgnoreCase);
                        }
                        catch
                        {
                            return false;
                        }
                    });
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

                var n = new AssemblyName(args.Name);

                if (n.Name.EndsWith(".xmlserializers", StringComparison.OrdinalIgnoreCase))
                    return null;

                if (n.Name.EndsWith(".resources", StringComparison.OrdinalIgnoreCase))
                    return null;

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
