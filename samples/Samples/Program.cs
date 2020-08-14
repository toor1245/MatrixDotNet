using BenchmarkDotNet.Running;
using Samples.Samples;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            /*Matrix<double> matrix1 = new double[,]
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };
            
            Console.WriteLine(matrix1.GetLUPDeterminant());
            Console.WriteLine(UnsafeDeterminant.GetLUPDeterminant(matrix1));
            */

            BenchmarkRunner.Run<LupDecompositonBenchmark>();
        }

    }
}