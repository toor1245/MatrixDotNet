using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Decompositions
{
    /// <summary>
    /// Represents any algorithm`s for decomposition of matrix.
    /// </summary>
    public static partial class Decomposition
    {
        /// <summary>
        /// Gets LU decomposition of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="lower">the lower triangular matrix.</param>
        /// <param name="upper">the upper triangular matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <exception cref="MatrixDotNetException">throws exception if matrix is not square</exception>
        public static void GetLowerUpper<T>(this Matrix<T> matrix,out Matrix<T> lower,out Matrix<T> upper) where T : unmanaged
        {
            if (!matrix.IsSquare)
                throw new MatrixDotNetException(
                    $"matrix is not square\n Rows: {matrix.Rows}\n Columns: {matrix.Columns}");

            int n = matrix.Columns;
            
            lower = new Matrix<T>(n,n);
            upper = new Matrix<T>(n, n)
            {
                [0, State.Row] = matrix[0, State.Row]
            };
            
            for (int i = 0; i < n; i++)
            {
                lower[0, i, State.Column] = MathExtension.Divide(matrix[0, State.Column][i], upper[0, 0]);
            }
            
            for (int i = 1; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    T sumL = default;
                    T sumU = default;
                    for (int k = 0; k < i; k++)
                    {
                        sumU = MathExtension.Add(sumU, MathExtension.Multiply(lower[i, k], upper[k, j]));
                        sumL = MathExtension.Add(sumL, MathExtension.Multiply(lower[j, k], upper[k, i]));
                    }
                    
                    upper[i, j] = MathExtension.Sub(matrix[i, j],sumU);
                    lower[j, i] = MathExtension.Divide(MathExtension.Sub(matrix[j, i],sumL),upper[i,i]);
                }
            }
        }
    }
}