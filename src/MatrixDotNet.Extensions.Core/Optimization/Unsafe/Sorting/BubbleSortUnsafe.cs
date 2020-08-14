using System;

namespace MatrixDotNet.Extensions.Core.Optimization.Unsafe.Sorting
{
    public static partial class UnsafeSort
    {
        public static unsafe void BubbleSortUnsafe(this Matrix<int> matrix)
        {
            fixed (int* ptr = matrix.GetMatrix())
            {
                int length = matrix.Length;
                Span<int> span = new Span<int>(ptr,length);
                
                for (int i = 0; i < length; i++)
                {
                    for (int j = i + 1; j < length; j++)
                    {
                        if (span[i] > span[j])
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