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
            
            // BenchmarkRunner.Run<BenchAddRowFixedVsUnsafeMatrix>();

            // ObjectLayoutInspector.TypeLayout.PrintLayout<MatrixAsFixedBuffer>();

            MatrixAsFixedBuffer matrixAsFixedBuffer1 = new double[,]
            {
                { 5, 5, 5, 5, 5 },
                { 1, 2, 3, 4, 5 },
                { 6, 7, 4, 9, 6 },
                { 6, 7, 8, 1, 4 },
                { 5, 4, 1, 3, 1 }
            };
            
            MatrixAsFixedBuffer matrix2 = new MatrixAsFixedBuffer(5,5);
 
            Converter.CopyTo(ref matrixAsFixedBuffer1,1,0,ref matrix2,3,0,6);
            Console.WriteLine(matrix2);
            
        }
    }
}