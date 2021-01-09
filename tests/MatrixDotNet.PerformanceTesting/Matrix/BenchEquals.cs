using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    public class BenchEquals : PerformanceTest
    {
        private Matrix<byte> _matrix;
        private Matrix<byte> _matrix2;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = BuildMatrix.RandomByte(1024, 1024);
            _matrix2 = _matrix.Clone() as Matrix<byte>;
        }

        [Benchmark]
        public bool EqualsDefault()
        {
            return _matrix.Equals(_matrix2);
        }
    }
}