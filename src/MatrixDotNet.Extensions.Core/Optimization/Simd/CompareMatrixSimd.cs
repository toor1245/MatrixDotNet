using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Optimization.Simd
{
    public static partial class Simd
    {
        public static unsafe bool Equals(Matrix<long> a, Matrix<long> b)
        {
            int lengthA = a.Length;
            int lengthB = b.Length;

            if(lengthA != lengthB)
                throw new MatrixDotNetException("matrix A length not equal matrix B");
            
            int size = 8;

            fixed (long* pointer1 = a.GetMatrix())
            fixed (long* pointer2 = b.GetMatrix())
            {
                int i = 0;

                Span<long> span1 = new Span<long>(pointer1,lengthA);
                Span<long> span2 = new Span<long>(pointer2,lengthB);

                while (i < lengthA - size)
                {
                    var vector1 = Avx.LoadVector256(pointer1 + i);
                    var vector2 = Avx.LoadVector256(pointer2 + i);

                    if (vector1.ToScalar() - vector2.ToScalar() != 0)
                    {
                        return false;
                    }
                    i += 4;
                }
                

                while (i < lengthA)
                {
                    if (span1[i] != span2[i])
                    {
                        return false;
                    }
                    i++;
                }
            }

            return true;
        }

        public static unsafe bool Equals(Matrix<int> a, Matrix<int> b)
        {
            int lengthA = a.Length;
            int lengthB = b.Length;

            if(lengthA != lengthB)
                throw new MatrixDotNetException("matrix A length not equal matrix B");
            
            int size = 8;

            fixed (int* pointer1 = a.GetMatrix())
            fixed (int* pointer2 = b.GetMatrix())
            {
                int i = 0;

                Span<int> span1 = new Span<int>(pointer1,lengthA);
                Span<int> span2 = new Span<int>(pointer2,lengthB);

                while (i < lengthA - size)
                {
                    var vector1 = Avx.LoadVector256(pointer1 + i);
                    var vector2 = Avx.LoadVector256(pointer2 + i);

                    if (vector1.ToScalar() - vector2.ToScalar() != 0)
                    {
                        return false;
                    }
                    i += 4;
                }
                

                while (i < lengthA)
                {
                    if (span1[i] != span2[i])
                    {
                        return false;
                    }
                    i++;
                }
            }

            return true;
        }
        
        public static unsafe bool Equals(Matrix<double> a, Matrix<double> b)
        {
            int lengthA = a.Length;
            int lengthB = b.Length;

            if(lengthA != lengthB)
                throw new MatrixDotNetException("matrix A length not equal matrix B");
            
            int size = 8;

            fixed (double* pointer1 = a.GetMatrix())
            fixed (double* pointer2 = b.GetMatrix())
            {
                int i = 0;

                Span<double> span1 = new Span<double>(pointer1,lengthA);
                Span<double> span2 = new Span<double>(pointer2,lengthB);

                while (i < lengthA - size)
                {
                    var vector1 = Avx.LoadVector256(pointer1 + i);
                    var vector2 = Avx.LoadVector256(pointer2 + i);

                    if (Math.Abs(vector1.ToScalar() - vector2.ToScalar()) > 0.00001d)
                    {
                        return false;
                    }
                    i += 4;
                }
                

                while (i < lengthA)
                {
                    if (Math.Abs(span1[i] - span2[i]) > 0.00001d)
                    {
                        return false;
                    }
                    i++;
                }
            }

            return true;
        }
        
        public static unsafe bool Equals(Matrix<float> a, Matrix<float> b)
        {
            int lengthA = a.Length;
            int lengthB = b.Length;

            if(lengthA != lengthB)
                throw new MatrixDotNetException("matrix A length not equal matrix B");
            
            int size = 8;

            fixed (float* pointer1 = a.GetMatrix())
            fixed (float* pointer2 = b.GetMatrix())
            {
                int i = 0;

                Span<float> span1 = new Span<float>(pointer1,lengthA);
                Span<float> span2 = new Span<float>(pointer2,lengthB);

                while (i < lengthA - size)
                {
                    var vector1 = Avx.LoadVector256(pointer1 + i);
                    var vector2 = Avx.LoadVector256(pointer2 + i);

                    if (Math.Abs(vector1.ToScalar() - vector2.ToScalar()) > 0.00001d)
                    {
                        return false;
                    }
                    i += 4;
                }
                

                while (i < lengthA)
                {
                    if (Math.Abs(span1[i] - span2[i]) > 0.00001f)
                    {
                        return false;
                    }
                    i++;
                }
            }

            return true;
        }
    }
}