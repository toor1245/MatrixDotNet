# Overview

### Install
Create new console application and install the [MatrixDotNet](https://www.nuget.org/packages/MatrixDotNet/) NuGet package. We support:

* Projects: classic and modern with PackageReferences
* Runtimes: .NETStandard 2.1, NET Core 3.1+
* OS: Windows, Linux, MacOS
* Languages: C#

MatrixDotNet contains three main structures: 
* <a href = "#Matrix">Matrix<T></a>
* <a href = "#Vector">Vector<T></a>
* <a href = "#MatrixComplex">MatrixComplex</a>

##### How to create matrix?

[!code-csharp[CreateMatrixSample.cs](../../samples/Samples/Samples/MatrixSamples/MatrixCreateSample.cs)]

##### Simple operations of matrix.

```c#
public class SimpleOperations
{
    public static void Run()
    {
        // init matrix
        int[,] a = new int[3, 3]
        {
            { 10, -7, 0 },
            { -3, 6,  2 },
            { 5, -1,  5 }
        };

        int[,] b = new int[3, 4]
        {
            { 11, -2, 1, 6 },
            { -8, 4,  2, 3 },
            { 4, -4,  5, 8 },
        };
        
        int[] vector = {2, 7, 5, 4};

        int k = 3;
        
        Matrix<int> matrixA = a;

        Matrix<int> matrixB = b;
            
        // Multiply.
        Matrix<int> matrixC = matrixA * matrixB;
        Matrix<int> matrixD = matrixC * k;
        
        // Sum .
        Matrix<int> matrixE = matrixB + k * matrixB;
        
        // Subtract.
        Matrix<int> matrixF = 2 * matrixA - matrixA;
        
        // Divide.
        Matrix<int> matrixG = matrixA / 2;
        
        // Multiply to vector
        int[] vectorA = matrixB * vector;

        // Pretty output.
        matrixA.Pretty();
        matrixB.Pretty();
        matrixC.Pretty();
        matrixD.Pretty();
        matrixE.Pretty();
        matrixF.Pretty();
        matrixG.Pretty();
        
        Console.Write("VectorA result: ");
        foreach (var i in vectorA)
        {
            Console.Write(i + " ");
        }
    }
}
```

##### Output
```ini
Number of rows: 3
Number of columns: 3

  10,00       |  -7,00       |  0,00        |
  -3,00       |  6,00        |  2,00        |
  5,00        |  -1,00       |  5,00        |


Number of rows: 3
Number of columns: 4

  11,00       |  -2,00       |  1,00        |  6,00        |
  -8,00       |  4,00        |  2,00        |  3,00        |
  4,00        |  -4,00       |  5,00        |  8,00        |


Number of rows: 3
Number of columns: 4

  166,00      |  -48,00      |  -4,00       |  39,00       |
  -73,00      |  22,00       |  19,00       |  16,00       |
  83,00       |  -34,00      |  28,00       |  67,00       |


Number of rows: 3
Number of columns: 4

  498,00      |  -144,00     |  -12,00      |  117,00      |
  -219,00     |  66,00       |  57,00       |  48,00       |
  249,00      |  -102,00     |  84,00       |  201,00      |


Number of rows: 3
Number of columns: 4

  44,00       |  -8,00       |  4,00        |  24,00       |
  -32,00      |  16,00       |  8,00        |  12,00       |
  16,00       |  -16,00      |  20,00       |  32,00       |


Number of rows: 3
Number of columns: 3

  10,00       |  -7,00       |  0,00        |
  -3,00       |  6,00        |  2,00        |
  5,00        |  -1,00       |  5,00        |


Number of rows: 3
Number of columns: 3

  5,00        |  -3,00       |  0,00        |
  -1,00       |  3,00        |  1,00        |
  2,00        |  0,00        |  2,00        |

VectorA result: 37 34 37 0
```

If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).





