using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a = new int[3, 3]
            {
                { 10, -7, 0 },
                { -3, 6,  2 },
                { 5, -1,  5 }
            };
            
            Matrix<int> matrix = a;
            matrix.BubbleSort();
            //Console.WriteLine(matrix);
            matrix.Pretty();
        }
    }
}