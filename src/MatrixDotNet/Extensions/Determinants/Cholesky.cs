using MatrixDotNet.Extensions.Decompositions;

namespace MatrixDotNet.Extensions.Determinants
{
    public static partial class Determinant
    {

        /// <summary>
        /// Gets determinant with happen Cholesky algorithm which decompose matrix A = L * L(transpose).
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>determinant.</returns>
        public static double GetCholeskyDeterminant(this Matrix<double> matrix)
        {
            matrix.GetCholesky(out var lower, out var transpose);
            double det = 1;
            double det2 = 1;
            for (int i = 0; i < matrix.Rows; i++)
            {
                det *= lower[i, i];
                det2 *= transpose[i, i];
            }


            return det * det2;
        }


        /// <summary>
        /// Gets determinant with happen Cholesky algorithm which decompose matrix A = L * L(transpose).
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>determinant.</returns>
        public static float GetCholeskyDeterminant(this Matrix<float> matrix)
        {
            matrix.GetCholesky(out var lower, out var transpose);
            float det = 1;
            float det2 = 1;
            for (int i = 0; i < matrix.Rows; i++)
            {
                det *= lower[i, i];
                det2 *= transpose[i, i];
            }


            return det * det2;
        }


        /// <summary>
        /// Gets determinant with happen Cholesky algorithm which decompose matrix A = L * L(transpose).
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>determinant.</returns>
        public static decimal GetCholeskyDeterminant(this Matrix<decimal> matrix)
        {
            matrix.GetCholesky(out var lower, out var transpose);
            decimal det = 1;
            decimal det2 = 1;
            for (int i = 0; i < matrix.Rows; i++)
            {
                det *= lower[i, i];
                det2 *= transpose[i, i];
            }


            return det * det2;
        }
    }
}