# Multiplication of matrices overview

In this section we consider ways multiplication of matrix and measure it on performance and will make conclusion.

MatrixDotNet have ways multiplications:
* Default multiplication
* Strassen multiplication
* BlockX16 multiplication 

### Multiplication with happen overload operator
For default multiplication need just to use overload operator `*`
```c#
Matrix<int> a = new Matrix<int>(5, 5);
Matrix<int> b = new Matrix<int>(5, 5);
Matrix<int> c = a * b;
```
It is the better way to use multiplication for readable.

### Multiplication optimize
Also in MatrixDotNet exists optimize multiplication which significant faster and soon will be move to overload operator

```c#
Matrix<int> a = new Matrix<int>(35, 35);
Matrix<int> b = new Matrix<int>(35, 35);
Matrix<int> c = Optimization.Multiply(a, b);
```

> [!NOTE] 
> For now supported int and double data type. 

### Strassen's multiplication
In linear algebra, the Strassen algorithm, named after Volker Strassen, 
is an algorithm for matrix multiplication.
It is faster than the standard matrix multiplication algorithm and is useful in practice for large matrices,
but would be slower than the fastest known algorithms for extremely large matrices.

Asymptotic complexity:

![](https://wikimedia.org/api/rest_v1/media/math/render/svg/511e64be8e75258905f4b3c61d73de72080e643c)

If you have matrix with big size and matrix is square and prime (n / 2), you can use method `MultiplyStrassen`
which located in `MatrixDotNet.Extensions.Performance.Operations`

```c#
Matrix<int> a = new Matrix<int>(1024, 1024);
Matrix<int> b = new Matrix<int>(1024, 1024);
Matrix<int> c = Optimization.MultiplyStrassen(a, b);
```

> [!NOTE] 
> for more information about Strassen's multiplication you can move to [Strassen algorithm](https://en.wikipedia.org/wiki/Strassen_algorithm)

### BlockX16 multiplication
It is multiplication works with simd and significant faster than all multiplication above, however it can be used only when matrix is square and have 
size of matrix multiply of 16. So for using blockX16 mul intended method 'Simd.BlockMultiply'

```c#
Matrix<float> a = new Matrix<float>(32, 32);
Matrix<float> b = new Matrix<float>(32, 32);
Matrix<float> c = Optimization.BlockMultiply(a, b);
```

### Benchmark for comparing multiplication of matrix

[!code-csharp[Run](../../../../tests/MatrixDotNet.PerformanceTesting/Matrix/MathOperations/MultTest.cs)]

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1110 (1909/November2018Update/19H2)
Intel Core i7-9700 CPU 3.00GHz, 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=5.0.100
[Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT

|              Method | Size |      Mean |     Error |    StdDev |   Gen 0 |   Gen 1 |   Gen 2 | Allocated |
|-------------------- |----- |----------:|----------:|----------:|--------:|--------:|--------:|----------:|
|             Default |  512 | 728.33 ms | 14.198 ms | 25.961 ms |       - |       - |       - |      2 MB |
| OptimizationDefault |  512 |  78.19 ms |  1.447 ms |  1.981 ms |       - |       - |       - |      2 MB |
|            Strassen |  512 |  75.71 ms |  0.760 ms |  0.674 ms |       - |       - |       - |      2 MB |
|            BlockX16 |  512 |  12.22 ms |  0.158 ms |  0.148 ms | 78.1250 | 78.1250 | 78.1250 |      1 MB |

> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).
 

