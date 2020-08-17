using System;
using MatrixDotNet.Extensions.Decomposition;

namespace MatrixDotNet.Extensions.Determinant
{
    public static partial class Decomposition
    {
        public static double GetCholeskyDeterminant(this Matrix<double> matrix)
        {
            matrix.GetCholesky(out var lower,out var transpose);
            double det = 1;
            double det2 = 1;
            for (int i = 0; i < matrix.Rows; i++)
            {
                det *= lower[i, i];
                det2 *= transpose[i, i];
            }
            
            
            return det * det2;
        }
        
        public static double GetCholeskyDeterminantLn(this Matrix<double> matrix)
        {
            matrix.GetCholesky(out var lower,out var transpose);
            
            double det = 1;
            for (int i = 0; i < matrix.Rows; i++)
            {
                det *= lower[i, i];
            }
            
            return Math.Log10(det * det);
        }
        
        public static double GetCholeskyDeterminantPower(this Matrix<double> matrix)
        {
            matrix.GetCholesky(out var lower,out var transpose);
            
            double sum = 0;
            for (int i = 0; i < matrix.Rows; i++)
            {
                sum += Math.Log(lower[i, i]);
            }
            
            return 2 * sum;
        }
    }
}