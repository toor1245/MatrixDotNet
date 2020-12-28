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
                return Vector256.Create(0xFFFFFFFF).AsInt64();
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
                return Vector256.Create(0xFFFFFFFF).AsUInt64();
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
                return Vector256.Create(0xFFFFFFFF).AsInt32();
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
                return Vector256.Create(0xFFFFFFFF).AsUInt32();
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
                return Vector256.Create(0xFFFFFFFF).AsInt16();
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
                return Vector256.Create(0xFFFFFFFF).AsUInt16();
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
                return Vector256.Create(0xFFFFFFFF).AsByte();
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
                return Vector256.Create(0xFFFFFFFF).AsSByte();
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
                return Vector256.Create(0xFFFFFFFF).AsSingle();
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
                return Vector256.Create(0xFFFFFFFF).AsDouble();
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
                return Vector128.Create(0xFFFFFFFF).AsInt64();
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
                return Vector128.Create(0xFFFFFFFF).AsUInt64();
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
                return Vector128.Create(0xFFFFFFFF).AsInt32();
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
                return Vector128.Create(0xFFFFFFFF).AsUInt32();
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
                return Vector128.Create(0xFFFFFFFF).AsInt16();
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
                return Vector128.Create(0xFFFFFFFF).AsUInt16();
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
                return Vector128.Create(0xFFFFFFFF).AsByte();
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
                return Vector128.Create(0xFFFFFFFF).AsSByte();
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
                return Vector128.Create(0xFFFFFFFF).AsSingle();
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
                return Vector128.Create(0xFFFFFFFF).AsDouble();
#endif
            }
        }
    }
}
#endif