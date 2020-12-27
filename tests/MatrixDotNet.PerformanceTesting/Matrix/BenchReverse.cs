using System;
using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    [MemoryDiagnoser]
    public class BenchReverse : PerformanceTest
    {
        private Matrix<int> _matrix;
        private Matrix<byte> _matrixByte;
        private Matrix<float> _matrixFloat;
        private Matrix<double> _matrixDouble;
        
        [GlobalSetup]
        public void Setup()
        {
            _matrix = BuildMatrix.RandomInt(1024, 1024,0, 20);
            _matrixByte = BuildMatrix.RandomByte(1024, 1024, 0, 20);
            _matrixFloat = BuildMatrix.RandomFloat(1024, 1024, 0, 20);
            _matrixDouble = BuildMatrix.RandomDouble(1024, 1024, 0, 20);
        }
        
        [Benchmark]
        public void ReverseIntBcl()
        {
            Array.Reverse(_matrix.GetArray());
        }

        [Benchmark]
        public void ReverseIntSimd()
        {
            MatrixConverter.Reverse(_matrix.GetArray());
        }
        
        [Benchmark]
        public void ReverseByteBcl()
        {
            Array.Reverse(_matrixByte.GetArray());
        }
        
        [Benchmark]
        public void ReverseByteSimd()
        {
            MatrixConverter.Reverse(_matrixByte);
        }
        
        [Benchmark]
        public void ReverseFloatBcl()
        {
            Array.Reverse(_matrixFloat.GetArray());
        }
        
        [Benchmark]
        public void ReverseFloatSimd()
        {
            MatrixConverter.Reverse(_matrixFloat);
        }
        
        [Benchmark]
        public void ReverseDoubleBcl()
        {
            Array.Reverse(_matrixDouble.GetArray());
        }
        
        [Benchmark]
        public void ReverseDoubleSimd()
        {
            MatrixConverter.Reverse(_matrixDouble);
        }
    }
}