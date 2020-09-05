using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.Extensions.Inverse
{
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Inverse matrix with happen Gaussian elimination.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <returns>Inverse matrix.</returns>
        /// <exception cref="MatrixDotNetException">matrix is not square.</exception>
        /// <exception cref="NullReferenceException"></exception>
        public static Matrix<double> GaussianEliminationInverse(this Matrix<double> matrix)
        {
            if (!matrix.IsSquare)
                throw new MatrixDotNetException("Matrix must be square");

            int size = matrix.Rows;
            Matrix<double> a = matrix.Clone() as Matrix<double>;
            Matrix<double> b = matrix.CreateIdentityMatrix();
            
            if(a is null || b is null)
                throw new NullReferenceException();
            
            // Forward substitution
            for (int i = 0; i < size - 1; i++) 
            {

                if (System.Math.Abs(a[i, i]) < 0.00001) 
                {
                    for (int j = i + 1; j < size; j++)
                    {
                        if (System.Math.Abs(a[j, i]) < 0.00001) 
                        {
                            if (j == size - 1) 
                            {
                                throw new MatrixDotNetException("Matrix is singular");
                            }
                        }
                        else 
                        {
                            a.SwapRows(i, j);
                            b.SwapRows(i, j);
                            break;
                        }
                    }
                }

                for (int k = i + 1; k < size; k++)
                {
                    double div = a[k,i] / a[i, i];
                    for (int j = 0; j < size; j++)
                    {
                        a[k, j] = a[k, j] - a[i, j] * div;
                        b[k, j] = b[k, j] - b[i, j] * div;
                    }
                }
            }

            // Back substitution
            for (int i = size - 1; i > 0; i--) 
            {
                if (System.Math.Abs(a[i, i]) < 0.00001) 
                {
                    for (int j = i + 1; j < size; j++) 
                    {
                        if (System.Math.Abs(a[j, i]) < 0.00001)
                        {
                            if (j == size - 1) 
                            {
                                throw new MatrixDotNetException("Matrix is singular");
                            }
                        }
                        else 
                        {
                            a.SwapRows(i, j);
                            b.SwapColumns(i,j);
                            break;
                        }
                    }
                }

                for (int k = i - 1; k >= 0; k--) 
                {
                    double div = a[k,i] / a[i, i];
                    
                    for (int j = size - 1; j >= 0; j--) 
                    {
                        a[k, j] =  a[k,j] - a[i,j] * div;
                        b[k, j] =  b[k,j] - b[i,j] * div;
                    }
                }
            }

            // Correction
            for (int i = 0; i < size; i++) 
            {
                double d = a[i, i];
                if (System.Math.Abs(d) < 0.00001) 
                    throw new MatrixDotNetException("Matrix is singular");
                
                for (int j = 0; j < size; j++) 
                {
                    b[i, j] =  b[i, j] / d;
                }
            }
            
            return b;
        }
    }
}