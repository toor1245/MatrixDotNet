using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Performance.Simd;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    [RyuJitX64Job]
    public class BenchSimdVsAvx : PerformanceTest
    {
        private Matrix<int> _matrix;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = BuildMatrix.RandomInt(4096, 4096, 1, 123);
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