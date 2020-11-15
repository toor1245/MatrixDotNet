using System.Text;
using MatrixDotNet;

namespace Samples.Samples.MatrixComplexSamples
{
    public class CreateMatrixComplex : SampleTest
    {
        // Below you can see all ways how to create MatrixComplex for now.
        public static string Run()
        {
            StringBuilder builder = new StringBuilder();
            MatrixComplex ma = new MatrixComplex(5,5);
            MatrixComplex mb = new MatrixComplex(5,5);
            builder.AppendLine("ma: " + ma);
            builder.AppendLine("mb: " + mb);

            // addition of two complex matrix.
            var mc = ma + mb;
            builder.AppendLine("mc: " + mc);

            // subtraction of two complex matrix.
            var md = ma - mc;
            builder.AppendLine("md: " + md);
            
            // addition of two complex matrix.
            var me = ma * md;
            builder.AppendLine("me: " + me);

            return builder.ToString();
        }
    }
}