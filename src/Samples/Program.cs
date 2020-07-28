using System;
using System.Threading.Channels;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[128,128];
            Random random = new Random();
            Random random2 = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(1, 10);
                }
            }

            int[,] matrix3 = new int[128,128];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix3[i, j] = random2.Next(1, 10);
                }
            }
            

            Matrix<int> matrix2 = new Matrix<int>(matrix3);
            Matrix<int> matrix4 = new Matrix<int>(matrix);
            //Console.WriteLine(matrix2 * matrix4);
            //matrix2.SplitMatrix(out var a11,out var a12,out var a21,out var a22);
            Console.WriteLine(MatrixExtension.MultiplyStrassen(matrix2,matrix4).Pretty());
        }
    }
}