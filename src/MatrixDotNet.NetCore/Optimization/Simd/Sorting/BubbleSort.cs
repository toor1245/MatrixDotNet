using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MatrixDotNet.Extensions.Core.Optimization.Simd.Sorting
{
    public static partial class Simd
    {
        public static unsafe void BubbleSort(Matrix<float> matrix)
        {
            fixed (float* ptr = matrix.GetMatrix())
            {
                int i = 0;
                int size = Vector256<float>.Count;
                var ymm8 = Vector256<float>.Zero; // 0 0 0 0
                var ymm9 = Avx.Compare(ymm8, ymm8, FloatComparisonMode.OrderedNonSignaling);
                for (; i < matrix.Length - size; i += size)
                {
                    var vector = Avx.LoadVector256(ptr + i);
                    var even = Avx.DuplicateEvenIndexed(vector);
                    var odd = Avx.DuplicateOddIndexed(vector);
                    var temp = Avx.Compare(even, odd, FloatComparisonMode.OrderedGreaterThanNonSignaling);
                    Console.WriteLine(temp);
                   // var mask1 = Avx.MaskLoad(ptr + i,temp);
                   // Console.WriteLine(mask1);
                    temp = Avx.Xor(temp,ymm9);
                    var even2 = Avx.DuplicateEvenIndexed(temp);
                    var odd2 = Avx.DuplicateOddIndexed(temp);
                    var temp2 = Avx.Compare(odd2, even2, FloatComparisonMode.OrderedGreaterThanNonSignaling);
                    Console.WriteLine(odd2);
                    Console.WriteLine(even2);
                    temp2 = Avx.MaskLoad(ptr + i,temp2);
                    Console.WriteLine(temp2);
                }
            }
            
        }
    }
}