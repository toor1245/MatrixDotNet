using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace MatrixDotNet.PerformanceTesting.Other
{
    [RyuJitX64Job]
    public class BenchCountBench : PerformanceTest
    {
        private List<int> arr;
        private int[] arr2;

        [GlobalSetup]
        public void Setup()
        {
            arr = new List<int>();
            arr2 = new int[10001];
            for (var i = 0; i < arr2.Length; i++)
            {
                arr.Add(i);
                arr2[i] = i;
            }
        }

        [Benchmark]
        public int CountLinq()
        {
            return arr.Count(x => x >= 1024);
        }

        [Benchmark]
        public int CountBitHacks()
        {
            var count = 0;
            for (var i = 0; i < arr.Count; i++)
            {
                var element = (arr2[i] - 30) >> 31;
                count += ~element & arr2[i];
            }

            return count;
        }

        [Benchmark]
        public int CountForeachBitHacks()
        {
            var count = 0;

            foreach (var i in arr)
            {
                var element = (i - 1024) >> 31;
                count += ~element & 1;
            }

            return count;
        }

        [Benchmark]
        public int CountBitHacksShift()
        {
            var count = 0;
            for (var i = 0; i < arr.Count; i++) count += arr[i] >> 7;
            return count;
        }
    }
}