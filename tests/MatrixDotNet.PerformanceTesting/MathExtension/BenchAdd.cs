using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Performance.Simd;

namespace MatrixDotNet.PerformanceTesting.MathExtension
{
    [RyuJitX64Job]
    public class BenchAdd : PerformanceTest
    {
        private Matrix<double> _matrix1;
        private Matrix<double> _matrix2;

        [GlobalSetup]
        public void Setup()
        {
            _matrix1 = BuildMatrix.RandomDouble(128, 128, 1, 123);
            _matrix2 = BuildMatrix.RandomDouble(128, 128, 1, 123);
        }
        
        public Matrix<double> AddGeneric()
        {
            return _matrix1 + _matrix2;
        }
    }
}