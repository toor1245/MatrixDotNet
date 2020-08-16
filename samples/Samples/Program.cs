using MatrixDotNet;
using MatrixDotNet.Extensions.Decomposition;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Matrix<double> matrix = new double[3, 3]
            {
                {  4,   12,   -16 },
                {  12,  37,  -43  },
                { -16, -43,   98  }
            };
            
              matrix.GetCholeskyDecomposition(out var lower);
            //  lower.Pretty();
        }
    }
}