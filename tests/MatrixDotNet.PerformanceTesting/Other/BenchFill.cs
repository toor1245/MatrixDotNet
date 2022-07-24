using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using BenchmarkDotNet.Attributes;

namespace MatrixDotNet.PerformanceTesting.Other
{
    public class BenchFill : PerformanceTest
    {
        private const float Value = 5;
        public float[] arr;

        [Params(4048)] public int Size;

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
            var i = 0;
            var length = arr.Length;
            fixed (float* ptr = arr)
            {
                var size = Vector256<float>.Count;
                var lastIndexBlock = length - length % size;
                for (; i < lastIndexBlock; i += size) Avx.Store(ptr + i, vector);
            }

            for (; i < length; i++) arr[i] = Value;
        }
    }
}