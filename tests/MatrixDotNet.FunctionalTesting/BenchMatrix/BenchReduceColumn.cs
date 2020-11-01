using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.FunctionalTesting.BenchMatrix
{
    public class BenchReduceColumn
    {
        private Matrix<int> _matrix;
        private int n = 1001;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = BuildMatrix.RandomInt(n, n, -100, 100);
        }

        [Benchmark]
        public Matrix<int> ReduceColumnDefault()
        {
            return _matrix.ReduceColumn(50);
        }
        
        /*[Benchmark]
        public Matrix<int> ReduceColumnUnsafe()
        {
            return _matrix.ReduceColumn(50);
        }
        */

    }
}