using MatrixDotNet;

namespace Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a = new int[3, 3]
            {
                { 10, -7, 0 },
                { -3, 6,  2 },
                { 5, -1,  5 }
            };
            
            // First way. 
            Matrix<int> matrixA = new Matrix<int>(a);
            
            // Second way: primitive way, assign by deep copy nor by reference!!!
            Matrix<int> matrixB = a;
            
            Matrix<int> matrixC = new int[10, 10];
            
            // Third way initialize all values 0 or constant value.
            Matrix<int> matrixD = new Matrix<int>(row:5,col:3);
            
            Matrix<int> matrixE = new Matrix<int>(row:3,col:5,value:5);
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