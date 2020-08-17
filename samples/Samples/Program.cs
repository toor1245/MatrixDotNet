
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
                { 0,   0,   3 },
                { 2,   0,   8 },
            };

            matrix.Pretty();
            var matrixB = matrix.GaussianEliminationInverse();
            matrixB.SaveAndOpenAsync("test");
            matrixB.Pretty();
        }
    }
}