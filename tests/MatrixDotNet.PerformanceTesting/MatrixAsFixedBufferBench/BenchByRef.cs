using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace MatrixDotNet.PerformanceTesting.MatrixAsFixedBufferBench
{
    [MemoryDiagnoser]
    [RyuJitX64Job]
    public class BenchByRef : PerformanceTest
    {
        private Struct32B _struct32B;
        private Struct112B _struct112B;

        [GlobalSetup]
        public void Setup()
        {
            _struct32B = new Struct32B();
            _struct112B = new Struct112B();
        }

        [Benchmark]
        public long Struct32BAccess()
        {
            long res = 0;
            for (int i = 0; i < 100; i++)
            {
                res += Helper1(_struct32B);
            }

            return res;
        }

        [Benchmark]
        public long Struct32BAccessByRef()
        {
            long res = 0;
            for (int i = 0; i < 100; i++)
            {
                res += Helper2(ref _struct32B);
            }

            return res;
        }

        [Benchmark]
        public long Struct112BAccess()
        {
            long res = 0;
            for (int i = 0; i < 100; i++)
            {
                res += Helper3(_struct112B);
            }

            return res;
        }

        [Benchmark]
        public long Struct112BAccessByRef()
        {
            long res = 0;
            for (int i = 0; i < 100; i++)
            {
                res += Helper4(ref _struct112B);
            }

            return res;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public int Helper1(Struct32B data)
        {
            return data.Value1;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public int Helper2(ref Struct32B data)
        {
            return data.Value1;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public long Helper3(Struct112B data)
        {
            return data.Value1;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public long Helper4(ref Struct112B data)
        {
            return data.Value1;
        }

        public struct Struct32B
        {
            public int Value1;
            public int Value2;
            public int Value3;
            public int Value4;
            public int Value5;
        }


        public struct Struct112B
        {
            public long Value1;
            public long Value2;
            public long Value3;
            public long Value4;
            public long Value5;
            public long Value6;
            public long Value7;
            public long Value8;
            public long Value9;
            public long Value10;
            public long Value11;
            public long Value12;
            public long Value13;
            public long Value14;
        }
    }
}