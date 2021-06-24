using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    [MemoryDiagnoser]
    public class BenchNegate : PerformanceTest
    {
        [Params(256)] public int Size;
        private Matrix<short> _matrixShort;
        private Matrix<int> _matrixInt;
        private Matrix<sbyte> _matrixSByte;
        private MathNet.Numerics.LinearAlgebra.Matrix<float> _matrix3;

        [GlobalSetup]
        public void Setup()
        {
            _matrixShort = BuildMatrix.RandomShort(Size, Size, 0, 20);
            _matrixInt = BuildMatrix.RandomInt(Size, Size, 0, 20);
            _matrixSByte = BuildMatrix.RandomSByte(Size, Size, 0, 20);
            _matrix3 = MathNet.Numerics.LinearAlgebra.Matrix<float>.Build.Dense(Size, Size);
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
        
        [Benchmark]
        public MathNet.Numerics.LinearAlgebra.Matrix<float> MathNumerics()
        {
            return -_matrix3;
        }
    }
}