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
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    builder.Append("".PadLeft(2) +  matrix[i, j] + "".PadRight(2) +  "|");
                }

                builder.AppendLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            return builder.ToString();
        }
    }
}