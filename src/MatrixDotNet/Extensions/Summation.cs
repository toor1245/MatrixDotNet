using MatrixDotNet.Exceptions;
using MatrixDotNet.Math;
using System;
using System.Collections.Generic;
using MatrixDotNet.Extensions.Performance.Simd;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        #region Sum

        /// <summary>
        /// Summation matrix. 
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Sum whole of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T Sum<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            return Simd.SumFast(matrix._Matrix, matrix.Length);
        }

        /// <summary>
        /// Summation matrix. 
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>Sum whole of matrix</returns>
        public static int Sum(this Matrix<int> matrix)
        {
            return Simd.Sum(matrix._Matrix);
        }

        /// <summary>
        /// Summation matrix. 
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>Sum whole of matrix</returns>
        public static short Sum(this Matrix<short> matrix)
        {
            return Simd.Sum(matrix._Matrix);
        }

        /// <summary>
        /// Summation matrix. 
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>Sum whole of matrix</returns>
        public static float Sum(this Matrix<float> matrix)
        {
            return Simd.Sum(matrix._Matrix);
        }

        /// <summary>
        /// Summation matrix. 
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>Sum whole of matrix</returns>
        public static double Sum(this Matrix<double> matrix)
        {
            return Simd.Sum(matrix._Matrix);
        }

        /// <summary>
        /// Gets sum by row of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Sum row by index</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T SumByRow<T>(this Matrix<T> matrix, int dimension)
            where T : unmanaged
        {
            T sum = default;

            for (int i = 0; i < matrix.Columns; i++)
            {
                sum = MathUnsafe<T>.Add(sum, matrix[dimension, i]);
            }

            return sum;
        }

        /// <summary>
        /// Gets sum by row of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Sum row by index</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static int SumByRow(this Matrix<int> matrix, int dimension)
        {
            return Simd.Sum(matrix[dimension]);
        }

        /// <summary>
        /// Gets sum by row of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Sum row by index</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static short SumByRow(this Matrix<short> matrix, int dimension)
        {
            return Simd.Sum(matrix[dimension]);
        }

        /// <summary>
        /// Gets sum by row of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Sum row by index</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static double SumByRow(this Matrix<double> matrix, int dimension)
        {
            return Simd.Sum(matrix[dimension]);
        }

        /// <summary>
        /// Gets sum by row of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Sum row by index</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static float SumByRow(this Matrix<float> matrix, int dimension)
        {
            return Simd.Sum(matrix[dimension]);
        }

        /// <summary>
        /// Gets sum by column of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">column index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Sum column by index</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T SumByColumn<T>(this Matrix<T> matrix, int dimension)
            where T : unmanaged
        {
            T sum = default;

            for (int i = 0; i < matrix.Rows; i++)
            {
                sum = MathUnsafe<T>.Add(sum, matrix[i, dimension]);
            }

            return sum;
        }

        /// <summary>
        /// Gets array of sum rows.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T[] SumByRows<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            var array = new T[matrix.Rows];

            for (int i = 0; i < matrix.Rows; i++)
            {
                T sum = default;

                for (int j = 0; j < matrix.Columns; j++)
                {
                    sum = MathUnsafe<T>.Add(sum, matrix[i, j]); // sum = sum + matrix[i,j];
                }

                array[i] = sum;
            }

            return array;
        }

        /// <summary>
        /// Gets array of sum columns.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T[] SumByColumns<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();

            var array = new T[matrix.Columns];

            for (int i = 0; i < matrix.Columns; i++)
            {
                T sum = default;

                for (int j = 0; j < matrix.Rows; j++)
                {
                    sum = MathUnsafe<T>.Add(sum, matrix[j, i]);
                }

                array[i] = sum;
            }

            return array;
        }


        /// <summary>
        /// Gets sum by diagonal.
        /// </summary>
        /// <param name="matrix"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T SumByDiagonal<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();

            T sum = default;

            for (int i = 0; i < matrix.Rows; i++)
            {
                sum = MathUnsafe<T>.Add(sum, matrix[i, i]);
            }

            return sum;
        }

        #endregion

        #region Klein

        public static T GetKleinSum<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            if (!MathGeneric.IsFloatingPoint<T>())
            {
                throw new MatrixDotNetException($"{typeof(T)} is not supported type must be floating type");
            }

            T sum = default;
            T cs = default;
            T ccs = default;

            var comparer = Comparer<T>.Default;

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    T t = MathUnsafe<T>.Add(sum, matrix[i, j]);
                    T error;

                    if (comparer.Compare(MathGeneric<T>.Abs(sum), matrix[i, j]) >= 0)
                    {
                        error = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(sum, t), matrix[i, j]);
                    }
                    else
                    {
                        error = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(matrix[i, j], t), sum);
                    }

                    sum = t;
                    t = MathUnsafe<T>.Add(cs, cs);
                    T error2;

                    if (comparer.Compare(MathGeneric<T>.Abs(cs), error) >= 0)
                    {
                        error2 = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(error, t), cs);
                    }
                    else
                    {
                        error2 = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(cs, t), error);
                    }

                    cs = t;
                    ccs = MathUnsafe<T>.Add(ccs, error2);
                }
            }
            return MathUnsafe<T>.Add(MathUnsafe<T>.Add(sum, cs), ccs);
        }

        public static T GetKleinSum<T>(this Matrix<T> matrix, int dimension, State state = State.Row)
            where T : unmanaged
        {
            if (!MathGeneric.IsFloatingPoint<T>())
            {
                throw new MatrixDotNetException($"{typeof(T)} is not supported type must be floating type");
            }

            return state == State.Row ? GetKleinSumByRows(matrix, dimension) : GetKleinSumByColumns(matrix, dimension);
        }

        private static T GetKleinSumByRows<T>(this Matrix<T> matrix, int dimension)
            where T : unmanaged
        {
            var comparer = Comparer<T>.Default;

            T sum = default;
            T cs = default;
            T ccs = default;

            for (int j = 0; j < matrix.Columns; j++)
            {
                T t = MathUnsafe<T>.Add(sum, matrix[dimension, j]);
                T error;

                if (comparer.Compare(MathGeneric<T>.Abs(sum), matrix[dimension, j]) >= 0)
                {
                    error = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(sum, t), matrix[dimension, j]);
                }
                else
                {
                    error = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(matrix[dimension, j], t), sum);
                }

                sum = t;
                t = MathUnsafe<T>.Add(cs, cs);
                T error2;

                if (comparer.Compare(MathGeneric<T>.Abs(cs), error) >= 0)
                {
                    error2 = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(error, t), cs);
                }
                else
                {
                    error2 = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(cs, t), error);
                }

                cs = t;
                ccs = MathUnsafe<T>.Add(ccs, error2);
            }
            return MathUnsafe<T>.Add(MathUnsafe<T>.Add(sum, cs), ccs);
        }

        private static T GetKleinSumByColumns<T>(this Matrix<T> matrix, int dimension)
            where T : unmanaged
        {
            T sum = default;
            T cs = default;
            T ccs = default;

            var comparer = Comparer<T>.Default;

            for (int j = 0; j < matrix.Rows; j++)
            {
                T t = MathUnsafe<T>.Add(sum, matrix[j, dimension]);
                T error;

                if (comparer.Compare(MathGeneric<T>.Abs(sum), matrix[j, dimension]) >= 0)
                {
                    error = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(sum, t), matrix[j, dimension]);
                }
                else
                {
                    error = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(matrix[j, dimension], t), sum);
                }

                sum = t;
                t = MathUnsafe<T>.Add(cs, cs);
                T error2;

                if (comparer.Compare(MathGeneric<T>.Abs(cs), error) >= 0)
                {
                    error2 = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(error, t), cs);
                }
                else
                {
                    error2 = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(cs, t), error);
                }

                cs = t;
                ccs = MathUnsafe<T>.Add(ccs, error2);
            }
            return MathUnsafe<T>.Add(MathUnsafe<T>.Add(sum, cs), ccs);
        }


        #endregion

        #region Kahan

        public static T GetKahanSum<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            T sum = default;
            T error = default;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    T y = MathUnsafe<T>.Sub(matrix[i, j], error);
                    T t = MathUnsafe<T>.Add(sum, y);
                    error = MathUnsafe<T>.Sub(MathUnsafe<T>.Sub(t, sum), matrix[i, j]);
                    sum = t;
                }
            }

            return sum;
        }

        public static T GetKahanSum<T>(this Matrix<T> matrix, int dimension, State state = State.Row)
            where T : unmanaged
        {
            T sum = default;
            T error = default;
            if (state == State.Row)
            {
                for (int i = 0; i < matrix.Columns; i++)
                {
                    T y = MathUnsafe<T>.Sub(matrix[dimension, i], error);
                    T t = MathUnsafe<T>.Add(sum, y);
                    error = MathUnsafe<T>.Sub(MathUnsafe<T>.Sub(t, sum), matrix[dimension, i]);
                    sum = t;
                }
            }
            else
            {
                for (int i = 0; i < matrix.Rows; i++)
                {
                    T y = MathUnsafe<T>.Sub(matrix[i, dimension], error);
                    T t = MathUnsafe<T>.Add(sum, y);
                    error = MathUnsafe<T>.Sub(MathUnsafe<T>.Sub(t, sum), matrix[i, dimension]);
                    sum = t;
                }
            }

            return sum;
        }

        #endregion
    }
}