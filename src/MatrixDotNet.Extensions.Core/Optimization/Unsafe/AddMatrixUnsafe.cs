using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Optimization.Unsafe
{
    public static partial class UnsafeMatrix
    {
        public static unsafe Matrix<int> Add(Matrix<int> matrixA,Matrix<int> matrixB)
        {
            if(matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");
            
            int m = matrixA.Rows;
            int n = matrixA.Columns;
            int length = matrixA.Length;

            Matrix<int> matrix = new Matrix<int>(m,n);

            fixed(int* pointer1 = matrixA.GetMatrix())
            fixed(int* pointer2 = matrixB.GetMatrix())
            fixed(int* pointer3 = matrix.GetMatrix())
            {
                Span<int> span1 = new Span<int>(pointer1,length);
                Span<int> span2 = new Span<int>(pointer2,length);
                Span<int> span3 = new Span<int>(pointer3,length);
                for (int i = 0; i < length; i++)
                {
                    span3[i] = span2[i] + span1[i];
                }
            }
            return matrix;
        }
    }
}