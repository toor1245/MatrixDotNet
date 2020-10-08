using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Optimization.Simd.Statistics
{
    public static partial class Simd
    {
        public static unsafe int GreaterThan(Matrix<float> matrixA,Matrix<float> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows || matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("matrixA not equal size matrixB");
            
            bool result = false;
            fixed(float* ptrB = matrixB.GetMatrix())
            fixed(float* ptrA = matrixA.GetMatrix())
            {
                int i = 0;
                int size = Vector128<float>.Count;
                var ymm1 = Vector128<float>.Zero;
                var ymm2 = Vector128<float>.Zero;
                while (i < matrixA.Length - size)
                {
                    ymm1 = Sse.LoadVector128(ptrA + i);
                    Console.WriteLine(ymm1);
                    ymm2 = Sse.LoadVector128(ptrB + i);
                    Console.WriteLine(ymm2);
                    var ymm3 = Sse.CompareGreaterThan(ymm2, ymm1);
                    Console.WriteLine(ymm3);
                    Avx.
                    i += size;
                }
                
            }

            return result;
        }
    }
}