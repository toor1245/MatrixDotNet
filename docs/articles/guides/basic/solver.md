# Solving a linear system overview

### Kramer solve
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
            { 5, 56, 7  },
            { 3, 6,  3  },
            { 5, 9,  15 }
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
```
x0: 12,393939393939394
x1: -0,6637806637806638
x2: -3,3997113997114
```

### Gauss solve
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
                { 5, 56, 7  },
                { 3, 6,  3  },
                { 5, 9,  15 }
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
```
x0: 12,393939393939394
x1: -0,6637806637806638
x2: -3,3997113997114
```

> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).