using System;
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
            Matrix<int> matrix = BuildMatrix.Random<int>(3, 3,-1000,1000);
            Template template = new TemplateMarkdown("title6");
            matrix.SaveAndOpenAsync(template);

            var matrix2 = matrix * matrix;
            var matrix3 = matrix2 * 3;
            Console.WriteLine(matrix2);
            Console.WriteLine(matrix3);
        }
    }
}