using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Performance.Simd;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    [RyuJitX64Job]
    public class BenchEqualsCompare : PerformanceTest
    {
        private Matrix<int> _matrix1;
        private Matrix<int> _matrix2;

        [GlobalSetup]
        public void Setup()
        {
            _matrix1 = BuildMatrix.RandomInt(1024, 1024, 1, 123);
            _matrix2 = _matrix1.Clone() as Matrix<int>;
        }

        [Benchmark]
        public bool EqualsUnsafe()
        {
            return Equals(_matrix1, _matrix2);
        }

        [Benchmark]
        public bool EqualsAvx()
        {
            return Simd.Equals(_matrix1, _matrix2);
        }
    }
}