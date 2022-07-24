using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    [MemoryDiagnoser]
    public class BenchNegate : PerformanceTest
    {
        private Matrix<short> _matrixShort;
        private Matrix<int> _matrixInt;
        private Matrix<sbyte> _matrixSByte;

        [GlobalSetup]
        public void Setup()
        {
            _matrixShort = BuildMatrix.RandomShort(1024, 1024, 0, 20);
            _matrixInt = BuildMatrix.RandomInt(1024, 1024, 0, 20);
            _matrixSByte = BuildMatrix.RandomSByte(1024, 1024, 0, 20);
        }

        [Benchmark]
        public Matrix<int> Negate()
        {
            return -_matrixInt;
        }

        [Benchmark]
        public Matrix<sbyte> NegateSByteSimd()
        {
            return Matrix<sbyte>.Negate(_matrixSByte);
        }

        [Benchmark]
        public Matrix<short> NegateShortSimd()
        {
            return Matrix<short>.Negate(_matrixShort);
        }

        [Benchmark]
        public Matrix<int> NegateIntSimd()
        {
            return Matrix<int>.Negate(_matrixInt);
        }
    }
}