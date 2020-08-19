<<<<<<< HEAD
﻿using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Inverse;
=======
﻿using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Options;
>>>>>>> 6b241e20fb3e2ffe2d2c4a3c44305f2b675accc1

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
<<<<<<< HEAD
            Matrix<double> matrix = new double[3,3]
            {
                { 1,   5,   2 },
                { 0,   0,   3 },
                { 2,   0,   8 },
            };

            matrix.Pretty();
            var matrixB = matrix.GaussianEliminationInverse();
            matrixB.SaveAndOpenAsync("test2");
            matrixB.Pretty();
=======
            Matrix<int> matrix = BuildMatrix.Random<int>(3, 3,-1000,1000);
            Template template = new TemplateMarkdown("title6");
            matrix.SaveAndOpenAsync(template);

            var matrix2 = matrix * matrix;
            var matrix3 = matrix2 * 3;
            Console.WriteLine(matrix2);
            Console.WriteLine(matrix3);
>>>>>>> 6b241e20fb3e2ffe2d2c4a3c44305f2b675accc1
        }
    }
}