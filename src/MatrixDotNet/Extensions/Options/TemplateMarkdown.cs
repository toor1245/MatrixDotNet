using System.Text;
using MatrixDotNet.Extensions.Statistics;

namespace MatrixDotNet.Extensions.Options
{
    public sealed class TemplateMarkdown : Template
    {
        public TemplateMarkdown(string title, bool hasSize = false) : base(title)
        {
            HasSize = hasSize;
        }

        public bool HasSize { get; }

        public override string FileExtension => ".md";

        public override string CreateText<T>(Matrix<T> matrix)
        {
            var builder = new StringBuilder();
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

            var a = $"{matrix.Min():G3}".Length;
            var b = $"{matrix.Max():G3}".Length;
            var width = (a > b ? a : b) + 2;

            for (var i = 0; i < matrix.Columns; i++) builder.AppendFormat($"| {{0, {width}:G3}}", i);

            builder.Append("|\n|");

            for (var i = 0; i < matrix.Columns; i++) builder.Append("-|");

            builder.AppendLine();

            for (var i = 0; i < matrix.Rows; i++)
            {
                builder.Append("|");
                for (var j = 0; j < matrix.Columns; j++) builder.AppendFormat($"{{0, {width}:G3}}  |", matrix[i, j]);

                builder.AppendLine();
            }

            return builder.ToString();
        }

        internal static int[] InitColumnSize<T>(Matrix<T> matrix) where T : unmanaged
        {
            var arr = matrix.MaxColumns();
            var arr2 = matrix.MinColumns();
            var output = new int[arr.Length];

            for (var i = 0; i < output.Length; i++)
            {
                var x = $"{arr[i]:f2}".Length;
                var y = $"{arr2[i]:f2}".Length;

                if (x > y)
                    output[i] = x;
                else
                    output[i] = y;
            }

            return output;
        }
    }
}