using System.Text;
using MatrixDotNet;

namespace Samples.Samples.MatrixSamples
{
    public class AccessToElements : SampleTest
    {
        public static string Run()
        {
            var builder = new StringBuilder();
            // Initialize matrix.
            Matrix<int> matrix = new int[3, 3]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 9 }
            };

            builder.AppendLine("Matrix: " + matrix);

            // Approaches for obtaining elements.

            // simple way for receive element.
            var first = matrix[1, 2];
            builder.AppendLine("first case: " + first);

            // gets element by State.Row
            var second = matrix[1, 1, State.Row];
            builder.AppendLine("second case gets element by row: " + second);

            // gets element by State.Column
            var third = matrix[0, 2, State.Column];
            builder.AppendLine("third case gets element by column: " + third);

            return builder.ToString();
        }
    }
}