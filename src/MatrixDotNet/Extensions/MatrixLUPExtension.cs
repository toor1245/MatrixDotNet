using System;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        public static T GetLUP_Solve<T>(this Matrix<T> matrix,out Matrix<T> lower,out Matrix<T> upper) where T : unmanaged
        {
            upper = new Matrix<T>(matrix.Rows,matrix.Columns);
            lower = new Matrix<T>(matrix.Rows,matrix.Columns);
            
        }

        public static Matrix<T> GetLowerDiagonal<T>(this Matrix<T> matrix) where T : unmanaged
        {
            Matrix<T> lower = new Matrix<T>(matrix.Rows,matrix.Columns);
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    // lower[i,j] = matrix[i,j] / matrix[i,i];
                    lower[i, j] = MathExtension.Divide(matrix[i,j],matrix[i,i]);
                }
            }
            
            return lower;
        }

        public static Matrix<T> GetUpperDiagonal<T>(this Matrix<T> matrix,Matrix<T> lower) where T : unmanaged
        {
            Matrix<T> upper = matrix.Clone() as Matrix<T>;
            
            if(upper is null)
                throw new NullReferenceException();

            
            for (int k = 1; k < matrix.Columns; k++)
            {
                for (int i = k; i < matrix.Columns; i++)
                {
                    for (int j = k - 1; j < matrix.Columns; j++)
                    {
                        // upper[i,j] = upper[i,j] - lower[i,j] * upper[i,j];
                        upper[i,j] = MathExtension.Sub(upper[i,j],
                            MathExtension.Multiply(lower[i,k - 1],upper[k - 1,j]));
                    }
                }
            }
            return upper;
        } 
    }
}