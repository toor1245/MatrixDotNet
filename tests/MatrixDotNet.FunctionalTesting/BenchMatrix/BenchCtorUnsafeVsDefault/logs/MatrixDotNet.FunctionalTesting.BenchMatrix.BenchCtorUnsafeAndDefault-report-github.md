``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.14393.3986 (1607/AnniversaryUpdate/Redstone1)
Intel Core i5-8250U CPU 1.60GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
Frequency=1757816 Hz, Resolution=568.8878 ns, Timer=TSC
.NET Core SDK=3.1.400
  [Host]     : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT
  DefaultJob : .NET Core 3.1.6 (CoreCLR 4.700.20.26901, CoreFX 4.700.20.31603), X64 RyuJIT


```
|          Method |      Mean |    Error |   StdDev |
|---------------- |----------:|---------:|---------:|
|  UnsafeCtorTest |  97.76 μs | 1.953 μs | 5.315 μs |
| DefaultCtorTest | 406.29 μs | 7.000 μs | 6.548 μs |
