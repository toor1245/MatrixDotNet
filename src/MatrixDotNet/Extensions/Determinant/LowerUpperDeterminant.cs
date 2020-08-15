using MatrixDotNet.Extensions.Decomposition;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Determinant
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
            matrix.GetLowerUpper(out var lower,out var upper);

            T lowerDet = MathExtension.Increment<T>(default);
            T upperDet = MathExtension.Increment<T>(default);

            for (int i = 0; i < matrix.Rows; i++)
            {
                lowerDet = MathExtension.Multiply(lowerDet, lower[i, i]);
                upperDet = MathExtension.Multiply(upperDet, upper[i, i]);
            }

            return MathExtension.Multiply(lowerDet,upperDet);
        }
    }
}