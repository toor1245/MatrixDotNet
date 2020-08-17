
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.Inverse;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Matrix<double> matrix = new double[3,3]
            {
                { 1,   5,   2 },
                { 31,   2341,   3 },
                { 2,   4234,   8324 },
            };

            matrix.Pretty();
            var matrixB = matrix.GaussianEliminationInverse();
            matrixB.SaveAndOpenAsync("test2");
            matrixB.Pretty();
        }
    }
}