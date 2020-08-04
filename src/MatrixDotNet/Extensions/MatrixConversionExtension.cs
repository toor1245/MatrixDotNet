using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Joins two matrix, matrix A rows must be equals matrix B rows.
        /// </summary>
        /// <param name="matrix1">The matrix A.</param>
        /// <param name="matrix2">The matrix B.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Join of wto matrix</returns>
        /// <exception cref="MatrixDotNetException">
        /// Throws if matrix1.Rows != matrix2.Rows.
        /// </exception>
        public static Matrix<T> Concat<T>(this Matrix<T> matrix1,Matrix<T> matrix2) 
            where T : unmanaged
        {
            if (matrix1.Rows != matrix2.Rows)
                throw new MatrixDotNetException("Rows must be equals");
            
            Matrix<T> res = new Matrix<T>(matrix1.Rows,matrix1.Columns + matrix2.Columns);
            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0, k = 0; j < matrix1.Columns + matrix2.Columns; j++)
                {
                    if (j < matrix1.Columns)
                    {
                        res[i, j] = matrix1[i, j];
                    }
                    else
                    {
                        res[i, k + matrix1.Columns ] = matrix2[i, k];
                        k++;
                    }
                }
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
            T[,] matrix1 = new T[matrix.Rows,matrix.Columns];
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    matrix1[i, j] = matrix[i, j];
                }
            }
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
            Matrix<T> matrix1 = new Matrix<T>(matrix.GetLength(0),matrix.GetLength(1));
            
            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix1.Columns; j++)
                {
                    matrix1[i, j] = matrix[i, j];
                }
            }
            return matrix1;
        }

        /// <summary>
        /// Reduces column of matrix by index.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="column">The index of matrix which reduce column.</param>
        /// <typeparam name="T">Unmanaged type.</typeparam>
        /// <returns>A new matrix without the chosen column.</returns>
        public static Matrix<T> ReduceColumn<T>(this Matrix<T> matrix,int column) where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            int newColumn = matrix.Columns - 1; 
            Matrix<T> temp = new Matrix<T>(matrix.Rows,newColumn);
            
            if (column == 0)
            {
                for (int i = 1, k = 0; k < newColumn; i++,k++)
                {
                    CopyTo(State.Column,matrix,i,0,temp,k,0,temp.Rows);
                }
            }
            else if (column == matrix.Columns - 1)
            {
                for (int i = 0; i < newColumn; i++)
                {
                    CopyTo(State.Column,matrix,i,0,temp,i,0,matrix.Rows);
                }
            }
            else
            {
                for (int i = 0; i < newColumn; i++)
                {
                    if (i < column)
                    {
                        CopyTo(State.Column,matrix,i,0,temp,i,0,matrix.Rows);
                    }
                    else if (i >= column)
                    {
                        CopyTo(State.Column,matrix,i + 1,0,temp,i,0,matrix.Rows);
                    }

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
        public static Matrix<T> ReduceRow<T>(this Matrix<T> matrix, int row) where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            int newRow = matrix.Rows - 1;
            Matrix<T> temp = new Matrix<T>(newRow,matrix.Columns);
            
            if (row == 0)
            {
                for (int i = 1, k = 0; k < newRow; i++,k++)
                {
                    CopyTo(State.Row,matrix,i,0,temp,k,0,temp.Columns);
                }
            }
            else if (row == matrix.Rows - 1)
            {
                for (int i = 0; i < newRow; i++)
                {
                    CopyTo(State.Row,matrix,i,0,temp,i,0,matrix.Columns);
                }
            }
            else
            {
                for (int i = 0; i < newRow; i++)
                {
                    if (i < row)
                    {
                        CopyTo(State.Row,matrix,i,0,temp,i,0,matrix.Columns);
                    }
                    else if (i >= row)
                    {
                        CopyTo(State.Row,matrix,i + 1,0,temp,i,0,matrix.Columns);
                    }
                }
            }
            
            return temp;
        }

        
        /// <summary>
        /// Add column of matrix by index.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="arr">the array.</param>
        /// <param name="columnIndex">column index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>A new matrix with new column.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static Matrix<T> AddColumn<T>(this Matrix<T> matrix, T[] arr, int columnIndex) where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            int newColumn = matrix.Columns + 1; 
            Matrix<T> temp = new Matrix<T>(matrix.Rows,newColumn);
            
            if (columnIndex == 0)
            {
                temp[0, State.Column] = arr;
                for (int i = 1; i < newColumn; i++)
                {
                    CopyTo(State.Column,matrix,i - 1,0,temp,i,0,temp.Rows);
                }
            }
            else if (temp.Columns - 1 == columnIndex )
            {
                temp[columnIndex, State.Column] = arr;
                for (int i = 0; i < matrix.Columns; i++)
                {
                    CopyTo(State.Column,matrix,i,0,temp,i,0,temp.Rows);
                }
            }
            else
            {
                temp[columnIndex, State.Column] = arr;
                for (int i = 0, k = 0; i < temp.Columns; i++)
                {
                    if (i < columnIndex)
                    {
                        CopyTo(State.Column,matrix,i,0,temp,i,0,temp.Rows);
                    }
                    else if (i == columnIndex)
                    {
                        temp[columnIndex, State.Column] = arr;
                    }
                    else
                    {
                        k = i - 1;
                        CopyTo(State.Column,matrix,k,0,temp,i,0,temp.Rows);
                    }
                }
            }
            return temp;
        }
        
        
        /// <summary>
        /// Add row of matrix by index.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="arr">the array</param>
        /// <param name="rowIndex">ow index</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>A new matrix with new row.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static Matrix<T> AddRow<T>(this Matrix<T> matrix, T[] arr, int rowIndex) where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            int newRows = matrix.Rows + 1; 
            Matrix<T> temp = new Matrix<T>(newRows,matrix.Columns);
            
            if (rowIndex == 0)
            {
                temp[0, State.Row] = arr;
                for (int i = 1; i < newRows; i++)
                {
                    CopyTo(State.Row,matrix,i - 1,0,temp,i,0,temp.Columns);
                }
            }
            else if (temp.Columns - 1 == rowIndex )
            {
                temp[rowIndex, State.Row] = arr;
                for (int i = 0; i < matrix.Rows; i++)
                {
                    CopyTo(State.Row,matrix,i,0,temp,i,0,temp.Columns);
                }
            }
            else
            {
                temp[rowIndex, State.Row] = arr;
                for (int i = 0, k; i < newRows; i++)
                {
                    if (i < rowIndex)
                    {
                        CopyTo(State.Row,matrix,i,0,temp,i,0,temp.Columns);
                    }
                    else if (i == rowIndex)
                    {
                        temp[rowIndex, State.Row] = arr;
                    }
                    else
                    {
                        k = i - 1;
                        CopyTo(State.Row,matrix,k,0,temp,i,0,temp.Columns);
                    }
                }
            }
            return temp;
        }
    }
}