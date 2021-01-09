using MatrixDotNet.Extensions.Decompositions;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Determinants
{
    public static partial class Determinant
    {
        /// <summary>
        /// Gets LU determinant.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Determinant</returns>
        public static T GetLowerUpperDeterminant<T>(this Matrix<T> matrix) where T : unmanaged
        {
            matrix.GetLowerUpper(out var lower, out var upper);

            T lowerDet = MathGeneric<T>.Increment(default);
            T upperDet = MathGeneric<T>.Increment(default);

            for (int i = 0; i < matrix.Rows; i++)
            {
                lowerDet = MathUnsafe<T>.Mul(lowerDet, lower[i, i]);
                upperDet = MathUnsafe<T>.Mul(upperDet, upper[i, i]);
            }

            return MathUnsafe<T>.Mul(lowerDet, upperDet);
        }
    }
}