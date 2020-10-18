using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Decompositions;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Matrix<double> a = new double[,]
            {
                {12, -51, 4},
                {6, 167, -68},
                {-4, 24, -41}
            };

            a.QrDecomposition(out var q,out var r);
            q.Pretty();
            r.Pretty();
        }
    }
}