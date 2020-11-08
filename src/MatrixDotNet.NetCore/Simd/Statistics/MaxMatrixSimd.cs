using System;
using System.Collections;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MatrixDotNet.Extensions.Core.Simd.Statistics
{
    public static partial class Simd
    {
        public static unsafe double Max(this Matrix<double> matrix)
        {
            int i = 0;
            fixed(double* ptr = matrix.GetArray())
            {
                var span = new Span<double>(ptr,matrix.Length);
                double maxScalar = span[0];
                if (Avx.IsSupported)
                {
                    var minValues = stackalloc double[] {span[0],span[0],span[0],span[0]};
                    var max = Avx.LoadVector256(minValues);
                    while (i < span.Length - 4)
                    {
                        Vector256<double> vector256 = Avx.LoadVector256(ptr + i);
                        max = Avx.Max(vector256,max);
                        i += 4;
                    }

                    maxScalar = max.MinVector256(4);
                }
                else if (Sse2.IsSupported)
                {
                    var maxValues = stackalloc double[2] {span[0],span[0]};
                    var max = Sse2.LoadVector128(maxValues);
                    while (i < span.Length - 2)
                    {
                        var vector128 = Sse2.LoadVector128(ptr + i);
                        max = Sse2.Max(vector128,max);
                        i += 2;
                    }

                    maxScalar = max.MaxVector128(2);
                }
                
                while (i < span.Length)
                {
                    if (maxScalar < span[i])
                    {
                        maxScalar = span[i];
                    }
                    i++;
                }

                return maxScalar;
            }
        }
        
        public static unsafe int Max(this Matrix<int> matrix)
        {
            int i = 0;
            fixed(int* ptr = matrix.GetArray())
            {
                var span = new Span<int>(ptr,matrix.Length);
                int maxScalar = span[0];
                if (Sse41.IsSupported)
                {
                    var maxValues = stackalloc int[4]
                    {
                        span[0],span[0],span[0],span[0],
                    };
                    var max = Sse2.LoadVector128(maxValues);
                    while (i < span.Length - 4)
                    {
                        Vector128<int> vector128 = Sse2.LoadVector128(ptr + i);
                        max = Sse41.Max(vector128,max);
                        i += 4;
                    }

                    int j = 0;
                    var x = max.GetElement(0);
                    while (j < 4)
                    {
                        var y = max.GetElement(j);
                        x = y & ((x - y) >> 31) | x & (~(x - y) >> 31);
                        j++;
                    }

                    maxScalar = x;
                }
                
                while (i < span.Length)
                {
                    var y = span[i];
                    maxScalar = y & ((maxScalar - y) >> 31) | maxScalar & (~(maxScalar - y) >> 31);
                    i++;
                }

                return maxScalar;
            }
        }
        
        public static unsafe float Max(this Matrix<float> matrix)
        {
            int i = 0;
            fixed(float* ptr = matrix.GetArray())
            {
                var span = new Span<float>(ptr,matrix.Length);
                float maxScalar = span[0];
                if (Avx.IsSupported)
                {
                    var maxValues = stackalloc float[8]
                    {
                        span[0],span[0],span[0],span[0],
                        span[0],span[0],span[0],span[0]
                    };
                    var max = Avx.LoadVector256(maxValues);
                    while (i < span.Length - 8)
                    {
                        Vector256<float> vector256 = Avx.LoadVector256(ptr + i);
                        max = Avx.Max(vector256,max);
                        i += 8;
                    }
                    maxScalar = max.MaxVector256(8);
                }
                else if(Sse.IsSupported)
                {
                    var maxValues = stackalloc float[4]
                    {
                        span[0],span[0],span[0],span[0],
                    };
                    var max = Sse.LoadVector128(maxValues);
                    while (i < span.Length - 4)
                    {
                        Vector128<float> vector128 = Sse.LoadVector128(ptr + i);
                        max = Sse.Max(vector128,max);
                        i += 4;
                    }

                    maxScalar = max.MaxVector128(4);
                }
                
                while (i < span.Length)
                {
                    if (maxScalar < span[i])
                    {
                        maxScalar = span[i];
                    }
                    i++;
                }

                return maxScalar;
            }
        }

        private static T MaxVector128<T>(this Vector128<T> min,int size) where T : unmanaged
        {
            int j = 0;
            var x = min.GetElement(0);
            while (j < size)
            {
                T val = min.GetElement(j);
                Comparer comparer = Comparer.Default;
                if (comparer.Compare(x,val) < 0)
                {
                    x = val;
                }
                j++;
            }
            return x;
        }
        
        private static T MaxVector256<T>(this Vector256<T> min,int size) where T : unmanaged
        {
            int j = 0;
            var x = min.GetElement(0);
            while (j < size)
            {
                T val = min.GetElement(j);
                Comparer comparer = Comparer.Default;
                if (comparer.Compare(x,val) < 0)
                {
                    x = val;
                }

                j++;
            }
            return x;
        }
    }
}