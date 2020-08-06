using System;
using System.Text;
using MatrixDotNet.Extensions.BitMatrix;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        public static void Pretty<T>(this Matrix<T> matrix) where T : unmanaged
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            SetColorMessage(matrix);
            Console.ResetColor();
        }

        private static void SetColorMessage<T>(Matrix<T> matrix) where  T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            T[] arr = matrix.MaxColumns();
            int[] output = new int[arr.Length];
            
            for (int i = 0; i < arr.Length; i++)
            {
                output[i] = string.Format($"{arr[i].ToString():2f}").Length;
            }
            SetColorMessageSmallSize(matrix, output);

        }

        
        private static void SetColorMessageSmallSize<T>(Matrix<T> matrix,int[] output) where T : unmanaged 
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine();
            
            builder.AppendLine($"Number of rows: {matrix.Rows}");
            builder.AppendLine($"Number of columns: {matrix.Columns}\n");
            
            for (int i = 0; i < matrix.Rows; i++)
            {

                for (int j = 0; j < matrix.Columns; j++)
                {
                    var n = output[j];
                    int length = $"{matrix[i, j].ToString():f2}".Length;
                    string format = $"{matrix[i, j].ToString():f2}";
                    builder.Append(" ".PadLeft(2) + format + "".PadRight(length - n + 1) + "  |");
                    
                }

                builder.AppendLine();
            }

            Console.WriteLine(builder.ToString());
        }
        
        internal static string OutputPretty<T>(Matrix<T> matrix) where  T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();

            StringBuilder builder = new StringBuilder();
            builder.AppendLine();

            int n = 9;
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    int length = $"{matrix[i, j]:f2}".Length;
                    string format = $"{matrix[i, j]:f2}";
                    
                    if (length > n)
                    {
                        builder.Append(" ".PadLeft(2) + format + "".PadRight(length - n) +   "|");    
                    }
                    else
                    {
                        builder.Append(" ".PadLeft(2) + format + "".PadRight(n + (n - length)) +   "|");
                    }
                }

                builder.AppendLine();
            }
            
            return builder.ToString();
        }
    }
}