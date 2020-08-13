using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe;

namespace Samples.Samples
{
    [RyuJitX64Job]
    public class AddUnrollVsAdd
    {
        private Matrix<int> _matrix1;
        private Matrix<int> _matrix2;

        [GlobalSetup]
        public void Setup()
        {
            _matrix1 = BuildMatrix.Random<int>(1024, 1024, 1, 123);
            _matrix2 = BuildMatrix.Random<int>(1024, 1024, 1, 123);
        }

        [Benchmark]
        public Matrix<int> AddUnsafe()
        {
            return UnsafeMatrix.Add(_matrix1,_matrix2);
        }
    }
}