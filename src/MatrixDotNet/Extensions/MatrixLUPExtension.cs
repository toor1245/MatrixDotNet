using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
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

       /* public static void GetLowerUpperPermutation<T>(this Matrix<T> matrix, out Matrix<T> lower, out Matrix<T> upper,
            Matrix<T> perm)
            where T : unmanaged
        {
            for (int i = 0; i < UPPER; i++)
            {
                for (int j = 0; j < UPPER; j++)
                {
                    
                }
            }
        }
        */
        
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

        /*public static double GetLowerUpperPermSolve<T>(this Matrix<T> matrix) where T : unmanaged
        {
            matrix.GetLowerUpper(out var lower,out var upper);
            int n = lower.Rows;
            T[] x = new T[n];
            T[] y = new T[n];
            for (int i = 1; i < n; i++)
            {
                y[i] = 
            }
        }*/
    }
}