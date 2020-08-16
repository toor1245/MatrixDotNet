using MatrixDotNet.Extensions.Core.Optimization.Unsafe.Decomposition;

namespace MatrixDotNet.Extensions.Core.Optimization.Unsafe.Determinant
{
    public static partial class UnsafeDeterminant
    {
        /// <summary>
        /// Finds determinant of matrix with happen LUP.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>the determinant of matrix.</returns>
        public static unsafe double GetLUPDeterminant(this Matrix<double> matrix)
        {
            matrix.GetLUP(out _,out var upper,out _);
            double det = 1;
            int m = matrix.Rows;
            fixed (double* ptr = upper.GetMatrix())
            {
                for (int i = 0; i < upper.Rows; i++)
                {
                    double* upper2 = ptr + i * m;
                    det *= upper2[i];
                }
            }

            return (UnsafeDecomposition._exchanges & 0b1) == 0 ?  det : -det;
        }
        
        /// <summary>
        /// Finds determinant of matrix with happen LUP.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>the determinant of matrix.</returns>
        public static unsafe float GetLUPDeterminant(this Matrix<float> matrix)
        {
            matrix.GetLUP(out _,out var upper,out _);
            float det = 1;
            int m = matrix.Rows;
            fixed (float* ptr = upper.GetMatrix())
            {
                for (int i = 0; i < m; i++)
                {
                    float* upper2 = ptr + i * m;
                    det *= upper2[i];
                }
            }

            return det;
        }
    }
}