using System;
using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe.Decomposition;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe.Determinant;

namespace Samples.Samples
{
    public class CholeckyVsLUP
    {
        private Matrix<double> _matrix1;

        [GlobalSetup]
        public void Setup()
        {
            Random _random = new Random();
            _matrix1 = new Matrix<double>(1024,1024);
            for (int i = 0; i < _matrix1.Rows; i++)
            {
                for (int j = 0; j < _matrix1.Columns; j++)
                {
                    _matrix1[i, j] = _random.NextDouble();
                }
            }
        }

        [Benchmark]
        public void LupDecomposition()
        {
            UnsafeDecomposition.GetLowerUpperPermutation(_matrix1,out var lower,out var upper,out var p);
        }
    }
}