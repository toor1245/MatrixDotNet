using System;
using System.Runtime.InteropServices;
using MatrixDotNet.Extensions.Core.Optimization;

namespace Samples
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            Matrix matrix1 = new Matrix(5,5);
            Console.WriteLine(matrix1.Rows);
            Console.WriteLine(matrix1.Columns);
            Console.WriteLine(matrix1.Length);
        }
    }
}