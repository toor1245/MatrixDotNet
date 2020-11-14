using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MatrixDotNet.Extensions.Core.Simd
{
    public static partial class Simd
    {
        public static unsafe int SumAll(this Matrix<int> matrix)
        {
            int result;

            fixed (int* pSource = matrix.GetArray())
            {
                var source = new Span<int>(pSource, matrix.Length);
                var i = 0;
                var size = source.Length - 8;

                if (Avx2.IsSupported)
                {
                    var vresult = Vector256<int>.Zero;
                    while (i < source.Length - size)
                    {
                        vresult = Avx2.Add(vresult, Avx.LoadVector256(pSource + i));
                        i += size;
                    }

                    vresult = Avx2.HorizontalAdd(vresult, vresult);
                    vresult = Avx2.HorizontalAdd(vresult, vresult);

                    result = vresult.ToScalar();

                    while (i < source.Length)
                    {
                        result += pSource[i];
                        i += 1;
                    }
                }
                else if (Sse3.IsSupported)
                {
                    var vresult = Vector128<int>.Zero;
                    while (i < source.Length - size)
                    {
                        vresult = Sse2.Add(vresult, Sse2.LoadVector128(pSource + i));
                        i += 4;
                    }

                    vresult = Ssse3.HorizontalAdd(vresult, vresult);
                    vresult = Ssse3.HorizontalAdd(vresult, vresult);

                    result = vresult.ToScalar();

                    while (i < source.Length)
                    {
                        result += pSource[i];
                        i += 1;
                    }
                }
                else
                {
                    var vresult = Vector128<int>.Zero;
                    vresult = Sse2.Add(vresult, Sse2.Shuffle(vresult, 0x4E));
                    vresult = Sse2.Add(vresult, Sse2.Shuffle(vresult, 0xB1));

                    result = vresult.ToScalar();

                    while (i < source.Length)
                    {
                        result += pSource[i];
                        i += 1;
                    }
                }
            }

            return result;
        }

        public static unsafe double SumAll(this Matrix<double> matrix)
        {
            double result;

            fixed (double* pSource = matrix.GetArray())
            {
                var source = new Span<double>(pSource, matrix.Length);
                var i = 0;
                var size = source.Length - 8;

                if (Avx2.IsSupported)
                {
                    var vresult = Vector256<double>.Zero;
                    while (i < source.Length - size)
                    {
                        vresult = Avx.Add(vresult, Avx.LoadVector256(pSource + i));
                        i += size;
                    }

                    vresult = Avx.HorizontalAdd(vresult, vresult);
                    vresult = Avx.HorizontalAdd(vresult, vresult);

                    result = vresult.ToScalar();
                }
                else
                {
                    var vresult = Vector128<double>.Zero;
                    while (i < source.Length - size)
                    {
                        vresult = Sse2.Add(vresult, Sse2.LoadVector128(pSource + i));
                        i += 4;
                    }

                    vresult = Sse3.HorizontalAdd(vresult, vresult);
                    vresult = Sse3.HorizontalAdd(vresult, vresult);

                    result = vresult.ToScalar();
                }

                while (i < source.Length)
                {
                    result += pSource[i];
                    i += 1;
                }
            }

            return result;
        }

        public static unsafe float SumAll(this Matrix<float> matrix)
        {
            float result;

            fixed (float* pSource = matrix.GetArray())
            {
                var source = new Span<float>(pSource, matrix.Length);
                var i = 0;
                var size = source.Length - 8;

                if (Avx2.IsSupported)
                {
                    var vresult = Vector256<float>.Zero;
                    while (i < source.Length - size)
                    {
                        vresult = Avx.Add(vresult, Avx.LoadVector256(pSource + i));
                        i += size;
                    }

                    vresult = Avx.HorizontalAdd(vresult, vresult);
                    vresult = Avx.HorizontalAdd(vresult, vresult);

                    result = vresult.ToScalar();
                }
                else
                {
                    var vresult = Vector128<float>.Zero;
                    while (i < source.Length - size)
                    {
                        vresult = Sse.Add(vresult, Sse.LoadVector128(pSource + i));
                        i += 4;
                    }

                    vresult = Sse3.HorizontalAdd(vresult, vresult);
                    vresult = Sse3.HorizontalAdd(vresult, vresult);

                    result = vresult.ToScalar();
                }

                while (i < source.Length)
                {
                    result += pSource[i];
                    i += 1;
                }
            }

            return result;
        }
    }
}