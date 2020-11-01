using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;

namespace MatrixDotNet.PerformanceTesting.MatrixMathOperations
{
    public class SumTests : PerformanceTest
    {
        private Matrix<double> _matrix1;
        private Matrix<double> _matrix2;

        [GlobalSetup]
        public void Setup()
        {
            _matrix1 = BuildMatrix.RandomDouble(128, 128, 1, 123);
            _matrix2 = BuildMatrix.RandomDouble(128, 128, 1, 123);
        }

        [Benchmark]
        public Matrix<double> UnsafeTest()
        {
            return Matrix<double>.Plus(_matrix1,_matrix2);
        }
        
        [Benchmark]
        public Matrix<double> GenericTest()
        {
            return _matrix1 + _matrix2;
        }
    }
}