using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Optimization.Unsafe
{
    public static partial class UnsafeMatrix
    {
        public static unsafe bool Equals(Matrix<int> matrixA,Matrix<int> matrixB)
        {
            if(matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");
            
            int length = matrixA.Length;
            fixed(int* pointer1 = matrixA.GetMatrix())
            fixed(int* pointer2 = matrixB.GetMatrix())
            {
                Span<int> span1 = new Span<int>(pointer1,length);
                Span<int> span2 = new Span<int>(pointer2,length);
                for (int i = 0; i < length; i++)
                {
                    if (span1[i] != span2[i]) return false;
                }
            }

            return true;
        }
        
        public static  bool Equals2(Matrix<int> matrixA,Matrix<int> matrixB)
        {
            if(matrixA.Rows != matrixB.Rows || matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");
            
            for (int i = 0; i < matrixA.Rows; i++)
            {
                for (int j = 0; j < matrixA.Columns; j++)
                {
                    if (matrixA[i,j] != matrixB[i,j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}