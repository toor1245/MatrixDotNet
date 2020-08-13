using BenchmarkDotNet.Running;
using Samples.Samples;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            /*Matrix<int> matrix1 = BuildMatrix.Random<int>(5, 5, 1, 10);
            Matrix<int> matrix2 = BuildMatrix.Random<int>(5, 5, 1, 10);
            var res = Optimization.AddAvx(matrix1, matrix2);
            matrix1.Pretty();
            matrix2.Pretty();
            res.Pretty();
            */
            BenchmarkRunner.Run<EqualsCompare>();
        }

    }
}