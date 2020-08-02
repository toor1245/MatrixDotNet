using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var res1 = Math.Pow(10,8);
            float res2 = (float)Math.Pow(10,8);
            for (int i = 0; i < 100; i++)
            {
                res1 += 1;
                res2 += 1;
            }
            Console.WriteLine("{0:N}",res1);
            Console.WriteLine("{0:N}",res2);
        }
    }
}