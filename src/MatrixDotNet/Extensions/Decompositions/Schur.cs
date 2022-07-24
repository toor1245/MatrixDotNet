using System.Collections.Generic;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Decompositions
{
    public static partial class Decomposition
    {
        public static void SchurDecomposition(this Matrix<double> matrix,
            out Matrix<double> orthogonal,
            out Matrix<double> upper,
            out Matrix<double> ortTranspose)
        {
            if (!matrix.IsSquare) throw new MatrixNotSquareException();

            orthogonal = matrix.ProcessGrammShmidtByColumns().GetNormByColumns();
            ortTranspose = orthogonal.Transpose();
            upper = matrix * orthogonal;
        }

        public static bool IsIdentity<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            var comparer = Comparer<T>.Default;

            for (var i = 0; i < matrix.Rows; i++)
            for (var j = 0; j < matrix.Columns; j++)
                if (i == j)
                {
                    if (comparer.Compare(matrix[i, j], MathGeneric<T>.Increment(default)) != 0) return false;
                }
                else if (comparer.Compare(matrix[i, j], default) != 0)
                {
                    return false;
                }

            return true;
        }

        public static Matrix<T> GetQuasiTriangular<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!matrix.IsSquare) throw new MatrixNotSquareException();

            var quasi = new Matrix<T>(matrix.Rows, matrix.Columns);
            for (var i = 0; i < matrix.Rows; i++) quasi[i, i] = matrix[i, i];

            return quasi;
        }
    }
}