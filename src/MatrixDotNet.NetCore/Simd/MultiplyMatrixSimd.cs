using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MatrixDotNet.Extensions.Core.Simd
{
    public static partial class Simd
    {
        public static unsafe Matrix<int> Multiply(Matrix<int> matrixA,Matrix<int> matrixB)
        {
            Matrix<int> matrix = new Matrix<int>(matrixA.Rows,matrixB.Columns);
            int m = matrixA.Rows;
            int n = matrixB.Columns;
            int K = matrixA.Columns;


            fixed(int* ptr1 = matrixA.GetArray())
            fixed(int* ptr2 = matrixB.GetArray())
            fixed(int* ptr3 = matrix.GetArray())
            {
                
                int size = Vector256<int>.Count;
                for (int i = 0; i < m; i++)
                {
                    int* ptrC = ptr3 + i * n;

                    for (int k = 0; k < K; k++)
                    {
                        int* ptrB = ptr2 + k * n;
                        int* stack = stackalloc int[8]{1,1,1,1,1,1,1,1};
                        Vector256<int> a = Avx.LoadVector256(stack); 
                        for (int j = 0; j < n - size; j += 16)
                        {
                            var c = Avx.LoadVector256(ptrC + j);
                            var b = Avx.LoadVector256(ptrB + j);
                            c = Avx2.Add(c, Avx2.Multiply(a, b).AsInt32());
                            var c2 = Avx.LoadVector256(ptrC + j + 8);
                            var b2 = Avx.LoadVector256(ptrB + j + 8);
                            c2 = Avx2.Add(c2, Avx2.Multiply(a, b2).AsInt32());
                            Avx.Store(ptrC + j,c);
                            Avx.Store(ptrC + j + 8,c2);
                        }
                    }
                }
                

                
                fixed(int* pointer1 = matrixA.GetArray())
                fixed(int* pointer2 = matrixB.GetArray())
                fixed(int* pointer3 = matrix.GetArray())
                {
                    
                    return matrix;
                }
            }
        }
        
    }
}