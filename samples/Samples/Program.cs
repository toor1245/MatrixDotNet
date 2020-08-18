
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
            Matrix<int> matrix = new Matrix<int>(20,20,5);
            matrix.SaveToHtmlAsync("test3");
        }
    }
}