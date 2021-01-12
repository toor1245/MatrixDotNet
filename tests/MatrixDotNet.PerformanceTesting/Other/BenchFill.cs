using BenchmarkDotNet.Attributes;
using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MatrixDotNet.PerformanceTesting.Other
{
    public class BenchFill : PerformanceTest
    {
        const int Value = 5;

        [Params(2000)]
        public int Size;
        public int[] arr;

        [IterationSetup]
        public void Setup()
        {
            arr = new int[Size];
        }

        [Benchmark(Baseline = true)] 
        public void ArrayFill()
        {
            Array.Fill(arr, Value );
        }

        [Benchmark] 
        public unsafe void AvxFill()
        {
            if (!Avx.IsSupported) 
                return;

            var vector = Vector256.Create(Value);
            int i = 0;
            int length = arr.Length;
            fixed (int* ptr = arr)
            {
                int size = Vector256<int>.Count;
                int lastIndexBlock = length - length % size;
                for (; i < lastIndexBlock; i += size)
                {
                    Avx.Store(ptr+i, vector);
                }
            }
            for (; i < length; i++)
            {
                arr[i] = Value;
            }
        }
    }
}
