# Sample: Introduction In Random

This sample demonstrates how to generate random matrix.

there are methods for generating random of the matrix:
* `BuildMatrix.RandomByte`
* `BuildMatrix.RandomSByte`
* `BuildMatrix.RandomInt`
* `BuildMatrix.RandomShort`
* `BuildMatrix.RandomLong`
* `BuildMatrix.RandomFloat`
* `BuildMatrix.RandomDouble`
* `BuildMatrix.BuildRandom<T>`

```c#
    using MatrixDotNet.Extensions.Builder;
    Matrix<int> matrixA = BuildMatrix.BuildRandom<int>(n, n);
    Matrix<int> matrixB = BuildMatrix.RandomInt(n, n);
```

If you want to specify a range for generating random values:
```c#
    int start = -10;
    int end = 10;
    Matrix<int> matrixB = BuildMatrix.RandomInt(n, n, start, end);
```

> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).

