using MatrixDotNet.Exceptions;
using System;

namespace MatrixDotNet.Extensions
{
    /// <summary>
    /// Represents Matrix extension.
    /// </summary>
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Gets row array of matrix by row index.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="index"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static T[] GetRow<T>(this Matrix<T> matrix, int index) where T : unmanaged
        {
            T[] array = new T[matrix.Columns];

            for (int j = 0; j < matrix.Columns; j++)
            {
                array[j] = matrix[index, j];
            }

            return array;
        }

        /// <summary>
        /// Gets column array of matrix by columns index.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="index">index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>column by index</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public static T[] GetColumn<T>(this Matrix<T> matrix, int index)
            where T : unmanaged
        {
            T[] array = new T[matrix.Rows];

            for (int j = 0; j < matrix.Rows; j++)
            {
                array[j] = matrix[j, index];
            }

            return array;
        }


        /// <summary>
        /// Gets diagonal of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>the diagonal of matrix.</returns>
        public static T[] GetDiagonal<T>(this Matrix<T> matrix) where T : unmanaged
        {
            int x = matrix.Rows;
            int y = matrix.Columns;
            int c = x & ((x - y) >> 31) | y & (~(x - y) >> 31);

            T[] array = new T[c];

            for (int i = 0; i < c; i++)
            {
                array[i] = matrix[i, i];
            }

            return array;
        }

        /// <summary>
        /// Sets diagonal value.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="array">the vector which we want to set diagonal of matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <exception cref="MatrixDotNetException">
        /// throws exception if <c>Length</c> of matrix not equal matrix <c>Min(Rows,Columns)</c>.
        /// </exception>
        public static void SetDiagonal<T>(this Matrix<T> matrix, T[] array) where T : unmanaged
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixNotSquareException();
            }

            for (int i = 0; i < matrix.Rows; i++)
            {
                matrix[i, i] = array[i];
            }
        }
    }
}