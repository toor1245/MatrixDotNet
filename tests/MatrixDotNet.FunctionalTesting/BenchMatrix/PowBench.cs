using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;

namespace MatrixDotNet.FunctionalTesting.BenchMatrix
{
    [MemoryDiagnoser]
    public class PowBench
    {
        private Matrix<int> _matrix;
        
        [Params(100)]
        public int Size;

        [Params(10)]
        public uint Power;

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