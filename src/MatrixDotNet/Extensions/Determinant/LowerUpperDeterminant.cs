using MatrixDotNet.Extensions.Decomposition;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Determinant
{
    public static partial class Determinant
    {
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