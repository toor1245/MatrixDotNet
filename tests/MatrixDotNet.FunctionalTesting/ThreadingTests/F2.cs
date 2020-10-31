using System;

namespace MatrixDotNet.FunctionalTesting.ThreadingTests
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
            var MG = new Matrix<int>(N,N,1);
            var MK = new Matrix<int>(N,N,1);
            var ML = new Matrix<int>(N,N,1);
            
            var MF = MG * (MK * ML) - ML;
            Console.WriteLine(MF + "\n");
        }
    }
}