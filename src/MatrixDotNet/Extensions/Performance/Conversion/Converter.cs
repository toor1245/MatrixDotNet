using System;
using MatrixDotNet.Exceptions;
#if NETCOREAPP3_1_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace MatrixDotNet.Extensions.Performance.Conversion
{
    /// <summary>
    ///     Represents conversion operations for matrix with fixed buffer size.
    /// </summary>
    public static class Converter
    {
        /// <summary>
        ///     Adds row for matrix with fixed buffer size.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="data">the data which assign by index</param>
        /// <param name="index">the row index</param>
        /// <returns>Matrix with new row.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe MatrixOnStack AddRow(ref MatrixOnStack matrix, double[] data, int index)
        {
            if (matrix.Columns != data.Length)
            {
                var message =
                    $"length {nameof(data)}:{data.Length} != {nameof(matrix.Columns)} of matrix:{matrix.Columns}";
                throw new MatrixDotNetException(message);
            }

            fixed (double* arr = data)
            {
                var n = matrix.Columns;
                var span3 = new Span<double>(arr, n);
                var matrixAsFixedBuffer = new MatrixOnStack((byte) (matrix.Rows + 1), n);

                for (var i = 0; i < index; i++)
                {
                    matrixAsFixedBuffer[i] = matrix[i];
                }

                matrixAsFixedBuffer[index] = span3;

                for (var i = index + 1; i < matrixAsFixedBuffer.Rows; i++)
                {
                    matrixAsFixedBuffer[i] = matrix[i - 1];
                }

                return matrixAsFixedBuffer;
            }
        }

        /// <summary>
        ///     Adds column for matrix with fixed buffer size.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="data">the data.</param>
        /// <param name="index">the column index.</param>
        /// <returns>Matrix with new column.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe MatrixOnStack AddColumn(ref MatrixOnStack matrix, double[] data, int index)
        {
            if (matrix.Rows != data.Length)
            {
                var message =
                    $"length {nameof(data)}:{data.Length} != {nameof(matrix.Rows)} of matrix:{matrix.Rows}";
                throw new MatrixDotNetException(message);
            }

            fixed (double* arr = data)
            {
                var m = matrix.Rows;
                var span3 = new Span<double>(arr, m);
                var matrixAsFixedBuffer = new MatrixOnStack(m, (byte) (matrix.Columns + 1));

                for (var i = 0; i < index; i++)
                {
                    matrixAsFixedBuffer.SetColumn(i, matrix.GetColumn(i));
                }

                matrixAsFixedBuffer.SetColumn(index, span3);

                for (var i = index + 1; i < matrixAsFixedBuffer.Columns; i++)
                {
                    matrixAsFixedBuffer.SetColumn(i, matrix.GetColumn(i - 1));
                }

                return matrixAsFixedBuffer;
            }
        }

        /// <summary>
        ///     Swaps rows with happen AVX2 or Unsafe swap.
        /// </summary>
        /// <param name="matrix">the matrix with fixed buffer</param>
        /// <param name="from">the index of row.</param>
        /// <param name="to">the index of row.</param>
        public static unsafe void SwapRows(ref MatrixOnStack matrix, int from, int to)
        {
#if NETCOREAPP3_1_OR_GREATER
            if (Avx.IsSupported)
            {
                var i = 0;
                int n = matrix.Columns;
                fixed (double* ptr1 = matrix[from])
                fixed (double* ptr2 = matrix[to])
                {
                    // Swaps rows.
                    while (i < n - Vector256<double>.Count)
                    {
                        var vector2 = Avx.LoadVector256(ptr2 + i);
                        var vector1 = Avx.LoadVector256(ptr1 + i);
                        Avx.Store(ptr2 + i, vector1);
                        Avx.Store(ptr1 + i, vector2);
                        i += 4;
                    }

                    // Swaps rows if columns length not prime.
                    while (i < n)
                    {
                        var temp = matrix[from, i];
                        matrix[from, i] = matrix[to, i];
                        matrix[to, i] = temp;
                        i++;
                    }
                }
            }
            else
#endif
            {
                fixed (double* ptr1 = matrix._array)
                {
                    var span = new Span<double>(ptr1, matrix.Length);
                    int m = matrix.Rows;
                    int n = matrix.Columns;
                    var index = from * n + m;
                    var i = from * n;
                    var j = to * n;

                    // Swaps rows.
                    while (i < index)
                    {
                        var tmp = span[i];
                        span[i] = span[j];
                        span[j] = tmp;
                        i++;
                        j++;
                    }
                }
            }
        }

        /// <summary>
        ///     Swaps columns with happen AVX2 or Unsafe swap.
        /// </summary>
        /// <param name="matrix">the matrix with fixed buffer</param>
        /// <param name="from">the index of column.</param>
        /// <param name="to">the index of column.</param>
        public static void SwapColumns(ref MatrixOnStack matrix, int from, int to)
        {
            int m = matrix.Rows;
            var i = 0;

            // Swaps columns.
            while (i < m)
            {
                var tmp = matrix[i, from];
                matrix[i, from] = matrix[i, to];
                matrix[i, to] = tmp;
                i++;
            }
        }


        /// <summary>
        ///     Copy matrix to by some criteria via AVX2.
        /// </summary>
        /// <param name="matrix1">the matrix1</param>
        /// <param name="dimension1">row index of matrix1</param>
        /// <param name="start">start index by row of matrix1</param>
        /// <param name="matrix2">the matrix2</param>
        /// <param name="dimension2">row index of matrix2</param>
        /// <param name="destinationIndex">start index by row of matrix2</param>
        /// <param name="length">the length of copy data</param>
        public static unsafe void CopyToAvx(ref MatrixOnStack matrix1, int dimension1, int start,
            ref MatrixOnStack matrix2, int dimension2, int destinationIndex, int length)
        {
#if NETCOREAPP3_1_OR_GREATER
            if (Avx2.IsSupported)
            {
                var i = start;
                var k = destinationIndex;
                fixed (double* ptr1 = matrix1[dimension1])
                fixed (double* ptr2 = matrix2[dimension2])
                {
                    while (k < length - Vector256<double>.Count)
                    {
                        var vector = Avx.LoadVector256(ptr1 + i);
                        Avx.Store(ptr2 + destinationIndex, vector);
                        i += 4;
                        k += 4;
                    }
                }

                for (; k < length; i++, k++) matrix2[dimension2, k] = matrix1[dimension1, i];
            }
            else
#endif
            {
                CopyTo(ref matrix1, dimension1, start, ref matrix2, dimension2, destinationIndex, length);
            }
        }

        /// <summary>
        ///     Copy matrix to by some criteria.
        /// </summary>
        /// <param name="matrix1">the matrix1</param>
        /// <param name="dimension1">row index of matrix1</param>
        /// <param name="start">start index by row of matrix1</param>
        /// <param name="matrix2">the matrix2</param>
        /// <param name="dimension2">row index of matrix2</param>
        /// <param name="destinationIndex">start index by row of matrix2</param>
        /// <param name="length">the length of copy data</param>
        public static unsafe void CopyTo(ref MatrixOnStack matrix1, int dimension1, int start,
            ref MatrixOnStack matrix2, int dimension2, int destinationIndex, int length)
        {
            fixed (double* ptr2 = matrix2._array)
            fixed (double* ptr1 = matrix1._array)
            {
                var span2 = new Span<double>(ptr2, matrix2.Length);
                var span1 = new Span<double>(ptr1, matrix1.Length);
                for (int i = start, k = destinationIndex; k < length; i++, k++)
                {
                    span2[dimension2 * matrix2.Columns + k] = span1[dimension1 * matrix1.Columns + i];
                }
            }
        }
    }
}