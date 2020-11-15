using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Decompositions;

namespace Samples.Samples.MatrixSamples
{
    public class LUPSample : SampleTest
    {
        public static void Run()
        {
            // initialize matrix with random values.
            Matrix<double> matrix = BuildMatrix.RandomDouble(3, 3, -10, 10);

            // display matrix.
            matrix.Pretty();

            // LU decomposition.
            matrix.GetLowerUpperPermutation(out var lower, out var upper, out var perm);

            // Gets permutation matrix and C = L + U - E.
            matrix.GetLowerUpperPermutation(out var matrixC, out var matrixP);

            // display lower-triangular matrix.
            Console.WriteLine("lower-triangular matrix");
            lower.Pretty();

            // display upper-triangular matrix.
            Console.WriteLine("upper-triangular matrix");
            upper.Pretty();

            // display permutation matrix.
            Console.WriteLine("permutation matrix");
            perm.Pretty();

            // display matrix C
            Console.WriteLine("matrix C = L + U - E");
            matrixC.Pretty();
        }
    }
}