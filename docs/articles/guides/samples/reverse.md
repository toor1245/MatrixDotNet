# Sample: Introduction In Reverse

This sample demonstrates how to reverse matrix or vector.

```c#
    using MatrixDotNet.Extensions.Conversion;

    var matrix = new Matrix<int>(n, n);
    var reverse = MatrixConverter.Reverse(matrix);
```

```c#
    using MatrixDotNet.Vectorization;

    var vector = new Vector<int>(n);
    var reverse = VectorExtension.Reverse(vector);
```

Let's compare BCL method 'Array.Reverse' with `MatrixConverter.Reverse` on performance.

```c#
using System;
using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Vectorization;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    [MemoryDiagnoser]
    public class BenchReverse : PerformanceTest
    {
        private Matrix<int> _matrix;
        private Matrix<byte> _matrixByte;
        private Matrix<float> _matrixFloat;
        private Matrix<double> _matrixDouble;
        
        [GlobalSetup]
        public void Setup()
        {
            _matrix = BuildMatrix.RandomInt(1024, 1024, 0, 20);
            _matrixByte = BuildMatrix.RandomByte(1024, 1024, 0, 20);
            _matrixFloat = BuildMatrix.RandomFloat(1024, 1024, 0, 20);
            _matrixDouble = BuildMatrix.RandomDouble(1024, 1024, 0, 20);
        }
        
        [Benchmark]
        public void ReverseIntBcl()
        {
            Array.Reverse(_matrix.GetArray());
        }

        [Benchmark]
        public void ReverseIntSimd()
        {
            MatrixConverter.Reverse(_matrix.GetArray());
        }
        
        [Benchmark]
        public void ReverseByteBcl()
        {
            Array.Reverse(_matrixByte.GetArray());
        }
        
        [Benchmark]
        public void ReverseByteSimd()
        {
            Vector<int> vector = new Vector<int>(4);
            VectorExtension.Reverse(vector);
            MatrixConverter.Reverse(_matrixByte);
        }
        
        [Benchmark]
        public void ReverseFloatBcl()
        {
            
            Array.Reverse(_matrixFloat.GetArray());
        }
        
        [Benchmark]
        public void ReverseFloatSimd()
        {
            MatrixConverter.Reverse(_matrixFloat);
        }
    }
}
```

> [!WARINING]
> Performance depends on the data type of the Matrix.
> SIMD only supports `long`, `ulong`, `int`, `uint`, `float`, `double`, `byte` `sbyte`.

|           Method |      Mean |     Error |    StdDev |    Median |
|----------------- |----------:|----------:|----------:|----------:|
|    ReverseIntBcl | 473.90 us | 34.182 us |  96.41 us | 436.48 us |
|   ReverseIntSimd | 169.55 us |  9.005 us |  24.80 us | 164.75 us |
|   ReverseByteBcl | 450.35 us | 53.236 us | 156.13 us | 374.64 us |
|  ReverseByteSimd |  46.14 us |  5.925 us |  17.38 us |  36.86 us |
|  ReverseFloatBcl | 510.02 us | 45.197 us | 131.12 us | 451.06 us |
| ReverseFloatSimd | 218.27 us | 20.249 us |  57.44 us | 200.13 us |


> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).