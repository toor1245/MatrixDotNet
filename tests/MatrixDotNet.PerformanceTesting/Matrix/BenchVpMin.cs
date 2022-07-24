using System;
using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Performance.Simd.Statistics;
using MatrixDotNet.Extensions.Statistics;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    public class BenchVpMinBench : PerformanceTest
    {
        private const int N = 1023;
        public Matrix<int> MatrixAvxIntX32 = new(N, N);
        public Matrix<float> MatrixAvxX32 = new(N, N);
        public Matrix<double> MatrixAvxX64 = new(N, N);

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random();
            for (var i = 0; i < N; i++)
            for (var j = 0; j < N; j++)
            {
                MatrixAvxX32[i, j] = (float)(random.NextDouble() * 1000);
                MatrixAvxX64[i, j] = random.NextDouble() * 1000;
                MatrixAvxIntX32[i, j] = (int)random.NextDouble() * 1000;
            }
        }

        [Benchmark]
        public float MinFloatAvx()
        {
            return MatrixAvxX32.Min();
        }

        [Benchmark]
        public double MinDoubleAvx()
        {
            return MatrixAvxX64.Min();
        }

        [Benchmark]
        public double MinIntAvx()
        {
            return MatrixAvxIntX32.Min();
        }

        [Benchmark]
        public double MinIntBit()
        {
            return MatrixAvxIntX32.BitMin();
        }
    }
}