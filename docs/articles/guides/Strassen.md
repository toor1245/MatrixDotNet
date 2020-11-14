# Strassen algorithm in MatrixDotNet overview
`Strassen` is a recursive algorithm, which multiplies two matrices of size `n x n` during O(n<sup>lg7</sup>)) = O(n<sup>2.81</sup>) For large enough n where `n > 32` algorithm Strassen works faster then usual multiply.

#### How to use algorithm Strassen?

Lets consider the following sample:

```c#
public class StrassenDemonstrate
{
    public static void Run()
    {
        // 1. Initialize matrix.
        Matrix<int> matrixA = new int[128, 128];
        Matrix<int> matrixB = new int[128, 128];
        
        // 2. Initialize random values two matrices.
        Random random1 = new Random();
        Random random2 = new Random();

        for (int i = 0; i < matrixA.Rows; i++)
        {
            for (int j = 0; j < matrixA.Columns; j++)
            {
                matrixA[i, j] = random1.Next(1,10);
                matrixB[i, j] = random2.Next(1,10);
            }
        }
        
        // 3. Use static class MatrixExtension to use algorithm Strassen multiply.
        Matrix<int> matrixC = MatrixExtension.MultiplyStrassen(matrixA,matrixB);
    }
}
```
As you can see for use algorithm `Strassen` multiply matrix used static class `MathExtension`.
#### When to use Strassen?

###### The important that you remember it is algorithm Strassen should use when matrix square, n is prime(for example matrix 65x65 not will be works!!!) and n > 32.
 
#### Lets consider benchmarking which we compare default multiply and `Strassen`
```c#
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
In this benchmark we multiplies two matrices of size `512x512` with random values.

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

As you can see algorithm Strassen multiply works significant faster(x1.625) than default multiply matrix on big size `512x512`.

##### If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).