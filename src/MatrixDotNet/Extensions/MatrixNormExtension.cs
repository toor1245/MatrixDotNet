using System;
using System.Collections.Generic;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        public static T LNorm<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();

            int rows = matrix.Rows;
            int columns = matrix.Columns;
            
            var array = new T[columns];
            
            for (int i = 0; i < columns; i++)
            {
                T sum = default;

                for (int j = 0; j < rows; j++)
                {
                    sum = MathExtension.Add(sum,MathExtension.Abs(matrix[j, i]));
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
        public static T MNorm<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();

            int rows = matrix.Rows;
            int columns = matrix.Columns;
            
            var array = new T[rows];
            
            for (int i = 0; i < rows; i++)
            {
                T sum = default;

                for (int j = 0; j < columns; j++)
                {
                    sum = MathExtension.Add(sum,MathExtension.Abs(matrix[i,j]));
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
            if (matrix is null)
                throw new NullReferenceException();

            if (!matrix.IsSquare)
                throw new MatrixDotNetException("Matrix is not square");

            T sum = default;
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                sum = MathExtension.Add(sum,matrix[i,i]);
            }

            return sum;
        }
        
        
        
    }
}