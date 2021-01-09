# MatrixAsFixedBuffer Overview

`MatrixOnStack` is a struct which intended for prevent memory allocation in difference of `Matrix<T>`.

`MatrixOnStack` works same like `Matrix<T>` but max length of fixed buffer 6 561, thus you can create
maximum size of matrix `80 x 80` and supports only `double` data type.

> Located in `MatrixDotNet.Extensions.Performance`

### Initialize of matrix as fixed buffer
First way for initialize of matrix is just assign number of rows and columns.

```c#
MatrixOnStack ma = new MatrixOnStack(5, 5);
```
Where rows and columns are `byte` data type for aligned struct.

next way is pass two-dimensional array `double[,]`.

```c#
double[,] matrix = new double[5, 5];
MatrixOnStack ma = new MatrixOnStack(matrix);
```

you even can pass `Matrix<T>` in implicit or explicit way.
```c#
Matrix<double> matrix = new Matrix<int>(5, 5);
MatrixOnStack ma = new MatrixOnStack(matrix);
```

Or 
```c#
Matrix<double> matrix = new Matrix<int>(5, 5);
MatrixOnStack ma = matrix;
```

### Access to buffer

In `MatrixAsFixedBuffer` not ways for access ot fixed buffer, 
however you can works with property `Data` which returns `Span<double>`

```c#
MatrixOnStack matrix = new MatrixOnStack(5, 5);
var span = ma.Data;
```
Provides a lot of functionality for working with a buffer and it will be even faster than `Matrix<T>`.

So, if you want to obtain row of matrix you need to use same like in `Matrix<T>` overload of index.
```c#
MatrixOnStack matrix = new MatrixOnStack(5, 5);
matrix[0] = { 1, 2, 3, 4, 5};
matrix[1, 1] = 10;
```

### Basic operations
Basic operations same like `Matrix<T>` too, 
but passing parameters works through `ref` it is intended for performance.

[!code-csharp[BenchByRef.cs](../../../../tests/MatrixDotNet.PerformanceTesting/MatrixOnStackBenchmarks/BenchByRef.cs)]

```ini 
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1110 (1909/November2018Update/19H2)
Intel Core i7-9700 CPU 3.00GHz, 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=5.0.100
[Host]    : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
RyuJitX64 : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT

Job=RyuJitX64  Jit=RyuJit  Platform=X64
```

|                Method |     Mean |   Error |  StdDev | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------------- |---------:|--------:|--------:|------:|------:|------:|----------:|
|       Struct32BAccess | 240.2 ns | 2.82 ns | 2.36 ns |     - |     - |     - |         - |
|  Struct32BAccessByRef | 211.2 ns | 0.92 ns | 0.82 ns |     - |     - |     - |         - |
|      Struct112BAccess | 703.1 ns | 6.01 ns | 5.62 ns |     - |     - |     - |         - |
| Struct112BAccessByRef | 240.5 ns | 4.73 ns | 4.86 ns |     - |     - |     - |         - |

Let's consider the following samples and make benchmarks.

`MatrixOnStack.MulByRef` is used for multiplication of two matrices.
```c#
MatrixOnStack ma = new MatrixOnStack(5, 5);
MatrixOnStack mb = new MatrixOnStack(5, 5);
MatrixOnStack mc = MatrixOnStack.MulByRef(ma, mb);
```

`MatrixOnStack.AddByRef` is used for add of two matrices.
```c#
MatrixOnStack ma = new MatrixOnStack(5, 5);
MatrixOnStack mb = new MatrixOnStack(5, 5);
MatrixOnStack mc = MatrixOnStack.AddByRef(ma, mb);
```
[!code-csharp[BenchAddMatrixOnStack.cs](../../../../tests/MatrixDotNet.PerformanceTesting/MatrixOnStackBenchmarks/BenchAddMatrixonStackVsMatrix.cs)]

```ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.18363.1110 (1909/November2018Update/19H2)
Intel Core i7-9700 CPU 3.00GHz, 1 CPU, 8 logical and 8 physical cores
.NET Core SDK=5.0.100
[Host]     : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
DefaultJob : .NET Core 5.0.0 (CoreCLR 5.0.20.51904, CoreFX 5.0.20.51904), X64 RyuJIT
```

|                 Method |     Mean |     Error |    StdDev |  Gen 0 |  Gen 1 | Gen 2 | Allocated |
|----------------------- |---------:|----------:|----------:|-------:|-------:|------:|----------:|
|       AddMatrixOnStack | 4.332 us | 0.0851 us | 0.1247 us |      - |      - |     - |         - |
|              AddMatrix | 4.543 us | 0.0901 us | 0.1800 us | 8.1253 | 2.0294 |     - |   51256 B |

As you can see difference only in memory allocation.

> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).