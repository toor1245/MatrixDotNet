using System.Text;
using MatrixDotNet;

namespace Samples.Samples.MatrixSamples
{
    public class MatrixCreateSample : SampleTest
    {
        public static string Run()
        {
            var builder = new StringBuilder();

            var a = new int[3, 3]
            {
                { 10, -7, 0 },
                { -3, 6, 2 },
                { 5, -1, 5 }
            };

            // First way. 
            var matrixA = new Matrix<int>(a);
            builder.AppendLine("Matrix A:\n" + matrixA);

            // Second way: primitive way, assign by deep copy nor by reference!!!
            Matrix<int> matrixB = a;

            Matrix<int> matrixC = new int[10, 10];
            builder.AppendLine("Matrix C:\n" + matrixC);

            Matrix<int> matrixD = new[,]
            {
                { 1, 2, 3 },
                { 2, 4, 6 }
            };
            builder.AppendLine("Matrix D:\n" + matrixD);

            // Third way initialize all values 0 or constant value.
            var matrixE = new Matrix<int>(5, 3);
            builder.AppendLine("Matrix E:\n" + matrixE);

            var matrixF = new Matrix<int>(3, 5, 5);
            builder.AppendLine("Matrix F:\n" + matrixF);

            return builder.ToString();
        }
    }
}