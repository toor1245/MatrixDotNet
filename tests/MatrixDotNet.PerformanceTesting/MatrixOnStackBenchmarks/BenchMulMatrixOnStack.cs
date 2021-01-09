using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Performance;

namespace MatrixDotNet.PerformanceTesting.MatrixOnStackBenchmarks
{
    [MemoryDiagnoser]
    public class BenchMulMatrixAsFixedBuffer : PerformanceTest
    {
        private MatrixOnStack _buffer;
        private Matrix<double> _matrix;
        
        [GlobalSetup]
        public void Setup()
        {
            _buffer = new MatrixOnStack(80,80);
            for (int i = 0; i < _buffer.Length; i++)
            {
                _buffer.Data[i] = 5;
            }
            _matrix = new Matrix<double>(80,80,5);
        }
        
        [Benchmark]
        public Matrix<double> MulMatrix()
        {
            return _matrix * _matrix;
        }
        
        [Benchmark]
        public MatrixOnStack MulByRefMatrixAsFixedBuffer()
        {
            return MatrixOnStack.MulByRef(ref _buffer, ref _buffer);
        }
    }
}