using BenchmarkDotNet.Attributes;
using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MatrixDotNet.PerformanceTesting.Other
{
    public class BenchFill : PerformanceTest
    {
        const float Value = 5;

        [Params(4048)]
        public int Size;
        public float[] arr;

        [GlobalSetup]
        public void Setup()
        {
            arr = new float[Size];
        }

        [Benchmark(Baseline = true)] 
        public void ArrayFill()
        {
            Array.Fill(arr, Value);
        }

        [Benchmark] 
        public void SpanFill()
        {
            var span = new Span<float>(arr);
            span.Fill(Value);
        }

        [Benchmark] 
        public unsafe void AvxFill()
        {
            if (!Avx.IsSupported) 
                return;

            var vector = Vector256.Create(Value);
            int i = 0;
            int length = arr.Length;
            fixed (float* ptr = arr)
            {
                int size = Vector256<float>.Count;
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
