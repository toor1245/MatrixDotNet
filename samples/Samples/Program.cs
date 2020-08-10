using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Core.Optimization;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            Matrix<int> matrix =new [,]
             {
                 {5,3,3}, // 6
                 {4,5,4}, // 15
                 {7,8,1}, // 24б
                 {8,2,9} // 24
             };
 
             int res = matrix.SumAllSse2();
             int res2 = matrix.SumAllAvx2();
             Console.WriteLine(res);
             Console.WriteLine(res2);
             
        }
    }
}