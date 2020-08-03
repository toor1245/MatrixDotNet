using System;
using System.Collections;

namespace MatrixDotNet.Extensions
{
    public static partial class BitMatrixExtension
    {
        public static T Max<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();

            T max = matrix[0,0];
            Comparer comparer = Comparer.Default;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    if(comparer.Compare(matrix[i,j],max) > 0)
                    {
                        max = matrix[i, j];
                    }
                }
            }
            return max;
        }
        
        public static T MaxByRow<T>(this Matrix<T> matrix,int dimension) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();

            T max = matrix[dimension,0];
            Comparer comparer = Comparer.Default;
            for (int j = 0; j < matrix.Columns; j++)
            {
                if(comparer.Compare(matrix[dimension,j],max) > 0)
                {
                    max = matrix[dimension, j];
                }
            }
            return max;
        }
        
        public static T MaxByColumn<T>(this Matrix<T> matrix,int dimension) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();

            T max = matrix[0,dimension];
            Comparer comparer = Comparer.Default;
            for (int j = 0; j < matrix.Rows; j++)
            {
                if(comparer.Compare(matrix[j,dimension],max) > 0)
                {
                    max = matrix[j, dimension];
                }
            }
            
            return max;
        }
        
        public static int BitMax(this Matrix<int> matrix)
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            int max = matrix[0, 0];
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    max = matrix[i, j] & ((max - matrix[i, j]) >> 31) | max & (~(max - matrix[i, j]) >> 31);
                }
            }
            return max;
        }
        
        public static int BitMax(this Matrix<byte> matrix)
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            int max = matrix[0, 0];
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    max = matrix[i, j] & ((max - matrix[i, j]) >> 31) | max & (~(max - matrix[i, j]) >> 31);
                }
            }
            return max;
        }
        
        public static long BitMax(this Matrix<long> matrix)
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            long max = matrix[0, 0];
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    max = matrix[i, j] & ((max - matrix[i, j]) >> 63) | max & (~(max - matrix[i, j]) >> 63);
                }
            }
            return max;
        }
        
        public static int BitMax(this Matrix<short> matrix)
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            int max = matrix[0, 0];
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    max = matrix[i, j] & ((max - matrix[i, j]) >> 15) | max & (~(max - matrix[i, j]) >> 15);
                }
            }
            return max;
        }
        
        public static int BitMaxByRow(this Matrix<int> matrix,int dimension)
        {
            if(matrix is null)
                throw new NullReferenceException();

            int max = matrix[dimension, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                max = matrix[dimension,i] & ((max - matrix[dimension,i]) >> 31) | max & (~(max - matrix[dimension,i]) >> 31);
            }
            return max;
        }
        
        public static int BitMaxByRow(this Matrix<byte> matrix,int dimension)
        {
            if(matrix is null)
                throw new NullReferenceException();

            int max = matrix[dimension, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                max = matrix[dimension,i] & ((max - matrix[dimension,i]) >> 7) | max & (~(max - matrix[dimension,i]) >> 7);
            }
            return max;
        }
        
        public static int BitMaxByRow(this Matrix<short> matrix,int dimension)
        {
            if(matrix is null)
                throw new NullReferenceException();

            int max = matrix[dimension, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                max = matrix[dimension,i] & ((max - matrix[dimension,i]) >> 15) | max & (~(max - matrix[dimension,i]) >> 15);
            }
            return max;
        }
        
        public static long BitMaxByRow(this Matrix<long> matrix,int dimension)
        {
            if(matrix is null)
                throw new NullReferenceException();

            long max = matrix[dimension, 0];
            for (int i = 0; i < matrix.Rows; i++)
            {
                max = matrix[dimension,i] & ((max - matrix[dimension,i]) >> 63) | max & (~(max - matrix[dimension,i]) >> 63);
            }
            return max;
        }
        
        public static int BitMaxByColumn(this Matrix<byte> matrix,int dimension)
        {
            if(matrix is null)
                throw new NullReferenceException();

            int max = matrix[0,dimension];
            for (int i = 0; i < matrix.Rows; i++)
            {
                max = matrix[i,dimension] & ((max - matrix[i,dimension]) >> 31) | max & (~(max - matrix[i,dimension]) >> 31);
            }
            return max;
        }
        
        public static int BitMaxByColumn(this Matrix<short> matrix,int dimension)
        {
            if(matrix is null)
                throw new NullReferenceException();

            int max = matrix[0,dimension];
            for (int i = 0; i < matrix.Rows; i++)
            {
                max = matrix[i,dimension] & ((max - matrix[i,dimension]) >> 15) | max & (~(max - matrix[i,dimension]) >> 15);
            }
            return max;
        }
        
        public static int BitMaxByColumn(this Matrix<int> matrix,int dimension)
        {
            if(matrix is null)
                throw new NullReferenceException();

            int max = matrix[0,dimension];
            for (int i = 0; i < matrix.Rows; i++)
            {
                max = matrix[i,dimension] & ((max - matrix[i,dimension]) >> 31) | max & (~(max - matrix[i,dimension]) >> 31);
            }
            return max;
        }
        
        public static long BitMaxByColumn(this Matrix<long> matrix,int dimension)
        {
            if(matrix is null)
                throw new NullReferenceException();

            long max = matrix[0,dimension];
            for (int i = 0; i < matrix.Rows; i++)
            {
                max = matrix[i,dimension] & ((max - matrix[i,dimension]) >> 63) | max & (~(max - matrix[i,dimension]) >> 63);
            }
            return max;
        }
    }
}