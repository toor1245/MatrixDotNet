using System;
using System.Collections;

namespace MatrixDotNet.Extensions.BitMatrix
{
    public static partial class BitMatrixExtension
    {
        public static T Min<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
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
            if(matrix is null)
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
            if(matrix is null)
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
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    var prefetch = matrix[i, j];
                    min = min & ((min - prefetch) >> 63) | prefetch & (~(min - prefetch) >> 63);
                }
            }
            return min;
        }
        
        public static int BitMin(this Matrix<int> matrix)
        {
            int min = matrix[0, 0];
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    var prefetch = matrix[i, j];
                    min = min & ((min - prefetch) >> 31) | prefetch & (~(min - prefetch) >> 31);
                }
            }
            return min;
        }
        
        public static int BitMin(this Matrix<short> matrix)
        {
            int min = matrix[0, 0];
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    var prefetch = matrix[i, j];
                    min = min & ((min - prefetch) >> 15) | prefetch & (~(min - prefetch) >> 15);
                }
            }
            return min;
        }
        
        public static int BitMin(this Matrix<byte> matrix)
        {
            int min = matrix[0, 0];
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    var prefetch = matrix[i, j];
                    min = min & ((min - prefetch) >> 7) | prefetch & (~(min - prefetch) >> 7);
                }
            }
            return min;
        }

        public static long BitMinByRow(this Matrix<long> matrix,int dimension)
        {
            long min = matrix[dimension, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                var prefetch = matrix[dimension, i];
                min = min & ((min - prefetch) >> 63) | prefetch & (~(min - prefetch) >> 63);
            }
            return min;
        }
        
        public static int BitMinByRow(this Matrix<int> matrix,int dimension)
        {
            int min = matrix[dimension, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                var prefetch = matrix[dimension, i];
                min = min & ((min - prefetch) >> 31) | prefetch & (~(min - prefetch) >> 31);
            }
            return min;
        }
        
        public static int BitMinByRow(this Matrix<short> matrix,int dimension)
        {
            int min = matrix[dimension, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                var prefetch = matrix[dimension, i];
                min = min & ((min - prefetch) >> 15) | prefetch & (~(min - prefetch) >> 15);
            }
            return min;
        }
        
        public static int BitMinByRow(this Matrix<byte> matrix,int dimension)
        {
            int min = matrix[dimension, 0];
            for (int i = 0; i < matrix.Columns; i++)
            {
                var prefetch = matrix[dimension, i];
                min = min & ((min - prefetch) >> 7) | prefetch & (~(min - prefetch) >> 7);
            }
            return min;
        }

        public static long BitMinByColumn(this Matrix<long> matrix, int dimension)
        {
            long min = matrix[0,dimension];
            for (int i = 0; i < matrix.Rows; i++)
            {
                var prefetch = matrix[i, dimension];
                min = min & ((min - prefetch) >> 63) | prefetch & (~(min - prefetch) >> 63);
            }
            return min;
        }
        
        public static int BitMinByColumn(this Matrix<int> matrix, int dimension)
        {
            int min = matrix[0,dimension];
            for (int i = 0; i < matrix.Rows; i++)
            {
                var prefetch = matrix[i, dimension];
                min = min & ((min - prefetch) >> 31) | prefetch & (~(min - prefetch) >> 31);
            }
            return min;
        }
        
        public static int BitMinByColumn(this Matrix<short> matrix, int dimension)
        {
            int min = matrix[0,dimension];
            for (int i = 0; i < matrix.Rows; i++)
            {
                var prefetch = matrix[i, dimension];
                min = min & ((min - prefetch) >> 15) | prefetch & (~(min - prefetch) >> 15);
            }
            return min;
        }
        
        public static int BitMinByColumn(this Matrix<byte> matrix, int dimension)
        {
            int min = matrix[0,dimension];
            for (int i = 0; i < matrix.Rows; i++)
            {
                var prefetch = matrix[i, dimension];
                min = min & ((min - prefetch) >> 31) | prefetch & (~(min - prefetch) >> 31);
            }
            return min;
        }
    }
}