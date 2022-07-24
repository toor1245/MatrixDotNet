using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.Extensions
{
    // Multiply Strassen
    public static partial class MatrixExtension
    {
        #region Strassen

        /// <summary>
        ///     Strassen`s multiply, use when matrix rows > 32.
        /// </summary>
        /// <param name="a">the matrix a.</param>
        /// <param name="b">the matrix b.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>the new matrix from multiply of two matrices.</returns>
        public static Matrix<T> MultiplyStrassen<T>(Matrix<T> a, Matrix<T> b) where T : unmanaged
        {
            if (a.Rows < 32) return a * b;

            a.SplitMatrix(out var a11, out var a12, out var a21, out var a22);
            b.SplitMatrix(out var b11, out var b12, out var b21, out var b22);

            var p1 = MultiplyStrassen(a11 + a22, b11 + b22);
            var p2 = MultiplyStrassen(a21 + a22, b11);
            var p3 = MultiplyStrassen(a11, b12 - b22);
            var p4 = MultiplyStrassen(a22, b21 - b11);
            var p5 = MultiplyStrassen(a11 + a12, b22);
            var p6 = MultiplyStrassen(a21 - a22, b11 + b12);
            var p7 = MultiplyStrassen(a12 - a22, b21 + b22);

            var c11 = p1 + p4 - p5 + p7;
            var c12 = p3 + p5;
            var c21 = p2 + p4;
            var c22 = p1 + p3 - p2 + p6;

            return MatrixConverter.CollectMatrix(c11, c12, c21, c22);
        }

        #endregion

        #region Degree

        /// <summary>
        ///     Raises a matrix to a power.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="degree">the degree</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>Pow</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static Matrix<T> Pow<T>(this Matrix<T> matrix, uint degree)
            where T : unmanaged
        {
            if (degree == 0)
                return BuildMatrix.CreateIdentityMatrix<T>(matrix.Rows, matrix.Columns);

            if ((degree & 1) == 1)
                return matrix.Pow(degree - 1) * matrix;

            var t = matrix.Pow(degree >> 1);
            return t * t;
        }

        /// <summary>
        ///     Raises a matrix to a power.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="degree">the degree</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>Pow</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static Matrix<T> PowStrassen<T>(this Matrix<T> matrix, uint degree) where T : unmanaged
        {
            if (!matrix.IsSquare || !matrix.IsPrime)
                throw new MatrixDotNetException("matrix is not square or not prime");

            var result = new Matrix<T>(matrix.Rows, matrix.Columns);

            if (degree == 1)
                return matrix;

            if (degree == 2)
                return matrix * matrix;

            MultiplyStrassen(matrix, matrix);

            for (var i = 0; i < degree; i++) result = MultiplyStrassen(result, matrix);

            return result;
        }

        #endregion
    }
}