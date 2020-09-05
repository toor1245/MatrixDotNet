using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe.Sorting;
using MatrixDotNet.Extensions.Sorting;

namespace Samples.Samples
{
    public class BubbleSortCompare
    {
        private Matrix<int> _matrix1;

        [GlobalSetup]
        public void Setup()
        {
            _matrix1 = BuildMatrix.Random<int>(64, 64, 1, 123);
            
        }

        [Benchmark]
        public void SortUnsafe()
        {
            _matrix1.BubbleSortUnsafe();
        }
    }
}