using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Optimization.Unsafe.Conversion
{
    public static partial class UnsafeConverter
    {
        public static unsafe void SwapRows<T>(this Matrix<T> matrix,int dimension1,int dimension2) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            int m = matrix.Rows;
            int n = matrix.Columns;

            int length = matrix.Length;

            fixed (T* ptr1 = matrix.GetMatrix())
            {
                Span<T> span = new Span<T>(ptr1,length);

                int index = dimension1 * n + m;
                int i = dimension1 * n;
                int j = dimension2 * n;
                
                while (i < index)
                {
                    var tmp = span[i];
                    span[i] = span[j];
                    span[j] = tmp;
                    
                    i++; j++;
                }
            }
        }
        
        public static unsafe Matrix<T> CloneObject<T>(this Matrix<T> matrix) where T : unmanaged
        {
            int m = matrix.Rows;
            int n = matrix.Columns;
            
            Matrix<T> res = new Matrix<T>(m,n);
            int len = res.Length;

            fixed (T* ptr1 = matrix.GetMatrix())
            fixed (T* ptr2 = res.GetMatrix())
            {
                int i = 0;
                Span<double> span1 = new Span<double>(ptr1,len);
                Span<double> span2 = new Span<double>(ptr2,len);
                while (i < len)
                {
                    span2[i] = span1[i];
                    i += 1;
                }

                return res;
            }
        }
        
        
    }
}