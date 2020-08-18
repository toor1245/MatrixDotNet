using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Options;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Matrix<int> matrix = BuildMatrix.Random<int>(124, 17,-1000,1000);
            Template template = new TemplateHtml("title5");
            matrix.SaveAsync(template);
        }
    }
}