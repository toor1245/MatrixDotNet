using System.Text;
using MatrixDotNet;

namespace Samples.Samples.VectorSamples
{
    public class CreateVectorSample : SampleTest
    {
        // Below you can see all ways how to create vector in explicit and implicit form
        public static string Run()
        {
            StringBuilder builder = new StringBuilder();
            
            // basic approach for create matrix just pass length of vector
            MatrixDotNet.Vectorization.Vector<int> va = new MatrixDotNet.Vectorization.Vector<int>(5);
            builder.AppendLine(va.ToString());
            
            // init vector with fill values.
            MatrixDotNet.Vectorization.Vector<int> vb = new MatrixDotNet.Vectorization.Vector<int>(5, 5);
            builder.AppendLine(vb.ToString());
            
            // third way is implicit assign array.
            MatrixDotNet.Vectorization.Vector<int> vc = new[] { 1, 2, 3, 4, 5 };
            builder.AppendLine(vc.ToString());
            
            return builder.ToString();
        }
    }
}