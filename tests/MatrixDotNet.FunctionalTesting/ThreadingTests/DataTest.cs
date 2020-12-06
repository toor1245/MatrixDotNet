using System;

namespace MatrixDotNet.FunctionalTesting.ThreadingTests
{
    public class DataTest
    {
        public int N { get; }
        
        public DataTest(int n)
        {
            N = n;
        }
        
        // E = A + B + C + D * (MA * MD)
        public void RunF1()
        {
            var A = new Vectorization.Vector<int>(N,1);
            var B = new Vectorization.Vector<int>(N,1);
            var C = new Vectorization.Vector<int>(N,1);
            var D = new Vectorization.Vector<int>(N,1);
            var MA = new Matrix<int>(N,N,1); 
            var MD = new Matrix<int>(N,N,1);

            var E = A + B + C + D.Array * (MA * MD);
            Console.WriteLine("E = " + E);
        }
        
        // MF = MG*(MK*ML) - MK
        public void RunF2()
        {
            Console.WriteLine("Thread 2 start");
            var MG = new Matrix<int>(N,N,1);
            var MK = new Matrix<int>(N,N,1);
            var ML = new Matrix<int>(N,N,1);
            
            var MF = MG * (MK * ML) - ML;
            Console.WriteLine("MF = \n" + MF + "\n");
        }
        
        // T = P * MO + (S * (MR * MS))
        public void RunF3()
        {
            // init vectors and matrices
            Console.WriteLine("Thread 3 start");
            var P = new Vectorization.Vector<int>(N,1);
            var S = new Vectorization.Vector<int>(N,1);
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