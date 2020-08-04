using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a = new int[3, 4]
            {
                { 10, -7, 0, 5 },
                { -3, 6,  2, 6 },
                { 5, -1,  5, 9 },
            };

            Matrix<int> matrix = a;
            matrix.AddRow(new int[] {1, 2, 3,4,5},2).Pretty();
        }
    }
}