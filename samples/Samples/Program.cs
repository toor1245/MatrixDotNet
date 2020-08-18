using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Matrix<int> matrix = new Matrix<int>(1024,1024,5);
            matrix.SaveToHtmlAsync("test3");
        }
    }
}