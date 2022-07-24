using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace MatrixDotNet.PerformanceTesting.Other
{
    public class BenchCountEqualBench : PerformanceTest
    {
        private List<int> arr;
        private int[] arr2;

        [GlobalSetup]
        public void Setup()
        {
            arr = new List<int>();
            arr2 = new int[15];
            arr.Add(1024);
            arr.Add(1024);
            for (var i = 0; i < arr2.Length; i++)
                if ((i & 0b1) == 0)
                    arr.Add(1024);
                else
                    arr.Add(i);
        }

        [Benchmark]
        public int CountLinq()
        {
            var find = 1024;
            return arr.Count(x => x == find);
        }

        [Benchmark]
        public int CountEqualBitHacks()
        {
            var count = 0;
            var find = 1024;
            for (var i = 0; i < arr.Count; i++) count = ((((arr[i] & find) - find) >> 31) ^ i) & i;
            return count;
        }

        [Benchmark]
        public int CountEqualFor()
        {
            var count = 0;
            var find = 1024;
            for (var i = 0; i < arr.Count; i++)
                if (arr[i] == find)
                    count = i;
            return count;
        }
    }
}