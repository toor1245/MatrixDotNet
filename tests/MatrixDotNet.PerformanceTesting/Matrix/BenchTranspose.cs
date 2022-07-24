using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    public class BenchTranspose : PerformanceTest
    {
        private Matrix<float> _matrixFloat;

        [GlobalSetup]
        public void Setup()
        {
            _matrixFloat = BuildMatrix.RandomFloat(8, 8, 0, 20);
        }

        [Benchmark]
        public void Transpose()
        {
            _matrixFloat.Transpose();
        }

        [Benchmark]
        public void TransposeAvx2()
        {
            _matrixFloat.TransposeXVectorSize();
        }
    }
}