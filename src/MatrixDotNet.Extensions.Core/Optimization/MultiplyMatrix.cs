using System;

namespace MatrixDotNet.Extensions.Core.Optimization
{
    public static partial class Optimization
    {
        
        public static unsafe Matrix<int> Multiply(Matrix<int> matrixA, Matrix<int> matrixB)
        {
            int m = matrixA.Rows;
            int n = matrixB.Columns;
            int K = matrixA.Columns;
            int len1 = matrixA.Length;

            Matrix<int> matrix = new Matrix<int>(m,n);

            fixed(int* pointer1 = matrixA.GetMatrix())
            fixed(int* pointer2 = matrixB.GetMatrix())
            fixed(int* pointer3 = matrix.GetMatrix())
            {
                Span<int> span1 = new Span<int>(pointer1,len1);
                
                for (int i = 0; i < m; i++)
                {
                    int* c = pointer3 + i * n;

                    for (int k = 0; k < K; k++)
                    {
                        int* b = pointer2 + k * n;
                        int a = span1[i * K + k];
                        for (int j = 0; j < n; j++)
                        {
                            c[j] += a * b[j];
                        }
                    }
                }
            }

            return matrix;
        }
        
        public static unsafe Matrix<int> MultiplyAvx(Matrix<int> matrixA,Matrix<int> matrixB)
        {
            Matrix<int> matrix = new Matrix<int>(matrixA.Rows,matrixB.Columns);
            int length = matrix.Length;
            
            fixed(int* pointer1 = matrixA.GetMatrix())
            fixed(int* pointer2 = matrixB.GetMatrix())
            {
                Span<int> span1 = new Span<int>(pointer1,matrixA.Length);
                Span<int> span2 = new Span<int>(pointer2,matrixB.Length);
                Span<int> span3 = new Span<int>(pointer2,matrix.Length);
            }

            return matrix;
        }
    }
}