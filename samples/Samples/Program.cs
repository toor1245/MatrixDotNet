using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a = new int[6, 6]
            {
                { -5,-1,-5,-7,-8,4},
                { -1,-7,-4,-6,-2,3},
                { -3,-2,1,-2,-1,1},
                { -4,-1,3,-3,-5,4},
                { -7,-8,4,-5,-7,6},
                { -8,-1,3,-6,-9,7},
            };

            Matrix<int> matrix = a;
            matrix.GetLUP(out var matrixC,out var matrixP);
            Console.WriteLine(matrixC);
            Console.WriteLine();
            Console.WriteLine(matrixP);
        }
    }
}