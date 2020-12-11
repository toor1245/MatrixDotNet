using System.Text;
using MatrixDotNet;
using MatrixDotNet.Vectorization;

namespace Samples.Samples.VectorSamples
{
    public class CreateVectorSample : SampleTest
    {
        // Below you can see all ways how to create vector in explicit and implicit form
        public static string Run()
        {
            StringBuilder builder = new StringBuilder();
            
            // basic approach for create matrix just pass length of vector
            Vector<int> va = new Vector<int>(5);
            builder.AppendLine(va.ToString());
            
            // init vector with fill values.
            Vector<int> vb = new Vector<int>(5, 5);
            builder.AppendLine(vb.ToString());
            
            // third way is implicit assign array.
            Vector<int> vc = new[] { 1, 2, 3, 4, 5 };
            builder.AppendLine(vc.ToString());
            
            return builder.ToString();
        }
    }
}