using System;
using MatrixDotNet;

namespace Samples.Samples
{
    public class F1
    {
        public int N { get; }

        public F1(int n)
        {
            N = n;
        }
        

        // E = A + B + C + D * (MA * MD)
        public void RunF1()
        {
            Console.WriteLine("Thread 1 start");
            
            // init vectors and matrices
            var A = new Vector(N);
            var B = new Vector(N);
            var C = new Vector(N);
            var D = new Vector(N);
            var MA = new Matrix<int>(N,N,1); 
            var MD = new Matrix<int>(N,N,1);

            var E = A + B + C + D.Array * (MA * MD);
            Console.WriteLine("\nE = " + E + "\n");
            Console.WriteLine("Thread 1 finish");
        }
    }
}