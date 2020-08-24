using MatrixDotNet.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;
using BenchmarkDotNet.Running;
using MatrixDotNet;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Determinants;
using MatrixDotNet.Extensions.Options;
using MatrixDotNet.Extensions.Statistics;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using Samples.Samples;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Matrix<double> matrix = new double[,]
            {
                { 5.7, 6.7,  6.2,  7.0  },
                { 6.7, 7.7,  7.2,  11.0 },
                { 7.7, 8.7,  8.2,  6.0  },
                { 8.7, 9.7,  9.2,  4.0  },
                { 9.7, 10.7, 10.2, 2.0  }
            };
            TableIntervals[] tables = { TableIntervals.Xi,TableIntervals.Ni };
            var intervals = new Intervals<double>(matrix,tables);
            Console.WriteLine(intervals.IntervalRowMean());

        }
    }
}