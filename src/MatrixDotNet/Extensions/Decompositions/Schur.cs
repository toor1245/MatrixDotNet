using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.Extensions.Decompositions
{
    public static partial class Decomposition
    {
        public static void SchurDecomposition(this Matrix<double> matrix,
            out Matrix<double> orthogonal,
            out Matrix<double> upper,
            out Matrix<double> ortTranspose)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixNotSquareException();
            }

            orthogonal = matrix.ProcessGrammShmidtByColumns().GetNormByColumns();
            ortTranspose = orthogonal.Transpose();
            upper = matrix * orthogonal;
        }

        public static Matrix<T> GetQuasiTriangular<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixNotSquareException();
            }

            var quasi = new Matrix<T>(matrix.Rows, matrix.Columns);
            for (int i = 0; i < matrix.Rows; i++)
            {
                quasi[i, i] = matrix[i, i];
            }

            return quasi;
        }
    }
}
