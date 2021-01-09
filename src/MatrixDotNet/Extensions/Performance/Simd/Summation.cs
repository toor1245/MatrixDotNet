using System.Runtime.CompilerServices;
using MatrixDotNet.Extensions.Performance.Simd.Handler;
using MatrixDotNet.Math;
#if NET5_0 || NETCOREAPP3_1
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace MatrixDotNet.Extensions.Performance.Simd
{
    public static unsafe partial class Simd
    {
        /// <summary>
        /// Gets sum of array
        /// </summary>
        /// <param name="array">array</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>Sum of array</returns>
        internal static T Sum<T>(T[] array)
            where T : unmanaged
        {
            var length = array.Length;
            if (length < 1)
            {
                return default;
            }
            var i = 0;
            var result = default(T);
            fixed (T* ptr = array)
            {
#if NET5_0 || NETCOREAPP3_1
                int size;
                int lastIndexBlock;
                if (Avx2.IsSupported && MathGeneric.IsSupported<T>())
                {
                    size = Vector256<T>.Count;
                    if (length < size)
                    {
                        return Sum(ptr, length);
                    }
                    lastIndexBlock = length - length % size;
                    var sum = Vector256<T>.Zero;

                    for (; i < lastIndexBlock; i += size)
                    {
                        var current = IntrinsicsHandler<T>.LoadVector256(ptr + i);
                        sum = IntrinsicsHandler<T>.AddVector256(current, sum);
                    }

                    result = MathUnsafe<T>.Add(result, IntrinsicsHandler<T>.SumVector(sum));
                }
                else if (Ssse3.IsSupported && MathGeneric.IsSupported<T>())
                {
                    size = Vector128<T>.Count;
                    if (length < size)
                    {
                        return Sum(ptr, length);
                    }
                    var vresult = Vector128<T>.Zero;
                    size = Vector128<T>.Count;
                    lastIndexBlock = length - length % size;
                    for (; i < lastIndexBlock; i += size)
                    {
                        vresult = IntrinsicsHandler<T>.AddVector128(vresult, IntrinsicsHandler<T>.LoadVector128(ptr + i));
                    }

                    result = IntrinsicsHandler<T>.SumVector128(vresult);
                }
#endif
                for (; i < length; i++)
                {
                    result = MathUnsafe<T>.Add(result, *(ptr + i));
                }
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T Sum<T>(T* array, int count)
            where T : unmanaged
        {
            T sum = default;
            for (int i = 0; i < count; i++)
            {
                sum = MathUnsafe<T>.Add(sum, *(array + i));
            }
            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T Sum<T>(T[] array, int count)
            where T : unmanaged
        {
            var ptr = UnsafeHandler.GetReference(array);
            T sum = default;
            for (int i = 0; i < count; i++)
            {
                sum = MathUnsafe<T>.Add(sum, *(ptr + i));
            }
            return sum;
        }
    }
}