using BenchmarkDotNet.Attributes;

using MatrixDotNet;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;

namespace Samples.Samples
{
    public class SwapCompare
    {
        private Matrix<int> _matrix1;

        [GlobalSetup]
        public void Setup()
        {
            _matrix1 = BuildMatrix.Random<int>(1024, 1024, 1, 123);
        }
        
        
        [Benchmark]
        public void SwapRows()
        {
            MatrixConverter.SwapRows(_matrix1,0,124);
        }
        
    }
}