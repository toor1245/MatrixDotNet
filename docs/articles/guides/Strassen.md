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
