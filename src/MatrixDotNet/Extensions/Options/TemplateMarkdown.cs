using System;
using System.Diagnostics;
using System.Text;
using MatrixDotNet.Extensions.Statistics;

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
            
            int a = $"{matrix.Min():G3}".Length;
            int b = $"{matrix.Max():G3}".Length;
            var width = (a > b ? a : b)+2;

            for (int i = 0; i < matrix.Columns; i++)
            {
                builder.AppendFormat($"| {{0, {width}:G3}}", i);
            }

            builder.Append("|\n|");

            for (int i = 0; i < matrix.Columns; i++)
            {
                builder.Append("-|");
            }

            builder.AppendLine();
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                builder.Append("|");
                for (int j = 0; j < matrix.Columns; j++)
                {
                    builder.AppendFormat($"{{0, {width}:G3}}  |", matrix[i, j]);
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