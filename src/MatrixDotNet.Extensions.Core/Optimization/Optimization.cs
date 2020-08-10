using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MatrixDotNet.Extensions.Core.Optimization
{
    public static partial class Optimization
    {
        public static unsafe int SumAllSse2(this Matrix<int> matrix)
        {
            int result;

            fixed (int* pSource = matrix.GetMatrix())
            {
                Span<int> source = new Span<int>(pSource,matrix.Length);
                Vector128<int> vresult = Vector128<int>.Zero;
                int i = 0;
                var lastBlockIndex = source.Length - (source.Length % 8);

                while (i < lastBlockIndex)
                {
                    vresult = Sse2.Add(vresult, Sse2.LoadVector128(pSource + i));
                    i += 4;
                }
                
                if (Sse3.IsSupported)
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
                
                while (i < source.Length)
                {
                    result += pSource[i];
                    i += 1;
                }
            }

            return result;
        }
        
        public static unsafe int SumAllAvx2(this Matrix<int> matrix)
        {
            int result;

            fixed (int* pSource = matrix.GetMatrix())
            {
                Span<int> source = new Span<int>(pSource,matrix.Length);
                Vector256<int> vresult = Vector256<int>.Zero;
                int i = 0;
                // var lastBlockIndex = source.Length - (source.Length % 8);
                int size = source.Length - (256 / 8 / 4);

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

            return result;
        }
    }
}