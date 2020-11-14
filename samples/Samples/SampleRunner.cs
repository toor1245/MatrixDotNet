using System;
using System.IO;
using System.Reflection;

namespace Samples
{
    public static class SampleRunner
    {
        private static string Folder => Path.GetRelativePath(@"MatrixDotNet/samples/Samples/", "logs");

        static SampleRunner()
        {
            var di = new DirectoryInfo(Folder);
            if (!di.Exists)
            {
                di.Create();
            }
        }
        
        public static void Run<T>()
        {
            Console.WriteLine(Folder);
            var t = typeof(T);
            var str = Path.Combine(Folder, t.Name);
            var info = new DirectoryInfo(str);
            
            if (!info.Exists)
            {
                info.Create();
            }
            
            var methods = t.GetMethods(BindingFlags.DeclaredOnly | 
                                       BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public );
            
            foreach (var mi in methods)
            {
                var call = (string) mi.Invoke(t, new object[] { });
                using var sw = new StreamWriter(Path.GetRelativePath(@".", Path.Combine(Folder, t.Name, mi.Name + ".md")),false);
                sw.WriteLine(call);
            }
            
            Parser parser = new Parser(@"D:\MatrixDotNet\samples\Samples\Samples\" + t.Name + ".cs");
            Console.WriteLine(parser.Source);
        }
    }
}