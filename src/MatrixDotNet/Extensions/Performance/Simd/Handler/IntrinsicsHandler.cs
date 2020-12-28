#if NETCOREAPP3_1 || NET5_0
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace MatrixDotNet.Extensions.Performance.Simd.Handler
{
    public static class IntrinsicsHandler<T> where T : struct
    {
        internal static Vector256<T> SetAllBits256
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector256<T>.AllBitsSet;
#elif NETCOREAPP3_1
                return Vector256.Create(0xFFFFFFFF).As<uint, T>();
#endif
            }
        }

        internal static Vector128<T> SetAllBits128
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector128<T>.AllBitsSet;
#elif NETCOREAPP3_1
                return Vector128.Create(0xFFFFFFFF).As<uint, T>();
#endif
            }
        }
    }
}
#endif