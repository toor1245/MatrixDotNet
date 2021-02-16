using System;
using System.Collections.Generic;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Math;


namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Gets l-norm.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>l-norm.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T LNorm<T>(this Matrix<T> matrix) where T : unmanaged
        {

            int rows = matrix.Rows;
            int columns = matrix.Columns;

            var array = new T[columns];

            for (int i = 0; i < columns; i++)
            {
                T sum = default;

                for (int j = 0; j < rows; j++)
                {
                    sum = MathUnsafe<T>.Add(sum, MathGeneric<T>.Abs(matrix[j, i]));
                }
                array[i] = sum;
            }

            Comparer<T> comparer = Comparer<T>.Default;
            T max = array[0];
            for (int i = 0; i < columns; i++)
            {
                T reg = array[i];
                if (comparer.Compare(max, array[i]) < 0)
                {
                    max = reg;
                }
            }

            return max;
        }

        /// <summary>
        /// Gets m-norm.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>m-norm.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T MNorm<T>(this Matrix<T> matrix) where T : unmanaged
        {
            int rows = matrix.Rows;
            int columns = matrix.Columns;

            var array = new T[rows];

            for (int i = 0; i < rows; i++)
            {
                T sum = default;

                for (int j = 0; j < columns; j++)
                {
                    sum = MathUnsafe<T>.Add(sum, MathGeneric<T>.Abs(matrix[i, j]));
                }
                array[i] = sum;
            }

            Comparer<T> comparer = Comparer<T>.Default;
            T max = array[0];
            for (int i = 0; i < rows; i++)
            {
                T reg = array[i];
                if (comparer.Compare(max, array[i]) < 0)
                {
                    max = reg;
                }
            }

            return max;
        }

        /// <summary>
        /// The trace <c>Tr</c> of a square matrix A is defined to be the sum of elements on the main diagonal.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Traceless of matrix.</returns>
        public static T Traceless<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException(ExceptionArgument.MatrixIsNotSquare);
            }

            T sum = default;

            for (int i = 0; i < matrix.Rows; i++)
            {
                sum = MathUnsafe<T>.Add(sum, matrix[i, i]);
            }

            return sum;
        }

        public static T NormFrobenius<T>(this Matrix<T> matrix) where T : unmanaged
        {
            T result = default;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result = MathUnsafe<T>.Add(result,
                        MathUnsafe<T>.Mul(matrix[i, j], matrix[i, j]));
                }
            }
            return MathGeneric<T>.Sqrt(result);
        }
    }
}