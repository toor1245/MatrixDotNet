using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Simd
{
    public static partial class Simd
    {
        /// <summary>
        /// Multiplies block matrix. 
        /// </summary>
        /// <param name="matrixA">left matrix</param>
        /// <param name="matrixB">right matrix</param>
        /// <returns>new matrix after multiply of two matrices</returns>
        /// <exception cref="MatrixDotNetException">matrices must be square with 16 block size</exception>
        public static unsafe Matrix<float> BlockMultiply(Matrix<float> matrixA, Matrix<float> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
            {
                throw new MatrixDotNetException("MatrixA must be square");
            }

            var m = matrixA.Rows;
            var n = matrixA.Columns;
            var length = matrixA.Length;
            var matrixC = new Matrix<float>(m, n);
            var K = matrixA.Columns;
            var size = Vector256<float>.Count;
            var ptrT = stackalloc float[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            fixed (float* ptrA = matrixA.GetArray())
            fixed (float* ptrB = matrixB.GetArray())
            fixed (float* ptrC = matrixC.GetArray())
            {
                var span1 = new Span<float>(ptrA, length);
                for (int i = 0; i < m; i++)
                {
                    float* c = ptrC + i * n;

                    for (int k = 0; k < K; k++)
                    {
                        float* b = ptrB + k * n;
                        FillFloatX4(ptrT, 8, span1[i * K + k]);
                        var va = Avx.LoadVector256(ptrT);
                        for (int j = 0; j < n; j += 16)
                        {
                            var vb1 = Avx.LoadVector256(b + j);
                            var vb2 = Avx.LoadVector256(b + j + size);

                            var vc1 = Avx.LoadVector256(c + j);
                            var vc2 = Avx.LoadVector256(c + j + size);

                            Avx.Store(c + j + 0, Fma.MultiplyAdd(va, vb1, vc1));
                            Avx.Store(c + j + size, Fma.MultiplyAdd(va, vb2, vc2));
                        }
                    }
                }
            }

            return matrixC;
        }

        private static unsafe void FillFloatX4(float* p, int count, float value)
        {
            for (; count != 0; count -= 4)
            {
                *p++ = value;
                *p++ = value;
                *p++ = value;
                *p++ = value;
            }
        }
    }
}