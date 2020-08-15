using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Optimization.Unsafe.Conversion
{
    public static partial class UnsafeConverter
    {
        public static unsafe void CopyTo<T>(Matrix<T> matrix1,int dimension1, int start,Matrix<T> matrix2,int dimension2,int destinationIndex,int length) 
            where T : unmanaged
        {
            fixed (T* ptr2 = matrix2.GetMatrix())
            fixed (T* ptr1 = matrix1.GetMatrix())
            {

                Span<T> span2 = new Span<T>(ptr2,matrix2.Length);
                Span<T> span1 = new Span<T>(ptr1,matrix1.Length);
                for (int i = start, k = destinationIndex; k < length; i++,k++)
                {
                    span2[dimension2 * matrix2.Columns + k] = span1[dimension1 * matrix1.Columns + i];
                }
                
            }
        }
    }
}