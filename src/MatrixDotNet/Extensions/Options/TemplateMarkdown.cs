using MatrixDotNet.Extensions.Statistics;
using System.Text;

namespace MatrixDotNet.Extensions.Options
{
    public sealed class TemplateMarkdown : Template
    {
        public bool HasSize { get; }

        public TemplateMarkdown(string title, bool hasSize = false) : base(title)
        {
            HasSize = hasSize;
        }

        public override string FileExtension => ".md";

        public override string CreateText<T>(Matrix<T> matrix)
        {
            StringBuilder builder = new StringBuilder();
            Rows = matrix.Rows;
            Columns = matrix.Columns;
            builder.AppendLine(@$"```ini 
{Assembly.FullName}");

            if (HasSize)
            {
                builder.AppendLine($"Number of rows: {Rows};");
                builder.AppendLine($"Number of columns: {Columns};");
            }

            builder.AppendLine("```");
            builder.AppendLine();

            int a = $"{matrix.Min():G3}".Length;
            int b = $"{matrix.Max():G3}".Length;
            var width = (a > b ? a : b) + 2;

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

        internal static int[] InitColumnSize<T>(Matrix<T> matrix) where T : unmanaged
        {
            var arr = matrix.MaxColumns();
            var arr2 = matrix.MinColumns();
            int[] output = new int[arr.Length];

            for (int i = 0; i < output.Length; i++)
            {
                var x = $"{arr[i]:f2}".Length;
                var y = $"{arr2[i]:f2}".Length;

                if (x > y)
                {
                    output[i] = x;
                }
                else
                {
                    output[i] = y;
                }
            }

            return output;
        }
    }
}