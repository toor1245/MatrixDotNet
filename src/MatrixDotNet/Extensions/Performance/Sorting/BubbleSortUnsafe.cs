using System;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Performance.Sorting
{
    public static partial class UnsafeSort
    {
        public static unsafe void BubbleSort<T>(Matrix<T> matrix) 
            where T : unmanaged
        {
            fixed (T* ptr = matrix.GetMatrix())
            {
                int length = matrix.Length;
                Span<T> span = new Span<T>(ptr,length);
                
                for (int i = 0; i < length; i++)
                {
                    for (int j = i + 1; j < length; j++)
                    {
                        if (MathExtension.GreaterThan(span[i],span[j]))
                        {
                            var temp = span[i];
                            span[i] = span[j];
                            span[j] = temp;
                        }
                    }
                }
            }
        }
        
        public static unsafe void BubbleSortUnsafeByRows<T>(Matrix<T> matrix)
            where T : unmanaged
        {
            fixed (T* ptr = matrix.GetMatrix())
            {
                int length = matrix.Length;
                int m = matrix.Rows;
                int n = matrix.Columns;
                Span<T> span = new Span<T>(ptr,length);
                
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (MathExtension.GreaterThan(span[i],span[j]))
                        {
                            var temp = span[i];
                            span[i] = span[j];
                            span[j] = temp;
                        }
                    }
                }
            }
        }
    }
}