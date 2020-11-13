using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Complement
{
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Gets algebraic complement.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        public static Matrix<T> AlgebraicComplement<T>(this Matrix<T> matrix) where T : unmanaged
        {
            var res = matrix.GetMinorMatrix();
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    if ((i + j & 0b01) != 0)
                    {
                        res[i, j] = MathGeneric<T>.Negate(res[i, j]);
                    }
                }
            }

            return res;
        }
    }
}