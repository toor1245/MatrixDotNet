using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Performance;

namespace MatrixDotNet.PerformanceTesting.MatrixAsFixedBufferBench
{
    [MemoryDiagnoser]
    public class BenchAddFixedMatrixVsMatrix : PerformanceTest
    {
        private MatrixAsFixedBuffer _buffer;
        private Matrix<double> _matrix;
        
        [GlobalSetup]
        public void Setup()
        {
            _buffer = new MatrixAsFixedBuffer(80,80);
            for (int i = 0; i < _buffer.Length; i++)
            {
                _buffer.Data[i] = 5;
            }
            _matrix = new Matrix<double>(80,80,5);
        }

        [Benchmark]
        public MatrixAsFixedBuffer AddMatrixAsFixedBuffer()
        {
            return MatrixAsFixedBuffer.AddByRef(ref _buffer, ref _buffer);
        }

        [Benchmark]
        public Matrix<double> AddMatrix()
        {
            return _matrix + _matrix;
        }
    }
}