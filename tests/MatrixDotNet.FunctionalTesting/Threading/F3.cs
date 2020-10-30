using System;

namespace MatrixDotNet.FunctionalTesting.Threading
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
            var P = new Vector<int>(N);
            var S = new Vector<int>(N);
            var MO = new Matrix<int>(N,N,1);
            var MR = new Matrix<int>(N,N,1);
            var MS = new Matrix<int>(N,N,1);
            
            // calculates by parts
            var res = new Vector<int>(P.Array * MO);
            var res2 = new Vector<int>(S.Array * (MR * MS));
            
            var T = res + res2;
            
            Console.WriteLine("\nT = " + T + "\n");
            Console.WriteLine("Thread 3 finish");
        }
    }
}