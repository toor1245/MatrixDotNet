using MatrixDotNet.Extensions.Complement;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.Determinant;

namespace MatrixDotNet.Extensions.Inverse
{
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Gets inverse matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>Inverse matrix.</returns>
        public static Matrix<double> InverseAlgebraicComplement(this Matrix<double> matrix)
        {
            var alg = matrix.AlgebraicComplement().Transpose();

            return 1 / matrix.GetLowerUpperDeterminant() * alg;
        }
        
        /// <summary>
        /// Gets inverse matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>Inverse matrix.</returns>
        public static Matrix<float> InverseAlgebraicComplement(this Matrix<float> matrix)
        {
            var alg = matrix.AlgebraicComplement().Transpose();

            return 1 / matrix.GetLowerUpperDeterminant() * alg;
        }
        
        /// <summary>
        /// Gets inverse matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>Inverse matrix.</returns>
        public static Matrix<decimal> InverseAlgebraicComplement(this Matrix<decimal> matrix)
        {
            var alg = matrix.AlgebraicComplement().Transpose();

            return 1 / matrix.GetLowerUpperDeterminant() * alg;
        }
    }
}