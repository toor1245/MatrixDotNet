using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Gets LU decomposition of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="lower">the lower triangle matrix.</param>
        /// <param name="upper">the upper triangle matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <exception cref="MatrixDotNetException">throws exception if matrix is not square</exception>
        public static void GetLowerUpper<T>(this Matrix<T> matrix,out Matrix<T> lower,out Matrix<T> upper) where T : unmanaged
        {
            if (!matrix.IsSquare)
                throw new MatrixDotNetException(
                    $"matrix is not square\n Rows: {matrix.Rows}\n Columns: {matrix.Columns}");

            int n = matrix.Columns;
            
            lower = new Matrix<T>(n,n);
            upper = new Matrix<T>(n, n)
            {
                [0, State.Row] = matrix[0, State.Row]
            };
            
            for (int i = 0; i < n; i++)
            {
                lower[0, i, State.Column] = MathExtension.Divide(matrix[0, State.Column][i], upper[0, 0]);
            }
            
            for (int i = 1; i < n; i++)
            {
                for (int j = i; j < n; j++)
                {
                    T sumL = default;
                    T sumU = default;
                    for (int k = 0; k < i; k++)
                    {
                        sumU = MathExtension.Add(sumU, MathExtension.Multiply(lower[i, k], upper[k, j]));
                        sumL = MathExtension.Add(sumL, MathExtension.Multiply(lower[j, k], upper[k, i]));
                    }
                    
                    upper[i, j] = MathExtension.Sub(matrix[i, j],sumU);
                    lower[j, i] = MathExtension.Divide(MathExtension.Sub(matrix[j, i],sumL),upper[i,i]);
                }
            }
        }
        
        /// <summary>
        /// Gets lower matrix, upper init zero values.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>The lower matrix.</returns>
        /// <exception cref="MatrixDotNetException">throws exception if matrix is not square.</exception>
        public static Matrix<T> GetLowerMatrix<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!matrix.IsSquare)
                throw new MatrixDotNetException(
                    $"matrix is not square\n Rows: {matrix.Rows}\n Columns: {matrix.Columns}");
            
            Matrix<T> lower = new Matrix<T>(matrix.Rows,matrix.Columns);
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    lower[i, j] = matrix[i,j];
                }
            }
            
            return lower;
        }
        
        /// <summary>
        /// Gets upper matrix, lower init zero values.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException">throws exception if matrix is not square.</exception>
        public static Matrix<T> GetUpperMatrix<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!matrix.IsSquare)
                throw new MatrixDotNetException(
                    $"matrix is not square\n Rows: {matrix.Rows}\n Columns: {matrix.Columns}");
            
            Matrix<T> upper = new Matrix<T>(matrix.Rows,matrix.Columns);
            
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    upper[j, i] = matrix[i,j];
                }
            }
            
            return upper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static void GetLUP<T>(this Matrix<T> matrix,out Matrix<T> matrixC,out Matrix<T> matrixP)  where T : unmanaged
        {
            int n = matrix.Rows;

            matrixC = matrix.Clone() as Matrix<T>;
            
            if(matrixC is null)
                throw new NullReferenceException();

            // load to P identity matrix.
            matrixP = matrix.CreateIdentityMatrix();

            for (int i = 0; i < n; i++)
            {
                T pivotValue = default;
                int pivot = -1;
                for (int j = i; j < n; j++)
                {
                    if(MathExtension.GreaterThan(MathExtension.Abs(matrixC[j,i]),pivotValue))
                    {
                        pivotValue = MathExtension.Abs(matrixC[j, i]);
                        pivot = j;
                    }
                }

                if (pivot != 0)
                {
                    matrixP.SwapRows(pivot,i);
                    matrixC.SwapRows(pivot,i);
                    for (int j = i + 1; j < n; j++)
                    {
                        matrixC[j, i] = MathExtension.Divide(matrixC[j, i],matrixC[i, i]);
                        for (int k = i + 1; k < n; k++)
                        {
                            matrixC[j, k] = MathExtension.Sub(matrixC[j, k],
                                MathExtension.Multiply(matrixC[j, i], matrix[i, k]));
                        }
                    }
                }
            }
        }
    }
}