using MatrixDotNet.Extensions;
using System;
using System.Threading.Tasks;
using MatrixDotNet;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Options;
namespace Samples
{
    class Program
    {
        
        static async Task Main(string[] args)
        {

            Matrix<int> matrix = BuildMatrix.Random<int>(3, 3,-1000,1000);
            Template template = new TemplateMarkdown("title6");
            await matrix.SaveAndOpenAsync(template);

            var matrix2 = matrix * matrix;
            var matrix3 = matrix2 * 3;
            Console.WriteLine(matrix2);
            Console.WriteLine(matrix3);
            var test = await template.BinaryOpenAsync<int>();
            Console.WriteLine(test);
        }
    }
}