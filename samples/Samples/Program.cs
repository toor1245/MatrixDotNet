using System;
using System.Linq;
using System.Threading.Tasks;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] matrix = new int[3, 3]
            {
                { 10, -7, 0 },
                { -3, 6,  2 },
                { 5, -1,  5 }
            };
            Test2(matrix);
            Matrix<int> matrix4 = new Matrix<int>(matrix);
            matrix4.GetLUP_Solve(out var lower,out var upper);
            Console.WriteLine("Lower matrix:");
            Console.WriteLine(lower.Pretty());
            Console.WriteLine("Upper matrix:");
            Console.WriteLine(upper.Pretty());

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