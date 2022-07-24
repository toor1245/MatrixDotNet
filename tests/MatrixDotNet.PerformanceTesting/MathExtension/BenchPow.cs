using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;

namespace MatrixDotNet.PerformanceTesting.MathExtension
{
    [MemoryDiagnoser]
    public class BenchPow : PerformanceTest
    {
        private Matrix<int> _matrix;

        [Params(10)] public uint Power;

        [Params(512)] public int Size;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = BuildMatrix.RandomInt(Size, Size);
        }

        [Benchmark(Baseline = true)]
        public Matrix<int> Pow()
        {
            return _matrix.Pow(Power);
        }

        [Benchmark]
        public Matrix<int> SPow()
        {
            return _matrix.PowStrassen(Power);
        }
    }
}