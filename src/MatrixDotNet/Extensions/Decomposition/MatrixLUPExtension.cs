using System;
using System.Runtime.InteropServices;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.BitMatrix;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Decomposition
{
    /// <summary>
    /// Represents any algorithm`s for decomposition of matrix.
    /// </summary>
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
        

        public static void GetLUP(this Matrix<double> matrix,out Matrix<double> lower,out Matrix<double> upper,out Matrix<double> matrixP)
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            int n = matrix.Rows;

            lower = matrix.CreateIdentityMatrix();
            upper = matrix.Clone() as Matrix<double>;
            
            if(upper is null)
                throw new NullReferenceException();
            
            
            // load to P identity matrix.
            matrixP = lower.Clone() as Matrix<double>;
            
            for (int i = 0; i < n - 1; i++)
            {
                int index = i;
                double max = upper[index, index];
                for (int j = i + 1; j < n; j++)
                {
                    double current = upper[i, j];
                    if (Math.Abs(current) > Math.Abs(max))
                    {
                        max = current;
                        index = j;
                    }
                }

                if (index != i)
                {
                    upper.SwapRows(i,index);
                    matrixP.SwapRows(i,index);
                }

                for (int j = i + 1; j < n; j++)
                {
                    double ji = upper[j, i];
                    double ii = upper[i, i];
                    double div = ji / ii;
                    lower[j, i] = div;
                    
                    for (int k = i; k < n; k++)
                    {
                        upper[j, k] = upper[j, k] - upper[i,k] * div;
                    }
                }
            }
        }
    }
}