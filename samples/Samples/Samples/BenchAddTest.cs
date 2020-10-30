using BenchmarkDotNet.Attributes;
using MatrixDotNet.Math;

namespace Samples.Samples
{
    [MemoryDiagnoser]
    public class BenchAddTest
    {
        [Params(4096)]
        public int A;

        [Params(4096)]
        public int B;
        
        [Benchmark]
        public int MExtension()
        {
            for (int i = 0; i < 10000; i++)
                MathExtension.GetAddFunc<int, int, int>()(A, B);
            return 0;
        }

        [Benchmark]
        public int MExtensionGeneric()
        {
            for (int i = 0; i < 10000; i++)
                MathGeneric<int, int, int>.GetAddFunc()(A, B);
            return 0;
        }
    }
}