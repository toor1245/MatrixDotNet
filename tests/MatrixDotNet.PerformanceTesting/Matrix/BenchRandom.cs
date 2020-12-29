using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    public class BenchRandom : PerformanceTest
    {
        [Params(1000)]
        public int N;

        [Benchmark(Baseline = true)]
        public Matrix<int> RandomInt()
        {
            return BuildMatrix.RandomInt(N, N);
        }
        
        [Benchmark]
        public Matrix<int> RandomGeneric()
        {
            return BuildMatrix.BuildRandom<int>(N, N);
        }
    }
}