# Summation Kahan and Klein overview.

The float and double types follow the IEEE 754 standard, which states that a
floating-point number is represented by a sign S, an exponent E, and a mantissa M which
can be converted to the real value by the following rule:

V = (-1)<sup> S </sup> * 1.M * 2<sup>E - E<sub>bias</sub></sup>

Computer can’t represent every real number in memory: the range
and the precision depends on the number of bits that we have. In table you can see
the main characteristics of the 32-bit, 64-bit, and 80-bit floating-point numbers.

|          |Sign|Exponent|Mantissa|   Digits   |E<sub>bias</sub>|
|----------|----|--------|--------|------------|----------------|  
|**32bit** | 1  |   8    |   23   | &asymp;7.2 |    127         |
|**64bit** | 1  |  11    |   52   | &asymp;15.9|    1023        |
|**81bit** | 1  |  15    |   64   | &asymp;19.2|    16383       |

Most of the classic arithmetic rules don’t work with floating-point numbers. Here is
one of the most famous IEEE 754 equations:
0.1d + 0.2 &ne; 0.3d

We have such situations because 0.1d, 0.2d, and 0.3d can’t be perfectly presented in
IEEE 754 notation:

    0.1d ~ 0.100000000000000005551115123125783
`+` 
    
    0.2d ~ 0.200000000000000011102230246251565
`=`

    0.300000000000000044408920985006262

    0.3d ~ 0.299999999999999988897769753748435

Many arithmetic rules don’t work with float and double in general:
* (a + b) + c &ne; a + (b + c) 
* (a * b) * c &ne; a * (b * c)
* (a + b) * c &ne; a * c + b * c
* a<sup>x + y</sup> &ne; a<sup>x</sup> * a<sup>y</sup>
    
##### Lets consider the following sample:
```c#
using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Sample
{
    public class AbsorptionDemonstrate
    {
        public static void Run()
        {
            // initialize Matrix
            Matrix<double> matrix64Klein = new double[5, 5];
            Matrix<double> matrix64Kahan = new double[5, 5];
            Matrix<decimal> matrix128 = new decimal[5, 5];

            matrix64Kahan[0, 0] = Math.Pow(10,16);
            matrix64Klein[0, 0] = Math.Pow(10,16);
            matrix128[0, 0] = (decimal)Math.Pow(10,28);
            
            for (int i = 0; i < matrix64Klein.Rows; i++)
            {
                for (int j = 1; j < matrix64Klein.Columns; j++)
                {
                    matrix64Kahan[i, j] = 1;
                    matrix64Klein[i, j] = 0.1;
                    matrix128[i, j] = 0.1m;
                }
            }
            
            double defaultSum0 = matrix64Kahan.Sum();
            double kahanSum0 = matrix64Kahan.KahanSum();
            double kleinSum0 = matrix64Kahan.KleinSum();

            double defaultSum1 = matrix64Klein.Sum();
            double kahanSum1 = matrix64Klein.KahanSum();
            double kleinSum1 = matrix64Klein.KleinSum();
            
            decimal defaultSum2 = matrix128.Sum();
            decimal kleinSum2 = matrix128.KleinSum();

            Console.WriteLine("\t\t64 bit");
            Console.WriteLine("Default sum: {0:N}", defaultSum0);
            Console.WriteLine("Kahan sum:   {0:N}", kahanSum0);
            Console.WriteLine("Klein sum:   {0:N}", kleinSum0);
            
            Console.WriteLine("\n\t\t64 bit");
            Console.WriteLine("Default sum: {0:N}", defaultSum1);
            Console.WriteLine("Kahan sum:   {0:N}", kahanSum1);
            Console.WriteLine("Klein sum:   {0:N}", kleinSum1);
            
            Console.WriteLine("\n\t\t128 bit");
            Console.WriteLine("Default sum: {0:N}", defaultSum2);
            Console.WriteLine("Klein sum:   {0:N}", kleinSum2);
        }
    }
}
```

#### Output
```ini
                64 bit
Default sum: 10 000 000 000 000 000,00
Kahan sum:   10 000 000 000 000 020,00
Klein sum:   10 000 000 000 000 020,00

                64 bit
Default sum: 10 000 000 000 000 000,00
Kahan sum:   10 000 000 000 000 000,00
Klein sum:   10 000 000 000 000 002,00

                128 bit
Default sum: 10 000 000 000 000 000 000 000 000 000,00
Klein sum:   10 000 000 000 000 000 000 000 000 002,00
```
As you can see, the default sum counts incorrectly, the runtime rounds it because the floating type precision is not enough to handle digit numbers.

Klein calculates with less error, but at the same time it works slower than Kahan.

> There is no Kahan algorithm for decimal, since the usual sum will count correctly, but the algorithm cannot cope with accuracy.


If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).
