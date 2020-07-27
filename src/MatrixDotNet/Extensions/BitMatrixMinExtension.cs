using System;
using System.Collections;

namespace MatrixDotNet.Extensions
{
    public static partial class BitMatrixExtension
    {

        public static T Min<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix == null)
                throw new NullReferenceException();

            T min = matrix[0,0];
            Comparer comparer = Comparer.Default;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    if(comparer.Compare(matrix[i,j],min) < 0)
                    {
                        min = matrix[i, j];
                    }
                }
            }

            return min;
        }

        public static T MinByRow<T>(this Matrix<T> matrix,int dimension) where T : unmanaged
        {
            if(matrix == null)
                throw new NullReferenceException();

            T min = matrix[dimension,0];
            Comparer comparer = Comparer.Default;

            for (int j = 0; j < matrix.Columns; j++)
            {
                if(comparer.Compare(matrix[dimension,j],min) < 0)
                {
                    min = matrix[dimension, j];
                }
            }

            return min;
        }
        
        public static T MinByColumn<T>(this Matrix<T> matrix,int dimension) where T : unmanaged
        {
            if(matrix == null)
                throw new NullReferenceException();

            T min = matrix[0,dimension];
            Comparer comparer = Comparer.Default;

            for (int j = 0; j < matrix.Rows; j++)
            {
                if(comparer.Compare(matrix[dimension,j],min) < 0)
                {
                    min = matrix[j,dimension];
                }
            }

            return min;
        }
        
        public static long BitMin(this Matrix<long> matrix)
        {
            long min = matrix[0, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[j,i] >> 63) | matrix[j,i] & (~(min - matrix[j,i]) >> 63);
                }
            }
            return min;
        }
        
        public static int BitMin(this Matrix<int> matrix)
        {
            int min = matrix[0, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[j,i] >> 31) | matrix[j,i] & (~(min - matrix[j,i]) >> 31);
                }
            }
            return min;
        }
        
        public static int BitMin(this Matrix<short> matrix)
        {
            int min = matrix[0, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[j, i] >> 15) | matrix[j, i] & (~(min - matrix[j, i]) >> 15);
                }
            }
            return min;
        }
        
        public static int BitMin(this Matrix<byte> matrix)
        {
            int min = matrix[0, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[j, i] >> 7) | matrix[j, i] & (~(min - matrix[j, i]) >> 7);
                }
            }
            return min;
        }

        public static long BitMinByRow(this Matrix<long> matrix,int dimension)
        {
            long min = matrix[dimension, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[dimension, i] >> 63) | matrix[j, i] & (~(min - matrix[dimension, i]) >> 63);
                }
            }
            return min;
        }
        
        public static int BitMinByRow(this Matrix<int> matrix,int dimension)
        {
            int min = matrix[dimension, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[dimension, i] >> 31) | matrix[dimension, i] & (~(min - matrix[dimension, i]) >> 31);
                }
            }
            return min;
        }
        
        public static int BitMinByRow(this Matrix<short> matrix,int dimension)
        {
            int min = matrix[dimension, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[dimension, i] >> 15) | matrix[dimension, i] & (~(min - matrix[dimension, i]) >> 15);
                }
            }
            return min;
        }
        
        public static int BitMinByRow(this Matrix<byte> matrix,int dimension)
        {
            int min = matrix[dimension, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[dimension, i] >> 7) | matrix[dimension, i] & (~(min - matrix[dimension, i]) >> 7);
                }
            }
            return min;
        }

        public static long BitMinByColumn(this Matrix<long> matrix, int dimension)
        {
            long min = matrix[0,dimension];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[i,dimension] >> 63) | matrix[i,dimension] & (~(min - matrix[i,dimension]) >> 63);
                }
            }
            return min;
        }
        
        public static int BitMinByColumn(this Matrix<int> matrix, int dimension)
        {
            int min = matrix[0,dimension];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[i,dimension] >> 31) | matrix[i,dimension] & (~(min - matrix[i,dimension]) >> 31);
                }
            }
            return min;
        }
        
        public static int BitMinByColumn(this Matrix<short> matrix, int dimension)
        {
            int min = matrix[0,dimension];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[i,dimension] >> 15) | matrix[i,dimension] & (~(min - matrix[i,dimension]) >> 15);
                }
            }
            return min;
        }
        
        public static int BitMinByColumn(this Matrix<byte> matrix, int dimension)
        {
            int min = matrix[0,dimension];
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    min = min & (min - matrix[i,dimension] >> 7) | matrix[i,dimension] & (~(min - matrix[i,dimension]) >> 7);
                }
            }
            return min;
        }
    }
}