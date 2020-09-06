using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions.Core;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe;

namespace Samples.Samples
{
    [MemoryDiagnoser]
    public class BenchMatrixAsFixedBuffer
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
        
        // [Benchmark]
        public Matrix<double> AddDefault()
        {
            return _matrix + _matrix;
        }

        // [Benchmark]
        public Matrix<double> AddUnsafe()
        {
            return UnsafeMatrix.Add(_matrix, _matrix);
        }
        

        [Benchmark]
        public MatrixAsFixedBuffer AddByRefTest()
        {
            return MatrixAsFixedBuffer.AddByRef(ref _buffer, ref _buffer);
        }
    }
}