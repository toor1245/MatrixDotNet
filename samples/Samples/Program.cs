using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            // init matrix

            Matrix<int> matrixA = new int[,]
            {
                { 11, -2, 1, 6 },
                { -8, 4,  2, 3 },
                { 4, -4,  5, 8 },
            };

            Console.WriteLine(matrixA.GetDeterminate());
            
        }

        public static void Test(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    matrix[i, j] = 1;
                }
            }
        }

        public static void Test2(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    matrix[j, i] = 1;
                }
            }
        }
        
    }
}