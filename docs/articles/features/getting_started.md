 # Works with big size matrix overview.
 Sometimes there are cases when matrix size very big, speed calculation may be about 20-30 minutes.
 For this cases created `MatrixDotNet.Extensions.Core`.
 
 ### Overview 
 
 #### Install 
 Create new console application and install the [MatrixDotNet](https://www.nuget.org/packages/MatrixDotNet/) NuGet package and [MatrixDotNet.Extensions.Core](https://www.nuget.org/packages/MatrixDotNet.Extensions.Core/). We support:
 
 * Projects: classic and modern with PackageReferences
 * Runtimes: .NET Core (3.1+)
 * OS: Windows, Linux, MacOS
 * Languages: C#
 
 #### Design namespace MatrixDotNet.Extensions.Core
 MatrixDotNet.Extensions.Core divided by two namespace: `Unsafe` and `Simd`.
 
 ##### Unsafe  
 `Unsafe` namespace use unsafe programming which more functionality than `Simd`.
 
 unsafe programming significant increase calculation matrix.
 
 ###### Lets consider simple operations add two matrices.
 ```c#
using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe;

namespace Sample
{
    [RyuJitX64Job]
    public class AddMatrix
    {
        private Matrix<int> _matrixA;
        private Matrix<int> _matrixB;

        [GlobalSetup]
        public void Setup()
        {
            _matrixA = BuildMatrix.Random<int>(1024,1024,1,10);
            _matrixB = BuildMatrix.Random<int>(1024, 1024,1,10);
        }

        [Benchmark]
        public Matrix<int> DefaultAdd()
        {
            return _matrixA + _matrixB;
        }

        [Benchmark]
        public Matrix<int> UnsafeAdd()
        {
            return UnsafeMatrix.Add(_matrixA, _matrixB);
        }
        
    }
}
```
 
  
 ``` ini
 BenchmarkDotNet=v0.12.1, OS=Windows 10.0.14393.3866 (1607/AnniversaryUpdate/Redstone1)
 Intel Core i5-8250U CPU 1.60GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
 Frequency=1757813 Hz, Resolution=568.8887 ns, Timer=TSC
 .NET Core SDK=3.1.400
   [Host]    : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
   RyuJitX64 : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
 
 Job=RyuJitX64  Jit=RyuJit  Platform=X64  
 ```
 |     Method |       Mean |     Error |    StdDev |
 |----------- |-----------:|----------:|----------:|
 | DefaultAdd | 115.288 ms | 0.8163 ms | 0.9073 ms |
 |  UnsafeAdd |   1.279 ms | 0.0253 ms | 0.0602 ms |

As you can see unsafe add two matrices calculates significant faster.


##### SIMD
 `Simd` namespace use `single instruction - multiple data`.
  SIMD -  it is instructions which works with vector and can increase performance 2x - 8x speed calculations.
 
 ```c#
using BenchmarkDotNet.Attributes;
using MatrixDotNet;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Core.Optimization.Simd;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe;

namespace Sample
{
    public class SimdSample
    {
        private Matrix<int> _matrixA;
        private Matrix<int> _matrixB;

        [GlobalSetup]
        public void Setup()
        {
            _matrixA = BuildMatrix.Random<int>(1024,1024,1,10);
            _matrixB = _matrixA.Clone() as Matrix<int>;
        }
        
        [Benchmark]
        public bool Equals()
        {
            return _matrixA == _matrixB;
        }

        [Benchmark]
        public bool EqualsSimd()
        {
            return Simd.Equals(_matrixA, _matrixB);
        }

        [Benchmark]
        public bool EqualsUnsafe()
        {
            return UnsafeMatrix.Equals(_matrixA, _matrixB);
        }
    }
}
```
 
 ``` ini
 
 BenchmarkDotNet=v0.12.1, OS=Windows 10.0.14393.3866 (1607/AnniversaryUpdate/Redstone1)
 Intel Core i5-8250U CPU 1.60GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
 Frequency=1757813 Hz, Resolution=568.8887 ns, Timer=TSC
 .NET Core SDK=3.1.400
   [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
   DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
 
 
 ```
 |       Method |        Mean |     Error |    StdDev |
 |------------- |------------:|----------:|----------:|
 |       Equals | 36,472.6 μs | 216.83 μs | 181.06 μs |
 |   EqualsSimd |    449.4 μs |   3.73 μs |   3.31 μs |
 | EqualsUnsafe |    523.9 μs |   3.06 μs |   2.71 μs |
 
 As you can see SIMD faster equals unsafe and default.
 
 If you have anu question, you can ask it on [gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge) 