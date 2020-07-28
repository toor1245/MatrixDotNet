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

            int n = 8;
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    int length = matrix[i, j].ToString().Length;
                    if (length > n)
                    {
                        builder.Append(" " + matrix[i, j] + "".PadRight(length - n) +   "|");    
                    }
                    else
                    {
                        builder.Append(" " + matrix[i, j] + "".PadRight(n - length) +   "|");
                    }
                    
                    
                    /*switch (matrix[i,j].ToString().Length)
                    {
                        case 1:
                            builder.Append(" " + matrix[i, j] + "".PadRight(4) +   "|");
                            break;
                        case 2:
                            builder.Append(" " + matrix[i, j] + "".PadRight(3) +   "|");
                            break;
                        case 3:
                            builder.Append(" " + matrix[i, j] + "".PadRight(2) +   "|");
                            break;
                        case 4:
                            builder.Append(" " + matrix[i, j] + "".PadRight(1) +   "|");
                            break;
                    }
                    */
                }

                builder.AppendLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            return builder.ToString();
        }
    }
}