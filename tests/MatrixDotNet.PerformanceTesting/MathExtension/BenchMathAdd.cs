using BenchmarkDotNet.Attributes;
using MatrixDotNet.Math;

namespace MatrixDotNet.PerformanceTesting.MathExtension
{
    [MemoryDiagnoser]
    public class BenchMathAdd : PerformanceTest
    {
        [Params(4096)]
        public int A;

        [Params(4096)]
        public int B;
        
        [Benchmark]
        public int MExtension()
        {
            for (int i = 0; i < 10000; i++)
#pragma warning disable 618
                Math.MathExtension.GetAddFunc<int, int, int>()(A, B);
#pragma warning restore 618
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