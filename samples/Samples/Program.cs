<<<<<<< HEAD
﻿using System;
using System.Threading.Tasks;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Options;

=======
>>>>>>> ca2c0ee4a092f618bf0ce9d033f565a373a8cd0b
namespace Samples
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
<<<<<<< HEAD
            Matrix<int> matrix = BuildMatrix.Random<int>(3, 3,-1000,1000);
            Template template = new TemplateMarkdown("title6");
            await matrix.SaveAndOpenAsync(template);

            var matrix2 = matrix * matrix;
            var matrix3 = matrix2 * 3;
            Console.WriteLine(matrix2);
            Console.WriteLine(matrix3);
            var test = await template.BinaryOpenAsync<int>();
            Console.WriteLine(test);
=======

>>>>>>> ca2c0ee4a092f618bf0ce9d033f565a373a8cd0b
        }
    }
}