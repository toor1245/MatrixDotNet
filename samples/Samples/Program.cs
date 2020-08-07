using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.Decomposition;
using MatrixDotNet.Extensions.Determinant;
using MatrixDotNet.Extensions.MathExpression;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] a = new Double[4,4]
            {
                {2,5,7,8},
                {6,3,4,9},
                {5,-2,-3,10},
                {8,1,-4,5}
            };
            
            Matrix<double> matrix = a;
            Console.WriteLine(matrix.GetDeterminant());
            Console.WriteLine(matrix.GetLowerUpperDeterminant());
            Console.WriteLine(matrix.GetShurDeterminant());
        }

    }
}