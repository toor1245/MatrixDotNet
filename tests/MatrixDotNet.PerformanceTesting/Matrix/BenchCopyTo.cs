using System;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.PerformanceTesting.Matrix
{
    public class BenchCopyTo : PerformanceTest
    {
        private Matrix<byte> _matrix;
        private Matrix<byte> _matrix2;

        [GlobalSetup]
        public void Setup()
        {
            _matrix = BuildMatrix.RandomByte(1024, 1024);
            _matrix2 = _matrix.Clone() as Matrix<byte>;
        }

        [Benchmark]
        public void CopyToMatrixConverter()
        {
            MatrixConverter.CopyTo(_matrix, 0, 0, _matrix2, 0, 0, 1024);
        }
        
        [Benchmark]
        public void CopyToBcl()
        {
            Array.Copy(_matrix.GetArray(), 0, _matrix2.GetArray(), 0, 1024);
        }
        
        [Benchmark]
        public unsafe void CopyToUnsafe()
        {
            fixed (byte* destPtr = _matrix2.GetArray())
            fixed (byte* srcPtr = _matrix.GetArray())
            {
                Unsafe.CopyBlock(destPtr, srcPtr, sizeof(byte) * 1024);
            }
        }
        
        [Benchmark]
        public unsafe void CopyToMemory()
        {
            fixed (byte* destPtr = _matrix2.GetArray())
            fixed (byte* srcPtr = _matrix.GetArray())
            {
                Unsafe.CopyBlock(destPtr, srcPtr, sizeof(byte) * 1024);
            }
        }
        
        [Benchmark]
        public unsafe void CopyToSpan()
        {
            fixed (byte* destPtr = _matrix2.GetArray())
            fixed (byte* srcPtr = _matrix.GetArray())
            {
                Span<byte> span = new Span<byte>(srcPtr, 1024);
                Span<byte> dest = new Span<byte>(destPtr, 1024);
                span.CopyTo(dest);
            }
        }
    }
}