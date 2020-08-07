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

            var res1 = a11.GetLowerUpperDeterminant();
            
            if(Math.Abs(res1) < 0.00001)
                throw new MatrixDotNetException("a11 matrix determinant = 0");

            return res1 * res.GetLowerUpperDeterminant();
        }
        
        
        public static float GetShurDeterminant(this Matrix<float> matrix)
        {
            if(!matrix.IsPrime)
                throw new MatrixDotNetException("matrix is not prime");
            
            var res = matrix.SchurComplement(out var a11);

            var res1 = a11.GetLowerUpperDeterminant();
            
            if(Math.Abs(res1) < 0.00001)
                throw new MatrixDotNetException("a11 matrix determinant = 0");

            return res1 * res.GetDeterminant();
        }
        
        public static decimal GetShurDeterminant(this Matrix<decimal> matrix)
        {
            if(!matrix.IsPrime)
                throw new MatrixDotNetException("matrix is not prime");
            
            var res = matrix.SchurComplement(out var a11);

            var res1 = a11.GetLowerUpperDeterminant();
            
            if(Math.Abs(res1) < 0.00001m)
                throw new MatrixDotNetException("a11 matrix determinant = 0");

            return res1 * res.GetDeterminant();
        }
    }
}