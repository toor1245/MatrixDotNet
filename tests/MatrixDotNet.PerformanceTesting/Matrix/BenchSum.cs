using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    [RyuJitX64Job]
    public class BenchSum : PerformanceTest
    {
        private Matrix<int> _matrix;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = BuildMatrix.RandomInt(4096, 4096, 1, 123);
        }

        [Benchmark]
        public int Sum()
        {
            return _matrix.Sum();
        }
    }
}