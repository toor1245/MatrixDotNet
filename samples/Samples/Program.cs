﻿using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
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

            // Pretty output.
            Console.WriteLine(matrixA.Pretty());
            Console.WriteLine(matrixB.Pretty());
            Console.WriteLine(matrixC.Pretty());
            Console.WriteLine(matrixD.Pretty());
            Console.WriteLine(matrixE.Pretty());
            Console.WriteLine(matrixF.Pretty());
            Console.WriteLine(matrixG.Pretty());

        }

        public static void Test(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    matrix[i, j] = 1;
                }
            }
        }

        public static void Test2(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int j = 0; j < i + 1; j++)
                {
                    matrix[j, i] = 1;
                }
            }
        }
        
    }
}