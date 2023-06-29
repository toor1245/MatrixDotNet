using MatrixDotNet.Exceptions;
using MatrixDotNet.Math;


namespace MatrixDotNet.Extensions.Builder
{
    /// <summary>
    /// Represents the functional of build matrix.
    /// </summary>
    public static partial class BuildMatrix
    {
        /// <summary>
        /// Creates identity matrix.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static Matrix<T> CreateIdentityMatrix<T>(int row, int col) where T : unmanaged
        {
            if (row != col)
            {
                throw new MatrixNotSquareException();
            }

            var matrix = new Matrix<T>(row, col);

            for (int i = 0; i < row; i++)
            {
                matrix[i, i] = MathGeneric<T>.Increment(default);
            }

            return matrix;
        }

        /// <summary>
        /// Creates identity matrix by this size of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Identity matrix.</returns>
        /// <exception cref="MatrixDotNetException">throws exception if matrix is not square</exception>
        public static Matrix<T> CreateIdentityMatrix<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!matrix.IsSquare)
                throw new MatrixNotSquareException();

            var result = new Matrix<T>(matrix.Rows, matrix.Columns);

            for (int i = 0; i < matrix.Rows; i++)
            {
                result[i, i] = MathGeneric<T>.Increment(default);
            }

            return result;
        }
    }
}
