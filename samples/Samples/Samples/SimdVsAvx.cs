using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Core.Optimization.Simd;

namespace Samples.Samples
{
    [RyuJitX64Job]
    public class SimdVsAvx
    {
        private Matrix<int> _matrix;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = BuildMatrix.Random<int>(4096, 4096, 1, 123);
        }

        [Benchmark]
        public int SumDefault()
        {
            return _matrix.Sum();
        }

        [Benchmark]
        public int SumSimd()
        {
            return _matrix.SumAll();
        }
    }
}