using BenchmarkDotNet.Attributes;

namespace Samples.Samples
{
    [MemoryDiagnoser]
    public class LOHBench
    {
        [Benchmark]
        public byte[,] Matrix()
        {
            return new byte[300,284];
        }
        
        [Benchmark]
        public byte[] Array()
        {
            return new byte[300 * 284];
        }
    }
}