using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Performance.Operations;
using System.Threading.Tasks;

namespace MatrixDotNet.PerformanceTesting.MatrixMathOperations
{
    public class MultTest : PerformanceTest
    {
        [Params(512)]
        public int Size;

        private Matrix<int> _matrix1;
        private Matrix<int> _matrix2;

        [GlobalSetup]
        public void Setup()
        {
            _matrix1 = BuildMatrix.RandomInt(Size, Size, 0, 100);
            _matrix2 = BuildMatrix.RandomInt(Size, Size, 0, 100);
        }

        [Benchmark]
        public async Task<Matrix<int>> Strassen()
        {
            return await Optimization.MultiplyStrassenAsync(_matrix1, _matrix2);
        }
        
        [Benchmark]
        public Matrix<int> OptimizationUsual()
        {
            return Optimization.Multiply(_matrix1, _matrix2);
        }

        [Benchmark(Baseline = true)]
        public Matrix<int> Default()
        {
            return _matrix1 * _matrix2;
        }
    }
}