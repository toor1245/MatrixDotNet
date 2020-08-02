using System;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        public static T Sum<T>(this Matrix<T> matrix) where T: unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            T sum = default;
            for (int i = 0; i < matrix._Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix._Matrix.GetLength(1); j++)
                {
                    sum = MathExtension.Add(sum,matrix[i,j]);
                }
            }
            
            return sum;
        }

        public static T SumByRow<T>(this Matrix<T> matrix, int dimension) where T: unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            T sum = default;
            
            for (int i = 0; i < matrix._Matrix.GetLength(1); i++)
            {
                sum = MathExtension.Add(sum,matrix[dimension,i]);
            }

            return sum;
        }
        
        public static T SumByColumn<T>(this Matrix<T> matrix, int dimension) where T: unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            T sum = default;
            
            for (int i = 0; i < matrix._Matrix.GetLength(0); i++)
            {
                sum = MathExtension.Add(sum,matrix[i,dimension]);
            }

            return sum;
        }

        public static T[] SumByRows<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            var array = new T[matrix._Matrix.GetLength(0)];
            
            for (int i = 0; i < matrix._Matrix.GetLength(0); i++)
            {
                T sum = default;
                
                for (int j = 0; j < matrix._Matrix.GetLength(1); j++)
                {
                    sum = MathExtension.Add(sum, matrix[i, j]); // sum = sum + matrix[i,j];
                }

                array[i] = sum;
            }

            return array;
        }
        
        public static T[] SumByColumns<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            var array = new T[matrix._Matrix.GetLength(1)];
            
            for (int i = 0; i < matrix._Matrix.GetLength(1); i++)
            {
                T sum = default;
                
                for (int j = 0; j < matrix._Matrix.GetLength(0); j++)
                {
                    sum = MathExtension.Add(sum, matrix[j, i]);
                }

                array[i] = sum;
            }

            return array;
        }
    }
}