using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Determinant;
using MatrixDotNet.Extensions.MathExpression;

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
            if(matrix is null)
                throw new NullReferenceException();
            
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
            if(matrix is null)
                throw new NullReferenceException();
            
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
                    MatrixConverter.CopyTo(State.Column,matrix,i,0,temp,k,0,temp.Rows);
                }
            }
            else if (column == matrix.Columns - 1)
            {
                for (int i = 0; i < newColumn; i++)
                {
                    MatrixConverter.CopyTo(State.Column,matrix,i,0,temp,i,0,matrix.Rows);
                }
            }
            else
            {
                for (int i = 0; i < newColumn; i++)
                {
                    if (i < column)
                    {
                        MatrixConverter.CopyTo(State.Column,matrix,i,0,temp,i,0,matrix.Rows);
                    }
                    else if (i >= column)
                    {
                        MatrixConverter.CopyTo(State.Column,matrix,i + 1,0,temp,i,0,matrix.Rows);
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
                    MatrixConverter.CopyTo(State.Row,matrix,i,0,temp,k,0,temp.Columns);
                }
            }
            else if (row == matrix.Rows - 1)
            {
                for (int i = 0; i < newRow; i++)
                {
                    MatrixConverter.CopyTo(State.Row,matrix,i,0,temp,i,0,matrix.Columns);
                }
            }
            else
            {
                for (int i = 0; i < newRow; i++)
                {
                    if (i < row)
                    {
                        MatrixConverter.CopyTo(State.Row,matrix,i,0,temp,i,0,matrix.Columns);
                    }
                    else if (i >= row)
                    {
                        MatrixConverter.CopyTo(State.Row,matrix,i + 1,0,temp,i,0,matrix.Columns);
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
                    MatrixConverter.CopyTo(State.Column,matrix,i - 1,0,temp,i,0,temp.Rows);
                }
            }
            else if (temp.Columns - 1 == columnIndex )
            {
                temp[columnIndex, State.Column] = arr;
                for (int i = 0; i < matrix.Columns; i++)
                {
                    MatrixConverter.CopyTo(State.Column,matrix,i,0,temp,i,0,temp.Rows);
                }
            }
            else
            {
                temp[columnIndex, State.Column] = arr;
                for (int i = 0, k = 0; i < temp.Columns; i++)
                {
                    if (i < columnIndex)
                    {
                        MatrixConverter.CopyTo(State.Column,matrix,i,0,temp,i,0,temp.Rows);
                    }
                    else if (i == columnIndex)
                    {
                        temp[columnIndex, State.Column] = arr;
                    }
                    else
                    {
                        k = i - 1;
                        MatrixConverter.CopyTo(State.Column,matrix,k,0,temp,i,0,temp.Rows);
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
                    MatrixConverter.CopyTo(State.Row,matrix,i - 1,0,temp,i,0,temp.Columns);
                }
            }
            else if (temp.Columns - 1 == rowIndex )
            {
                temp[rowIndex, State.Row] = arr;
                for (int i = 0; i < matrix.Rows; i++)
                {
                    MatrixConverter.CopyTo(State.Row,matrix,i,0,temp,i,0,temp.Columns);
                }
            }
            else
            {
                temp[rowIndex, State.Row] = arr;
                for (int i = 0, k; i < newRows; i++)
                {
                    if (i < rowIndex)
                    {
                        MatrixConverter.CopyTo(State.Row,matrix,i,0,temp,i,0,temp.Columns);
                    }
                    else if (i == rowIndex)
                    {
                        temp[rowIndex, State.Row] = arr;
                    }
                    else
                    {
                        k = i - 1;
                        MatrixConverter.CopyTo(State.Row,matrix,k,0,temp,i,0,temp.Columns);
                    }
                }
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
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    if (i == j)
                        matrix[i, j] = MathExtension.Increment<T>(default);
                    else
                        matrix[i, j] = default;
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
        public static void SwapRows<T>(this Matrix<T> matrix,int indexDimension1, int indexDimension2) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            var temp = matrix[indexDimension1, State.Row];
            matrix[indexDimension1, State.Row] = matrix[indexDimension2, State.Row];
            matrix[indexDimension2, State.Row] = temp;
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
            Matrix<T> transport = new Matrix<T>(matrix.Columns,matrix.Rows);
            for (int i = 0; i < transport.Rows; i++)
            {
                for (int j = 0; j < transport.Columns; j++)
                {
                    transport[i, j] = matrix[j, i];
                }
            }

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
            
            int n = a.Rows >> 1;
            
            a11 = new Matrix<T>(n,n);
            a12 = new Matrix<T>(n,n);
            a21 = new Matrix<T>(n,n);
            a22 = new Matrix<T>(n,n);

            
            for (int i = 0; i < n; i++)
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
            Matrix<T> a = new Matrix<T>(n << 1,n << 1);
            for (int i = 0; i < n; i++)
            {
                CopyTo(a11,i, 0, a,i,0,n);
                CopyTo(a12,i, 0, a,i,n,n << 1);
                CopyTo(a21,i, 0, a,i + n,0,n);
                CopyTo(a22,i, 0, a,i + n,n,n << 1);
            }

            return a;
        }
        
        /// <summary>
        /// Gets inverse matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>Inverse matrix.</returns>
        public static Matrix<double> Inverse(this Matrix<double> matrix)
        {
            var alg = matrix.AlgebraicComplement().Transpose();

            return (1 / matrix.GetLowerUpperDeterminant()) * alg;
        }
        
        /// <summary>
        /// Gets inverse matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>Inverse matrix.</returns>
        public static Matrix<float> Inverse(this Matrix<float> matrix)
        {
            var alg = matrix.AlgebraicComplement().Transpose();

            return (1 / matrix.GetLowerUpperDeterminant()) * alg;
        }
        
        /// <summary>
        /// Gets inverse matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>Inverse matrix.</returns>
        public static Matrix<decimal> Inverse(this Matrix<decimal> matrix)
        {
            var alg = matrix.AlgebraicComplement().Transpose();

            return (1 / matrix.GetLowerUpperDeterminant()) * alg;
        }
    }
}