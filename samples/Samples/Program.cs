using System;
using BenchmarkDotNet.Running;
using MatrixDotNet.Extensions.Core;
using Samples.Samples;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            


          

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