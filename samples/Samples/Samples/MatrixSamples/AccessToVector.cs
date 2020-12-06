using System.Text;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Vectorization;

namespace Samples.Samples.MatrixSamples
{
    public class AccessToVector : SampleTest
    {
        public static string Run()
        {
            StringBuilder builder = new StringBuilder();
            // initialize random matrix.
            Matrix<int> matrix = BuildMatrix.RandomInt(4, 4, -10, 10);
            
            // gets vector of matrix by row index.
            Vector<int> arr1 = matrix[0, State.Row];
            builder.AppendLine("gets vector by row index: " + arr1);

            // gets vector of matrix by column index
            Vector<int> arr2 = matrix[0, State.Column];
            builder.AppendLine("gets vector by column index: " + arr2);
            
            // gets vector via method
            Vector<int> arr3 = matrix.GetColumn(1);
            Vector<int> arr4 = matrix.GetRow(2);
            builder.AppendLine("gets vector by column index via method: " + arr3);
            builder.AppendLine("gets vector by row index via method: " + arr4);
            
            // assign vector to matrix by column and row.
            matrix[0, State.Column] = arr3.Array;
            matrix[1, State.Row] = arr4.Array;
            builder.AppendLine("matrix after conversion: ");
            builder.AppendLine(matrix.ToString());

            return builder.ToString();
        }
    }
}