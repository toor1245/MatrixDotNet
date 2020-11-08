using MatrixDotNet.Extensions.Decompositions;

namespace MatrixDotNet.Extensions.Determinants
{
    public static partial class Determinant 
    {
        /// <summary>
        /// Gets LUP determinant.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns></returns>
        public static double GetLowerUpperPermutationDeterminant(this Matrix<double> matrix)
        {
            matrix.GetLowerUpperPermutation(out _,out var upper,out _);
            double det = 1;
            for (int i = 0; i < upper.Rows; i++)
            {
                det *= upper[i, i];
            }

            return (Decomposition.Exchanges & 0b1) == 0 ?  det : -det;
        }
        
        /// <summary>
        /// Finds determinant of matrix with happen LUP.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>the determinant of matrix.</returns>
        public static unsafe double GetLupDeterminantUnsafe(this Matrix<double> matrix)
        {
            matrix.GetLowerUpperPermutationUnsafe(out _,out var upper,out _);
            double det = 1;
            int m = matrix.Rows;
            fixed (double* ptr = upper.GetArray())
            {
                for (int i = 0; i < upper.Rows; i++)
                {
                    double* upper2 = ptr + i * m;
                    det *= upper2[i];
                }
            }

            return (Decomposition.Exchanges & 0b1) == 0 ?  det : -det;
        }
        
        /// <summary>
        /// Finds determinant of matrix with happen LUP.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>the determinant of matrix.</returns>
        public static unsafe float GetLupDeterminantUnsafe(this Matrix<float> matrix)
        {
            matrix.GetLowerUpperPermutationUnsafe(out _,out var upper,out _);
            float det = 1;
            int m = matrix.Rows;
            fixed (float* ptr = upper.GetArray())
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