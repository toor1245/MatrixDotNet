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
            int[,] matrix = new int[6,6];
            Random random = new Random();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(1, 10);
                }
            }
            
            Matrix<int> matrix4 = new Matrix<int>(matrix);
            Console.WriteLine(matrix4.Pretty());
            matrix4.BubbleSortByRows();
            Console.WriteLine(matrix4.Pretty());
        }
    }
}