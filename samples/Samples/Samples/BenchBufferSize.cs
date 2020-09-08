using System;
using BenchmarkDotNet.Attributes;

namespace Samples.Samples
{
    [MemoryDiagnoser]
    [RyuJitX64Job]
    public class BenchBufferSize
    {
        public unsafe void Get()
        {
            var data = stackalloc FixedBuffer[10];
            for (int i = 0; i < data->GetData.Length; i++)
            {
                Console.WriteLine(data[i].Array[i] = 10);
            }
        }
    }

    public unsafe struct FixedBuffer
    {
        public const int Size = 2048;
        public fixed int Array[Size];
        
        public Span<int> GetData
        {
            get
            {
                fixed (int* ptr = Array)
                {
                    return new Span<int>(ptr,Size);
                }
            }            
        }
    }
    
}