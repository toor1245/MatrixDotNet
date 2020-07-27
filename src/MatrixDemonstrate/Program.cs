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
            int[,] arr =
            {
                {5,8,-4},
                {6,9,-5},
                {4,7,-3}
            };
            
            
            Matrix<int> matrix = new Matrix<int>(arr);
            Console.WriteLine(matrix.Pretty());
            Console.WriteLine("Text");
        }
        
    }
}