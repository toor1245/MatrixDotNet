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
        
        [GlobalSetup]
        public void Setup()
        {
            _matrix = BuildMatrix.RandomInt(1024, 1024,0, 20);
        }
        
        [Benchmark]
        public void ReverseDefault()
        {
            Array.Reverse(_matrix.GetArray());
        }

        [Benchmark]
        public void ReverseSimd()
        {
            MatrixConverter.Reverse(_matrix.GetArray());
        }
    }
}