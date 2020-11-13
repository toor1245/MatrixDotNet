# Overview

### Install
Create new console application and install the [MatrixDotNet](https://www.nuget.org/packages/MatrixDotNet/) NuGet package. We support:

* Projects: classic and modern with PackageReferences
* Runtimes: Full .NET Framework (4.6+), .NET Core (2.0+), Mono
* OS: Windows, Linux, MacOS
* Languages: C#

### How to use Matrix?
MatrixDotNet supports all simple operations related matrix.

##### How to create matrix?

```c#
class Program
{
	static void Main(string[] args)
	{
        int[,] a = new int[3, 3]
        {
            { 10, -7, 0 },
            { -3, 6,  2 },
            { 5, -1,  5 }
        };
        
        // First way. 
        Matrix<int> matrixA = new Matrix<int>(a);
        
        // Second way: primitive way, assign by deep copy nor by reference!!!
        Matrix<int> matrixB = a;
        
        Matrix<int> matrixC = new int[10, 10];

        Matrix<int> matrixD = new[,]
        {
            {1, 2, 3},
            {2 ,4, 6},
        };
        
        // Third way initialize all values 0 or constant value.
        Matrix<int> matrixE = new Matrix<int>(row:5,col:3);
        
        Matrix<int> matrixF = new Matrix<int>(row:3,col:5,value:5);
    }
}
```
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





