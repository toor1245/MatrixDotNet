using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.Extensions.Determinant
{
    public static partial class Determinant
    {
        public static double GetShurDeterminant(this Matrix<double> matrix)
        {
            if(!matrix.IsPrime)
                throw new MatrixDotNetException("matrix is not prime");
            var res = matrix.SchurComplement(out var a11);

            return a11.GetLowerUpperDeterminant() * res.GetLowerUpperDeterminant();
        }
        
        
        public static float GetShurDeterminant(this Matrix<float> matrix)
        {
            var res = matrix.SchurComplement(out var a11);
            if (Math.Abs(a11.GetDeterminant()) < 0.00001)
                throw new MatrixDotNetException("matrix determinant = 0");

            return a11.GetDeterminant() * res.GetDeterminant();
        }
        
        public static decimal GetShurDeterminant(this Matrix<decimal> matrix)
        {
            var res = matrix.SchurComplement(out var a11);
            if (Math.Abs(a11.GetDeterminant()) < 0.00001m)
                throw new MatrixDotNetException("matrix determinant = 0");

            return a11.GetDeterminant() * res.GetDeterminant();
        }
    }
}