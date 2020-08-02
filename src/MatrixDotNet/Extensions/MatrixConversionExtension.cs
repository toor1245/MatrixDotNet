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
    }
}