using MatrixDotNet.Exceptions;
using MathExtension = MatrixDotNet.Math.MathExtension;

namespace MatrixDotNet.Extensions.Builder
{
    /// <summary>
    /// Represents the functional of build matrix.
    /// </summary>
    public static partial class BuildMatrix
    {
        /// <summary>
        /// Creates identity matrix.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static Matrix<T> CreateIdentityMatrix<T>(int row, int col) where T : unmanaged
        {
            if(row != col)
                throw new MatrixDotNetException($"Matrix is not square!!!\nRows: {row}\nColumns: {col}");
            
            Matrix<T> matrix = new Matrix<T>(row,col);
            
            for (int i = 0; i < row; i++)
            {
                matrix[i, i] = MathExtension.Increment<T>(default);
            }

            return matrix;
        }
        
        /// <summary>
        /// Creates identity matrix by this size of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Identity matrix.</returns>
        /// <exception cref="MatrixDotNetException">throws exception if matrix is not square</exception>
        public static Matrix<T> CreateIdentityMatrix<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(!matrix.IsSquare)
                throw new MatrixDotNetException($"Matrix is not square!!!\nRows: {matrix.Rows}\nColumns: {matrix.Columns}");
            
            Matrix<T> result = new Matrix<T>(matrix.Rows,matrix.Columns);
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                result[i, i] = MathExtension.Increment<T>(default);
            }
            
            return result;
        }
        
         public static unsafe Matrix<double> CreateIdentityMatrix(this Matrix<double> matrix)
        {
            if(!matrix.IsSquare)
                throw new MatrixDotNetException($"Matrix is not square!!!");
            
            int m = matrix.Rows;
            int n = matrix.Columns;
            
            Matrix<double> result = new Matrix<double>(m,n);
            fixed(double* ptr = result.GetArray())
            {
                for (int i = 0; i < m; i++)
                {
                    double* diagonal = ptr + i * m;
                    diagonal[i] = 1;
                }
                return result;
            }
        }
        
        public static unsafe Matrix<int> CreateIdentityMatrix(this Matrix<int> matrix)
        {
            if(!matrix.IsSquare)
                throw new MatrixDotNetException($"Matrix is not square!!!");
            
            int m = matrix.Rows;
            int n = matrix.Columns;
            
            Matrix<int> result = new Matrix<int>(m,n);
            fixed(int* ptr = result.GetArray())
            {
                for (int i = 0; i < m; i++)
                {
                    int* diagonal = ptr + i * m;
                    diagonal[i] = 1;
                }
                return result;
            }
        }
        
        public static unsafe Matrix<float> CreateIdentityMatrix(this Matrix<float> matrix)
        {
            if(!matrix.IsSquare)
                throw new MatrixDotNetException($"Matrix is not square!!!");
            
            int m = matrix.Rows;
            int n = matrix.Columns;
            
            Matrix<float> result = new Matrix<float>(m,n);
            fixed(float* ptr = result.GetArray())
            {
                for (int i = 0; i < m; i++)
                {
                    float* diagonal = ptr + i * m;
                    diagonal[i] = 1;
                }
                return result;
            }
        }
        
        public static unsafe Matrix<long> CreateIdentityMatrix(this Matrix<long> matrix)
        {
            if(!matrix.IsSquare)
                throw new MatrixDotNetException($"Matrix is not square!!!");
            
            int m = matrix.Rows;
            int n = matrix.Columns;
            
            Matrix<long> result = new Matrix<long>(m,n);
            fixed(long* ptr = result.GetArray())
            {
                for (int i = 0; i < m; i++)
                {
                    long* diagonal = ptr + i * m;
                    diagonal[i] = 1;
                }
                return result;
            }
        }
    }
}