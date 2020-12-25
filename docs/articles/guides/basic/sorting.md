# Sorting overview.

#### Lets consider how in MatrixDotNet can sort matrix.

Sorting of matrix consist in `MatrixDotNet.Extensions.Sorting`. Also we will use `MatrixDotNet.Extensions.Builder` which contains static class `BuildMatrix` for 
to simplify matrix creation.

```c#
using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Sorting;

namespace Sample
{
    public class BubbleSortSample
    {
        public static void Run()
        {
            Matrix<int> matrixA = BuildMatrix.Build(5, 5, 
                (x,y) => x * x + y,
                new[] { -1, -232, 3, 4, 5 },
                new[] { -5, -6, -9, 132, 12 } 
                );

            Console.WriteLine("initial matrix view:");
            matrixA.Pretty();
            
            // 1. Sorts whole matrix. 
            matrixA.Sort();
            Console.WriteLine("Bubble sort MatrixA");
            matrixA.Pretty();
            
            // Builds Identity matrix.
            Matrix<int> matrixE = BuildMatrix.CreateIdentityMatrix<int>(4,4);
            
            // 2. Sorts all rows of matrix.
            matrixE.SortByRows();
            Console.WriteLine("Sorts matrixE by rows:");
            matrixE.Pretty();
            
            
            // 3. Sorts all columns of matrix.
            matrixE.SortByColumns();
            Console.WriteLine("Sorts matrixE by columns:");
            matrixE.Pretty();
        }
    }
}
```

#### Output

```
initial matrix view:

Number of rows: 5
Number of columns: 5

  -4     |  -5     |  -8     |  133    |  13     |
  53819  |  53818  |  53815  |  53956  |  53836  |
  4      |  3      |  0      |  141    |  21     |
  11     |  10     |  7      |  148    |  28     |
  20     |  19     |  16     |  157    |  37     |

Bubble sort MatrixA

Number of rows: 5
Number of columns: 5

  -8     |  -5     |  -4     |  0      |  3      |
  4      |  7      |  10     |  11     |  13     |
  16     |  19     |  20     |  21     |  28     |
  37     |  133    |  141    |  148    |  157    |
  53815  |  53818  |  53819  |  53836  |  53956  |

Sorts matrixE by rows:

Number of rows: 4
Number of columns: 4

  0  |  0  |  0  |  1  |
  0  |  0  |  0  |  1  |
  0  |  0  |  0  |  1  |
  0  |  0  |  0  |  1  |

Sorts matrixE by columns:

Number of rows: 4
Number of columns: 4

  0  |  0  |  0  |  1  |
  0  |  0  |  0  |  1  |
  0  |  0  |  0  |  1  |
  0  |  0  |  0  |  1  |
```

> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).
