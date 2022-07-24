using System.Text;
using MatrixDotNet;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Vectorization;

namespace Samples.Samples.VectorSamples
{
    public class VectorSimpleOperations : SampleTest
    {
        // Below you can see all ways how to execute routine operations with vector.
        public static string Run()
        {
            StringBuilder builder = new StringBuilder();

            // init vector va with fill three value.
            Vector<int> va = new Vector<int>(5, 3);
            builder.AppendLine("va = " + va);

            // init vector vb.
            Vector<int> vb = new[] { 1, 2, 3, 4, 5 };
            builder.AppendLine("vb = " + vb);

            // add of two vectors.
            var vc = vb + va;
            builder.AppendLine("vc = " + vc);

            // multiply vectors on constant.
            var vk = 5 * vc;
            builder.AppendLine("vk = " + vk);

            // subtract of two vectors.
            var vd = vk - vc;
            builder.AppendLine("vd = " + vd);

            // subtract of two vectors.
            var ve = vd - vk;
            builder.AppendLine("ve = " + ve);

            // multiply vectors on constant.
            var vg = ve * vc;
            builder.AppendLine("vg = " + vg);

            // multiply vector on matrix and vice versa
            Matrix<int> ma = BuildMatrix.RandomInt(5, 5, -10, 10);
            var vq = ma * ve;
            var vt = ve * ma;
            builder.AppendLine("vq = " + vq);
            builder.AppendLine("vt = " + vt);

            return builder.ToString();
        }
    }
}