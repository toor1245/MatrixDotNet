using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;

namespace MatrixDotNet.FunctionalTesting.MathTests
{
    [RyuJitX64Job]
    public class GenericAddByFuncVsUnsafeAddBench
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
        public Matrix<double> AddUnsafe()
        {
            return Matrix<double>.Plus(_matrix1,_matrix2);
        }
        
        [Benchmark]
        public Matrix<double> AddGeneric()
        {
            return _matrix1 + _matrix2;
        }
    }
}