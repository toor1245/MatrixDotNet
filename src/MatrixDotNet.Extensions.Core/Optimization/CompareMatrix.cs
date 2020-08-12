using System;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Optimization
{
    public static partial class Optimization
    {
        public static unsafe bool EqualsAvx(Matrix<int> a, Matrix<int> b)
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
                    Vector256<int> vector1 = Avx.LoadVector256(pointer1 + i);
                    Vector256<int> vector2 = Avx.LoadVector256(pointer2 + i);

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
    }
}