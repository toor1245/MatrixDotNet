using System.Text;
using MatrixDotNet;

namespace Samples.Samples.MatrixSamples
{
    [Output(DefineProject.MatrixSamples)]
    public class MatrixCreateSample : SampleTest
    {
        public static string Run()
        {
            StringBuilder builder = new StringBuilder();
            
            int[,] a = new int[3, 3]
            {
                {10, -7, 0},
                {-3, 6, 2},
                {5, -1, 5}
            };

            // First way. 
            Matrix<int> matrixA = new Matrix<int>(a);
            builder.AppendLine("Matrix A:\n" + matrixA);

            // Second way: primitive way, assign by deep copy nor by reference!!!
            Matrix<int> matrixB = a;

            Matrix<int> matrixC = new int[10, 10];
            builder.AppendLine("Matrix C:\n" + matrixC);

            Matrix<int> matrixD = new[,]
            {
                {1, 2, 3},
                {2, 4, 6},
            };
            builder.AppendLine("Matrix D:\n" + matrixD);

            // Third way initialize all values 0 or constant value.
            Matrix<int> matrixE = new Matrix<int>(row: 5, col: 3);
            builder.AppendLine("Matrix E:\n" + matrixE);

            Matrix<int> matrixF = new Matrix<int>(row: 3, col: 5, value: 5);
            builder.AppendLine("Matrix F:\n" + matrixF);
            
            return builder.ToString();
        }
    }
}