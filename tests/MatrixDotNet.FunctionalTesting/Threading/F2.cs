using System;

namespace MatrixDotNet.FunctionalTesting.Threading
{
    public class F2
    {
        public int N { get; }

        public F2(int n)
        {
            N = n;
        }

        // MF = MG*(MK*ML) - MK
        public void RunF2()
        {
            Console.WriteLine("Thread 2 start");
            // init vectors and matrices
            var MG = new Matrix<int>(N,N,1);
            var MK = new Matrix<int>(N,N,1);
            var ML = new Matrix<int>(N,N,1);

            // calculate
            var MF = MG * (MK * ML) - ML;
            Console.WriteLine("MF = \n");
            Console.WriteLine(MF + "\n");
            Console.WriteLine("Thread 2 finish");
        }
    }
}