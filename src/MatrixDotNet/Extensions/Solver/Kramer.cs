﻿using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Determinants;
using MatrixDotNet.Math;
using MatrixDotNet.Vectorization;

namespace MatrixDotNet.Extensions.Solver
{
    public static partial class Solve
    {
        /// <summary>
        ///     Gets determinant matrix by Kramer algorithm.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="arr">array.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Gets array x.</returns>
        /// <exception cref="MatrixDotNetException">
        ///     array length not equal matrix rows.
        /// </exception>
        public static Vector<T> KramerSolve<T>(this Matrix<T> matrix, T[] arr) where T : unmanaged
        {
            if (matrix.Rows != arr.Length)
                throw new SizeNotEqualException(ExceptionArgument.RowSizeOfMatrixIsNotEqualSizeOfVector);

            var det = matrix.GetDeterminant();
            var vr = new Vector<T>(matrix.Columns);

            if (!(matrix.Clone() is Matrix<T> temp)) throw new NullReferenceException();

            for (var i = 0; i < matrix.Columns; i++)
            {
                for (var j = 0; j < matrix.Rows; j++) temp[j, i] = arr[j];
                vr[i] = MathUnsafe<T>.Div(temp.GetDeterminant(), det);
            }

            return vr;
        }

        /// <summary>
        ///     Gets determinant matrix by Kramer algorithm.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="arr">array.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Gets array x.</returns>
        /// <exception cref="MatrixDotNetException">
        ///     array length not equal matrix rows.
        /// </exception>
        public static Vector<T> KramerSolve<T>(this Matrix<T> matrix, Vector<T> arr) where T : unmanaged
        {
            if (matrix.Rows != arr.Length)
                throw new SizeNotEqualException(ExceptionArgument.RowSizeOfMatrixIsNotEqualSizeOfVector);

            var det = matrix.GetDeterminant();
            var vr = new Vector<T>(matrix.Columns);

            if (!(matrix.Clone() is Matrix<T> temp)) throw new NullReferenceException();

            for (var i = 0; i < matrix.Columns; i++)
            {
                for (var j = 0; j < matrix.Rows; j++) temp[j, i] = arr[j];

                vr[i] = MathUnsafe<T>.Div(temp.GetDeterminant(), det);
            }

            return vr;
        }
    }
}