using System;
using MatrixDotNet;
using MatrixDotNet.Extensions.Solver;

namespace Samples.Samples
{
    public class GaussSolveSample
    {
        public static void Run()
        {
            // initialize matrix.
            Matrix<double> matrix = new double[,]
            {
                {5, 56, 7},
                {3, 6, 3},
                {5, 9, 15}
            };

            double[] right = { 1, 23, 5 };

            double[] res = matrix.GaussSolve(right);
            for (var i = 0; i < res.Length; i++)
            {
                Console.Write($"x{i}: {res[i]}\n");
            }
        }
    }
}