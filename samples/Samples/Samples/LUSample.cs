using System;
using System.Text;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Decompositions;

namespace Samples.Samples
{
    [Output]
    public class LUSample
    {
        public static string Run()
        {
            StringBuilder builder = new StringBuilder();
            
            // initialize matrix with random values.
            Matrix<double> matrix = BuildMatrix.RandomDouble(5, 5, -10, 10);
            
            // display matrix.
            builder.AppendLine(matrix.ToString());
            
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

            return builder.ToString();
        }
    }
}