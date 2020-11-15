using System;
using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples.Samples
{
    public class StrassenSample
    {
        private int[,] matrix = new int[5000, 5000];
        private int[,] matrix2 = new int[5000, 5000];

        private Matrix<int> _matrix3;
        private Matrix<int> _matrix4;

        [GlobalSetup]
        public void Setup()
        {
            Random random = new Random();
            Random random2 = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(1, 10);
                }
            }

            _matrix3 = new Matrix<int>(matrix);

            for (int i = 0; i < matrix2.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    matrix2[i, j] = random2.Next(1, 10);
                }
            }

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