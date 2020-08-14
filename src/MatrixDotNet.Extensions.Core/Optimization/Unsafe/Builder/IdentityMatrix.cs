using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Optimization.Unsafe.Builder
{
    public static partial class UnsafeBuilder
    {
         public static unsafe Matrix<double> CreateIdentityMatrix(this Matrix<double> matrix)
        {
            if(!matrix.IsSquare)
                throw new MatrixDotNetException($"Matrix is not square!!!");
            
            int m = matrix.Rows;
            int n = matrix.Columns;
            
            Matrix<double> result = new Matrix<double>(m,n);
            fixed(double* ptr = result.GetMatrix())
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
            fixed(int* ptr = result.GetMatrix())
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
            fixed(float* ptr = result.GetMatrix())
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
            fixed(long* ptr = result.GetMatrix())
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