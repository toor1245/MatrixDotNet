using MatrixDotNet.Extensions;
using System;
using System.Threading.Tasks;
using MatrixDotNet;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Determinants;
using MatrixDotNet.Extensions.Options;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using Samples.Samples;

namespace Samples
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            
            Matrix<double> matrix = new double[,]
            {
                {4, 12, -16,5},
                {12, 37, -43,6},
                {-16, -43, 98,10},
                {3,5,2,1}
            };

            Console.WriteLine(matrix.GetLowerUpperPermutationDeterminant());
            
            /*

            Matrix<int> matrix = BuildMatrix.Random<int>(3, 3,-1000,1000);
            Template template = new TemplateHtml("title6");
            await matrix.SaveAndOpenAsync(template);

            var matrix2 = matrix * matrix;
            var matrix3 = matrix2 * 3;
            Console.WriteLine(matrix2);
            Console.WriteLine(matrix3);
            var test = await template.BinaryOpenAsync<int>();
            Console.WriteLine(test);
            
            */
        }
    }
}