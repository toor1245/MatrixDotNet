using System;
using System.Runtime.CompilerServices;
using MatrixDotNet.Exceptions;
using MathExtension = MatrixDotNet.Math.MathExtension;

namespace MatrixDotNet.Extensions.Conversion
{
    /// <summary>
    /// Represents converter which can change matrix.
    /// </summary>
    public static partial class MatrixConverter
    {
        /// <summary>
        /// Joins two matrix, matrix A rows must be equals matrix B rows.
        /// </summary>
        /// <param name="matrix1">The matrix A.</param>
        /// <param name="matrix2">The matrix B.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Joins two matrix</returns>
        /// <exception cref="MatrixDotNetException">
        /// Throws if matrix1.Rows != matrix2.Rows.
        /// </exception>
        public static Matrix<T> Concat<T>(this Matrix<T> matrix1,Matrix<T> matrix2) 
            where T : unmanaged
        {
            if(matrix1 is null || matrix2 is null)
                throw new NullReferenceException();
            
            if (matrix1.Rows != matrix2.Rows)
                throw new MatrixDotNetException("Rows must be equals");
            
            var res = new Matrix<T>(matrix1.Rows,matrix1.Columns + matrix2.Columns);
            for (var i = 0; i < matrix1.Rows; i++)
            for (int j = 0, k = 0; j < matrix1.Columns + matrix2.Columns; j++)
                if (j < matrix1.Columns)
                {
                    res[i, j] = matrix1[i, j];
                }
                else
                {
                    res[i, k + matrix1.Columns ] = matrix2[i, k];
                    k++;
                }

            return res;
        }
        
        /// <summary>
        /// Convert matrix to primitive matrix.
        /// </summary>
        /// <param name="matrix">the matrix A</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>primitive matrix</returns>
        public static T[,] ToPrimitive<T>(this Matrix<T> matrix) 
            where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            var matrix1 = new T[matrix.Rows,matrix.Columns];
            
            for (var i = 0; i < matrix.Rows; i++)
            for (var j = 0; j < matrix.Columns; j++) matrix1[i, j] = matrix[i, j];
            return matrix1;
        }
        
        /// <summary>
        /// Convert primitive matrix to <see cref="Matrix{T}"/>.
        /// </summary>
        /// <param name="matrix">primitive matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        public static Matrix<T> ToMatrix<T>(this T[,] matrix) 
            where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            var matrix1 = new Matrix<T>(matrix.GetLength(0),matrix.GetLength(1));
            
            for (var i = 0; i < matrix1.Rows; i++)
            for (var j = 0; j < matrix1.Columns; j++) matrix1[i, j] = matrix[i, j];
            return matrix1;
        }

        /// <summary>
        /// Reduces column of matrix by index.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="column">The index of matrix which reduce column.</param>
        /// <typeparam name="T">Unmanaged type.</typeparam>
        /// <returns>A new matrix without the chosen column.</returns>
        public static unsafe Matrix<T> ReduceColumn<T>(this Matrix<T> matrix, int column) where T : unmanaged
        {
            var newColumns = matrix.Columns - 1; 
            var temp = new Matrix<T>(matrix.Rows,newColumns);
            fixed (T* ptr2 = temp.GetMatrix())
            fixed (T* ptr3 = matrix.GetMatrix())
            {
                int m = temp.Columns;
                for (int i = 0; i < temp.Rows; i++)
                {
                    Unsafe.CopyBlock(ptr2 + i * m,ptr3 + i * matrix.Columns,(uint) (sizeof(T) * column));
                    int len = temp.Columns - column;
                    Unsafe.CopyBlock(ptr2 + i * m + column ,ptr3 + i * matrix.Columns + column + 1,(uint) (sizeof(T) * len));
                }
            }
            
            return temp;
        }

        /// <summary>
        /// Reduces row of matrix by index.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="row">index of matrix which reduce column.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>A new matrix without the chosen row.</returns>
        /// <exception cref="NullReferenceException">.</exception>
        public static unsafe Matrix<T> ReduceRow<T>(this Matrix<T> matrix, int row) where T : unmanaged
        {
            var newRows = matrix.Rows - 1; 
            var temp = new Matrix<T>(newRows,matrix.Columns);
            fixed (T* ptr2 = temp.GetMatrix())
            fixed (T* ptr3 = matrix.GetMatrix())
            {
                int m = temp.Columns;
                Array.Copy(matrix._Matrix, temp._Matrix, row * m);
                // finds difference len between whole matrix and length to index row.
                int diff = sizeof(T) * temp.Length - (sizeof(T) * row * m);
                Unsafe.CopyBlock(ptr2 + row * m,ptr3 + (row + 1) * m,(uint) diff);
            }
            
            return temp;
        }

        
        /// <summary>
        /// Add column of matrix by index.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="arr">the array.</param>
        /// <param name="column">column index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>A new matrix with new column.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static Matrix<T> AddColumn<T>(this Matrix<T> matrix, T[] arr, int column) where T : unmanaged
        {
            if (matrix.Rows != arr.Length)
            {
                string message = $"length {nameof(arr)}:{arr.Length} != {nameof(matrix.Rows)} of matrix:{matrix.Rows}";
                throw new MatrixDotNetException(message);
            }

            var m = matrix.Rows;
            var result = new Matrix<T>(m, matrix.Columns + 1);

            for (int i = 0; i < column; i++)
            {
                result[i, State.Column] = matrix[i, State.Column];
            }

            result[column, State.Column] = arr;

            for (int i = column + 1; i < result.Columns; i++)
            {
                result[i, State.Column] = matrix[i - 1, State.Column];
            }

            return result;
            
        }
        
        
        /// <summary>
        /// Returns new matrix with added row.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="array">the row for new matrix</param>
        /// <param name="row">index of row</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe Matrix<T> AddRow<T>(this Matrix<T> matrix,T[] array,int row)
            where T : unmanaged
        {
            if (matrix.Columns != array.Length)
            {
                var message =
                    $"length {nameof(array)}:{array.Length} != {nameof(matrix.Columns)} of matrix:{matrix.Columns}";
                throw new MatrixDotNetException(message);
            }
            var newRows = matrix.Rows + 1; 
            var temp = new Matrix<T>(newRows,matrix.Columns);
            fixed (T* ptr1 = array)
            fixed (T* ptr2 = temp.GetMatrix())
            fixed (T* ptr3 = matrix.GetMatrix())
            {
                int m = temp.Columns;
                int aLength = array.Length;
                Array.Copy(matrix._Matrix, temp._Matrix, row * m);
                Unsafe.CopyBlock(ptr2 + row * m,ptr1,(uint) (sizeof(T) * aLength));
                // finds difference len between whole matrix and length to index row.
                int diff = sizeof(T) * temp.Length - (sizeof(T) * row * m + sizeof(T) * aLength);
                Unsafe.CopyBlock(ptr2 + (row + 1) * m,ptr3 + row * m,(uint) diff);
            }
            
            return temp;
        }

        /// <summary>
        /// Changes this matrix to identity matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Identity matrix.</returns>
        /// <exception cref="MatrixDotNetException">throws exception if matrix is not square</exception>
        public static void ToIdentityMatrix<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            if(!matrix.IsSquare)
                throw new MatrixDotNetException($"matrix is not square!!!\nRows: {matrix.Rows}\nColumns: {matrix.Columns}");
            
            for (var i = 0; i < matrix.Rows; i++)
            for (var j = 0; j < matrix.Columns; j++)
                if (i == j)
                    matrix[i, j] = MathExtension.Increment<T>(default);
                else
                    matrix[i, j] = default;
        }

        /// <summary>
        /// Swap rows of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension1">the dimension 1</param>
        /// <param name="dimension2">the dimension 2</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <exception cref="MatrixDotNetException">throws exception if indexDimension1 equals indexDimension2 or matrix is null</exception>
        public static unsafe void SwapRows<T>(this Matrix<T> matrix,int dimension1,int dimension2) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            int m = matrix.Rows;
            int n = matrix.Columns;

            int length = matrix.Length;

            fixed (T* ptr1 = matrix.GetMatrix())
            {
                Span<T> span = new Span<T>(ptr1,length);

                int index = dimension1 * n + m;
                int i = dimension1 * n;
                int j = dimension2 * n;
                
                while (i < index)
                {
                    var tmp = span[i];
                    span[i] = span[j];
                    span[j] = tmp;
                    
                    i++; j++;
                }
            }
        }
        
        /// <summary>
        /// Swap rows of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="indexDimension1">the dimension 1</param>
        /// <param name="indexDimension2">the dimension 2</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <exception cref="MatrixDotNetException">throws exception if indexDimension1 equals indexDimension2 or matrix is null</exception>
        public static void SwapColumns<T>(this Matrix<T> matrix,int indexDimension1, int indexDimension2) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            var temp = matrix[indexDimension1, State.Column];
            matrix[indexDimension1, State.Column] = matrix[indexDimension2, State.Column];
            matrix[indexDimension2, State.Column] = temp;
        }

        
        /// <summary>
        /// Gets transport matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Transpose matrix</returns>
        public static Matrix<T> Transpose<T>(this Matrix<T> matrix) 
            where T : unmanaged
        {
            var transport = new Matrix<T>(matrix.Columns,matrix.Rows);
            for (var i = 0; i < transport.Rows; i++)
                for (var j = 0; j < transport.Columns; j++) 
                    transport[i, j] = matrix[j, i];

            return transport;
        }
        
        /// <summary>
        /// Splits matrix by 4 parts.
        /// </summary>
        /// <param name="a">the matrix which want splits.</param>
        /// <param name="a11">the matrix a11.</param>
        /// <param name="a12">the matrix a12.</param>
        /// <param name="a21">the matrix a21.</param>
        /// <param name="a22">the matrix a22.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <exception cref="MatrixDotNetException">
        ///  The matrix is not square.
        /// </exception>
        public static void SplitMatrix<T>(this Matrix<T> a, out Matrix<T> a11, out Matrix<T> a12, out Matrix<T> a21, out Matrix<T> a22) 
            where T : unmanaged
        {
            if (!a.IsSquare)
                throw new MatrixDotNetException("Matrix is not square");
            
            var n = a.Rows >> 1;
            
            a11 = new Matrix<T>(n,n);
            a12 = new Matrix<T>(n,n);
            a21 = new Matrix<T>(n,n);
            a22 = new Matrix<T>(n,n);

            
            for (var i = 0; i < n; i++)
            {
                CopyTo(a,i, 0, a11,i,0,n);
                CopyTo(a,i, n, a12,i,0,n);
                CopyTo(a,i + n, 0, a21,i,0,n);
                CopyTo(a,i + n, n, a22,i,0,n);
            }
        }
        
        /// <summary>
        /// Collects square matrix.
        /// </summary>
        /// <param name="a11">the matrix a11</param>
        /// <param name="a12">the matrix a12</param>
        /// <param name="a21">the matrix a21</param>
        /// <param name="a22">the matrix a22</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>Collect matrix.</returns>
        public static Matrix<T> CollectMatrix<T>(Matrix<T> a11, Matrix<T> a12, Matrix<T> a21, Matrix<T> a22)
            where T : unmanaged
        {
            int n = a11.Rows;
            int sl = n << 1;
            Matrix<T> a = new Matrix<T>(sl,sl);
            for (int i = 0; i < n; i++)
            {
                CopyTo(a11,i, 0, a,i,0,n);
                CopyTo(a12,i, 0, a,i,n,sl);
                CopyTo(a21,i, 0, a,i + n,0,n);
                CopyTo(a22,i, 0, a,i + n,n,sl);
            }

            return a;
        }
    }
}