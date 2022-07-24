using System;
using System.Collections.Generic;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Decompositions
{
    /// <summary>
    ///     Represents any algorithm`s for decomposition of matrix.
    /// </summary>
    public static partial class Decomposition
    {
        internal static int Exchanges { get; set; }

        /// <summary>
        ///     Gets lower-triangular matrix, upper init zero values.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>The lower matrix.</returns>
        /// <exception cref="MatrixDotNetException">throws exception if matrix is not square.</exception>
        public static Matrix<T> GetLower<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!matrix.IsSquare) throw new MatrixNotSquareException();

            var lower = new Matrix<T>(matrix.Rows, matrix.Columns);

            for (var i = 0; i < matrix.Rows; i++)
            for (var j = 0; j < i + 1; j++)
                lower[i, j] = matrix[i, j];

            return lower;
        }

        /// <summary>
        ///     Gets upper-triangular matrix, lower init zero values.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException">throws exception if matrix is not square.</exception>
        public static Matrix<T> GetUpper<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!matrix.IsSquare) throw new MatrixNotSquareException();

            var upper = new Matrix<T>(matrix.Rows, matrix.Columns);

            for (var i = 0; i < matrix.Columns; i++)
            for (var j = 0; j < i + 1; j++)
                upper[j, i] = matrix[i, j];

            return upper;
        }

        /// <summary>
        ///     Gets lower upper permutation with matrix C which calculate by formula:
        ///     <c>C=L+U-E</c>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void GetLowerUpperPermutation<T>(this Matrix<T> matrix, out Matrix<T> matrixC,
            out Matrix<T> matrixP) where T : unmanaged
        {
            var n = matrix.Rows;

            matrixC = matrix.Clone() as Matrix<T>;

            if (matrixC is null)
                throw new NullReferenceException();

            // load to P identity matrix.
            matrixP = BuildMatrix.CreateIdentityMatrix<T>(matrix.Rows, matrix.Columns);

            var comparer = Comparer<T>.Default;

            for (var i = 0; i < n; i++)
            {
                T pivotValue = default;
                var pivot = -1;
                for (var j = i; j < n; j++)
                    if (comparer.Compare(MathGeneric<T>.Abs(matrixC[j, i]), pivotValue) > 0)
                    {
                        pivotValue = MathGeneric<T>.Abs(matrixC[j, i]);
                        pivot = j;
                    }

                if (pivot != 0)
                {
                    matrixP.SwapRows(pivot, i);
                    matrixC.SwapRows(pivot, i);
                    for (var j = i + 1; j < n; j++)
                    {
                        matrixC[j, i] = MathGeneric<T>.Divide(matrixC[j, i], matrixC[i, i]);
                        for (var k = i + 1; k < n; k++)
                            matrixC[j, k] = MathUnsafe<T>.Sub(matrixC[j, k],
                                MathUnsafe<T>.Mul(matrixC[j, i], matrix[i, k]));
                    }
                }
            }
        }


        /// <summary>
        ///     Gets lower upper permutation;
        /// </summary>
        /// <returns></returns>
        public static void GetLowerUpperPermutation(this Matrix<double> matrix, out Matrix<double> lower,
            out Matrix<double> upper, out Matrix<double> matrixP)
        {
            var n = matrix.Rows;

            lower = matrix.CreateIdentityMatrix();
            upper = matrix.Clone() as Matrix<double>;

            if (upper is null) throw new NullReferenceException();

            // load to P identity matrix.
            matrixP = lower.Clone() as Matrix<double>;

            for (var i = 0; i < n - 1; i++)
            {
                var index = i;
                var max = upper[index, index];
                for (var j = i + 1; j < n; j++)
                {
                    var current = upper[i, j];
                    if (System.Math.Abs(current) > System.Math.Abs(max))
                    {
                        max = current;
                        index = j;
                    }
                }

                if (System.Math.Abs(max) < 0.0001) continue;

                if (index != i)
                {
                    upper.SwapRows(i, index);
                    matrixP.SwapRows(i, index);
                    Exchanges++;
                }

                for (var j = i + 1; j < n; j++)
                {
                    var ji = upper[j, i];
                    var ii = upper[i, i];
                    var div = ji / ii;
                    lower[j, i] = div;

                    for (var k = i; k < n; k++) upper[j, k] = upper[j, k] - upper[i, k] * div;
                }
            }
        }

        public static unsafe void GetLowerUpperPermutationUnsafe(this Matrix<double> matrix, out Matrix<double> lower,
            out Matrix<double> upper, out Matrix<double> matrixP)
        {
            Exchanges = 0;

            var n = matrix.Rows;
            var m = matrix.Columns;
            lower = matrix.CreateIdentityMatrix();
            upper = matrix.Clone() as Matrix<double>;
            // load to P identity matrix.
            matrixP = lower.Clone() as Matrix<double>;
            fixed (double* ptrU = upper.GetArray())
            fixed (double* ptrL = lower.GetArray())
            {
                for (var i = 0; i < n - 1; i++)
                {
                    var index = i;
                    var max = *(ptrU + index * m);
                    var mx = ptrU + i * m;
                    var test1 = ptrU + i * n;
                    for (var j = i + 1; j < n; j++)
                    {
                        var current = test1[j];
                        if (System.Math.Abs(current) > System.Math.Abs(max))
                        {
                            max = current;
                            index = j;
                        }
                    }

                    if (System.Math.Abs(max) < 0.0001) continue;

                    if (index != i)
                    {
                        upper.SwapRows(i, index);
                        matrixP.SwapRows(i, index);
                        Exchanges++;
                    }

                    for (var j = i + 1; j < n; j++)
                    {
                        var test2 = ptrU + j * m;
                        var div = test2[i] / mx[i];
                        *(ptrL + j * m) = div;
                        test2 = ptrU + j * n;

                        for (var k = i; k < n; k++) test2[k] = test2[k] - mx[k] * div;
                    }
                }
            }
        }

        public static unsafe void GetLowerUpperPermutationUnsafe(this Matrix<float> matrix, out Matrix<float> lower,
            out Matrix<float> upper, out Matrix<float> matrixP)
        {
            var n = matrix.Rows;
            var m = matrix.Columns;
            lower = matrix.CreateIdentityMatrix();
            upper = matrix.Clone() as Matrix<float>;

            Exchanges = 0;

            // load to P identity matrix.
            matrixP = lower.Clone() as Matrix<float>;
            fixed (float* ptrU = upper.GetArray())
            fixed (float* ptrL = lower.GetArray())
            {
                for (var i = 0; i < n - 1; i++)
                {
                    var index = i;
                    double max = *(ptrU + index * m);
                    var mx = ptrU + i * m;
                    var test1 = ptrU + i * n;
                    for (var j = i + 1; j < n; j++)
                    {
                        var current = test1[j];
                        if (System.Math.Abs(current) > System.Math.Abs(max))
                        {
                            max = current;
                            index = j;
                        }
                    }

                    if (System.Math.Abs(max) < 0.0001) continue;

                    if (index != i)
                    {
                        upper.SwapRows(i, index);
                        matrixP.SwapRows(i, index);
                        Exchanges++;
                    }

                    for (var j = i + 1; j < n; j++)
                    {
                        var test2 = ptrU + j * m;
                        var div = test2[i] / mx[i];
                        *(ptrL + j * m) = div;
                        test2 = ptrU + j * n;

                        for (var k = i; k < n; k++) test2[k] = test2[k] - mx[k] * div;
                    }
                }
            }
        }
    }
}