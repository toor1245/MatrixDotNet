using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.Core;
using MatrixDotNet.Extensions.Core.Extensions.Conversion;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe.Conversion;

namespace Samples.Samples
{
    [MemoryDiagnoser]
    [RyuJitX64Job]
    public class BenchAddRowFixedVsUnsafeMatrix
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
        public void SwapDefault()
        {
            MatrixConverter.SwapRows(_matrix, 1,6);
        }
        
        // [Benchmark]
        public void SwapUnsafeDefault()
        {
            UnsafeConverter.SwapRows(_matrix,1,6);
        }
        
        // [Benchmark]
        public void SwapFixed()
        {
            Converter.SwapRows(ref _buffer,1,6);
        }
        
        // [Benchmark]
        public void SwapColumnDefault()
        {
            _matrix.SwapColumns(1, 6);
        }
        
        // [Benchmark]
        public void SwapColumnFixed()
        {
            Converter.SwapColumns(ref _buffer,1,6);
        }

        // [Benchmark]
        public void CopyToDefault()
        {
            MatrixConverter.CopyTo( _matrix,0,1,_matrix,1,0,_matrix.Columns - 1);
        }
        
        // [Benchmark]
        public void CopyToUnsafe()
        { 
            UnsafeConverter.CopyTo( _matrix,0,1,_matrix,1,0,_matrix.Columns);
        }
        
        // [Benchmark]
        public void CopyToFixed()
        {
            Converter.CopyTo(ref _buffer,0,1,ref _buffer,1,0,_buffer.Columns);
        }
        
        // [Benchmark]
        public void CopyToFixedAvx()
        {
            Converter.CopyToAvx(ref _buffer,0,1,ref _buffer,1,0,_buffer.Columns);
        }
        
        [Benchmark]
        public MatrixAsFixedBuffer AddFixed()
        {
            return MatrixAsFixedBuffer.AddByRef(ref _buffer, ref _buffer);
        }
    }
}