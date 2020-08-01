using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples.Samples
{
    public class SimpleOperations
    {
        static void Run()
        {
            // init matrix
            int[,] a = new int[3, 3]
            {
                { 10, -7, 0 },
                { -3, 6,  2 },
                { 5, -1,  5 }
            };

            int[,] b = new int[3, 4]
            {
                { 11, -2, 1, 6 },
                { -8, 4,  2, 3 },
                { 4, -4,  5, 8 },
            };
            
            int[] vector = {2, 7, 5, 4};

            int k = 3;
            
            Matrix<int> matrixA = a;

            Matrix<int> matrixB = b;
                
            // Multiply.
            Matrix<int> matrixC = matrixA * matrixB;
            Matrix<int> matrixD = matrixC * k;
            
            // Sum .
            Matrix<int> matrixE = matrixB + k * matrixB;
            
            // Subtract.
            Matrix<int> matrixF = 2 * matrixA - matrixA;
            
            // Divide.
            Matrix<int> matrixG = matrixA / 2;
            
            // Multiply to vector
            int[] vectorA = matrixB * vector;

            // Pretty output.
            Console.WriteLine(matrixA.Pretty());
            Console.WriteLine(matrixB.Pretty());
            Console.WriteLine(matrixC.Pretty());
            Console.WriteLine(matrixD.Pretty());
            Console.WriteLine(matrixE.Pretty());
            Console.WriteLine(matrixF.Pretty());
            Console.WriteLine(matrixG.Pretty());
            
            Console.Write("VectorA result:");
            foreach (var i in vectorA)
            {
                Console.Write(i + " ");
            }
        }
    }
}