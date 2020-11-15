using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Samples
{
    public static class SampleRunner
    {
        private static string Folder => Path.GetRelativePath(@"MatrixDotNet/samples/Samples/", "logs");

        private static readonly DefineProject[] Projects;

        static SampleRunner()
        {
            var di = new DirectoryInfo(Folder);
            if (!di.Exists)
            {
                di.Create();
            }

            Projects = new[]
            {
                DefineProject.MatrixSamples,
                DefineProject.VectorSamples,
                DefineProject.MatrixComplexSamples,
                DefineProject.MatrixAsFixedSamples
            };
        }

        public static void Run(Type t, DefineProject project)
        {
            var data = Projects
                .Where(i => project == i)
                .Aggregate(@"D:\MatrixDotNet\samples\Samples\Samples\",
                    (current, i) => Path.Combine(current, i.ToString()));


            var str = Path.Combine(Folder, t.Name);
            var info = new DirectoryInfo(str);

            if (!info.Exists)
            {
                info.Create();
            }

            var methods = t.GetMethods(BindingFlags.DeclaredOnly |
                                       BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);

            foreach (var mi in methods)
            {
                var call = (string) mi.Invoke(t, new object[] { });
                using var sw =
                    new StreamWriter(Path.GetRelativePath(@".", Path.Combine(Folder, t.Name, mi.Name + ".txt")), false);
                sw.WriteLine(call);
            }

            var src = Path.Combine(data, t.Name) + ".cs";
            var path = Path.GetRelativePath(@".", Path.Combine(str, t.Name + "Docs.cs"));
            Parser.Parse(src, path, t);
        }
    }
}