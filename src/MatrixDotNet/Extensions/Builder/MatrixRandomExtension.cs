using System;
using MatrixDotNet.Exceptions;
using MathExtension = MatrixDotNet.Math.MathExtension;

namespace MatrixDotNet.Extensions.Builder
{
    public static partial class BuildMatrix
    {
        /// <summary>
        /// Builds matrix by expression;
        /// </summary>
        /// <param name="row">row length of matrix.</param>
        /// <param name="column">column length of matrix.</param>
        /// <param name="expression">expression.</param>
        /// <param name="arg1">argument1.</param>
        /// <param name="arg2">argument2.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Creates Matrix by formula.</returns>
        /// <exception cref="MatrixDotNetException">
        /// throws exception if arg1 or arg2 length not equal Max(row,column).
        /// </exception>
        public static Matrix<T> Build<T>(int row,int column,Func<T,T,T> expression,T[] arg1,T[] arg2) where T : unmanaged
        {
            int max = column & ((row - column) >> 31) | row & (~(row - column) >> 31);
            if(arg1.Length != max || arg2.Length != max)
                throw new MatrixDotNetException($"array length error:\n arr1: {arg1.Length}\n arr2: {arg2.Length}\n  not equal length dimension {max} of matrix");
            
            Matrix<T> matrix = new Matrix<T>(row,column);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = expression(arg1[i],arg2[j]);
                }
            }

            return matrix;
        }
        
        
        /// <summary>
        /// Builds matrix by expression;
        /// </summary>
        /// <param name="row">row length of matrix.</param>
        /// <param name="column">column length of matrix.</param>
        /// <param name="expression">expression.</param>
        /// <param name="arg1">argument1.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Creates Matrix by formula.</returns>
        /// <exception cref="MatrixDotNetException">
        /// throws exception if arg1 length not equal Max(row,column).
        /// </exception>
        public static Matrix<T> Build<T>(int row,int column,Func<T,T> expression,T[] arg1) where T : unmanaged
        {
            int max = column & ((row - column) >> 31) | row & (~(row - column) >> 31);
            if(arg1.Length != max)
                throw new MatrixDotNetException($"array length error:\n arr1: {arg1.Length}\n not equal length dimension {max} of matrix");
            
            Matrix<T> matrix = new Matrix<T>(row,column);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = expression(arg1[j]);
                }
            }
            
            return matrix;
        }
        
        /// <summary>
        /// Builds matrix by expression;
        /// </summary>
        /// <param name="row">row length of matrix.</param>
        /// <param name="column">column length of matrix.</param>
        /// <param name="expression">expression.</param>
        /// <param name="arg1">argument1.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Creates Matrix by formula.</returns>
        /// <exception cref="MatrixDotNetException">
        /// throws exception if arg1 length not equal Max(row,column).
        /// </exception>
        public static Matrix<T> Build<T>(int row,int column,Func<T,T,T> expression,T[] arg1) where T : unmanaged
        {
            int max = column & ((row - column) >> 31) | row & (~(row - column) >> 31);
            if(arg1.Length != max)
                throw new MatrixDotNetException($"array length error:\n arr1: {arg1.Length}\n not equal length dimension {max} of matrix");
            
            Matrix<T> matrix = new Matrix<T>(row,column);
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = expression(arg1[j],arg1[j]);
                }
            }
            
            return matrix;
        }

        [Obsolete("bool shit = true;", true)]
        public static Matrix<T> Random<T>(int row, int column, int startRandom, int endRandom)
            where T : unmanaged
        {
            return new Matrix<T>(row, column);
        }
        
        public static Matrix<int> RandomInt(int row, int column, int startRandom = int.MinValue, int endRandom = int.MaxValue)
        {
            Matrix<int> matrix = new Matrix<int>(row,column);
            Random random = new Random();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = random.Next(startRandom,endRandom);
                }
            }

            return matrix;
        }

        public static Matrix<double> RandomDouble(int row, int column, int startRandom = int.MinValue, int endRandom = int.MaxValue)
        {
            Matrix<double> matrix = new Matrix<double>(row,column);
            Random random = new Random();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = random.Next(startRandom,endRandom);
                }
            }

            return matrix;
        }
    }
}