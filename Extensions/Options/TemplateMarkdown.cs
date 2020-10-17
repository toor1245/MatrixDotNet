using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MatrixDotNet.Extensions.Options
{
    public sealed class TemplateMarkdown : Template
    {
        protected override string Text => @$"```ini 
{Assembly.FullName}";

        internal override string Path
        {
            get
            {
#if OS_WINDOWS
                return @$"{Folder}\{Title}.md";
#elif OS_LINUX
                
                return @$"{Folder}/{Title}.md";
#endif
            }
        }

        internal override string FullPath
        {
            get
            {
#if OS_WINDOWS
                return @$"{RootPath}\{Title}.md";
#elif OS_LINUX
                
                return @$"{RootPath}/{Title}.md";
#endif
            }
        }

        public bool HasSize { get; }

        public TemplateMarkdown(string title,bool hasSize = false) : base(title)
        {
            HasSize = hasSize;
        }
        
        public override string Save<T>(Matrix<T> matrix)
        {
            StringBuilder builder = new StringBuilder();
            Rows = matrix.Rows;
            Columns = matrix.Columns;
            builder.AppendLine(Text);
            
            if (HasSize)
            {
                builder.AppendLine($"Number of rows: {Rows};");
                builder.AppendLine($"Number of columns: {Columns};");
            }

            builder.AppendLine("```");
            builder.AppendLine();
            
            var output = InitColumnSize(matrix);
            int sum = 0;
            int[] slash = new int[matrix.Columns];
            for (int i = 0; i < matrix.Columns; i++)
            {
                sum += output[i] + 3;
                string format = $"| {i} " + "".PadRight(output[i]);
                slash[i] = format.Length;
                builder.Append(format);
            }

            builder.Append("|");
            builder.AppendLine();
            builder.Append("\n|");
            int value = slash[0];

            for (int i = 1, j = 0; i < matrix.Columns + sum;i++)
            {  
                if (i == value)
                {
                    builder.Append("|");
                    j++;
                    value += slash[j];
                }
                else
                {
                    builder.Append("-");
                }
            }

            builder.Append("|");
            builder.AppendLine();
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                builder.Append("|");
                for (int j = 0; j < matrix.Columns; j++)
                {
                    var n = output[j];
                    int length = string.Format("{0:f2}",matrix[i, j]).Length;
                    string format = string.Format("{0:f2}",matrix[i, j]);
                    if (length >= n)
                    {
                        builder.Append(format + "".PadRight(length - n + 3) + "|");
                    }
                    else
                    {
                        builder.Append(format + "".PadRight(n - length + 3) + "|");
                    }
                }

                builder.AppendLine();
            }

            return builder.ToString();
        }

        public override Task Open()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
#if OS_WINDOWS
            Process.Start("explorer.exe",Path);
#elif OS_LINUX
            Process.Start("cat", Path);
#endif
            Console.ResetColor();
            return Task.CompletedTask;
        }
    }
}