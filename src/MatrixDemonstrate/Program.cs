using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace MatrixDemonstrate
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            // initialize matrix.
            double[,] arr =
            {
                {5,56,7},
                {3,6,3},
                {5,9,15}
            };
            
            
            Matrix<double> matrix = new Matrix<double>(arr);

            double[] right = { 1,23,5};
            
            // KramerSolve works only with floating type.
            double[] res = matrix.KramerSolve(right);
            for(var i = 0; i < res.Length; i++)
            {
                Console.Write($"x{i}: {res[i]}\n");
            }

        }

        private static void Test()
        {
            long c = 0x80000000;
            long n = 10;
            bool test = Convert.ToBoolean(n >> 31);
            Console.WriteLine(test);
            n = c - n;
            Console.WriteLine(n);
        }
        
        private static void Swap(ref int x,ref int y)
        {

        }
    }
}