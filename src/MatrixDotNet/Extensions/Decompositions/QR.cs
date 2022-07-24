using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Math;
using MatrixDotNet.Vectorization;

namespace MatrixDotNet.Extensions.Decompositions
{
    public static partial class Decomposition
    {
        public static void QrDecomposition<T>(this Matrix<T> matrix, out Matrix<T> q, out Matrix<T> r)
            where T : unmanaged
        {
            q = ProcessGrammShmidtByColumns(matrix).GetNormByColumns();
            r = q.Transpose() * matrix;
        }

        public static void EigenVectorQrIterative<T>(this Matrix<T> matrix, double accuracy, int maxIterations,
            out Matrix<T> iter, out Matrix<T> qIter) where T : unmanaged
        {
            iter = matrix.Clone() as Matrix<T>;
            qIter = null;
            for (var i = 0; i < maxIterations; i++)
            {
                iter.QrDecomposition(out var q, out var r);
                iter = r * q;
                if (qIter is null)
                {
                    qIter = q;
                }
                else
                {
                    var qNew = qIter * q;
                    var isAchieved = true; // checks accuracy
                    for (var j = 0; j < q.Columns; j++)
                    {
                        for (var k = 0; k < q.Rows; k++)
                        {
                            var sub = MathUnsafe<T>.Sub(MathGeneric<T>.Abs(qNew[j, k]),
                                MathGeneric<T>.Abs(qIter[j, k]));
                            if (accuracy.CompareTo(MathGeneric<T>.Abs(sub)) <= 0)
                                continue;
                            isAchieved = false;
                            break;
                        }

                        if (!isAchieved) break;
                    }

                    qIter = qNew;
                    if (isAchieved) break;
                }
            }
        }


        public static Matrix<T> ProcessGrammShmidtByRows<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!MathGeneric.IsFloatingPoint<T>())
                throw new NotSupportedTypeException(ExceptionArgument.NotSupportedTypeFloatType);

            if (!matrix.IsSquare) throw new MatrixDotNetException("matrix is not square");

            var m = matrix.Rows;

            var b = new Matrix<T>(m, matrix.Columns)
            {
                [0] = matrix[0]
            };

            for (var i = 1; i < m; i++)
            {
                Vector<T> ai = matrix[i];
                Vector<T> sum = new T[m];
                for (var j = 0; j < i; j++)
                {
                    Vector<T> bi = b[j];
                    var scalarProduct = ai * bi;
                    var biMul = bi * bi;
                    var ci = MathGeneric<T>.Divide(scalarProduct, biMul);
                    sum += ci * bi;
                }

                var res = ai - sum;
                b[i] = res.Array;
            }

            return b;
        }


        public static Matrix<T> ProcessGrammShmidtByColumns<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!MathGeneric.IsFloatingPoint<T>())
                throw new NotSupportedTypeException(ExceptionArgument.NotSupportedTypeFloatType);

            if (!matrix.IsSquare) throw new MatrixNotSquareException();

            var m = matrix.Rows;
            var n = matrix.Columns;

            var b = new Matrix<T>(m, n)
            {
                [0, State.Column] = matrix[0, State.Column]
            };

            for (var i = 1; i < n; i++)
            {
                Vector<T> ai = matrix[i, State.Column];
                Vector<T> sum = new T[n];
                for (var j = 0; j < i; j++)
                {
                    Vector<T> bi = b[j, State.Column];
                    var scalarProduct = ai * bi;
                    var biMul = bi * bi;
                    var ci = MathGeneric<T>.Divide(scalarProduct, biMul);
                    sum += ci * bi;
                }

                var res = ai - sum;
                b[i, State.Column] = res.Array;
            }

            return b;
        }

        public static Matrix<T> GetNormByColumns<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!MathGeneric.IsFloatingPoint<T>())
                throw new NotSupportedTypeException(ExceptionArgument.NotSupportedTypeFloatType);

            var m = matrix.Rows;
            var n = matrix.Columns;
            var orthogonal = new Matrix<T>(m, n);
            for (var i = 0; i < n; i++)
            {
                var vector = new Vector<T>(matrix[i, State.Column]);
                var val = vector.GetLengthVec();
                for (var j = 0; j < m; j++) orthogonal[j, i] = MathGeneric<T>.Divide(matrix[j, i], val);
            }

            return orthogonal;
        }

        public static Matrix<T> GetNormByRows<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!MathGeneric.IsFloatingPoint<T>())
                throw new NotSupportedTypeException(ExceptionArgument.NotSupportedTypeFloatType);

            var m = matrix.Rows;
            var n = matrix.Columns;
            var orthogonal = new Matrix<T>(m, n);
            for (var i = 0; i < m; i++)
            {
                var vector = new Vector<T>(matrix[i]);
                var val = vector.GetLengthVec();
                for (var j = 0; j < n; j++) orthogonal[i, j] = MathGeneric<T>.Divide(matrix[i, j], val);
            }

            return orthogonal;
        }
    }
}