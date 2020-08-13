using System;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.Extensions.Core.Optimization.Unsafe
{
    public static partial class UnsafeMatrix
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
        
        public static Matrix<int> MultiplyStrassen(Matrix<int> a, Matrix<int> b)
        {
            if (a.Rows <= 1024) 
            {
                return Multiply(a,b);
            }
            
            a.SplitMatrix(out var a11,out var a12,out var a21,out var a22);
            b.SplitMatrix(out var b11,out var b12,out var b21,out var b22);
            
            Matrix<int> p1 = MultiplyStrassen(a11 + a22, b11 + b22);
            Matrix<int> p2 = MultiplyStrassen(a21 + a22, b11);
            Matrix<int> p3 = MultiplyStrassen(a11, b12 - b22);
            Matrix<int> p4 = MultiplyStrassen(a22, b21 - b11);
            Matrix<int> p5 = MultiplyStrassen(a11 + a12, b22);
            Matrix<int> p6 = MultiplyStrassen(a21 - a22, b11 + b12);
            Matrix<int> p7 = MultiplyStrassen(a12 - a22, b21 + b22);

            Matrix<int> c11 = p1 + p4 - p5 + p7;
            Matrix<int> c12 = p3 + p5;
            Matrix<int> c21 = p2 + p4;
            Matrix<int> c22 = p1 + p3 - p2 + p6;

            return MatrixConverter.CollectMatrix(c11, c12, c21, c22);
        }
    }
}