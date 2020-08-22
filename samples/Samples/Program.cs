using MatrixDotNet.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;
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
        
        static async Task Main(string[] args)
        {
            
            Matrix<double> matrix = new double[,]
            {
                { 4, 3, -3 },
                { 2, 4, 4  },
                {-1, 0, -5 }
            };
            
            // 21 / 6 = 

            Console.WriteLine(matrix.Mean());
            

        }
    }
}