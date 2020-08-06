using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Decomposition;
using MatrixDotNet.Extensions.Determinant;
using MatrixDotNet.Extensions.MathExpression;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] a = new Double[3,3]
            {
                {1,2,3},
                {4,5,6},
                {7,8,12}
            };
            
            Matrix<double> matrix = a;
            Console.WriteLine(matrix.GetLowerUpperDeterminant());
        }
    }
}