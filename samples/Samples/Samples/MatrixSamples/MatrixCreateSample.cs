using System;
using MatrixDotNet;

namespace Samples.Samples.MatrixSamples
{
    public class MatrixCreateSample
    {
        public static void Run()
        {
            int[,] a = new int[3, 3]
            {
                {10, -7, 0},
                {-3, 6, 2},
                {5, -1, 5}
            };

            // First way. 
            Matrix<int> matrixA = new Matrix<int>(a);

            // Second way: primitive way, assign by deep copy nor by reference!!!
            Matrix<int> matrixB = a;

            Matrix<int> matrixC = new int[10, 10];

            Matrix<int> matrixD = new[,]
            {
                {1, 2, 3},
                {2, 4, 6},
            };

            // Third way initialize all values 0 or constant value.
            Matrix<int> matrixE = new Matrix<int>(row: 5, col: 3);

            Matrix<int> matrixF = new Matrix<int>(row: 3, col: 5, value: 5);
        }
    }
}