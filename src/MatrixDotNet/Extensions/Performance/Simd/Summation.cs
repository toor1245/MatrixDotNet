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
        /// <returns></returns>
        internal static int Sum(int[] array)
        {
            int length = array.Length;
            
            if (length < 1)
            {
                return 0;
            }
            
            int i = 0;
            int result = 0;
            
            fixed (int* pSource = array)
            {
#if NET5_0 || NETCOREAPP3_1
                int size;
                int lastIndexBlock;
                if (Avx2.IsSupported)
                {
                    if (length < 8)
                    {
                        return SumFast(pSource, length);
                    }
                    
                    size = Vector256<int>.Count;
                    lastIndexBlock = length - length % size;
                    var sum = Vector256<int>.Zero;
                    
                    for (; i < lastIndexBlock; i += size)
                    {
                        var current = Avx.LoadVector256(pSource + i);
                        sum = Avx2.Add(current, sum);
                    }
                    
                    var t1 = Avx2.HorizontalAdd(sum, sum);
                    var t2 = Avx2.HorizontalAdd(t1, t1);
                    var t3 = Avx2.ExtractVector128(t2, 1);
                    var t4 = Sse2.Add(t2.GetLower(), t3);
                    result = t4.ToScalar();
                    
                    for (; i < length; i++)
                    {
                        result += SumFast(pSource + i, length - i);
                    }

                    return result;

                }
                
                if (length < 4)
                {
                    return SumFast(pSource, length);
                }
                
                var vresult = Vector128<int>.Zero;
                size = Vector128<int>.Count;
                lastIndexBlock = length - length % size;

                while (i < lastIndexBlock)
                {
                    vresult = Sse2.Add(vresult, Sse2.LoadVector128(pSource + i));
                    i += 4;
                }

                if (Ssse3.IsSupported)
                {
                    vresult = Ssse3.HorizontalAdd(vresult, vresult);
                    vresult = Ssse3.HorizontalAdd(vresult, vresult);
                }
                else
                {
                    vresult = Sse2.Add(vresult, Sse2.Shuffle(vresult, 0x4E));
                    vresult = Sse2.Add(vresult, Sse2.Shuffle(vresult, 0xB1));
                }

                result = vresult.ToScalar();
#endif
                for (; i < length; i++)
                {
                    result += pSource[i];
                }
                
                return result;
            }
            
        }
        
        /// <summary>
        /// Gets sum of array
        /// </summary>
        /// <returns></returns>
        internal static float Sum(float[] array)
        {
            int length = array.Length;
            
            if (length < 1)
            {
                return 0;
            }
            
            int i = 0;
            float result = 0;
            
            fixed (float* pSource = array)
            {
#if NET5_0 || NETCOREAPP3_1
                int size;
                int lastIndexBlock;
                if (Avx2.IsSupported)
                {
                    if (length < 8)
                    {
                        return SumFast(pSource, length);
                    }
                    
                    size = Vector256<float>.Count;
                    lastIndexBlock = length - length % size;
                    var sum = Vector256<float>.Zero;
                    
                    for (; i < lastIndexBlock; i += size)
                    {
                        var current = Avx.LoadVector256(pSource + i);
                        sum = Avx.Add(current, sum);
                    }
                    
                    var t1 = Avx.HorizontalAdd(sum, sum);
                    var t2 = Avx.HorizontalAdd(t1, t1);
                    var t3 = Avx.ExtractVector128(t2, 1);
                    var t4 = Sse.Add(t2.GetLower(), t3);
                    result = t4.ToScalar();
                    
                    for (; i < length; i++)
                    {
                        result += SumFast(pSource + i, length - i);
                    }

                    return result;
                }
                if (Sse3.IsSupported)
                {
                    if (length < 4)
                    {
                        return SumFast(pSource, length);
                    }

                    var vresult = Vector128<float>.Zero;
                    size = Vector128<short>.Count;
                    lastIndexBlock = length - length % size;

                    while (i < lastIndexBlock)
                    {
                        vresult = Sse.Add(vresult, Sse.LoadVector128(pSource + i));
                        i += 4;
                    }
                    
                    vresult = Sse3.HorizontalAdd(vresult, vresult);
                    vresult = Sse3.HorizontalAdd(vresult, vresult);

                    result = vresult.ToScalar();
                }
#endif
                for (; i < length; i++)
                {
                    result += pSource[i];
                }
                
                return result;
            }
            
        }
         
        /// <summary>
        /// Gets sum of array
        /// </summary>
        /// <returns></returns>
        internal static int Sum(short[] array)
        {
            int length = array.Length;
            
            if (length < 1)
            {
                return 0;
            }
            
            int i = 0;
            int result = 0;
            
            fixed (short* pSource = array)
            {
#if NET5_0 || NETCOREAPP3_1
                int size;
                int lastIndexBlock;
                if (Avx2.IsSupported)
                {
                    if (length < 8)
                    {
                        return SumFast(pSource, length);
                    }
                    
                    size = Vector256<short>.Count;
                    lastIndexBlock = length - length % size;
                    var sum = Vector256<short>.Zero;
                    
                    for (; i < lastIndexBlock; i += size)
                    {
                        var current = Avx.LoadVector256(pSource + i);
                        sum = Avx2.Add(current, sum);
                    }
                    
                    var t1 = Avx2.HorizontalAdd(sum, sum);
                    var t2 = Avx2.HorizontalAdd(t1, t1);
                    var t3 = Avx2.ExtractVector128(t2, 1);
                    var t4 = Sse2.Add(t2.GetLower(), t3);
                    result = t4.ToScalar();
                    
                    for (; i < length; i++)
                    {
                        result += SumFast(pSource + i, length - i);
                    }

                    return result;

                }

                if (Ssse3.IsSupported)
                {
                    if (length < 4)
                    {
                        return SumFast(pSource, length);
                    }

                    var vresult = Vector128<short>.Zero;
                    size = Vector128<short>.Count;
                    lastIndexBlock = length - length % size;

                    while (i < lastIndexBlock)
                    {
                        vresult = Sse2.Add(vresult, Sse2.LoadVector128(pSource + i));
                        i += 4;
                    }
                    
                    vresult = Ssse3.HorizontalAdd(vresult, vresult);
                    vresult = Ssse3.HorizontalAdd(vresult, vresult);

                    result = vresult.ToScalar();
                }
#endif
                for (; i < length; i++)
                {
                    result += pSource[i];
                }
                
                return result;
            }
            
        }
        
        /// <summary>
        /// Gets sum of array
        /// </summary>
        /// <returns></returns>
        internal static double Sum(double[] array)
        {
            int length = array.Length;
            
            if (length < 1)
            {
                return 0;
            }
            
            int i = 0;
            double result = 0;
            
            fixed (double* pSource = array)
            {
#if NET5_0 || NETCOREAPP3_1
                int size;
                int lastIndexBlock;
                if (Avx2.IsSupported)
                {
                    if (length < 8)
                    {
                        return SumFast(pSource, length);
                    }
                    
                    size = Vector256<double>.Count;
                    lastIndexBlock = length - length % size;
                    var sum = Vector256<double>.Zero;
                    
                    for (; i < lastIndexBlock; i += size)
                    {
                        var current = Avx.LoadVector256(pSource + i);
                        sum = Avx.Add(current, sum);
                    }
                    
                    var t1 = Avx.HorizontalAdd(sum, sum);
                    var t2 = Avx.HorizontalAdd(t1, t1);
                    var t3 = Avx.ExtractVector128(t2, 1);
                    var t4 = Sse2.Add(t2.GetLower(), t3);
                    result = t4.ToScalar();
                    
                    for (; i < length; i++)
                    {
                        result += SumFast(pSource + i, length - i);
                    }

                    return result;
                }
                if (Sse3.IsSupported)
                {
                    if (length < 4)
                    {
                        return SumFast(pSource, length);
                    }
                    var vresult = Vector128<double>.Zero;
                    size = Vector128<double>.Count;
                    lastIndexBlock = length - length % size;

                    while (i < lastIndexBlock)
                    {
                        vresult = Sse2.Add(vresult, Sse2.LoadVector128(pSource + i));
                        i += 4;
                    }
                    
                    vresult = Sse3.HorizontalAdd(vresult, vresult);
                    vresult = Sse3.HorizontalAdd(vresult, vresult);

                    result = vresult.ToScalar();
                }
#endif
                for (; i < length; i++)
                {
                    result += pSource[i];
                }
                
                return result;
            }
            
        }

        private static T SumFast<T>(T* array, int count)
            where T : unmanaged
        {
            T sum = default;
            for (int i = 0; i < count; i++)
            {
                sum = MathUnsafe<T>.Add(sum, *(array + i));
            }
            return sum;
        }
    }
}