using System.Numerics;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet
{
    public class MatrixComplex
    {
        private Complex[,] _matrix;
        public Complex[,] Matrix => _matrix;
        public int Rows { get; }
        public int Columns { get; }
        public int Length { get; }
        
        public MatrixComplex(int m,int n)
        {
            Rows = m;
            Columns = n;
            Length = m * n;
            _matrix = new Complex[m,n];
        }

        public Complex this[int i,int j]
        {
            get => _matrix[i, j];
            set => _matrix[i, j] = value;
        }
        
        public static MatrixComplex operator +(MatrixComplex left,MatrixComplex right)
        {
            int m = left.Rows;
            int n = left.Columns;
            
            if (m != right.Rows || n != right.Columns)
                throw new MatrixDotNetException("left matrix is not equals right matrix");

            MatrixComplex matrix = new MatrixComplex(m,n);
            
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = left[i, j] + right[i, j];
                }
            }
            
            return matrix;
        }
        
        public static MatrixComplex operator -(MatrixComplex left,MatrixComplex right)
        {
            int m = left.Rows;
            int n = left.Columns;
            
            if (m != right.Rows || n != right.Columns)
                throw new MatrixDotNetException("left matrix is not equals right matrix");

            MatrixComplex matrix = new MatrixComplex(m,n);
            
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = left[i, j] - right[i, j];
                }
            }
            
            return matrix;
        }

        public static unsafe MatrixComplex operator *(MatrixComplex left,MatrixComplex right)
        {
            int m = left.Rows;
            int n = right.Columns;
            int l = left.Columns;

            if (m != right.Rows || n != l)
                throw new MatrixDotNetException("left matrix is not equals right matrix");

            var matrix = new MatrixComplex(m,n);
            fixed (Complex* ptrB = left.Matrix)
            fixed (Complex* ptrC = matrix.Matrix)
            {
                for (int i = 0; i < m; i++)
                {
                    Complex* c = ptrC + i * n;
                    for (int k = 0; k < l; k++)
                    {
                        Complex* b = ptrB + k * n;
                        Complex a = left[i,k];
                        for (int j = 0; j < n; j++)
                        {
                            c[j] += a * b[j];
                        }
                    }
                }
                return matrix;
            }
        }

        public static MatrixComplex operator /(MatrixComplex left,double right)
        {
            int m = left.Rows;
            int n = left.Columns;
            var matrix = new MatrixComplex(m,n);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = left[i, j] / right;
                }
            }
            return matrix;
        }
        
        public static MatrixComplex operator /(double right,MatrixComplex left)
        {
            int m = left.Rows;
            int n = left.Columns;
            var matrix = new MatrixComplex(m,n);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = right / left[i, j];
                }
            }
            return matrix;
        }
        
        public static MatrixComplex operator *(double right,MatrixComplex left)
        {
            int m = left.Rows;
            int n = left.Columns;
            var matrix = new MatrixComplex(m,n);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = right * left[i, j];
                }
            }
            return matrix;
        }
        
        public static MatrixComplex operator *(MatrixComplex left,double right)
        {
            int m = left.Rows;
            int n = left.Columns;
            var matrix = new MatrixComplex(m,n);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = left[i, j] * right;
                }
            }
            return matrix;
        }
    }
}