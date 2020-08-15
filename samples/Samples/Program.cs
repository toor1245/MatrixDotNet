using BenchmarkDotNet.Running;
using Samples.Samples;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<UnsafeMulBenchmark>();
        }

    }
}