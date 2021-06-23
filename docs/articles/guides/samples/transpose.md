# Sample: Introduction In Transpose

This sample demonstrates how to transpose matrix.

```c#
    using MatrixDotNet.Extensions.Conversion;

    var matrix = new Matrix<int>(n, n);
    matrix.Transpose();
```

If you use matrix *8x8* with float data type on .NET Core 3.1+, 
you can improve performance of the calculation with happen `TransposeXVectorSize`

```c#
    using MatrixDotNet.Extensions.Conversion;

    var matrix = BuildMatrix.RandomFloat(m, n, start, end);
    matrix.TransposeXVectorSize();
```

> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).