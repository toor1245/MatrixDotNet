using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Math;
using System;

namespace MatrixDotNet.Extensions.Determinants
{
    public static partial class Determinant
    {

        /// <summary>
        /// Gets determinant of matrix.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>double.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static double GetDoubleDeterminant<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square", nameof(matrix));
            }

            if (!(matrix.ToPrimitive() is double[,] temp))
                throw new NullReferenceException();

            if (temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }

            double sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<T> minr = matrix.GetMinor(i);
                result += sign * temp[0, i] * GetDoubleDeterminant(minr);
                sign = -sign;
            }

            return result;
        }

        /// <summary>
        /// Gets determinant of matrix.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>double.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static T GetDeterminant<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square", nameof(matrix));
            }

            Matrix<T> temp = matrix.ToPrimitive();

            if (temp.Length == 4)
            {
                return MathUnsafe<T>.Sub(
                    MathUnsafe<T>.Mul(temp[0, 0], temp[1, 1]),
                    MathUnsafe<T>.Mul(temp[0, 1], temp[1, 0]));
            }

            T result = default;
            T sign = MathGeneric<T>.Increment(default);

            for (int i = 0; i < temp.Columns; i++)
            {
                Matrix<T> minr = matrix.GetMinor(i);
                result = MathUnsafe<T>.Add(result,
                    MathUnsafe<T>.Mul(MathUnsafe<T>.Mul(sign, temp[0, i]), GetDeterminant<T>(minr)));
                sign = MathUnsafe<T>.Sub(sign, MathUnsafe<T>.Sub(sign, sign));
            }

            return result;
        }


        /// <summary>
        /// Gets determinant of matrix.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>double.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static int GetMinorDeterminant(this Matrix<int> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square", nameof(matrix));
            }

            var temp = matrix.ToPrimitive();

            if (temp == null)
                throw new NullReferenceException();

            if (temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }

            int sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<int> minor = matrix.GetMinor(i);
                result += sign * temp[0, i] * minor.GetDeterminant();
                sign = -sign;
            }

            return result;
        }

        /// <summary>
        /// Gets determinant of matrix.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>double.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static long GetMinorDeterminant(this Matrix<long> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square", nameof(matrix));
            }

            var temp = matrix.ToPrimitive();

            if (temp == null)
                throw new NullReferenceException();

            if (temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }

            long sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<long> minor = matrix.GetMinor(i);
                result += sign * temp[0, i] * minor.GetDeterminant();
                sign = -sign;
            }

            return result;
        }

        /// <summary>
        /// Gets determinant of matrix.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>double.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static float GetMinorDeterminant(this Matrix<float> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square", nameof(matrix));
            }

            var temp = matrix.ToPrimitive();

            if (temp == null)
                throw new NullReferenceException();

            if (temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }

            float sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<float> minor = matrix.GetMinor(i);
                result += sign * temp[0, i] * minor.GetDeterminant();
                sign = -sign;
            }

            return result;
        }

        /// <summary>
        /// Gets determinant of matrix.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>double.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static double GetMinorDeterminant(this Matrix<double> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square", nameof(matrix));
            }

            var temp = matrix.ToPrimitive();

            if (temp == null)
                throw new NullReferenceException();


            if (temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }

            double sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<double> minor = matrix.GetMinor(i);
                result += sign * temp[0, i] * minor.GetMinorDeterminant();
                sign = -sign;
            }
            return result;
        }

        /// <summary>
        /// Gets determinant of matrix.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>double.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static decimal GetMinorDeterminant(this Matrix<decimal> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square", nameof(matrix));
            }

            var temp = matrix.ToPrimitive();

            if (temp == null)
                throw new NullReferenceException();

            if (temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }

            decimal sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<decimal> minor = matrix.GetMinor(i);
                result += sign * temp[0, i] * minor.GetDeterminant();
                sign = -sign;
            }
            return result;
        }

        /// <summary>
        /// Gets determinant of corner Minor`s matrix
        /// </summary>
        /// <param name="matrix">matrix</param>
        /// <param name="row">index</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>determinant of corner Minor</returns>
        public static T GetCornerMinorDeterminant<T>(this Matrix<T> matrix, int row)
            where T : unmanaged
        {
            return matrix.GetCornerMinor(row).GetLowerUpperDeterminant();
        }
    }
}