#if NETCOREAPP3_1 || NET5_0
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MatrixDotNet.Extensions.Performance.Simd.Handler
{
    public static class IntrinsicsHandler
    {
        internal static Vector256<long> SetAllBits256Int64
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector256<long>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx2.CompareEqual(Vector256<long>.Zero, Vector256<long>.Zero);
#endif
            }
        }

        internal static Vector256<ulong> SetAllBits256UInt64
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector256<ulong>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx2.CompareEqual(Vector256<ulong>.Zero, Vector256<ulong>.Zero);
#endif
            }
        }

        internal static Vector256<int> SetAllBits256Int32
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector256<int>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx2.CompareEqual(Vector256<int>.Zero, Vector256<int>.Zero);
#endif
            }
        }

        internal static Vector256<uint> SetAllBits256UInt32
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector256<uint>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx2.CompareEqual(Vector256<uint>.Zero, Vector256<uint>.Zero);
#endif
            }
        }

        internal static Vector256<short> SetAllBits256Int16
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector256<short>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx2.CompareEqual(Vector256<short>.Zero, Vector256<short>.Zero);
#endif
            }
        }

        internal static Vector256<ushort> SetAllBits256UInt16
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector256<ushort>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx2.CompareEqual(Vector256<ushort>.Zero, Vector256<ushort>.Zero);
#endif
            }
        }

        internal static Vector256<byte> SetAllBits256Byte
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector256<byte>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx2.CompareEqual(Vector256<byte>.Zero, Vector256<byte>.Zero);
#endif
            }
        }

        internal static Vector256<sbyte> SetAllBits256SByte
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector256<sbyte>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx2.CompareEqual(Vector256<sbyte>.Zero, Vector256<sbyte>.Zero);
#endif
            }
        }

        internal static Vector256<float> SetAllBits256Single
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector256<float>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx.Compare(Vector256<float>.Zero, Vector256<float>.Zero,
                    FloatComparisonMode.OrderedNonSignaling);
#endif
            }
        }

        internal static Vector256<double> SetAllBits256Double
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector256<double>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx.Compare(Vector256<double>.Zero, Vector256<double>.Zero,
                    FloatComparisonMode.OrderedNonSignaling);
#endif
            }
        }

        internal static Vector128<long> SetAllBits128Int64
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector128<long>.AllBitsSet;
#elif NETCOREAPP3_1
                return Sse41.CompareEqual(Vector128<long>.Zero, Vector128<long>.Zero);
#endif
            }
        }

        internal static Vector128<ulong> SetAllBits128UInt64
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector128<ulong>.AllBitsSet;
#elif NETCOREAPP3_1
                return Sse41.CompareEqual(Vector128<ulong>.Zero, Vector128<ulong>.Zero);
#endif
            }
        }

        internal static Vector128<int> SetAllBits128Int32
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector128<int>.AllBitsSet;
#elif NETCOREAPP3_1
                return Sse2.CompareEqual(Vector128<int>.Zero, Vector128<int>.Zero);
#endif
            }
        }

        internal static Vector128<uint> SetAllBits128UInt32
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector128<uint>.AllBitsSet;
#elif NETCOREAPP3_1
                return Sse2.CompareEqual(Vector128<uint>.Zero, Vector128<uint>.Zero);
#endif
            }
        }

        internal static Vector128<short> SetAllBits128Int16
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector128<short>.AllBitsSet;
#elif NETCOREAPP3_1
                return Sse2.CompareEqual(Vector128<short>.Zero, Vector128<short>.Zero);
#endif
            }
        }

        internal static Vector128<ushort> SetAllBits128UInt16
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector128<ushort>.AllBitsSet;
#elif NETCOREAPP3_1
                return Sse2.CompareEqual(Vector128<ushort>.Zero, Vector128<ushort>.Zero);
#endif
            }
        }

        internal static Vector128<byte> SetAllBits128Byte
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector128<byte>.AllBitsSet;
#elif NETCOREAPP3_1
                return Sse2.CompareEqual(Vector128<byte>.Zero, Vector128<byte>.Zero);
#endif
            }
        }

        internal static Vector128<sbyte> SetAllBits128SByte
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector128<sbyte>.AllBitsSet;
#elif NETCOREAPP3_1
                return Sse2.CompareEqual(Vector128<sbyte>.Zero, Vector128<sbyte>.Zero);
#endif
            }
        }

        internal static Vector128<float> SetAllBits128Single
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector128<float>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx.Compare(Vector128<float>.Zero, Vector128<float>.Zero,
                    FloatComparisonMode.OrderedNonSignaling);
#endif
            }
        }

        internal static Vector128<double> SetAllBits128Double
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
#if NET5_0
                return Vector128<double>.AllBitsSet;
#elif NETCOREAPP3_1
                return Avx.Compare(Vector128<double>.Zero, Vector128<double>.Zero,
                    FloatComparisonMode.OrderedNonSignaling);
#endif
            }
        }
    }
}
#endif