using System;
using System.IO;
using System.Text;

namespace Samples
{
    public static class Parser
    {
        private static string _source;

        public static void Parse(string src, string path, Type type)
        {
            _source = File.ReadAllText(src);
            string[] stringSeparators = { "\r\n" };

            _source = _source.Replace("string", "void");

            var builder = new StringBuilder();
            var lines = _source.Split(stringSeparators, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]) ||
                    lines[i].Contains("builder") || lines[i].Contains("Console") || lines[i].Contains("Pretty"))
                {
                    continue;
                }

                if (lines[i].Contains("[Output"))
                {
                    continue;
                }

                if (lines[i].Contains(type.Name))
                {
                    lines[i] = lines[i].Replace($"    public class {type.Name} : SampleTest",
                        $"    public class {type.Name + "Docs"}");
                }

                if (lines[i].Contains("namespace"))
                {
                    int indexOf = lines[i].IndexOf("Samples", StringComparison.Ordinal);

                    lines[i] = lines[i]
                        .Remove(indexOf, lines[i].Length - indexOf)
                        .Insert(indexOf, "Samples.logs." + type.Name);

                    builder.AppendLine();
                }

                if (lines[i].Contains("//"))
                {
                    builder.AppendLine();
                }

                builder.AppendLine(lines[i]);
            }

            using (var sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.WriteLine(builder.ToString());
            }

            Console.WriteLine(builder.ToString());
        }
    }
}