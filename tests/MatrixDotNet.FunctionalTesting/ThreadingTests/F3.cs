using System;

namespace MatrixDotNet.FunctionalTesting.ThreadingTests
{
    public class F3
    {
        public int N { get; }

        public F3(int n)
        {
            N = n;
        }
        
        
        // T = P * MO + (S * (MR * MS))
        public void RunF3()
        {
            // init vectors and matrices
            Console.WriteLine("Thread 3 start");
            var P = new Vectorization.Vector<int>(N);
            var S = new Vectorization.Vector<int>(N);
            var MO = new Matrix<int>(N,N,1);
            var MR = new Matrix<int>(N,N,1);
            var MS = new Matrix<int>(N,N,1);
            
            // calculates by parts
            var res = new Vectorization.Vector<int>(P.Array * MO);
            var res2 = new Vectorization.Vector<int>(S.Array * (MR * MS));
            var T = res + res2;
            Console.WriteLine("\nT = " + T + "\n");
        }
    }
}