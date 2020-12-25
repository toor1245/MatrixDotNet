using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.Performance;
using MatrixDotNet.Extensions.Performance.Conversion;

namespace MatrixDotNet.PerformanceTesting.MatrixAsFixedBufferBench
{
    [MemoryDiagnoser]
    [RyuJitX64Job]
    public class BenchSwapRowsFixedVsUnsafeVsDefault : PerformanceTest
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
        public Matrix<double> AddRowDefault()
        {
            double[] row = new double[80];
            return _matrix.AddRow(row,6);
        }
        

        [Benchmark]
        public MatrixAsFixedBuffer AddByRefTest()
        {
            double[] row = new double[80];
            return Converter.AddRow(ref _buffer,row,6);
        }
    }
}