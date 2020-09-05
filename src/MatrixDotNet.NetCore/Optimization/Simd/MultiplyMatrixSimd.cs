using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace MatrixDotNet.Extensions.Core.Optimization.Simd
{
    public static partial class Simd
    {
        private static unsafe Matrix<int> Multiply(Matrix<int> matrixA,Matrix<int> matrixB)
        {
            Matrix<int> matrix = new Matrix<int>(matrixA.Rows,matrixB.Columns);
            int m = matrixA.Rows;
            int n = matrixB.Columns;
            int K = matrixA.Columns;
            

            fixed(int* pointer1 = matrixA.GetMatrix())
            fixed(int* pointer2 = matrixB.GetMatrix())
            fixed(int* pointer3 = matrix.GetMatrix())
            {
                for (int i = 0; i < m; i += 4)
                {
                    for (int j = 0; j < n; j += 8)
                    {
                        Micro16X6(K,pointer1 + i * K,K,1,pointer2 + j,n,pointer3 + i * n + j,n);
                    }
                }

            }

            return matrix;
        }

        private static unsafe void Micro16X6(int k,int* matrixA,int lda,int step,int* matrixB,int ldb,int* matrixC,int ldc)
        {
            Vector256<long> c00 = Vector256<long>.Zero;
            Vector256<long> c10 = Vector256<long>.Zero;
            Vector256<long> c20 = Vector256<long>.Zero;
            Vector256<long> c30 = Vector256<long>.Zero;
            Vector256<long> c01 = Vector256<long>.Zero;
            Vector256<long> c11 = Vector256<long>.Zero;
            Vector256<long> c21 = Vector256<long>.Zero;
            Vector256<long> c31 = Vector256<long>.Zero;

            int offset0 = 0;
            int offset1 = lda * 1;
            int offset2 = lda * 2;
            int offset3 = lda * 3;
            int test = matrixA[offset0];
            Console.WriteLine(test);


            for (int i = 0; i < k; i++)
            {
                var b0 = Avx.LoadVector256(matrixB + 0);
                var b1 = Avx.LoadVector256(matrixB + 8);
                var a0 = Avx.LoadVector256((int*) matrixA + 0);
                var a1 = Avx.LoadVector256((int*) matrixA + 8);
                
                c00 = Avx2.Add(Avx2.Multiply(a0, b0),c00 );
                c01 = Avx2.Add(Avx2.Multiply(a0, b1),c01 );
                c10 = Avx2.Add(Avx2.Multiply(a1, b0),c10 );
                c11 = Avx2.Add(Avx2.Multiply(a1, b1),c11 );
                
                a0 = Avx.LoadVector256(matrixA + offset2);
                a1 = Avx.LoadVector256(matrixA + offset3);
                
                c20 = Avx2.Add(Avx2.Multiply(a0, b0),c20 );
                c21 = Avx2.Add(Avx2.Multiply(a0, b1),c21 );
                c30 = Avx2.Add(Avx2.Multiply(a1, b0),c30 );
                c31 = Avx2.Add(Avx2.Multiply(a1, b1),c31 );
                

                matrixB += ldb;
                matrixA += step;
            }
            
            Avx.Store(matrixC + 0,Avx2.Add(c00.AsInt32(),Avx.LoadVector256(matrixC + 0)));
            Avx.Store(matrixC + 8,Avx2.Add(c01.AsInt32(),Avx.LoadVector256(matrixC + 8)));
            matrixC += ldc;
            
            Avx.Store(matrixC + 0,Avx2.Add(c20.AsInt32(),Avx.LoadVector256(matrixC + 0)));
            Avx.Store(matrixC + 8,Avx2.Add(c21.AsInt32(),Avx.LoadVector256(matrixC + 8)));
            matrixC += ldc;
            
            Avx.Store(matrixC + 0,Avx2.Add(c30.AsInt32(),Avx.LoadVector256(matrixC + 0)));
            Avx.Store(matrixC + 8,Avx2.Add(c31.AsInt32(),Avx.LoadVector256(matrixC + 8)));
        }
    }
}