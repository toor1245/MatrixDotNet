using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Performance;

namespace MatrixDotNet.PerformanceTesting.Matrix.MathOperations
{
    public class BenchMul : PerformanceTest
    {
        [Params(256)]
        public int Size;
        private Matrix<byte> _matrix1;
        private Matrix<byte> _matrix2;

        [GlobalSetup]
        public void Setup()
        {
            _matrix1 = BuildMatrix.RandomByte(Size, Size, 0, 100);
            _matrix2 = BuildMatrix.RandomByte(Size, Size, 0, 100);
        }

        [Benchmark]
        public async ValueTask<Matrix<byte>> StrassenParallel()
        {
            return await Optimization.MultiplyStrassenAsync(_matrix1, _matrix2);
        }

        [Benchmark]
        public Matrix<byte> Strassen()
        {
            return Optimization.MultiplyStrassen(_matrix1, _matrix2);
        }

        [Benchmark]
        public Matrix<byte> Default()
        {
            return _matrix1 * _matrix2;
        }
    }
}