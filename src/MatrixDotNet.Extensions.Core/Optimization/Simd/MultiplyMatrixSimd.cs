using System;

namespace MatrixDotNet.Extensions.Core.Optimization.Simd
{
    public static partial class Simd
    {
        public static unsafe Matrix<int> Multiply(Matrix<int> matrixA,Matrix<int> matrixB)
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