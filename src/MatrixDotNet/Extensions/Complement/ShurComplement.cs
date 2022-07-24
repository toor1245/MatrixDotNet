using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.Inverse;

namespace MatrixDotNet.Extensions.Complement
{
    public static partial class MatrixExtension
    {
        /// <summary>
        ///     Schur's complement is a square matrix obtained by splitting a square matrix into four parts.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="a11">first part of matrix</param>
        /// <returns></returns>
        public static Matrix<double> SchurComplement(this Matrix<double> matrix, out Matrix<double> a11)
        {
            matrix.SplitMatrix(out a11, out var a12, out var a21, out var a22);
            return a22 - a21 * a11.InverseAlgebraicComplement() * a12;
        }

        /// <summary>
        ///     Schur's complement is a square matrix obtained by splitting a square matrix into four parts.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns></returns>
        public static Matrix<float> SchurComplement(this Matrix<float> matrix, out Matrix<float> a11)
        {
            matrix.SplitMatrix(out a11, out var a12, out var a21, out var a22);
            return a22 - a21 * a11.InverseAlgebraicComplement() * a12;
        }

        /// <summary>
        ///     Schur's complement is a square matrix obtained by splitting a square matrix into four parts.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns></returns>
        public static Matrix<decimal> SchurComplement(this Matrix<decimal> matrix, out Matrix<decimal> a11)
        {
            matrix.SplitMatrix(out a11, out var a12, out var a21, out var a22);
            return a22 - a21 * a11.InverseAlgebraicComplement() * a12;
        }
    }
}