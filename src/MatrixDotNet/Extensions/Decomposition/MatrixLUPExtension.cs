using System;

using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Decomposition
{
    public static partial class Decomposition
    {
        /// <summary>
        /// Gets lower-triangular matrix, upper init zero values.
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
        /// Gets upper-triangular matrix, lower init zero values.
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
        /// Gets lower upper permutation with matrix C which calculate by formula:
        /// <c>C=L+U-E</c> 
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
            matrixP = BuildMatrix.CreateIdentityMatrix<T>(matrix.Rows,matrix.Columns);

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