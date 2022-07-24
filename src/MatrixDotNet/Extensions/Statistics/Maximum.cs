using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace MatrixDotNet.Extensions.Statistics
{
    /// <summary>
    ///     Represents the functional of bit operations with a matrix
    /// </summary>
    public static partial class MatrixExtension
    {
        /// <summary>
        ///     Gets maximum value of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>maximum of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T Max<T>(this Matrix<T> matrix) where T : unmanaged
        {
            var size = Vector<T>.Count;
            var vmax = new Vector<T>(matrix[0, 0]);
            for (var i = 0; i < matrix.Length / size; i++)
            {
                var va = new Vector<T>(matrix.GetArray(), i * size);
                var vless = Vector.GreaterThan(va, vmax);
                vmax = Vector.ConditionalSelect(vless, va, vmax);
            }

            var max = vmax[0];
            var cmp = Comparer<T>.Default;
            for (var j = 1; j < size; j++)
                if (cmp.Compare(max, vmax[j]) < 0)
                    max = vmax[j];

            for (var i = 0; i < matrix.Length % size; i++)
                if (cmp.Compare(max, matrix._Matrix[i]) < 0)
                    max = matrix._Matrix[i];

            return max;
        }

        /// <summary>
        ///     Gets maximum value of matrix by row index.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">the index row.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Maximum element by index row.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T MaxByRow<T>(this Matrix<T> matrix, int dimension) where T : unmanaged
        {
            var max = matrix[dimension, 0];
            var comparer = Comparer.Default;

            for (var j = 0; j < matrix.Columns; j++)
                if (comparer.Compare(matrix[dimension, j], max) > 0)
                    max = matrix[dimension, j];

            return max;
        }


        /// <summary>
        ///     Gets maximum value of matrix by column index.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="dimension">the index column.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T MaxByColumn<T>(this Matrix<T> matrix, int dimension) where T : unmanaged
        {
            var max = matrix[0, dimension];
            var comparer = Comparer.Default;
            for (var j = 0; j < matrix.Rows; j++)
                if (comparer.Compare(matrix[j, dimension], max) > 0)
                    max = matrix[j, dimension];

            return max;
        }

        /// <summary>
        ///     Gets maximum value of matrix by column index.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="dimension">the index column.</param>
        /// <param name="index">index row.</param>
        /// >
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T MaxByColumn<T>(this Matrix<T> matrix, int dimension, out int index) where T : unmanaged
        {
            var max = matrix[0, dimension];
            index = 0;
            var comparer = Comparer.Default;
            for (var j = 0; j < matrix.Rows; j++)
                if (comparer.Compare(matrix[j, dimension], max) > 0)
                {
                    max = matrix[j, dimension];
                    index = j;
                }

            return max;
        }

        /// <summary>
        ///     Gets maximum value by each row.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T[] MaxRows<T>(this Matrix<T> matrix) where T : unmanaged
        {
            var result = new T[matrix.Rows];
            var comparer = Comparer.Default;
            for (var i = 0; i < matrix.Rows; i++)
            {
                var max = matrix[i, 0];
                for (var j = 0; j < matrix.Columns; j++)
                    if (comparer.Compare(matrix[i, j], max) > 0)
                        max = matrix[i, j];

                result[i] = max;
            }

            return result;
        }

        /// <summary>
        ///     Gets maximum value by each column.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>Array which contains maximum values of each column.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T[] MaxColumns<T>(this Matrix<T> matrix) where T : unmanaged
        {
            var result = new T[matrix.Columns];
            var comparer = Comparer.Default;
            for (var i = 0; i < matrix.Columns; i++)
            {
                var max = matrix[0, i];
                for (var j = 0; j < matrix.Rows; j++)
                    if (comparer.Compare(matrix[j, i], max) > 0)
                        max = matrix[j, i];

                result[i] = max;
            }

            return result;
        }

        /// <summary>
        ///     Gets maximum by diagonal.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T MaxByMainDiagonal<T>(this Matrix<T> matrix) where T : unmanaged
        {
            var comparer = Comparer.Default;
            var max = matrix[0, 0];

            var x = matrix.Columns;
            var y = matrix.Rows;
            var min = (x & ((x - y) >> 31)) | (y & (~(x - y) >> 31));

            for (var i = 0; i < min; i++)
                if (comparer.Compare(matrix[i, i], max) > 0)
                    max = matrix[i, i];

            return max;
        }

        /// <summary>
        ///     Gets maximum of matrix with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>maximum of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static int BitMax(this Matrix<int> matrix)
        {
            var max = matrix[0, 0];
            for (var i = 0; i < matrix.Rows; i++)
            for (var j = 0; j < matrix.Columns; j++)
            {
                var prefetch = matrix[i, j];
                max = (prefetch & ((max - prefetch) >> 31)) | (max & (~(max - prefetch) >> 31));
            }

            return max;
        }

        /// <summary>
        ///     Gets maximum of matrix with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>maximum of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static int BitMax(this Matrix<byte> matrix)
        {
            int max = matrix[0, 0];
            for (var i = 0; i < matrix.Rows; i++)
            for (var j = 0; j < matrix.Columns; j++)
            {
                var prefetch = matrix[i, j];
                max = (prefetch & ((max - prefetch) >> 31)) | (max & (~(max - prefetch) >> 31));
            }

            return max;
        }

        /// <summary>
        ///     Gets maximum of matrix with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>maximum of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static long BitMax(this Matrix<long> matrix)
        {
            var max = matrix[0, 0];
            for (var i = 0; i < matrix.Rows; i++)
            for (var j = 0; j < matrix.Columns; j++)
            {
                var prefetch = matrix[i, j];
                max = (prefetch & ((max - prefetch) >> 63)) | (max & (~(max - prefetch) >> 63));
            }

            return max;
        }

        /// <summary>
        ///     Gets maximum of matrix with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>maximum of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static int BitMax(this Matrix<short> matrix)
        {
            int max = matrix[0, 0];
            for (var i = 0; i < matrix.Rows; i++)
            for (var j = 0; j < matrix.Columns; j++)
            {
                var prefetch = matrix[i, j];
                max = (prefetch & ((max - prefetch) >> 15)) | (max & (~(max - prefetch) >> 15));
            }

            return max;
        }

        /// <summary>
        ///     Gets maximum value by row index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <returns>maximum value by row.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static int BitMaxByRow(this Matrix<int> matrix, int dimension)
        {
            var max = matrix[dimension, 0];
            for (var i = 0; i < matrix.Columns; i++)
            {
                var prefetch = matrix[dimension, i];
                max = (prefetch & ((max - prefetch) >> 31)) | (max & (~(max - prefetch) >> 31));
            }

            return max;
        }


        /// <summary>
        ///     Gets maximum value by row index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <returns>maximum value by row.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static int BitMaxByRow(this Matrix<byte> matrix, int dimension)
        {
            int max = matrix[dimension, 0];
            for (var i = 0; i < matrix.Columns; i++)
            {
                var prefetch = matrix[dimension, i];
                max = (prefetch & ((max - prefetch) >> 31)) | (max & (~(max - prefetch) >> 31));
            }

            return max;
        }

        /// <summary>
        ///     Gets maximum value by row index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <returns>maximum value by row.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static int BitMaxByRow(this Matrix<short> matrix, int dimension)
        {
            int max = matrix[dimension, 0];
            for (var i = 0; i < matrix.Columns; i++)
            {
                var prefetch = matrix[dimension, i];
                max = (prefetch & ((max - prefetch) >> 31)) | (max & (~(max - prefetch) >> 31));
            }

            return max;
        }

        /// <summary>
        ///     Gets maximum value by row index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <returns>maximum value by row.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static long BitMaxByRow(this Matrix<long> matrix, int dimension)
        {
            var max = matrix[dimension, 0];
            for (var i = 0; i < matrix.Rows; i++)
            {
                var prefetch = matrix[dimension, i];
                max = (prefetch & ((max - prefetch) >> 63)) | (max & (~(max - prefetch) >> 63));
            }

            return max;
        }

        /// <summary>
        ///     Gets maximum value by column index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">column index.</param>
        /// <returns>maximum value by column.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static int BitMaxByColumn(this Matrix<byte> matrix, int dimension)
        {
            int max = matrix[0, dimension];
            for (var i = 0; i < matrix.Rows; i++)
            {
                var prefetch = matrix[i, dimension];
                max = (prefetch & ((max - prefetch) >> 31)) | (max & (~(max - prefetch) >> 31));
            }

            return max;
        }

        /// <summary>
        ///     Gets maximum value by column index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">column index.</param>
        /// <returns>maximum value by column.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static int BitMaxByColumn(this Matrix<short> matrix, int dimension)
        {
            int max = matrix[0, dimension];
            for (var i = 0; i < matrix.Rows; i++)
            {
                var prefetch = matrix[i, dimension];
                max = (prefetch & ((max - prefetch) >> 31)) | (max & (~(max - prefetch) >> 31));
            }

            return max;
        }

        /// <summary>
        ///     Gets maximum value by column index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">column index.</param>
        /// <returns>maximum value by column.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static int BitMaxByColumn(this Matrix<int> matrix, int dimension)
        {
            var max = matrix[0, dimension];
            for (var i = 0; i < matrix.Rows; i++)
            {
                var prefetch = matrix[i, dimension];
                max = (prefetch & ((max - prefetch) >> 31)) | (max & (~(max - prefetch) >> 31));
            }

            return max;
        }

        /// <summary>
        ///     Gets maximum value by column index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">column index.</param>
        /// <returns>maximum value by column.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static long BitMaxByColumn(this Matrix<long> matrix, int dimension)
        {
            var max = matrix[0, dimension];
            for (var i = 0; i < matrix.Rows; i++)
            {
                var prefetch = matrix[i, dimension];
                max = (prefetch & ((max - prefetch) >> 63)) | (max & (~(max - prefetch) >> 63));
            }

            return max;
        }
    }
}