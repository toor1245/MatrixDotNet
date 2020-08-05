using MatrixDotNet;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a = new int[4, 6]
            {
                { 8, 7, 0,6,7,8},
                { 3, 6, 2,7,6,4 },
                { 5, 1, 5,3,4,5 },
                { 2, 6, 5,5,6,7 },
            };
            
            Matrix<int> matrix = a;
        }
    }
}