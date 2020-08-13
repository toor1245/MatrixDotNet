using System;
using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions.Core.Optimization;

namespace Benchmarking
{
    [RyuJitX64Job]
    public class Mul
    {
        public Matrix<int> _matrix1 = new Matrix<int>(128,128);
        public Matrix<int> _matrix2 = new Matrix<int>(128,1228);
        public Random _random = new Random();
       
        [GlobalSetup]
        public void Setup()
        {
            for (int i = 0; i < _matrix1.Rows; i++)
            {
                for (int j = 0; j < _matrix1.Columns; j++)
                {
                    _matrix1[i, j] = _random.Next(1, 123);
                    _matrix2[i, j] = _random.Next(1, 123);
                }
            }
        }
        
        [Benchmark]
        public Matrix<int> Default()
        {
            return _matrix1 * _matrix2;
        }
        
        [Benchmark]
        public Matrix<int> Unsafe()
        {
            return Optimization.Multiply(_matrix1,_matrix2);
        }
    }
}