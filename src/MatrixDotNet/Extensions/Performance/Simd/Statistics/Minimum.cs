#if NETCOREAPP3_1_OR_GREATER
using System;
using System.Collections;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MatrixDotNet.Extensions.Performance.Simd.Statistics
{
    public static partial class Simd
    {
        public static unsafe double Min(this Matrix<double> matrix)
        {
            var i = 0;
            fixed (double* ptr = matrix.GetArray())
            {
                var span = new Span<double>(ptr, matrix.Length);
                var minScalar = span[0];
                if (Avx.IsSupported)
                {
                    var minValues = stackalloc double[] { span[0], span[0], span[0], span[0] };
                    var min = Avx.LoadVector256(minValues);
                    while (i < span.Length - 4)
                    {
                        var vector256 = Avx.LoadVector256(ptr + i);
                        min = Avx.Min(vector256, min);
                        i += 4;
                    }

                    minScalar = min.MinVector256(4);
                }
                else if (Sse2.IsSupported)
                {
                    var minValues = stackalloc double[2] { span[0], span[0] };
                    var min = Sse2.LoadVector128(minValues);
                    while (i < span.Length - 2)
                    {
                        var vector128 = Sse2.LoadVector128(ptr + i);
                        min = Sse2.Min(vector128, min);
                        i += 2;
                    }

                    minScalar = min.MinVector128(2);
                }

                while (i < span.Length)
                {
                    if (minScalar > span[i]) minScalar = span[i];
                    i++;
                }

                return minScalar;
            }
        }

        public static unsafe int Min(this Matrix<int> matrix)
        {
            var i = 0;
            fixed (int* ptr = matrix.GetArray())
            {
                var span = new Span<int>(ptr, matrix.Length);
                var minScalar = span[0];
                if (Sse41.IsSupported)
                {
                    var minValues = stackalloc int[4]
                    {
                        span[0],
                        span[0],
                        span[0],
                        span[0]
                    };
                    var min = Sse2.LoadVector128(minValues);
                    while (i < span.Length - 4)
                    {
                        var vector128 = Sse2.LoadVector128(ptr + i);
                        min = Sse41.Min(vector128, min);
                        i += 4;
                    }

                    var j = 0;
                    var x = min.GetElement(0);
                    while (j < 4)
                    {
                        var y = min.GetElement(j);
                        x = (x & ((x - y) >> 31)) | (y & (~(x - y) >> 31));
                        j++;
                    }

                    minScalar = x;
                }

                while (i < span.Length)
                {
                    var y = span[i];
                    minScalar = (minScalar & ((minScalar - y) >> 31)) | (y & (~(minScalar - y) >> 31));
                    i++;
                }

                return minScalar;
            }
        }

        public static unsafe float Min(this Matrix<float> matrix)
        {
            var i = 0;
            fixed (float* ptr = matrix.GetArray())
            {
                var span = new Span<float>(ptr, matrix.Length);
                var minScalar = span[0];
                if (Avx.IsSupported)
                {
                    var minValues = stackalloc float[8]
                    {
                        span[0],
                        span[0],
                        span[0],
                        span[0],
                        span[0],
                        span[0],
                        span[0],
                        span[0]
                    };
                    var min = Avx.LoadVector256(minValues);
                    while (i < span.Length - 8)
                    {
                        var vector256 = Avx.LoadVector256(ptr + i);
                        min = Avx.Min(vector256, min);
                        i += 8;
                    }

                    minScalar = min.MinVector256(8);
                }
                else if (Sse.IsSupported)
                {
                    var minValues = stackalloc float[4]
                    {
                        span[0],
                        span[0],
                        span[0],
                        span[0]
                    };
                    var min = Sse.LoadVector128(minValues);
                    while (i < span.Length - 4)
                    {
                        var vector128 = Sse.LoadVector128(ptr + i);
                        min = Sse.Min(vector128, min);
                        i += 4;
                    }

                    minScalar = min.MinVector128(4);
                }

                while (i < span.Length)
                {
                    if (minScalar > span[i]) minScalar = span[i];
                    i++;
                }

                return minScalar;
            }
        }

        private static T MinVector128<T>(this Vector128<T> min, int size) where T : unmanaged
        {
            var j = 0;
            var x = min.GetElement(0);
            while (j < size)
            {
                var val = min.GetElement(j);
                var comparer = Comparer.Default;
                if (comparer.Compare(x, val) > 0) x = val;
                j++;
            }

            return x;
        }

        private static T MinVector256<T>(this Vector256<T> min, int size) where T : unmanaged
        {
            var j = 0;
            var x = min.GetElement(0);
            while (j < size)
            {
                var val = min.GetElement(j);
                var comparer = Comparer.Default;
                if (comparer.Compare(x, val) > 0) x = val;

                j++;
            }

            return x;
        }
    }
}
#endif