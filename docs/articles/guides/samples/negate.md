# Sample: Introduction In Negate

This sample demonstrates how to negate matrix.

```c#
    var matrix = new Matrix<short>(n, n);
    var negate = -matrix;
```

You can use `Negate` method for .NET Core 3.1+ will work faster due to SIMD.
```c#
    var matrix = new Matrix<short>(n, n);
    var negate = Matrix<short>.Negate(matrix);
```

|          Method |       Mean |    Error |   StdDev |     Median |    Gen 0 |    Gen 1 |    Gen 2 | Allocated |
|---------------- |-----------:|---------:|---------:|-----------:|---------:|---------:|---------:|----------:|
|     NegateSByte | 2,852.3 us | 225.1 us | 656.5 us | 2,995.7 us | 109.3750 | 109.3750 | 109.3750 |      4 MB |
| NegateSByteSimd |   710.4 us | 107.1 us | 314.1 us |   838.8 us |  54.6875 |  54.6875 |  54.6875 |      1 MB |

> [!WARNING]
> SIMD supports only `int`, `short`, `byte`. 
> Performance depends on the data type of the Matrix.

> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).