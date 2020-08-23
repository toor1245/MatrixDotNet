using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;

namespace Samples.Samples
{
    public class CountEqualBench
    {
        private List<int> arr;
        private int[] arr2;

        [GlobalSetup]
        public void Setup()
        {
            arr = new List<int>();
            arr2 = new int[1000001];
            for (int i = 0; i < arr2.Length ; i++)
            {
                arr.Add(i);
                arr2[i] = i;
            }
        }

        [Benchmark]
        public int CountLinq()
        {
            int find = 1024;
            return arr.Count(x => x == find);
        }

        [Benchmark]
        public int CountEqualBitHacks()
        {
            int count = 0;
            int find = 1024;
            for (int i = 0; i < arr.Count; i++)
            {
                count += (~((arr[i] & find) - find) >> 31) & 1;
            }
            return count;
        }
        
    }
}