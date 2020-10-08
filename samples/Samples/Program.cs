using System;
using System.Runtime.Intrinsics;
using System.Threading;
using BenchmarkDotNet.Running;
using MatrixDotNet;
using MatrixDotNet.Extensions.Core.Optimization.Simd.Sorting;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            /*VmaskmovBench vmaskmov = new VmaskmovBench();
            vmaskmov.VMaskMov();
            
            foreach (var i in vmaskmov.B)
            {
                Console.Write(i + " ");
            }
            vmaskmov.WithoutVMaskMov();
            Console.WriteLine();
            foreach (var i in vmaskmov.B)
            {
                Console.Write(i + " ");
            }
            
            */
            Matrix<float> matrix = new float[3,4]
            {
                {1,2,3,4},
                {4,3,2,1},
                {4,3,2,1}
                
            };
            
            Matrix<float> matrix1 = new float[3,4]
            {
                {1,0,-2,0},
                {10,1,7,8},
                {9,10,11,12}
            };

            Simd.BubbleSort(matrix1);
            
            //UnsafeMatrix.MultiplyStrassenAsync(matrix, matrix);
            // BenchmarkRunner.Run<VpMinBench>();
        }
    }
    
    
    
}