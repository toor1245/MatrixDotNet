using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Determinant
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
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            double[,] temp = matrix.ToPrimitive() as double[,];
            
            if(temp is null)
                throw new NullReferenceException();
            
            if(temp.Length == 4)
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
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            Matrix<T> temp = matrix.ToPrimitive();
            
            if(temp.Length == 4)
            {
                return  MathExtension.Sub(
                    MathExtension.Multiply(temp[0, 0],temp[1, 1]),
                    MathExtension.Multiply(temp[0, 1],temp[1, 0]));
            }

            T result = default;
            T sign = MathExtension.Increment<T>(default);

            for (int i = 0; i < temp.Columns; i++)
            {
                Matrix<T> minr = matrix.GetMinor(i);
                result = MathExtension.Add(result,
                    MathExtension.Multiply(MathExtension.Multiply(sign, temp[0, i]), GetDeterminant<T>(minr)));
                sign = MathExtension.Sub(sign,MathExtension.Sub(sign,sign));
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
        public static int GetDeterminant(this Matrix<int> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            var temp = matrix.ToPrimitive();
            
            if(temp == null)
                throw new NullReferenceException();
            
            if(temp.Length == 4)
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
        public static long GetDeterminant(this Matrix<long> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            var temp = matrix.ToPrimitive();
            
            if(temp == null)
                throw new NullReferenceException();
            
            if(temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }
            
            long sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<long> minor =  matrix.GetMinor(i);
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
        public static float GetDeterminant(this Matrix<float> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            var temp = matrix.ToPrimitive();
            
            if(temp == null)
                throw new NullReferenceException();
            
            if(temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }
            
            float sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<float> minor =  matrix.GetMinor(i);
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
        public static double GetDeterminant(this Matrix<double> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            var temp = matrix.ToPrimitive();
            
            if(temp == null)
                throw new NullReferenceException();
            
            
            if(temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }
            
            double sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<double> minor =  matrix.GetMinor(i);
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
        public static decimal GetDeterminant(this Matrix<decimal> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            var temp = matrix.ToPrimitive();
            
            if(temp == null)
                throw new NullReferenceException();
            
            if(temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }
            
            decimal sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<decimal> minor =  matrix.GetMinor(i);
                result += sign * temp[0, i] * minor.GetDeterminant();
                sign = -sign;
            }
            return result;
        }
    }
}