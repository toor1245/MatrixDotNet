using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions
{
    // Multiply Strassen
    public static partial class MatrixExtension
    {
        #region Strassen
        
        public static void SplitMatrix<T>(this Matrix<T> a, out Matrix<T> a11, out Matrix<T> a12, out Matrix<T> a21, out Matrix<T> a22) 
            where T : unmanaged
        {
            int n = a.Rows >> 1;
            
            
            a11 = new Matrix<T>(n,n);
            a12 = new Matrix<T>(n,n);
            a21 = new Matrix<T>(n,n);
            a22 = new Matrix<T>(n,n);

            
            for (int i = 0; i < n; i++)
            {
                CopyTo(a,i, 0, a11,i,0,n);
                CopyTo(a,i, n, a12,i,0,n);
                CopyTo(a,i + n, 0, a21,i,0,n);
                CopyTo(a,i + n, n, a22,i,0,n);
            }
        }

        public static Matrix<T> CollectMatrix<T>(Matrix<T> a11, Matrix<T> a12, Matrix<T> a21, Matrix<T> a22)
            where T : unmanaged
        {
            int n = a11.Rows;
            Matrix<T> a = new Matrix<T>(n << 1,n << 1);
            for (int i = 0; i < n; i++)
            {
                CopyTo(a11,i, 0, a,i,0,n);
                CopyTo(a12,i, 0, a,i,n,n << 1);
                CopyTo(a21,i, 0, a,i + n,0,n);
                CopyTo(a22,i, 0, a,i + n,n,n << 1);
            }

            return a;
        }
        
        
        public static Matrix<T> MultiplyStrassen<T>(Matrix<T> a, Matrix<T> b) where T : unmanaged
        {
            if (a.Rows < 32)
            {
                return a * b;
            }
            
            a.SplitMatrix(out var a11,out var a12,out var a21,out var a22);
            b.SplitMatrix(out var b11,out var b12,out var b21,out var b22);
            
            Matrix<T> p1 = MultiplyStrassen(a11 + a22, b11 + b22);
            Matrix<T> p2 = MultiplyStrassen(a21 + a22, b11);
            Matrix<T> p3 = MultiplyStrassen(a11, b12 - b22);
            Matrix<T> p4 = MultiplyStrassen(a22, b21 - b11);
            Matrix<T> p5 = MultiplyStrassen(a11 + a12, b22);
            Matrix<T> p6 = MultiplyStrassen(a21 - a11, b11 + b22);
            Matrix<T> p7 = MultiplyStrassen(a12 - a22, b21 + b22);

            Matrix<T> c11 = p1 + p4 + (p7 - p5);
            Matrix<T> c12 = p3 + p5;
            Matrix<T> c21 = p2 + p4;
            Matrix<T> c22 = p1 - p2 + (p3 - p6);

            return CollectMatrix(c11, c12, c21, c22);
        }
        
        #endregion
        
        #region Degree

        
        /// <summary>
        /// Raises a matrix to a power.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="degree">the degree</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>Pow</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static Matrix<T> Pow<T>(this Matrix<T> matrix,int degree) where T : unmanaged
        {
            if(!matrix.IsSquare)
                throw new MatrixDotNetException("matrix is not square or not prime");
            
            Matrix<T> result = new Matrix<T>(matrix.Rows,matrix.Columns);
            
            for (int i = 0; i < degree; i++)
            {
                if (i == 0) result = matrix * matrix;
                result *= matrix;
            }

            return result;
        }
        
        /// <summary>
        /// Raises a matrix to a power.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="degree">the degree</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>Pow</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static Matrix<T> PowStrassen<T>(this Matrix<T> matrix,int degree) where T : unmanaged
        {
            if(!matrix.IsSquare || !matrix.IsPrime)
                throw new MatrixDotNetException("matrix is not square or not prime");
            
            Matrix<T> result = new Matrix<T>(matrix.Rows,matrix.Columns);
            
            for (int i = 0; i < degree; i++)
            {
                if (i == 0) result = MultiplyStrassen(matrix,matrix);
                result = MultiplyStrassen(result,matrix);
            }

            return result;
        }
        
        #endregion
    }
}