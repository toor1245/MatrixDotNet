using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Math;
using System;

namespace MatrixDotNet.Extensions.Determinants
{
    public static partial class Determinant
    {

        /// <summary>
        /// Gets determinant of matrix.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>double.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static T GetDeterminant<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square", nameof(matrix));
            }

            if (matrix.Length == 4)
            {
                return MathUnsafe<T>.Sub(
                    MathUnsafe<T>.Mul(matrix[0, 0], matrix[1, 1]),
                    MathUnsafe<T>.Mul(matrix[0, 1], matrix[1, 0]));
            }

            T result = default;
            var sign = MathGeneric<T>.Increment(default);

            for (int i = 0; i < matrix.Columns; i++)
            {
                var minor = matrix.GetMinor(i);
                result = MathUnsafe<T>.Add(result,
                    MathUnsafe<T>.Mul(MathUnsafe<T>.Mul(sign, matrix[0, i]), GetDeterminant(minor)));
                sign = MathUnsafe<T>.Sub(sign, MathUnsafe<T>.Sub(sign, sign));
            }

            return result;
        }

        /// <summary>
        /// Gets determinant of corner Minor`s matrix
        /// </summary>
        /// <param name="matrix">matrix</param>
        /// <param name="row">index</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>determinant of corner Minor</returns>
        public static T GetCornerMinorDeterminant<T>(this Matrix<T> matrix, int row)
            where T : unmanaged
        {
            return matrix.GetCornerMinor(row).GetLowerUpperDeterminant();
        }
    }
}