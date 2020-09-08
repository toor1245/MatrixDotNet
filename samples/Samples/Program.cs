using System;
using BenchmarkDotNet.Running;
using MatrixDotNet;
using MatrixDotNet.Extensions.Core;
using MatrixDotNet.Extensions.Core.Extensions.Conversion;
using MatrixDotNet.Extensions.Core.Extensions.Sorting;
using Samples.Samples;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            Matrix<double> matrix = new double[,]
            {
                { 0,1,2 },
                { 1,2,3 }, 
                { 2,3,4 }
            };

            Console.WriteLine(matrix * matrix);

        }
    }
}