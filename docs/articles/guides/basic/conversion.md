# Conversion matrix overview.
Conversion of matrix consist in `MatrixDotNet.Extensions.Conversion` which contains static class `MatrixConverter`.

### How change size of Matrix?

##### Lets consider the following sample.
In this sample demonstrates how you can add column or row in any position of matrix and reduce them.

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
> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).
