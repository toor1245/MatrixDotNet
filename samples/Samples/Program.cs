using System;
using System.Threading;
using BenchmarkDotNet.Running;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe.Sorting;
using MatrixDotNet.Extensions.Sorting;
using MatrixDotNet.Extensions.Statistics;
using Samples.Samples;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Matrix<double> matrix1 = new[,]
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
        }
    }
}