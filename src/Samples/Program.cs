using System;
using System.Linq;
using System.Threading.Tasks;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int[,] matrix = new int[2,2];
            Random random = new Random();
            Random random2 = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(1, 10);
                }
            }

            int[,] matrix3 = new int[2,2];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix3[i, j] = random2.Next(1, 10);
                }
            }
            
            
            Matrix<int> matrix2 = new Matrix<int>(matrix3);
            Matrix<int> matrix4 = new Matrix<int>(matrix);
            Console.WriteLine(matrix4.Pretty());
            Console.WriteLine("sum: "+ matrix4.Sum());
        }
    }
}