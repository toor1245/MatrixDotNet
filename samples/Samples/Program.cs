using System;
using BenchmarkDotNet.Running;
using MatrixDotNet;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.Core;
using MatrixDotNet.Extensions.Core.Extensions.Conversion;
using Samples.Samples;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            /*Matrix<double> matrix1 = new[,]
            {
                { 5.7, 6.7,  6.2,  7.0  },
                { 6.7, 7.7,  7.2,  11.0 },
                { 7.7, 8.7,  8.2,  6.0  },
                { 8.7, 9.7,  9.2,  4.0  },
                { 9.7, 10.7, 10.2, 2.0  }
            };
            
            
            Matrix<int> matrix = BuildMatrix.Random<int>(5,5,-10,10);
            matrix.Pretty();
            CountingSort<int> sort;
            sort.Sort(matrix);
            matrix.Pretty();
            */
            
            /*MatrixAsFixedBuffer buffer = new MatrixAsFixedBuffer(5,5);
            for (int i = 0; i < buffer.Rows; i++)
            {
                for (int j = 0; j < buffer.Columns; j++)
                {
                    buffer[i, j] = i + j;
                }
            }
            
            buffer = MatrixAsFixedBuffer.AddByRef(ref buffer,ref buffer);
            */

            /*MatrixAsFixedBuffer matrixAsFixedBuffer1 = new MatrixAsFixedBuffer(80,80);
            MatrixAsFixedBuffer matrixAsFixedBuffer2 = new MatrixAsFixedBuffer(80,80);
            MatrixAsFixedBuffer matrixAsFixedBuffer3 = new MatrixAsFixedBuffer(80,80);
            MatrixAsFixedBuffer matrixAsFixedBuffer4 = new MatrixAsFixedBuffer(80,80);
            MatrixAsFixedBuffer matrixAsFixedBuffer5 = new MatrixAsFixedBuffer(80,80);
            MatrixAsFixedBuffer matrixAsFixedBuffer6 = new MatrixAsFixedBuffer(80,80);
            MatrixAsFixedBuffer matrixAsFixedBuffer7 = new MatrixAsFixedBuffer(80,80);
            MatrixAsFixedBuffer matrixAsFixedBuffer8 = new MatrixAsFixedBuffer(80,80);
            MatrixAsFixedBuffer matrixAsFixedBuffer9 = new MatrixAsFixedBuffer(80,80);
            MatrixAsFixedBuffer matrixAsFixedBuffer10 = new MatrixAsFixedBuffer(80,80);
            MatrixAsFixedBuffer matrixAsFixedBuffer11 = new MatrixAsFixedBuffer(80,80);
            MatrixAsFixedBuffer.AddByRef(ref matrixAsFixedBuffer1, ref matrixAsFixedBuffer2);
            MatrixAsFixedBuffer.AddByRef(ref matrixAsFixedBuffer4, ref matrixAsFixedBuffer3);
            MatrixAsFixedBuffer.AddByRef(ref matrixAsFixedBuffer5, ref matrixAsFixedBuffer6);
            MatrixAsFixedBuffer.AddByRef(ref matrixAsFixedBuffer1, ref matrixAsFixedBuffer7);
            MatrixAsFixedBuffer.AddByRef(ref matrixAsFixedBuffer8, ref matrixAsFixedBuffer9);
            MatrixAsFixedBuffer.AddByRef(ref matrixAsFixedBuffer11, ref matrixAsFixedBuffer10);

            Console.WriteLine(matrixAsFixedBuffer1);
            Console.WriteLine(matrixAsFixedBuffer2);
            Console.WriteLine(matrixAsFixedBuffer3);
            */

            BenchmarkRunner.Run<BenchAddRowFixedVsUnsafeMatrix>();

            /*ObjectLayoutInspector.TypeLayout.PrintLayout<MatrixAsFixedBuffer>();

            MatrixAsFixedBuffer matrixAsFixedBuffer1 = new double[,]
            {
                {5, 5, 5, 5,5,6,7},
                {1, 2, 3, 4,5,3,2},
                {6, 7, 4, 9,6,4,2},
                {6, 7, 8, 1,4,6,6},
                {5, 4, 1, 3,1,2,3}
            };
            
            Converter.SwapRows(ref matrixAsFixedBuffer1,2,1);
            Console.WriteLine(matrixAsFixedBuffer1);
            */
            
        }
    }
}