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
            Matrix<int> matrix = new Matrix<int>(4,4,1);
            Table[] tables = {Table.Xi,Table.Ni,Table.Column,Table.Column};
            Console.WriteLine((int)tables[0]);
            ConfigIntervals<int> intervals = new ConfigIntervals<int>(matrix,tables,"Find IRV");
            matrix.Pretty();
            intervals.IntervalRowMean();
        }
    }
}