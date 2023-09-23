# MatrixDotNet

![](https://github.com/toor1245/MatrixDotNet/blob/main/docs/images/MatrixDotNet.png)

<h3 align="center">

[![NuGet](https://img.shields.io/nuget/v/MatrixDotNet.svg)](https://www.nuget.org/packages/MatrixDotNet/) 
[![Downloads](https://img.shields.io/nuget/dt/matrixdotnet.svg)](https://www.nuget.org/packages/MatrixDotNet/)
[![Stars](https://img.shields.io/github/stars/toor1245/MatrixDotNet?color=brightgreen)](https://github.com/toor1245/MatrixDotNet/stargazers)
[![Gitter](https://badges.gitter.im/MatrixDotNet/community.svg)](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](https://www.nuget.org/packages/MatrixDotNet/)

</h3>

<h3 align="center">
  <a href="#Features">Features</a>
  <span> · </span>
  <a href="https://toor1245.github.io/MatrixDotNet/articles/intro.html">Getting started</a>
  <span> · </span>
  <a href="https://toor1245.github.io/MatrixDotNet/index.html">Documentation</a>
  <span> · </span>
  <a href="https://toor1245.github.io/MatrixDotNet/api/index.html">API Reference</a>
</h3> 

MatrixDotNet is a powerful .NET library for calculate matrix. You can install MatrixDotNet via [NuGet package](https://www.nuget.org/packages/MatrixDotNet/).

## Features
* Have many algorithms such sections as: factorizations, solving a linear system, conversion matrix, statistics
* Have algorithms which enhance precision
* You can write your matrix to markdown or html and open them with happen .dat file

### Have many algorithms such sections as: factorization, solver, conversion matrix, statistics
Lets consider several algorithms on each section.

* <a href = "#Factorizations">Factorizations</a>
* <a href = "#Solving a linear system">Solving a linear system</a>
* <a href = "#Conversion matrix">Conversion matrix</a>
* <a href = "#Statistics">Statistics</a>

#### Factorizations

In many applications, it is useful to decompose a matrix using other representations.
All decompositions available in namespace `MatrixDotNet.Extensions.Decomposition`.
So lets consider decompositions which supported by MatrixDotNet.

##### LUP decomposition

The LU decomposition finds a representation for the square matrix A as:

<h5> A = LU</h5>

 *  where L is lower-triangular matrix `n x n` 
 *  where U is upper-triangular matrix `n x n`

for example lets take matrix size `3 x 3` 

![example](https://wikimedia.org/api/rest_v1/media/math/render/svg/d536704df4f1374607bef1519ce452a28ea4a03a)

Without a proper ordering or permutations in the matrix, the factorization may fail to materialize. For example, it is easy to verify (by expanding the matrix multiplication) that a<sub>11</sub> = l<sub>11</sub> * u<sub>11</sub>
If a<sub>11</sub> = 0, then at least one of  l<sub>11</sub> and u<sub>11</sub>  has to be zero, which implies that either `L` or `U` is singular.  This is impossible if A is nonsingular (invertible). This is a procedural problem. It can be removed by simply reordering the rows of `A` so that the first element of the permuted matrix is nonzero.
The same problem in subsequent factorization steps can be removed the same way.

Lets see LU factorization in `MatrixDotNet`

```c#
public class LUSample
{
    public static void Run()
    {
        // initialize matrix with random values.
        Matrix<double> matrix = BuildMatrix.Random(5, 5, -10, 10);
        
        // display matrix.
        matrix.Pretty();
        
        // LU decomposition.
        matrix.GetLowerUpper(out var lower,out var upper);
        
        // display lower-triangular matrix.
        Console.WriteLine("lower-triangular matrix");
        lower.Pretty();
        
        // display upper-triangular matrix.
        Console.WriteLine("upper-triangular matrix");
        upper.Pretty();
        
        // A = LU
        Console.WriteLine("A = LU");
        Console.WriteLine(lower * upper);
    }
}
```
###### Output
```ini

Number of rows: 5
Number of columns: 5


  -8,00  |  9,00   |  -3,00  |  2,00   |  6,00   |
  7,00   |  -6,00  |  -5,00  |  -9,00  |  6,00   |
  5,00   |  4,00   |  6,00   |  -1,00  |  -9,00  |
  -4,00  |  3,00   |  -8,00  |  -9,00  |  5,00   |
  6,00   |  0,00   |  3,00   |  3,00   |  8,00   |

lower-triangular matrix
Number of rows: 5
Number of columns: 5


  1,00   |  0,00   |  0,00   |  0,00   |  0,00  |
  -0,88  |  1,00   |  0,00   |  0,00   |  0,00  |
  -0,62  |  5,13   |  1,00   |  0,00   |  0,00  |
  0,50   |  -0,80  |  -0,29  |  1,00   |  0,00  |
  -0,75  |  3,60   |  0,65   |  -1,26  |  1,00  |

upper-triangular matrix
Number of rows: 5
Number of columns: 5


  -8,00  |  9,00  |  -3,00  |  2,00   |  6,00    |
  0,00   |  1,88  |  -7,62  |  -7,25  |  11,25   |
  0,00   |  0,00  |  43,27  |  37,47  |  -63,00  |
  0,00   |  0,00  |  0,00   |  -4,89  |  -7,35   |
  0,00   |  0,00  |  0,00   |  0,00   |  3,77    |

A = LU

  -8,00  |  9,00   |  -3,00  |  2,00   |  6,00   |
  7,00   |  -6,00  |  -5,00  |  -9,00  |  6,00   |
  5,00   |  4,00   |  6,00   |  -1,00  |  -9,00  |
  -4,00  |  3,00   |  -8,00  |  -9,00  |  5,00   |
  6,00   |  0,00   |  3,00   |  3,00   |  8,00   |

```

#### LUP decomposition
It turns out that a proper permutation in rows (or columns) is sufficient for LU factorization. LU factorization with partial pivoting (LUP) refers often to LU factorization with row permutations only:

<h5>PA = LU</h5>

where L and U are again lower and upper triangular matrices, and P is a permutation matrix, which, when left-multiplied to A, reorders the rows of A. It turns out that all square matrices can be factorized in this form, and the factorization is numerically stable in practice.This makes LUP decomposition a useful technique in practice. 

```c#
public class LUPSample
{
    public static void Run()
    {
        // initialize matrix with random values.
        Matrix<double> matrix = BuildMatrix.Random(3, 3, -10, 10);
        
        // display matrix.
        matrix.Pretty();
        
        // LU decomposition.
        matrix.GetLowerUpperPermutation(out var lower,out var upper,out var perm);
        
        // Gets permutation matrix and C = L + U - E.
        matrix.GetLowerUpperPermutation(out var matrixC,out var matrixP);
        
        // display lower-triangular matrix.
        Console.WriteLine("lower-triangular matrix");
        lower.Pretty();
        
        // display upper-triangular matrix.
        Console.WriteLine("upper-triangular matrix");
        upper.Pretty();
        
        // display permutation matrix.
        Console.WriteLine("permutation matrix");
        perm.Pretty();
        
        // display matrix C
        Console.WriteLine("matrix C = L + U - E");
        matrixC.Pretty();
    }
}

```
###### Output
```ini 
Number of rows: 3
Number of columns: 3

  -8,00  |  -6,00  |  -1,00  |
  5,00   |  6,00   |  6,00   |
  -1,00  |  -2,00  |  -5,00  |

lower-triangular matrix
Number of rows: 3
Number of columns: 3

  1,00   |  0,00   |  0,00  |
  -0,62  |  1,00   |  0,00  |
  0,12   |  -1,80  |  1,00  |

upper-triangular matrix
Number of rows: 3
Number of columns: 3

  -8,00  |  -6,00  |  -1,00  |
  0,00   |  -1,25  |  -4,88  |
  0,00   |  0,00   |  -3,40  |

permutation matrix
Number of rows: 3
Number of columns: 3

  1,00  |  0,00  |  0,00  |
  0,00  |  0,00  |  1,00  |
  0,00  |  1,00  |  0,00  |

matrix C = L + U - E
Number of rows: 3
Number of columns: 3


  -8,00  |  -6,00  |  -1,00  |
  5,00   |  6,00   |  6,00   |
  -1,00  |  -0,33  |  -3,00  |
```

#### Solving a linear system

##### Kramer solve
Cramer's rule is an explicit formula for the solution of a system of linear equations with as many equations as unknowns, 
valid whenever the system has a unique solution. It expresses the solution in terms of the determinants of the (square) coefficient matrix and
of matrices obtained from it by replacing one column by the column vector of right-hand-sides of the equations. 

```c#
public sealed class Program
{
    static void Main(string[] args)
    {
        // initialize matrix.
        double[,] arr =
        {
            {5,56,7},
            {3,6,3},
            {5,9,15}
        };


        Matrix<double> matrix = new Matrix<double>(arr);

        double[] right = { 1, 23, 5 };

        double[] res = matrix.KramerSolve(right);
        for(var i = 0; i < res.Length; i++)
        {
            Console.Write($"x{i}: {res[i]}\n");
        }
    }
}
 
```
###### Output
```ini
x0: 12,393939393939394
x1: -0,6637806637806638
x2: -3,3997113997114
```

#### Gauss solve
Gaussian elimination, also known as row reduction, is an algorithm in linear algebra for solving a system of linear equations. 
It is usually understood as a sequence of operations performed on the corresponding matrix of coefficients. 
This method can also be used to find the rank of a matrix, to calculate the determinant of a matrix, and to calculate the inverse of an invertible square matrix.

```c#
using System;
using MatrixDotNet;
using MatrixDotNet.Extensions.Solver;

namespace Samples.Samples
{
    public class GaussSolveSample
    {

        public static void Run()
        {
            // initialize matrix.
            Matrix<double> matrix = new double[,]
            {
                {5, 56, 7},
                {3, 6, 3},
                {5, 9, 15}
            };

            double[] right = { 1, 23, 5 };

            double[] res = matrix.GaussSolve(right);
            for(var i = 0; i < res.Length; i++)
            {
                Console.Write($"x{i}: {res[i]}\n");
            }
        }
    }
}
```

###### Output
```ini
x0: 12,393939393939394
x1: -0,6637806637806638
x2: -3,3997113997114
```
 
#### Conversion Matrix

Conversion of matrix consist in `MatrixDotNet.Extensions.Conversion` which contains static class `MatrixConverter`.

##### How change size of Matrix?

In this sample i`ll demonstrate how you can add column or row in any position of matrix and reduce them.

```c#
using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;

namespace Sample
{
    public class ConverterSample
    {
        public static void Run()
        {
            // Build matrix.
            Matrix<int> matrix = BuildMatrix.Build(3,3,
                (x, y) => x + y * y,
                new[] { 43, 23, 54 },
                new[] { 52, 12, 21 });

            Console.WriteLine("Before conversion:");
            matrix.Pretty();
            
            // Add row to matrix by index.
            Matrix<int> matrixA = matrix.AddRow(new[] { 1, 2, 3 }, 1);
            Console.WriteLine("Add row to matrix on second row index:");
            matrixA.Pretty();
            
            // Add column to matrix by index.
            Matrix<int> matrixB = matrix.AddColumn(new[] { 4, 5, 6 }, 2);
            Console.WriteLine("Add column to matrix on second column index:");
            matrixB.Pretty();
            
            // now we will reduce this new column and row.
            Matrix<int> matrixC = matrix.ReduceColumn(2);
            Matrix<int> matrixD = matrix.ReduceRow(1);
            Console.WriteLine("After reduce:");
            matrixC.Pretty();
            matrixD.Pretty();

            // Also in MatrixDotNet you can join two matrix with the same size row length.
            Matrix<int> matrixF = matrixA.Concat(matrixA);
            matrixF.Pretty();
        }
    }
}
```
#### Output
```ini
Before conversion:

Number of rows: 3
Number of columns: 3

  2747  |  187  |  484  |
  2727  |  167  |  464  |
  2758  |  198  |  495  |

Add row to matrix on second row index:

Number of rows: 4
Number of columns: 3

  2747  |  187  |  484  |
  1     |  2    |  3    |
  2727  |  167  |  464  |
  2758  |  198  |  495  |

Add column to matrix on second column index:

Number of rows: 3
Number of columns: 4

  2747  |  187  |  4  |  484  |
  2727  |  167  |  5  |  464  |
  2758  |  198  |  6  |  495  |

After reduce:

Number of rows: 3
Number of columns: 2

  2747  |  187  |
  2727  |  167  |
  2758  |  198  |


Number of rows: 2
Number of columns: 3

  2747  |  187  |  484  |
  2758  |  198  |  495  |


Number of rows: 4
Number of columns: 6

  2747  |  187  |  484  |  2747  |  187  |  484  |
  1     |  2    |  3    |  1     |  2    |  3    |
  2727  |  167  |  464  |  2727  |  167  |  464  |
  2758  |  198  |  495  |  2758  |  198  |  495  |
```

##### How to cast matrix to two-dimensional array?

```c#
using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;

namespace Sample {

    public class ConverterSample()
    {
        public static void CastRun()
        {
            // Build matrix.
            Matrix<int> matrixA = BuildMatrix.Build(3,3,
                (x, y) => x + y * y,
                new[] { 43, 23, 54 },
                new[] { 52, 12, 21 });
            
            // If you want cast Matrix to two-dimensional
            // array use method ToPrimitive.
            int[,] array = matrixA.ToPrimitive();
            
            // Converts two-dimensional array to Matrix.
            var matrixB = array.ToMatrix();
        }
    }
}
``` 
As you can see for convert matrix to two-dimensional array you can easily use method `ToPrimitive`.

```C#
public class StrassenSample
{
    int[,] matrix = new int[512,512];
    int[,] matrix2 = new int[512,512];

    private Matrix<int> matrix3;
    private Matrix<int> matrix4;

    [GlobalSetup]
    public void Setup()
    {
        Random random = new Random();
        Random random2 = new Random();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = random.Next(1, 10);
            }
        }
        matrix3 = new Matrix<int>(matrix);

        for (int i = 0; i < matrix2.GetLength(0); i++)
        {
            for (int j = 0; j < matrix2.GetLength(1); j++)
            {
                matrix2[i, j] = random2.Next(1, 10);
            }
        }

        matrix4 = new Matrix<int>(matrix2);
    }

    [Benchmark]
    public Matrix<int> Default()
    {
        return matrix3 * matrix4;
    }

    [Benchmark]
    public Matrix<int> Strassen()
    {
        return MatrixExtension.MultiplyStrassen(matrix3, matrix4);
    }
}
```

``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.14393.3808 (1607/AnniversaryUpdate/Redstone1)
Intel Core i5-8250U CPU 1.60GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
Frequency=1757816 Hz, Resolution=568.8878 ns, Timer=TSC
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  Job-YFITZW : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT

IterationCount=5  LaunchCount=1  WarmupCount=5  

```

|   Method |    Mean |   Error |  StdDev |      Gen 0 |     Gen 1 |     Gen 2 | Allocated |
|--------- |--------:|--------:|--------:|-----------:|----------:|----------:|----------:|
|  Default | 69.88 s | 1.241 s | 0.322 s |          - |         - |         - |   1.01 MB |
| Strassen | 43.23 s | 0.991 s | 0.153 s | 30000.0000 | 5000.0000 | 2000.0000 | 174.32 MB |

As you can see algorithm `Strassen` multiply works significant faster(x1.625) than default multiply matrix on big size `M x N`.



#### Statistics

In section statistics yo can find any metrics such as minimum, maximum, median. relative frequencies, distribution 

Also some algorithms works faster to due bit hacks which eliminate branch prediction.

Lets consider simple benchmark which measure finding minimum of matrix with bitwise operation and custom. 
```c#

public class MatrixBitMinVsDefaultMin
{
    private int N = 256;
    private int[,] matrix;
    private Matrix<int> matrix3;
    private Random random = new Random();
    private int[] arr;

    [GlobalSetup]
    public void Setup()
    {
        matrix = new int[N,N];
        arr = new int[N];
        // init matrix random data
        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                matrix[i, j] = random.Next(-255, 255);
            }
        }
        matrix3 = new Matrix<int>(matrix);


    }

    [Benchmark]
    public void DefaultMin()
    { 
        for (int i = 0; i < N; i++)
        {
            arr[i] = matrix3.Min();   
        }
    }
    
    [Benchmark]
    public void BitMin()
    {
        for (int i = 0; i < N; i++)
        {
            arr[i] = matrix3.BitMin();   
        }
    }
}

```

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.14393.3808 (1607/AnniversaryUpdate/Redstone1)
Intel Core i5-8250U CPU 1.60GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
Frequency=1757816 Hz, Resolution=568.8878 ns, Timer=TSC
  [Host]     : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
  DefaultJob : .NET Framework 4.8 (4.8.4180.0), X86 LegacyJIT
```
|     Method |       Mean |    Error |   StdDev | Code Size |
|----------- |-----------:|---------:|---------:|----------:|
| DefaultMin | 1,117.2 ms | 12.93 ms | 11.46 ms |     292 B |
|     BitMin |   646.5 ms |  4.16 ms |  3.25 ms |     165 B |

As you can see BitMin() method works faster(x1.725) than DefaultMin(). Because we eliminate branch prediction.
See more information about Bitwise operations in [article](https://toor1245.github.io/MatrixDotNet/articles/intro.html).


> See more information [site](https://toor1245.github.io/MatrixDotNet/) 
