using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;

namespace MatrixDotNet.PerformanceTesting.Matrix.MathOperations
{
    public class BenchSum : PerformanceTest
    {
        [Params(128, 256)]
        public int Size;

        private Matrix<double> _matrix1;
        private Matrix<double> _matrix2;

        [GlobalSetup]
        public void Setup()
        {
            _matrix1 = BuildMatrix.RandomDouble(Size, Size, 1, 123);
            _matrix2 = BuildMatrix.RandomDouble(Size, Size, 1, 123);
        }

        [Benchmark(Baseline = true)]
        public Matrix<double> GenericTest()
        {
            return _matrix1 + _matrix2;
        }
    }
}