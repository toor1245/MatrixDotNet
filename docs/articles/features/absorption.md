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

[!code-csharp[AbsorptionSample.cs](../../../../samples/Samples/logs/AbsorptionSample/AbsorptionSampleDocs.cs)]

As you can see we have three matrix with 64 and 128 bit, so next step assign max values for this matrices for demonstration hit in absorption.
For invoke Klein's or Kahan's algorithm must invoke method `GetKahanSum()` and `GetKleinSum()`  
Klein's algorithm have the most accuracy summation of matrix than Kahan's, however if values not big you can use Kahan's algorithm.     

#### Output

[!code-csharp[AbsorptionSample.cs](../../../../samples/Samples/logs/AbsorptionSample/Run.txt)]

Thus, the default sum counts incorrectly, the runtime rounds it because the floating type precision is not enough to handle digit numbers.
Klein calculates with less error, but at the same time it works slower than Kahan.

> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).
