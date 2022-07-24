using System;
using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples.Samples
{
    public class StrassenSample
    {
        private Matrix<int> _matrix3;
        private Matrix<int> _matrix4;
        private readonly int[,] matrix = new int[5000, 5000];
        private readonly int[,] matrix2 = new int[5000, 5000];

        [GlobalSetup]
        public void Setup()
        {
            var random = new Random();
            var random2 = new Random();
            for (var i = 0; i < matrix.GetLength(0); i++)
            for (var j = 0; j < matrix.GetLength(1); j++)
                matrix[i, j] = random.Next(1, 10);

            _matrix3 = new Matrix<int>(matrix);

            for (var i = 0; i < matrix2.GetLength(0); i++)
            for (var j = 0; j < matrix2.GetLength(1); j++)
                matrix2[i, j] = random2.Next(1, 10);

            _matrix4 = new Matrix<int>(matrix2);
        }

        //[Benchmark]
        public Matrix<int> Default()
        {
            return _matrix3 * _matrix4;
        }

        [Benchmark]
        public Matrix<int> StrassenPara()
        {
            return MatrixExtension.MultiplyStrassen(_matrix3, _matrix4);
        }

        [Benchmark]
        public Matrix<int> Strassen()
        {
            return MatrixExtension.MultiplyStrassen(_matrix3, _matrix4);
        }
    }
}