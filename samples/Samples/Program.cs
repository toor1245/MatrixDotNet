using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            long[,] a = new long[4,4];
            a[0, 0] = 100000;
            Random random = new Random();
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    a[i, j] = random.Next(1, 1034534544);
                }
            }
            



            Matrix<long> matrix = a;
            matrix.Pretty();
        }
    }
}