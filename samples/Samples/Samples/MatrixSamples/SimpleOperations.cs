using System.Text;
using MatrixDotNet;

namespace Samples.Samples.MatrixSamples
{
    [Output]
    public class SimpleOperations : SampleTest
    {
        public static string Run()
        {
            StringBuilder builder = new StringBuilder();
            
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
            
            builder.AppendLine(matrixA.ToString());
            builder.AppendLine(matrixB.ToString());
            builder.AppendLine(matrixC.ToString());
            builder.AppendLine(matrixD.ToString());
            builder.AppendLine(matrixE.ToString());
            builder.AppendLine(matrixF.ToString());
            builder.AppendLine(matrixG.ToString());
            return builder.ToString();
        }
    }
}