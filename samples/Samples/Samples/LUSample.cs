using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Decomposition;

namespace Samples.Samples
{
    public class LUSample
    {
        public static void Run()
        {
            // initialize matrix with random values.
            Matrix<double> matrix = BuildMatrix.Random(5, 5, -10, 10);
            
            // display matrix.
            matrix.Pretty();
            
            // LU decomposition.
            matrix.GetLowerUpper(out var lower,out var upper);
            
            // display lower-triangular matrix.
            Console.WriteLine("lower-triangular matrix");
            lower.Pretty();
            
            // display upper-triangular matrix.
            Console.WriteLine("upper-triangular matrix");
            upper.Pretty();
            
            // A = LU
            Console.WriteLine("A = LU");
            Console.WriteLine(lower * upper);
        }
    }
}