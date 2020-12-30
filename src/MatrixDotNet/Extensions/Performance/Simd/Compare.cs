#if NET5_0 || NETCOREAPP3_1
using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Performance.Simd
{
    public static partial class Simd
    {
        public static unsafe bool Equals(Matrix<long> a, Matrix<long> b)
        {
            var lengthA = a.Length;
            var lengthB = b.Length;

            if (lengthA != lengthB)
                throw new MatrixDotNetException("matrix A length not equal matrix B");

            var size = 8;

            fixed (long* pointer1 = a.GetArray())
            fixed (long* pointer2 = b.GetArray())
            {
                var i = 0;

                var span1 = new Span<long>(pointer1, lengthA);
                var span2 = new Span<long>(pointer2, lengthB);

                while (i < lengthA - size)
                {
                    var vector1 = Avx.LoadVector256(pointer1 + i);
                    var vector2 = Avx.LoadVector256(pointer2 + i);

                    if (vector1.ToScalar() - vector2.ToScalar() != 0) return false;
                    i += 4;
                }


                while (i < lengthA)
                {
                    if (span1[i] != span2[i]) return false;
                    i++;
                }
            }

            return true;
        }

        public static unsafe bool Equals(Matrix<int> a, Matrix<int> b)
        {
            var lengthA = a.Length;
            var lengthB = b.Length;

            if (lengthA != lengthB)
                throw new MatrixDotNetException("matrix A length not equal matrix B");

            var size = 8;

            fixed (int* pointer1 = a.GetArray())
            fixed (int* pointer2 = b.GetArray())
            {
                var i = 0;

                var span1 = new Span<int>(pointer1, lengthA);
                var span2 = new Span<int>(pointer2, lengthB);

                while (i < lengthA - size)
                {
                    var vector1 = Avx.LoadVector256(pointer1 + i);
                    var vector2 = Avx.LoadVector256(pointer2 + i);

                    if (vector1.ToScalar() - vector2.ToScalar() != 0) return false;
                    i += 8;
                }


                while (i < lengthA)
                {
                    if (span1[i] != span2[i]) return false;
                    i++;
                }
            }

            return true;
        }

        public static unsafe bool Equals(Matrix<double> a, Matrix<double> b)
        {
            var lengthA = a.Length;
            var lengthB = b.Length;

            if (lengthA != lengthB)
                throw new MatrixDotNetException("matrix A length not equal matrix B");

            var size = 4;

            fixed (double* pointer1 = a.GetArray())
            fixed (double* pointer2 = b.GetArray())
            {
                var i = 0;

                var span1 = new Span<double>(pointer1, lengthA);
                var span2 = new Span<double>(pointer2, lengthB);

                while (i < lengthA - size)
                {
                    var vector1 = Avx.LoadVector256(pointer1 + i);
                    var vector2 = Avx.LoadVector256(pointer2 + i);

                    if (System.Math.Abs(vector1.ToScalar() - vector2.ToScalar()) > 0.00001d) return false;
                    i += 4;
                }


                while (i < lengthA)
                {
                    if (System.Math.Abs(span1[i] - span2[i]) > 0.00001d) return false;
                    i++;
                }
            }

            return true;
        }

        public static unsafe bool Equals(Matrix<float> a, Matrix<float> b)
        {
            var lengthA = a.Length;
            var lengthB = b.Length;

            if (lengthA != lengthB)
                throw new MatrixDotNetException("matrix A length not equal matrix B");

            var size = 8;

            fixed (float* pointer1 = a.GetArray())
            fixed (float* pointer2 = b.GetArray())
            {
                var i = 0;

                var span1 = new Span<float>(pointer1, lengthA);
                var span2 = new Span<float>(pointer2, lengthB);

                while (i < lengthA - size)
                {
                    var vector1 = Avx.LoadVector256(pointer1 + i);
                    var vector2 = Avx.LoadVector256(pointer2 + i);

                    if (System.Math.Abs(vector1.ToScalar() - vector2.ToScalar()) > 0.00001d) return false;
                    i += 8;
                }


                while (i < lengthA)
                {
                    if (System.Math.Abs(span1[i] - span2[i]) > 0.00001f) return false;
                    i++;
                }
            }

            return true;
        }
    }
}
#endif