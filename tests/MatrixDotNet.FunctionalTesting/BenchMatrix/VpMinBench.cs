using System;
using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions.Statistics;
using Simd = MatrixDotNet.Extensions.Core.Simd.Statistics.Simd;

namespace Samples.Vectorization
{
    public class VpMinBench
    {
        const int N = 1023;
        public Matrix<double> MatrixAvxX64 = new Matrix<double>(N,N);
        public Matrix<float> MatrixAvxX32 = new Matrix<float>(N,N);
        public Matrix<int> MatrixAvxIntX32 = new Matrix<int>(N,N);

        [GlobalSetup]
        public void Setup()
        {
            Random random = new Random();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    MatrixAvxX32[i, j] = (float)(random.NextDouble() * 1000);
                    MatrixAvxX64[i, j] = random.NextDouble() * 1000;
                    MatrixAvxIntX32[i, j] = (int)random.NextDouble() * 1000;
                }
            }
        }

        [Benchmark]
        public float MinFloatAvx()
        {
            return Simd.Min(MatrixAvxX32);
        }
        
        [Benchmark]
        public double MinDoubleAvx()
        {
            return Simd.Min(MatrixAvxX64);
        }
        
        [Benchmark]
        public double MinIntAvx()
        {
            return Simd.Min(MatrixAvxIntX32);
        }
        
        [Benchmark]
        public double MinIntBit()
        {
            return MatrixAvxIntX32.BitMin();
        }
    }
}