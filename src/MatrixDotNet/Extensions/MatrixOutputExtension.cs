using System;
using System.Text;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        public static string Pretty<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (matrix == null)
            {
                throw new NullReferenceException();
            }
            
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();

            int n = 7;
            builder.AppendLine($"Number of rows: {matrix.Rows}");
            builder.AppendLine($"Number of columns: {matrix.Columns}\n");
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    int length = matrix[i, j].ToString().Length;
                    if (length > n)
                    {
                        builder.Append(" ".PadLeft(2) + matrix[i, j] + "".PadRight(length - n) +   "|");    
                    }
                    else
                    {
                        builder.Append(" ".PadLeft(2) + matrix[i, j] + "".PadRight(n - length) +   "|");
                    }
                }

                builder.AppendLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            return builder.ToString();
        }
    }
}