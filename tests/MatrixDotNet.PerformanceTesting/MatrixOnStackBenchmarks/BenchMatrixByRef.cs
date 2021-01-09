using BenchmarkDotNet.Attributes;

namespace MatrixDotNet.PerformanceTesting.MatrixOnStackBenchmarks
{
    // equally
    [MemoryDiagnoser]
    public class BenchMatrixByRef : PerformanceTest
    {
        private Matrix<long> _matrix;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = new Matrix<long>(100, 1000);
            for (int i = 0; i < _matrix.Rows; i++)
            {
                for (int j = 0; j < _matrix.Columns; j++)
                {
                    _matrix[i, j] = i + j;
                }
            }
        }

        [Benchmark]
        public ref long FindMaxByRefWithTemp()
        {
            ref long maxRef = ref _matrix.GetByRef(0,0);
            for (int i = 0; i < _matrix.Rows; i++)
            {
                for (int j = 0; j < _matrix.Columns; j++)
                {
                    ref long value = ref _matrix.GetByRef(i,j);
                    if (maxRef > value)
                    {
                        maxRef = value;
                    }
                }
            }
            
            return ref maxRef;
        }
        
        [Benchmark]
        public ref long FindMaxByRefWithoutTemp()
        {
            ref long maxRef = ref _matrix.GetByRef(0,0);
            for (int i = 0; i < _matrix.Rows; i++)
            {
                for (int j = 0; j < _matrix.Columns; j++)
                {
                    if (maxRef > _matrix.GetByRef(i,j))
                    {
                        maxRef = _matrix.GetByRef(i,j);
                    }
                }
            }
            
            return ref maxRef;
        }
        
        [Benchmark]
        public long FindMaxWithTemp()
        {
            long maxRef = _matrix[0,0];
            for (int i = 0; i < _matrix.Rows; i++)
            {
                for (int j = 0; j < _matrix.Columns; j++)
                {
                    long value = _matrix[i,j];
                    if (maxRef > value)
                    {
                        maxRef = value;
                    }
                }
            }
            
            return maxRef;
        }
        
        [Benchmark]
        public long FindMaxWithoutTemp()
        {
            long maxRef = _matrix[0,0];
            for (int i = 0; i < _matrix.Rows; i++)
            {
                for (int j = 0; j < _matrix.Columns; j++)
                {
                    if (maxRef > _matrix[i,j])
                    {
                        maxRef = _matrix[i,j];
                    }
                }
            }
            return maxRef;
        }
    }
}