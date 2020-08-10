using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Core.Optimization;

namespace MatrixDotNet.Core.Benchmarks
{
    public class SumVectors
    {
        private int[,] _array;
        private Matrix<int> _matrix;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int SumVector(ReadOnlySpan<int> source)
        {
            int result = 0;

            Vector<int> vresult = Vector<int>.Zero;

            int i = 0;
            int lastBlockIndex = source.Length - (source.Length % Vector<int>.Count);

            while (i < lastBlockIndex)
            {
                vresult += new Vector<int>(source.Slice(i));
                i += Vector<int>.Count;
            }

            for (int n = 0; n < Vector<int>.Count; n++)
            {
                result += vresult[n];
            }

            while (i < source.Length)
            {
                result += source[i];
                i += 1;
            }

            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int Sum(ReadOnlySpan<int> span)
        {
            int sum = 0;
            
            for (int i = 0; i < span.Length; i++)
            {
                sum += span[i];
            }

            return sum;
        }

        [GlobalSetup]
        public void Setup()
        {
            Matrix<int> _matrix = new int[16383, 16383];
        }
        
        [Benchmark]
        public int SumDefault()
        {
            return _matrix.Sum();
        }
        
        [Benchmark]
        public int SumSse2()
        {
            return _matrix.SumAllSse2();
        }
        
        [Benchmark]
        public int SumAvx()
        {
            return _matrix.SumAllAvx2();
        }
    }
}