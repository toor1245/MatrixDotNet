using System;
using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe.Determinant;
using MatrixDotNet.Extensions.Determinants;
using Determinant = MatrixDotNet.Extensions.Determinants.Determinant;

namespace Samples.Samples
{
    [RyuJitX64Job]
    public class LupDecompositonBenchmark
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

        //[Benchmark]
        public double LUPDecomposition()
        {
            return _matrix1.GetMinorDeterminant();
        }
        
        [Benchmark]
        public double LUPDecompositionUnsafe()
        {
            return  UnsafeDeterminant.GetLUPDeterminant(_matrix1);
        }
    }
}