using System;
using System.IO;
using System.Reflection;

namespace Samples
{
    public static class SampleRunner
    {
        private static string LogsFolder => Path.GetRelativePath(@"MatrixDotNet/samples/Samples/", "logs");

        private static string SourcesFolder => Directory.GetParent(Environment.CurrentDirectory)?.Parent?.Parent?.FullName;

        private static string SamplesFolder => Path.Combine(SourcesFolder, "Samples/");

        static SampleRunner()
        {
            var di = new DirectoryInfo(LogsFolder);
            if (!di.Exists)
            {
                di.Create();
            }
        }

        public static void Run(Type t)
        {
            var str = Path.Combine(LogsFolder, t.Name);
            var info = new DirectoryInfo(str);

            if (!info.Exists)
            {
                info.Create();
            }

            var methods = t.GetMethods(BindingFlags.DeclaredOnly |
                                       BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);

            foreach (var mi in methods)
            {
                var call = (string)mi.Invoke(t, new object[] { });
                using var sw =
                    new StreamWriter(Path.GetRelativePath(@".", Path.Combine(LogsFolder, t.Name, mi.Name + ".txt")), false);
                sw.WriteLine(call);
            }

            var src = Directory.GetFiles(SamplesFolder, $"{t.Name}.cs", SearchOption.AllDirectories)[0];
            var path = Path.Combine(str, t.Name + "Docs.cs");
            Parser.Parse(src, path, t);
        }
    }
}