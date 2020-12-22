#if NET5_0 || NETCOREAPP3_1
using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.NetCore.Simd
{
    public static partial class Simd
    {
        public static unsafe Matrix<int> Add(Matrix<int> matrixA, Matrix<int> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");

            var m = matrixA.Rows;
            var n = matrixA.Columns;
            var length = matrixA.Length;

            var i = 0;

            var matrix = new Matrix<int>(m, n);
            var lastIndexBlock = 8;

            fixed (int* pointer1 = matrixA.GetArray())
            fixed (int* pointer2 = matrixB.GetArray())
            fixed (int* pointer3 = matrix.GetArray())
            {
                var span1 = new Span<int>(pointer1, length);
                var span2 = new Span<int>(pointer2, length);
                var span3 = new Span<int>(pointer3, length);

                if (Avx2.IsSupported)
                {
                    var vresult = Vector256<int>.Zero;
                    while (i < length - lastIndexBlock)
                    {
                        vresult = Avx2.Add(Avx.LoadVector256(pointer1 + i), Avx.LoadVector256(pointer2 + i));
                        Avx.Store(pointer3 + i, vresult);
                        i += 4;
                    }
                }
                else if (Sse2.IsSupported)
                {
                    var vresult = Vector128<int>.Zero;
                    while (i < length - lastIndexBlock)
                    {
                        vresult = Sse2.Add(Sse2.LoadVector128(pointer1 + i), Sse2.LoadVector128(pointer2 + i));
                        Sse2.Store(pointer3 + i, vresult);
                        i += 4;
                    }
                }


                while (i < length)
                {
                    span3[i] = span1[i] + span2[i];
                    i++;
                }
            }

            return matrix;
        }

        public static unsafe Matrix<long> Add(Matrix<long> matrixA, Matrix<long> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");

            var m = matrixA.Rows;
            var n = matrixA.Columns;
            var length = matrixA.Length;

            var i = 0;

            var matrix = new Matrix<long>(m, n);
            var lastIndexBlock = 8;

            fixed (long* pointer1 = matrixA.GetArray())
            fixed (long* pointer2 = matrixB.GetArray())
            fixed (long* pointer3 = matrix.GetArray())
            {
                var span1 = new Span<long>(pointer1, length);
                var span2 = new Span<long>(pointer2, length);
                var span3 = new Span<long>(pointer3, length);

                if (Avx2.IsSupported)
                {
                    var vresult = Vector256<long>.Zero;
                    while (i < length - lastIndexBlock)
                    {
                        vresult = Avx2.Add(Avx.LoadVector256(pointer1 + i), Avx.LoadVector256(pointer2 + i));
                        Avx.Store(pointer3 + i, vresult);
                        i += 4;
                    }
                }
                else if (Sse2.IsSupported)
                {
                    var vresult = Vector128<long>.Zero;
                    while (i < length - lastIndexBlock)
                    {
                        vresult = Sse2.Add(Sse2.LoadVector128(pointer1 + i), Sse2.LoadVector128(pointer2 + i));
                        Sse2.Store(pointer3 + i, vresult);
                        i += 4;
                    }
                }

                while (i < length)
                {
                    span3[i] = span1[i] + span2[i];
                    i++;
                }
            }

            return matrix;
        }

        public static unsafe Matrix<double> Add(Matrix<double> matrixA, Matrix<double> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");

            var m = matrixA.Rows;
            var n = matrixA.Columns;
            var length = matrixA.Length;

            var i = 0;

            var matrix = new Matrix<double>(m, n);
            var lastIndexBlock = 8;

            fixed (double* pointer1 = matrixA.GetArray())
            fixed (double* pointer2 = matrixB.GetArray())
            fixed (double* pointer3 = matrix.GetArray())
            {
                var span1 = new Span<double>(pointer1, length);
                var span2 = new Span<double>(pointer2, length);
                var span3 = new Span<double>(pointer3, length);

                if (Avx2.IsSupported)
                {
                    var vresult = Vector256<double>.Zero;
                    while (i < length - lastIndexBlock)
                    {
                        vresult = Avx.Add(Avx.LoadVector256(pointer1 + i), Avx.LoadVector256(pointer2 + i));
                        Avx.Store(pointer3 + i, vresult);
                        i += 4;
                    }
                }
                else if (Sse2.IsSupported)
                {
                    var vresult = Vector128<double>.Zero;
                    while (i < length - lastIndexBlock)
                    {
                        vresult = Sse2.Add(Sse2.LoadVector128(pointer1 + i), Sse2.LoadVector128(pointer2 + i));
                        Sse2.Store(pointer3 + i, vresult);
                        i += 4;
                    }
                }

                while (i < length)
                {
                    span3[i] = span1[i] + span2[i];
                    i++;
                }
            }

            return matrix;
        }

        public static unsafe Matrix<float> Add(Matrix<float> matrixA, Matrix<float> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");

            var m = matrixA.Rows;
            var n = matrixA.Columns;
            var length = matrixA.Length;

            var i = 0;

            var matrix = new Matrix<float>(m, n);
            var lastIndexBlock = 8;

            fixed (float* pointer1 = matrixA.GetArray())
            fixed (float* pointer2 = matrixB.GetArray())
            fixed (float* pointer3 = matrix.GetArray())
            {
                var span1 = new Span<float>(pointer1, length);
                var span2 = new Span<float>(pointer2, length);
                var span3 = new Span<float>(pointer3, length);

                if (Avx2.IsSupported)
                {
                    var vresult = Vector256<float>.Zero;
                    while (i < length - lastIndexBlock)
                    {
                        vresult = Avx.Add(Avx.LoadVector256(pointer1 + i), Avx.LoadVector256(pointer2 + i));
                        Avx.Store(pointer3 + i, vresult);
                        i += 4;
                    }
                }
                else if (Sse2.IsSupported)
                {
                    var vresult = Vector128<float>.Zero;
                    while (i < length - lastIndexBlock)
                    {
                        vresult = Sse.Add(Sse.LoadVector128(pointer1 + i), Sse.LoadVector128(pointer2 + i));
                        Sse.Store(pointer3 + i, vresult);
                        i += 4;
                    }
                }

                while (i < length)
                {
                    span3[i] = span1[i] + span2[i];
                    i++;
                }
            }

            return matrix;
        }
    }
}
#endif