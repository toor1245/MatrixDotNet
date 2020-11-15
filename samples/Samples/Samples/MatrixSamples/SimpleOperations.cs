using System.Text;
using MatrixDotNet;

namespace Samples.Samples.MatrixSamples
{
    public class SimpleOperations : SampleTest
    {
        public static string Run()
        {
            StringBuilder builder = new StringBuilder();

            // init matrix
            int[,] a = new int[3, 3]
            {
                {10, -7, 0},
                {-3, 6, 2},
                {5, -1, 5}
            };

            int[,] b = new int[3, 4]
            {
                {11, -2, 1, 6},
                {-8, 4, 2, 3},
                {4, -4, 5, 8},
            };

            int[] vector = {2, 7, 5, 4};

            int k = 3;

            Matrix<int> matrixA = a;
            builder.AppendLine("matrix A:\n" + matrixA);

            Matrix<int> matrixB = b;
            builder.AppendLine("matrix B:\n" + matrixB);

            // Multiply.
            Matrix<int> matrixC = matrixA * matrixB;
            builder.AppendLine("matrix C:\n" + matrixC);
            Matrix<int> matrixD = matrixC * k;
            builder.AppendLine("matrix D:\n" + matrixD);

            // Sum.
            Matrix<int> matrixE = matrixB + k * matrixB;
            builder.AppendLine("matrix E:\n" + matrixE);

            // Subtract.
            Matrix<int> matrixF = 2 * matrixA - matrixA;
            builder.AppendLine("matrix F:\n" + matrixF);

            // Divide.
            Matrix<int> matrixG = matrixA / 2;
            builder.AppendLine("matrix G:\n" + matrixG);

            return builder.ToString();
        }
    }
}