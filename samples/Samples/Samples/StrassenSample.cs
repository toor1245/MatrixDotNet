using System;
using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Sorting;

namespace Samples.Samples
{
    public class StrassenSample
    {
        int[,] matrix = new int[512,512];
        int[,] matrix2 = new int[512,512];
        
        private Matrix<int> matrix3;
        private Matrix<int> matrix4;

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
            matrix3 = new Matrix<int>(matrix);
            
            for (int i = 0; i < matrix2.GetLength(0); i++)
            {
                for (int j = 0; j < matrix2.GetLength(1); j++)
                {
                    matrix2[i, j] = random2.Next(1, 10);
                }
            }
            
            matrix4 = new Matrix<int>(matrix2);
        }

        [Benchmark]
        public Matrix<int> Default()
        {
            return matrix3 * matrix4;
        }
        
        [Benchmark]
        public Matrix<int> Strassen()
        {
            return MatrixExtension.MultiplyStrassen(matrix3, matrix4);
        }
    }
}