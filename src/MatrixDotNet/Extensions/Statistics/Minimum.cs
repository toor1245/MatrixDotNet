using System;
using System.Collections;
using System.Numerics;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Statistics
{
    /// <summary>
    /// Represents the functional of bit operations with a matrix
    /// </summary>
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Gets minimum value of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>minimum of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T Min<T>(this Matrix<T> matrix) where T : unmanaged
        {
            int size = System.Numerics.Vector<T>.Count;
            System.Numerics.Vector<T> vmin = new System.Numerics.Vector<T>(matrix[0,0]);
            int i = 0;
            for (; i < matrix.Length - size; i += size )
            {
                var va = new System.Numerics.Vector<T>(matrix.GetArray(),i);
                var vless = Vector.LessThan(va,vmin);
                vmin = Vector.ConditionalSelect(vless, va, vmin);
            }

            T min = vmin[0];
            Comparer cmp = Comparer.Default;
            for (int j = 0; j < size; j++)
            {
                if (cmp.Compare(min,vmin[j]) > 0)
                {
                    min = vmin[j];
                }
            }

            for (; i < matrix.Length; i++)
            {
                if (cmp.Compare(min,matrix._Matrix[i]) > 0)
                {
                    min = matrix._Matrix[i];
                }
            }

            return min;
        }

        /// <summary>
        /// Gets minimum value of matrix by row index.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">the index row.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Minimum element by index row.</returns>
        /// <exception cref="NullReferenceException"></exception>
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
        
        /// <summary>
        /// Gets minimum value of matrix by column index.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="dimension">the index column.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Minimum value by column index.</returns>
        /// <exception cref="NullReferenceException"></exception>
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
        
        /// <summary>
        /// Gets minimum value by each column.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>Array which contains maximum values of each column.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T[] MinColumns<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();

            T[] result = new T[matrix.Columns];
            Comparer comparer = Comparer.Default;
            for (int i = 0; i < matrix.Columns; i++)
            {
                T max = matrix[0,i];
                for (int j = 0; j < matrix.Rows; j++)
                {
                    if(comparer.Compare(matrix[j,i],max) < 0)
                    {
                        max = matrix[j,i];
                    }
                }

                result[i] = max;

            }
            return result;
        }
        
        
        /// <summary>
        /// Gets minimum value by each row.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>The matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T[] MinRows<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();

            T[] result = new T[matrix.Rows];
            Comparer comparer = Comparer.Default;
            for (int i = 0; i < matrix.Rows; i++)
            {
                T max = matrix[i,0];
                for (int j = 0; j < matrix.Columns; j++)
                {
                    if(comparer.Compare(matrix[i,j],max) < 0)
                    {
                        max = matrix[i,j];
                    }
                }

                result[i] = max;

            }
            return result;
        }
        
        /// <summary>
        /// Gets minimum of matrix with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>Minimum of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
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
        
        /// <summary>
        /// Gets minimum of matrix with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>Minimum of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
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
        
        /// <summary>
        /// Gets minimum of matrix with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>Minimum of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
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
        
        /// <summary>
        /// Gets minimum of matrix with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>Minimum of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
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

        /// <summary>
        /// Gets minimum value by row index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <returns>minimum value by row.</returns>
        /// <exception cref="NullReferenceException"></exception>
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
        
        /// <summary>
        /// Gets maximum value by row index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <returns>maximum value by row.</returns>
        /// <exception cref="NullReferenceException"></exception>
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
        
        /// <summary>
        /// Gets maximum value by row index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <returns>maximum value by row.</returns>
        /// <exception cref="NullReferenceException"></exception>
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
        
        /// <summary>
        /// Gets maximum value by row index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <returns>maximum value by row.</returns>
        /// <exception cref="NullReferenceException"></exception>
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

        /// <summary>
        /// Gets minimum value by column index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">column index.</param>
        /// <returns>minimum value by column.</returns>
        /// <exception cref="NullReferenceException"></exception>
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
        
        /// <summary>
        /// Gets minimum value by column index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">column index.</param>
        /// <returns>minimum value by column.</returns>
        /// <exception cref="NullReferenceException"></exception>
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
        
        /// <summary>
        /// Gets minimum value by column index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">column index.</param>
        /// <returns>minimum value by column.</returns>
        /// <exception cref="NullReferenceException"></exception>
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
        
        /// <summary>
        /// Gets minimum value by column index with happen bitwise operations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">column index.</param>
        /// <returns>minimum value by column.</returns>
        /// <exception cref="NullReferenceException"></exception>
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