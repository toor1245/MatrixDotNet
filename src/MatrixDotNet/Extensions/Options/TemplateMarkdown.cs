using System;
using System.Diagnostics;
using System.Text;

namespace MatrixDotNet.Extensions.Options
{
    public sealed class TemplateMarkdown : Template
    {
        protected override string Text => @$"```ini 
{Assembly.FullName}";
        
        protected internal override string GetPath()
        {
            return base.GetPath() + FormatStorage.Markdown;
        }
        
        protected override string FullPath => System.IO.Path.Combine(RootPath, Title) + FormatStorage.Markdown;

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
                    int length = $"{matrix[i, j]:f2}".Length;
                    string format = $"{matrix[i, j]:f2}";
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

        public override void Open()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Process.Start(GetPath());
            Console.ResetColor();
        }
    }
}