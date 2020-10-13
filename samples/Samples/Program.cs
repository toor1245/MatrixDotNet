using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.Decompositions;
using MatrixDotNet.Extensions.Determinants;
using MatrixDotNet.Extensions.Inverse;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsWPF;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Matrix<double> matrix = new double[3,3]
            {
                { 3, 6, 1},
                { 23,13, 1},
                { 0, 3, 4}
            };

            matrix.ShurDecomposition(out var q, out var t,out var qt);
            (q * t * qt).Pretty();
        }
    }
}