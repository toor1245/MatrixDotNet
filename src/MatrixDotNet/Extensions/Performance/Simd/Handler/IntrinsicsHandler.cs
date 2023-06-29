#if NETCOREAPP3_1 || NET5_0
using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Performance.Simd.Handler
{
    internal static class IntrinsicsHandler<T> where T : unmanaged
    {
        #region Vector256

        private static Vector256<int> _blockMultiplyMask = Vector256.Create(16, 32, 64, 128, 256, 512, 1024, 2048);

        /// <summary>
        /// Determines whether a sequence contains a specified value through SIMD.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool ContainsMultiplyMask(int value)
        {
            var vector = Vector256.Create(value);
            var mask = Avx2.CompareEqual(vector, _blockMultiplyMask);
            return !Avx.TestZ(mask, mask);
        }

        /// <summary>
        /// Determines whether a specified value can multiply through SIMD.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool CanMultiply(int value) => Avx2.IsSupported && IsSupportedMultiplyAddVector256 && ContainsMultiplyMask(value);

        /// <summary>
        /// Gets a new Vector256<T/> with all bits set to 1.
        /// </summary>
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

        /// <summary>__m256i _mm256_permute2x128_si256 (__m256i a, __m256i b, const int imm8)</summary>
        /// <remarks>VPERM2I128 ymm, ymm, ymm/m256, imm8</remarks>
        /// <remarks>Shuffle 128-bits (composed of integer data) selected by imm8 from a and b, and store the results in dst.</remarks>
        /// <remarks>Supports: AVX(float, double), AVX2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> Permute2X128Vector256(Vector256<T> va, Vector256<T> vb, byte control)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Avx2.Permute2x128(va.As<T, sbyte>(), vb.As<T, sbyte>(), control).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Avx2.Permute2x128(va.As<T, byte>(), vb.As<T, byte>(), control).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Avx2.Permute2x128(va.As<T, short>(), vb.As<T, short>(), control).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Avx2.Permute2x128(va.As<T, ushort>(), vb.As<T, ushort>(), control).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Avx2.Permute2x128(va.As<T, int>(), vb.As<T, int>(), control).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Avx2.Permute2x128(va.As<T, uint>(), vb.As<T, uint>(), control).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Avx2.Permute2x128(va.As<T, long>(), vb.As<T, long>(), control).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Avx2.Permute2x128(va.As<T, ulong>(), vb.As<T, ulong>(), control).As<ulong, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Avx.Permute2x128(va.As<T, float>(), vb.As<T, float>(), control).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Avx.Permute2x128(va.As<T, double>(), vb.As<T, double>(), control).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets sum of Vector256
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T SumVector(Vector256<T> a)
        {
            var sum = default(T);
            for (var i = 0; i < Vector256<T>.Count; i++)
            {
                sum = MathUnsafe<T>.Add(sum, a.GetElement(i));
            }
            return sum;
        }

        /// <summary>
        /// Gets sum of Vector128
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T SumVector(Vector128<T> a)
        {
            var sum = default(T);
            for (var i = 0; i < Vector128<T>.Count; i++)
            {
                sum = MathUnsafe<T>.Add(sum, a.GetElement(i));
            }
            return sum;
        }

        /// <summary>__m256X _mm256_add_epiX (__m256X a, __m256X b)</summary>
        /// <remarks>VPADDB ymm, ymm, ymm/m256</remarks>
        /// <remarks>Description: Add packed 8-bit integers in a and b, and store the results in dst.</remarks>
        /// <remarks>Supports: AVX(float, double), AVX2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> AddVector256(Vector256<T> va, Vector256<T> vb)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Avx2.Add(va.As<T, sbyte>(), vb.As<T, sbyte>()).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Avx2.Add(va.As<T, byte>(), vb.As<T, byte>()).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Avx2.Add(va.As<T, short>(), vb.As<T, short>()).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Avx2.Add(va.As<T, ushort>(), vb.As<T, ushort>()).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Avx2.Add(va.As<T, int>(), vb.As<T, int>()).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Avx2.Add(va.As<T, uint>(), vb.As<T, uint>()).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Avx2.Add(va.As<T, long>(), vb.As<T, long>()).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Avx2.Add(va.As<T, ulong>(), vb.As<T, ulong>()).As<ulong, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Avx.Add(va.As<T, float>(), vb.As<T, float>()).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Avx.Add(va.As<T, double>(), vb.As<T, double>()).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m256i _mm256_shuffle_epi8 (__m256i a, __m256i b)</summary>
        /// <remarks>VPSHUFB ymm, ymm, ymm/m256</remarks>
        /// <remarks>
        /// Description: Shuffle 8-bit integers in a within 128-bit lanes according to shuffle control mask
        /// in the corresponding 8-bit element of b, and store the results in dst.
        /// </remarks>
        /// <remarks>Supports: AVX2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> ShuffleVector256(Vector256<T> va, Vector256<T> vb)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Avx2.Shuffle(va.As<T, sbyte>(), vb.As<T, sbyte>()).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Avx2.Shuffle(va.As<T, byte>(), vb.As<T, byte>()).As<byte, T>();
            }

            throw new NotSupportedException();
        }


        /// <summary>__m256i _mm256_shuffle_epi32 (__m256i a, const int imm8)</summary>
        /// <remarks>VPSHUFD ymm, ymm/m256, imm8</remarks>
        /// <remarks>
        /// Description: Shuffle 8-bit integers in a within 128-bit lanes according to shuffle control mask
        /// in the corresponding 8-bit element of b, and store the results in dst.
        /// </remarks>
        /// <remarks>Supports: AVX2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> ShuffleVector256(Vector256<T> va, byte control)
        {
            if (typeof(T) == typeof(int))
            {
                return Avx2.Shuffle(va.As<T, int>(), control).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Avx2.Shuffle(va.As<T, uint>(), control).As<uint, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m256 _mm256_shuffle_ps (__m256 a, __m256 b, const int imm8)</summary>
        /// <remarks>VSHUFPS ymm, ymm, ymm/m256, imm8</remarks>
        /// <remarks>
        /// Description: Shuffle 8-bit integers in a within 128-bit lanes according to shuffle control mask
        /// in the corresponding 8-bit element of b, and store the results in dst.
        /// </remarks>
        /// <remarks>Supports: AVX(float, double)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> ShuffleVector256(Vector256<T> va, Vector256<T> vb, byte control)
        {
            if (typeof(T) == typeof(float))
            {
                return Avx.Shuffle(va.As<T, float>(), vb.As<T, float>(), control).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Avx.Shuffle(va.As<T, double>(), vb.As<T, double>(), control).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m256X _mm256_add_epiX (__m256X a, __m256X b)</summary>
        /// <remarks>VPADDB ymm, ymm, ymm/m256</remarks>
        /// <remarks>Description: Shuffle 64-bit integers in a across lanes using the control in imm8, and store the results in dst.</remarks>
        /// <remarks>Supports: AVX(float, double), AVX2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> Permute4X64Vector256(Vector256<T> va, byte control)
        {
            if (typeof(T) == typeof(long))
            {
                return Avx2.Permute4x64(va.As<T, long>(), control).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Avx2.Permute4x64(va.As<T, ulong>(), control).As<ulong, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Avx2.Permute4x64(va.As<T, double>(), control).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m256i _mm256_inserti128_si256 (__m256i a, __m128i b, const int imm8)</summary>
        /// <remarks>VINSERTI128 ymm, ymm, xmm, imm8</remarks>
        /// <remarks>Description: Copy a to dst, then insert 128 bits (composed of integer data) from b into dst at the location specified by imm8.</remarks>
        /// <remarks>Supports: AVX2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> Insert128Vector256(Vector256<T> value, Vector128<T> data, byte index)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Avx2.InsertVector128(value.As<T, sbyte>(), data.As<T, sbyte>(), index).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Avx2.InsertVector128(value.As<T, byte>(), data.As<T, byte>(), index).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Avx2.InsertVector128(value.As<T, short>(), data.As<T, short>(), index).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Avx2.InsertVector128(value.As<T, ushort>(), data.As<T, ushort>(), index).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Avx2.InsertVector128(value.As<T, int>(), data.As<T, int>(), index).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Avx2.InsertVector128(value.As<T, uint>(), data.As<T, uint>(), index).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Avx2.InsertVector128(value.As<T, long>(), data.As<T, long>(), index).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Avx2.InsertVector128(value.As<T, ulong>(), data.As<T, ulong>(), index).As<ulong, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Avx.InsertVector128(value.As<T, float>(), data.As<T, float>(), index).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Avx.InsertVector128(value.As<T, double>(), data.As<T, double>(), index).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m256i _mm256_unpacklo_epi8 (__m256i a, __m256i b)</summary>
        /// <remarks>VPUNPCKLBW ymm, ymm, ymm/m25</remarks>
        /// <remarks>
        /// Description: Unpack and interleave 8-bit integers from the low half of each 128-bit
        /// lane in a and b, and store the results in dst.
        /// </remarks>
        /// <remarks>Supports: AVX2, Avx(float, double)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> UnpackLowVector256(Vector256<T> value, Vector256<T> data)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Avx2.UnpackLow(value.As<T, sbyte>(), data.As<T, sbyte>()).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Avx2.UnpackLow(value.As<T, byte>(), data.As<T, byte>()).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Avx2.UnpackLow(value.As<T, short>(), data.As<T, short>()).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Avx2.UnpackLow(value.As<T, ushort>(), data.As<T, ushort>()).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Avx2.UnpackLow(value.As<T, int>(), data.As<T, int>()).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Avx2.UnpackLow(value.As<T, uint>(), data.As<T, uint>()).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Avx2.UnpackLow(value.As<T, long>(), data.As<T, long>()).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Avx2.UnpackLow(value.As<T, ulong>(), data.As<T, ulong>()).As<ulong, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Avx.UnpackLow(value.As<T, float>(), data.As<T, float>()).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Avx.UnpackLow(value.As<T, double>(), data.As<T, double>()).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m256i _mm256_unpackhi_epi8 (__m256i a, __m256i b)</summary>
        /// <remarks>VPUNPCKLBW ymm, ymm, ymm/m25</remarks>
        /// <remarks>Description: Unpack and interleave 8-bit integers from the high half of each 128-bit lane in a and b, and store the results in dst.</remarks>
        /// <remarks>Supports: AVX2, Avx(float, double)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> UnpackHighVector256(Vector256<T> value, Vector256<T> data)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Avx2.UnpackHigh(value.As<T, sbyte>(), data.As<T, sbyte>()).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Avx2.UnpackHigh(value.As<T, byte>(), data.As<T, byte>()).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Avx2.UnpackHigh(value.As<T, short>(), data.As<T, short>()).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Avx2.UnpackHigh(value.As<T, ushort>(), data.As<T, ushort>()).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Avx2.UnpackHigh(value.As<T, int>(), data.As<T, int>()).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Avx2.UnpackHigh(value.As<T, uint>(), data.As<T, uint>()).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Avx2.UnpackHigh(value.As<T, long>(), data.As<T, long>()).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Avx2.UnpackHigh(value.As<T, ulong>(), data.As<T, ulong>()).As<ulong, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Avx.UnpackHigh(value.As<T, float>(), data.As<T, float>()).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Avx.UnpackHigh(value.As<T, double>(), data.As<T, double>()).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m256i _mm256_sub_epi8 (__m256i a, __m256i b)</summary>
        /// <remarks>VPSUBB ymm, ymm, ymm/m256</remarks>
        /// <remarks>Description: Subtract packed 8-bit integers in b from packed 8-bit integers in a, and store the results in dst.</remarks>
        /// <remarks>Supports: AVX(float, double), AVX2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> SubtractVector256(Vector256<T> va, Vector256<T> vb)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Avx2.Subtract(va.As<T, sbyte>(), vb.As<T, sbyte>()).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Avx2.Subtract(va.As<T, byte>(), vb.As<T, byte>()).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Avx2.Subtract(va.As<T, short>(), vb.As<T, short>()).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Avx2.Subtract(va.As<T, ushort>(), vb.As<T, ushort>()).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Avx2.Subtract(va.As<T, int>(), vb.As<T, int>()).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Avx2.Subtract(va.As<T, uint>(), vb.As<T, uint>()).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Avx2.Subtract(va.As<T, long>(), vb.As<T, long>()).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Avx2.Subtract(va.As<T, ulong>(), vb.As<T, ulong>()).As<ulong, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Avx.Subtract(va.As<T, float>(), vb.As<T, float>()).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Avx.Subtract(va.As<T, double>(), vb.As<T, double>()).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m256i _mm256_loadu_si256 (__m256i const * mem_addr)</summary>
        /// <remarks>VMOVDQU ymm, m256</remarks>
        /// <remarks>Description: Load 256-bits of integer data from memory into dst. mem_addr does not need to be aligned on any particular boundary.</remarks>
        /// <remarks>Supports: AVX</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<T> LoadVector256(T* address)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Avx.LoadVector256((sbyte*) address).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Avx.LoadVector256((byte*) address).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Avx.LoadVector256((short*) address).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Avx.LoadVector256((ushort*) address).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Avx.LoadVector256((int*) address).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Avx.LoadVector256((uint*) address).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Avx.LoadVector256((long*) address).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Avx.LoadVector256((ulong*) address).As<ulong, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Avx.LoadVector256((float*) address).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Avx.LoadVector256((double*) address).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m256i _mm256_lddqu_si256 (__m256i const * mem_addr) </summary>
        /// <remarks>VLDDQU ymm, m256</remarks>
        /// <remarks>Description: Load 256-bits of integer data from memory into dst. mem_addr does not need to be aligned on any particular boundary.</remarks>
        /// <remarks>Supports: AVX</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector256<T> LoadDquVector256(T* address)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Avx.LoadDquVector256((sbyte*) address).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Avx.LoadDquVector256((byte*) address).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Avx.LoadDquVector256((short*) address).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Avx.LoadDquVector256((ushort*) address).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Avx.LoadDquVector256((int*) address).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Avx.LoadDquVector256((uint*) address).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Avx.LoadDquVector256((long*) address).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Avx.LoadDquVector256((ulong*) address).As<ulong, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>void _mm256_storeu_si256 (__m256i * mem_addr, __m256i a)</summary>
        /// <remarks>MOVDQU m256, ymm</remarks>
        /// <remarks>Description: Store 256-bits of integer data from a into memory. mem_addr does not need to be aligned on any particular boundary.</remarks>
        /// <remarks>Supports: AVX</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void StoreVector256(T* address, Vector256<T> vector256)
        {
            if (typeof(T) == typeof(sbyte))
            {
                Avx.Store((sbyte*) address, vector256.As<T, sbyte>());
            }
            else if (typeof(T) == typeof(byte))
            {
                Avx.Store((byte*) address, vector256.As<T, byte>());
            }
            else if (typeof(T) == typeof(short))
            {
                Avx.Store((short*) address, vector256.As<T, short>());
            }
            else if (typeof(T) == typeof(ushort))
            {
                Avx.Store((ushort*) address, vector256.As<T, ushort>());
            }
            else if (typeof(T) == typeof(int))
            {
                Avx.Store((int*) address, vector256.As<T, int>());
            }
            else if (typeof(T) == typeof(uint))
            {
                Avx.Store((uint*) address, vector256.As<T, uint>());
            }
            else if (typeof(T) == typeof(long))
            {
                Avx.Store((long*) address, vector256.As<T, long>());
            }
            else if (typeof(T) == typeof(ulong))
            {
                Avx.Store((ulong*) address, vector256.As<T, ulong>());
            }
            else if (typeof(T) == typeof(float))
            {
                Avx.Store((float*) address, vector256.As<T, float>());
            }
            else if (typeof(T) == typeof(double))
            {
                Avx.Store((double*) address, vector256.As<T, double>());
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Creates a new Vector256<T/> instance with all elements initialized to the specified value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> CreateVector256(T value)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Vector256.Create(Unsafe.As<T, sbyte>(ref value)).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Vector256.Create(Unsafe.As<T, byte>(ref value)).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Vector256.Create(Unsafe.As<T, short>(ref value)).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Vector256.Create(Unsafe.As<T, ushort>(ref value)).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Vector256.Create(Unsafe.As<T, int>(ref value)).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Vector256.Create(Unsafe.As<T, uint>(ref value)).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Vector256.Create(Unsafe.As<T, long>(ref value)).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Vector256.Create(Unsafe.As<T, ulong>(ref value)).As<ulong, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Vector256.Create(Unsafe.As<T, float>(ref value)).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Vector256.Create(Unsafe.As<T, double>(ref value)).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m256 _mm256_fmadd_ps (__m256 a, __m256 b, __m256 c)</summary>
        /// <remarks>VFMADDPS ymm, ymm, ymm/m256</remarks>
        /// <remarks>
        /// Description: Multiply packed double-precision (64-bit) floating-point elements in a and b,
        /// add the intermediate result to packed elements in c, and store the results in dst.
        /// </remarks>
        /// <remarks>Supports: AVX2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector256<T> MultiplyAddVector256(Vector256<T> a, Vector256<T> b, Vector256<T> c)
        {
            if (typeof(T) == typeof(int))
            {
                var va = a.As<T, int>();
                var vb = b.As<T, int>();
                var vl = Avx2.MultiplyLow(va, vb);
                var vh = Sse41.MultiplyLow(va.GetUpper(), vb.GetUpper());
                return Avx2.Add(Vector256.Create(vl.GetLower(), vh), c.As<T, int>()).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                var va = a.As<T, uint>();
                var vb = b.As<T, uint>();
                var vl = Avx2.MultiplyLow(va, vb);
                var vh = Sse41.MultiplyLow(va.GetUpper(), vb.GetUpper());
                return Avx2.Add(Vector256.Create(vl.GetLower(), vh), c.As<T, uint>()).As<uint, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Fma.MultiplyAdd(a.As<T, float>(), b.As<T, float>(), c.As<T, float>()).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Fma.MultiplyAdd(a.As<T, double>(), b.As<T, double>(), c.As<T, double>()).As<double, T>();
            }

            throw new NotSupportedException();
        }

        internal static bool IsSupportedMultiplyAddVector256
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get =>
                typeof(T) == typeof(int) ||
                typeof(T) == typeof(uint) ||
                typeof(T) == typeof(float) ||
                typeof(T) == typeof(double);
        }

        #endregion

        #region Vector128

        /// <summary>_m128i _mm_sub_epiX (__m128i a, __m128i b)</summary>
        /// <remarks>PSUBW xmm, xmm/m128</remarks>
        /// <remarks>Description: Subtract packed X-bit integers in b from packed X-bit integers in a, and store the results in dst.</remarks>
        /// <remarks>Supports: SSE2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<T> SubtractVector128(Vector128<T> va, Vector128<T> vb)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Sse2.Subtract(va.As<T, sbyte>(), vb.As<T, sbyte>()).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Sse2.Subtract(va.As<T, byte>(), vb.As<T, byte>()).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Sse2.Subtract(va.As<T, short>(), vb.As<T, short>()).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Sse2.Subtract(va.As<T, ushort>(), vb.As<T, ushort>()).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Sse2.Subtract(va.As<T, int>(), vb.As<T, int>()).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Sse2.Subtract(va.As<T, uint>(), vb.As<T, uint>()).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Sse2.Subtract(va.As<T, long>(), vb.As<T, long>()).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Sse2.Subtract(va.As<T, ulong>(), vb.As<T, ulong>()).As<ulong, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m128i _mm_lddqu_si128 (__m128i const* mem_addr) </summary>
        /// <remarks>LDDQU xmm, m128</remarks>
        /// <remarks>Description: Load 128-bits of integer data from memory into dst. mem_addr does not need to be aligned on any particular boundary.</remarks>
        /// <remarks>Supports: SSE3</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector128<T> LoadDquVector128(T* address)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Sse3.LoadDquVector128((sbyte*) address).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Sse3.LoadDquVector128((byte*) address).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Sse3.LoadDquVector128((short*) address).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Sse3.LoadDquVector128((ushort*) address).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Sse3.LoadDquVector128((int*) address).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Sse3.LoadDquVector128((uint*) address).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Sse3.LoadDquVector128((long*) address).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Sse3.LoadDquVector128((ulong*) address).As<ulong, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m128X _mm_add_epiX (__m128X a, __m128X b)</summary>
        /// <remarks>PADDB xmm, xmm/m128</remarks>
        /// <remarks>Description: Addition packed X-bit integers in b from packed X-bit integers in a, and store the results in dst.</remarks>
        /// <remarks>Supports: SSE2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<T> AddVector128(Vector128<T> va, Vector128<T> vb)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Sse2.Add(va.As<T, sbyte>(), vb.As<T, sbyte>()).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Sse2.Add(va.As<T, byte>(), vb.As<T, byte>()).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Sse2.Add(va.As<T, short>(), vb.As<T, short>()).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Sse2.Add(va.As<T, ushort>(), vb.As<T, ushort>()).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Sse2.Add(va.As<T, int>(), vb.As<T, int>()).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Sse2.Add(va.As<T, uint>(), vb.As<T, uint>()).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Sse2.Add(va.As<T, long>(), vb.As<T, long>()).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Sse2.Add(va.As<T, ulong>(), vb.As<T, ulong>()).As<ulong, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Sse.Add(va.As<T, float>(), vb.As<T, float>()).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Sse2.Add(va.As<T, double>(), vb.As<T, double>()).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// __m128i _mm_loadu_si128 (__m128i const* mem_address)
        /// </summary>
        /// <remarks>MOVDQU xmm, m12</remarks>
        /// <remarks>Description: Load 128-bits of integer data from memory into dst. mem_addr does not need to be aligned on any particular boundary.</remarks>
        /// <remarks>Supports: SSE(float), SSE2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe Vector128<T> LoadVector128(T* address)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Sse2.LoadVector128((sbyte*) address).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Sse2.LoadVector128((byte*) address).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Sse2.LoadVector128((short*) address).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Sse2.LoadVector128((ushort*) address).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Sse2.LoadVector128((int*) address).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Sse2.LoadVector128((uint*) address).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Sse2.LoadVector128((long*) address).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Sse2.LoadVector128((ulong*) address).As<ulong, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Sse.LoadVector128((float*) address).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Sse2.LoadVector128((double*) address).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// void _mm_storeu_si128 (__m128i* mem_addr, __m128i a)
        /// </summary>
        /// <remarks>MOVDQU m128, xmm</remarks>
        /// <remarks>
        /// Description: Store 128-bits of integer data from a into memory.
        /// mem_addr does not need to be aligned on any particular boundary.
        /// </remarks>
        /// <remarks>Supports: SSE(float), SSE2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void StoreVector128(T* address, Vector128<T> vector256)
        {
            if (typeof(T) == typeof(sbyte))
            {
                Sse2.Store((sbyte*) address, vector256.As<T, sbyte>());
            }
            else if (typeof(T) == typeof(byte))
            {
                Sse2.Store((byte*) address, vector256.As<T, byte>());
            }
            else if (typeof(T) == typeof(short))
            {
                Sse2.Store((short*) address, vector256.As<T, short>());
            }
            else if (typeof(T) == typeof(ushort))
            {
                Sse2.Store((ushort*) address, vector256.As<T, ushort>());
            }
            else if (typeof(T) == typeof(int))
            {
                Sse2.Store((int*) address, vector256.As<T, int>());
            }
            else if (typeof(T) == typeof(uint))
            {
                Sse2.Store((uint*) address, vector256.As<T, uint>());
            }
            else if (typeof(T) == typeof(long))
            {
                Sse2.Store((long*) address, vector256.As<T, long>());
            }
            else if (typeof(T) == typeof(ulong))
            {
                Sse2.Store((ulong*) address, vector256.As<T, ulong>());
            }
            else if (typeof(T) == typeof(float))
            {
                Sse.Store((float*) address, vector256.As<T, float>());
            }
            else if (typeof(T) == typeof(double))
            {
                Sse2.Store((double*) address, vector256.As<T, double>());
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// Gets a new Vector128<T/> with all bits set to 1.
        /// </summary>
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

        /// <summary>
        /// Creates a new Vector128<T/> instance with all elements initialized to the specified value.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<T> CreateVector128(T value)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return Vector128.Create(Unsafe.As<T, sbyte>(ref value)).As<sbyte, T>();
            }
            if (typeof(T) == typeof(byte))
            {
                return Vector128.Create(Unsafe.As<T, byte>(ref value)).As<byte, T>();
            }
            if (typeof(T) == typeof(short))
            {
                return Vector128.Create(Unsafe.As<T, short>(ref value)).As<short, T>();
            }
            if (typeof(T) == typeof(ushort))
            {
                return Vector128.Create(Unsafe.As<T, ushort>(ref value)).As<ushort, T>();
            }
            if (typeof(T) == typeof(int))
            {
                return Vector128.Create(Unsafe.As<T, int>(ref value)).As<int, T>();
            }
            if (typeof(T) == typeof(uint))
            {
                return Vector128.Create(Unsafe.As<T, uint>(ref value)).As<uint, T>();
            }
            if (typeof(T) == typeof(long))
            {
                return Vector128.Create(Unsafe.As<T, long>(ref value)).As<long, T>();
            }
            if (typeof(T) == typeof(ulong))
            {
                return Vector128.Create(Unsafe.As<T, ulong>(ref value)).As<ulong, T>();
            }
            if (typeof(T) == typeof(float))
            {
                return Vector128.Create(Unsafe.As<T, float>(ref value)).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Vector128.Create(Unsafe.As<T, double>(ref value)).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>Gets sum by GetElement or __m128i _mm_hadd_epi16 (__m128i a, __m128i b)</summary>
        /// <remarks>PHADDW xmm, xmm/m128</remarks>
        /// <remarks>Supports: Ssse3(int, short), Sse3(float, double)</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static T SumVector128(Vector128<T> va)
        {
            if (typeof(T) == typeof(sbyte))
            {
                return SumVector(va);
            }
            if (typeof(T) == typeof(byte))
            {
                return SumVector(va);
            }
            if (typeof(T) == typeof(short))
            {
                var sum = va.As<T, short>();
                sum = Ssse3.HorizontalAdd(sum, sum);
                sum = Ssse3.HorizontalAdd(sum, sum);
                sum = Ssse3.HorizontalAdd(sum, sum);
                return sum.As<short, T>().ToScalar();
            }
            if (typeof(T) == typeof(ushort))
            {
                return SumVector(va);
            }
            if (typeof(T) == typeof(int))
            {
                var sum = va.As<T, int>();
                sum = Ssse3.HorizontalAdd(sum, sum);
                sum = Ssse3.HorizontalAdd(sum, sum);
                return sum.As<int, T>().ToScalar();
            }
            if (typeof(T) == typeof(uint))
            {
                return SumVector(va);
            }
            if (typeof(T) == typeof(long))
            {
                return SumVector(va);
            }
            if (typeof(T) == typeof(ulong))
            {
                return SumVector(va);
            }
            if (typeof(T) == typeof(float))
            {
                var sum = va.As<T, float>();
                sum = Sse3.HorizontalAdd(sum, sum);
                sum = Sse3.HorizontalAdd(sum, sum);
                return sum.As<float, T>().ToScalar();
            }
            if (typeof(T) == typeof(double))
            {
                var sum = va.As<T, double>();
                sum = Sse3.HorizontalAdd(sum, sum);
                return sum.As<double, T>().ToScalar();
            }

            throw new NotSupportedException();
        }

        /// <summary>__m128 _mm_fmadd_ps (__m128 a, __m128 b, __m128 c)</summary>
        /// <remarks>VFMADDPS xmm, xmm, xmm/m128 </remarks>
        /// <remarks>
        /// Description: Multiply packed single-precision (32-bit) floating-point elements in a and b,
        /// add the intermediate result to packed elements in c, and store the results in dst.
        /// </remarks>
        /// <remarks> Supports: FMA, AVX</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static Vector128<T> MultiplyAddVector128(Vector128<T> a, Vector128<T> b, Vector128<T> c)
        {
            if (typeof(T) == typeof(float))
            {
                return Fma.MultiplyAdd(a.As<T, float>(), b.As<T, float>(), c.As<T, float>()).As<float, T>();
            }
            if (typeof(T) == typeof(double))
            {
                return Fma.MultiplyAdd(a.As<T, double>(), b.As<T, double>(), c.As<T, double>()).As<double, T>();
            }

            throw new NotSupportedException();
        }

        /// <summary>Transpose array</summary>
        /// <remarks>Supports: AVX2</remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static unsafe void TransposeVector256(T* ptrM, T* ptrT)
        {
            if (typeof(T) == typeof(float))
            {
                var ymm0 = Insert128Vector256(LoadVector256(ptrM + 0 * 8 + 0), LoadVector128(ptrM + 4 * 8 + 0), 0x1);
                var ymm1 = Insert128Vector256(LoadVector256(ptrM + 1 * 8 + 0), LoadVector128(ptrM + 5 * 8 + 0), 0x1);
                var ymm2 = Insert128Vector256(LoadVector256(ptrM + 2 * 8 + 0), LoadVector128(ptrM + 6 * 8 + 0), 0x1);
                var ymm3 = Insert128Vector256(LoadVector256(ptrM + 3 * 8 + 0), LoadVector128(ptrM + 7 * 8 + 0), 0x1);

                var ymm4 = Insert128Vector256(LoadVector256(ptrM + 0 * 8 + 4), LoadVector128(ptrM + 4 * 8 + 4), 0x1);
                var ymm5 = Insert128Vector256(LoadVector256(ptrM + 1 * 8 + 4), LoadVector128(ptrM + 5 * 8 + 4), 0x1);
                var ymm6 = Insert128Vector256(LoadVector256(ptrM + 2 * 8 + 4), LoadVector128(ptrM + 6 * 8 + 4), 0x1);
                var ymm7 = Insert128Vector256(LoadVector256(ptrM + 3 * 8 + 4), LoadVector128(ptrM + 7 * 8 + 4), 0x1);

                var ymm8 = UnpackLowVector256(ymm0, ymm1);
                var ymm9 = UnpackHighVector256(ymm0, ymm1);
                var ymm10 = UnpackLowVector256(ymm2, ymm3);
                var ymm11 = UnpackHighVector256(ymm2, ymm3);

                var ymm12 = UnpackLowVector256(ymm4, ymm5);
                var ymm13 = UnpackHighVector256(ymm4, ymm5);
                var ymm14 = UnpackLowVector256(ymm6, ymm7);
                var ymm15 = UnpackHighVector256(ymm6, ymm7);

                ymm0 = ShuffleVector256(ymm8, ymm10, 0x44);
                ymm1 = ShuffleVector256(ymm8, ymm10, 0xEE);
                ymm2 = ShuffleVector256(ymm9, ymm11, 0x44);
                ymm3 = ShuffleVector256(ymm9, ymm11, 0xEE);

                ymm4 = ShuffleVector256(ymm12, ymm14, 0x44);
                ymm5 = ShuffleVector256(ymm12, ymm14, 0xEE);
                ymm6 = ShuffleVector256(ymm13, ymm15, 0x44);
                ymm7 = ShuffleVector256(ymm13, ymm15, 0xEE);

                StoreVector256(ptrT + 0 * 8, ymm0);
                StoreVector256(ptrT + 1 * 8, ymm1);
                StoreVector256(ptrT + 2 * 8, ymm2);
                StoreVector256(ptrT + 3 * 8, ymm3);

                StoreVector256(ptrT + 4 * 8, ymm4);
                StoreVector256(ptrT + 5 * 8, ymm5);
                StoreVector256(ptrT + 6 * 8, ymm6);
                StoreVector256(ptrT + 7 * 8, ymm7);
                return;
            }

            throw new NotSupportedException();
        }

        internal static bool IsSupportedMultiplyAddVector128
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get =>
                typeof(T) == typeof(float) ||
                typeof(T) == typeof(double);
        }

        #endregion
    }
}
#endif
