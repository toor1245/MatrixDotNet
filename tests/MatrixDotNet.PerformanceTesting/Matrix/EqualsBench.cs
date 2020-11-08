using System;
using System.Linq;
using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.NotStableFeatures;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    public class EqualsBench : PerformanceTest
    {
        private Matrix<byte> _matrix;
        private Matrix<byte> _matrix2;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = BuildMatrix.RandomByte(1024, 1024);
            _matrix2 = _matrix.Clone() as Matrix<byte>;
        }

        [Benchmark]
        public bool EqualsDefault()
        {
            return _matrix.Equals(_matrix2);
        }

        [Benchmark]
        public bool EqualsMemcpy()
        {
            return UnsafeEqualsUnrolled.EqualBytesLongUnrolled(_matrix.GetArray(), _matrix2.GetArray());
        }
    }
}