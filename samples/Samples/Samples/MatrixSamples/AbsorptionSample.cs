using System;
using System.Text;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples.Samples.MatrixSamples
{
    public class AbsorptionSample : SampleTest
    {
        public static string Run()
        {
            StringBuilder builder = new StringBuilder();

            // initialize Matrix.
            Matrix<double> matrix64Klein = new double[5, 5];
            Matrix<double> matrix64Kahan = new double[5, 5];
            Matrix<decimal> matrix128 = new decimal[5, 5];

            // assign max value for demonstration absorption.
            matrix64Kahan[0, 0] = Math.Pow(10, 16);
            matrix64Klein[0, 0] = Math.Pow(10, 16);
            matrix128[0, 0] = (decimal)Math.Pow(10, 28);

            // make error
            for (int i = 0; i < matrix64Klein.Rows; i++)
            {
                for (int j = 1; j < matrix64Klein.Columns; j++)
                {
                    matrix64Kahan[i, j] = 1;
                    matrix64Klein[i, j] = 0.1;
                    matrix128[i, j] = 0.1m;
                }
            }

            // compare algorithms of summation first case
            double defaultSum0 = matrix64Kahan.Sum();
            double kahanSum0 = matrix64Kahan.GetKahanSum();
            double kleinSum0 = matrix64Kahan.GetKleinSum();

            // compare algorithms of summation second case
            double defaultSum1 = matrix64Klein.Sum();
            double kahanSum1 = matrix64Klein.GetKahanSum();
            double kleinSum1 = matrix64Klein.GetKleinSum();

            // compare algorithms of summation third case
            decimal defaultSum2 = matrix128.Sum();
            decimal kahanSum2 = matrix128.GetKahanSum();
            decimal kleinSum2 = matrix128.GetKleinSum();

            builder.AppendLine("\t\t64 bit");
            builder.AppendLine($"Default sum: {defaultSum0:N}");
            builder.AppendLine($"Kahan sum:   {kahanSum0:N}");
            builder.AppendLine($"Klein sum:   {kleinSum0:N}");

            builder.AppendLine("\n\t\t64 bit");
            builder.AppendLine($"Default sum: {defaultSum1:N}");
            builder.AppendLine($"Kahan sum:   {kahanSum1:N}");
            builder.AppendLine($"Klein sum:   {kleinSum1:N}");

            builder.AppendLine("\n\t\t128 bit");
            builder.AppendLine($"Default sum: {defaultSum2:N}");
            builder.AppendLine($"Default sum: {kahanSum2:N}");
            builder.AppendLine($"Klein sum:   {kleinSum2:N}");
            return builder.ToString();
        }
    }
}