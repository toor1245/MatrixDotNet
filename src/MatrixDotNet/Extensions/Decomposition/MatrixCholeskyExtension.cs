using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.Extensions.Decomposition
{
    public static partial class Decomposition
    {
        public static unsafe void GetCholesky(this Matrix<double> matrix,out Matrix<double> lower,out Matrix<double> transpose)
        {
            if(!matrix.IsSquare)
                throw new MatrixDotNetException("Matrix must be square");
            
            if(matrix != matrix.Transpose())
                throw new MatrixDotNetException("matrix is not symmetric");

            int m = matrix.Rows;
            int n = matrix.Columns;

            lower = new Matrix<double>(m,n);

            // Decomposing a matrix  
            // into Lower Triangular 
            for (int i = 0; i < n; i++) 
            { 
                for (int j = 0; j <= i; j++) 
                { 
                    double sum = 0; 

                    // summation for diagnols 
                    if (j == i)  
                    {
                        for (int k = 0; k < j; k++)
                        {
                            sum += (int) Math.Pow(lower[j, k], 2);
                        }

                        lower[j, j] = (int)Math.Sqrt(matrix[j, j] - sum); 
                    }  
          
                    else
                    { 

                        // Evaluating L(i, j)  
                        // using L(j, j) 
                        for (int k = 0; k < j; k++)
                        {
                            sum += (lower[i, k] * lower[j, k]);
                        }

                        lower[i, j] = (matrix[i,j] - sum) / lower[j, j]; 
                    } 
                } 
            }

            transpose = lower.Transpose();
        }
    }
}