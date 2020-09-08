using System;
using BenchmarkDotNet.Running;
using MatrixDotNet.Extensions.Core;
using MatrixDotNet.Extensions.Core.Extensions.Conversion;
using Samples.Samples;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            BenchmarkRunner.Run<BenchAddRowFixedVsUnsafeMatrix>();

            // ObjectLayoutInspector.TypeLayout.PrintLayout<MatrixAsFixedBuffer>();

            /*MatrixAsFixedBuffer matrixAsFixedBuffer1 = new double[,]
            {
                { 5, 5, 5, 5, 5 },
                { 1, 2, 3, 4, 5 },
                { 6, 7, 4, 9, 6 },
                { 6, 7, 8, 1, 4 },
                { 5, 4, 1, 3, 1 }
            };

            Console.WriteLine(MatrixAsFixedBuffer.AddByRef(ref matrixAsFixedBuffer1,ref matrixAsFixedBuffer1));
            */
        }
    }
}