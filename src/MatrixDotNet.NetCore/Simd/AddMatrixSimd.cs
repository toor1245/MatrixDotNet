using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Simd
{
    public static partial class Simd 
    {
        public static unsafe Matrix<int> Add(Matrix<int> matrixA,Matrix<int> matrixB)
        {
            if(matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");
            
            int m = matrixA.Rows;
            int n = matrixA.Columns;
            int length = matrixA.Length;

            int i = 0;
            
            Matrix<int> matrix = new Matrix<int>(m,n);
            int lastIndexBlock = 8;

            fixed(int* pointer1 = matrixA.GetArray())
            fixed(int* pointer2 = matrixB.GetArray())
            fixed(int* pointer3 = matrix.GetArray())
            {
                Span<int> span1 = new Span<int>(pointer1,length);
                Span<int> span2 = new Span<int>(pointer2,length);
                Span<int> span3 = new Span<int>(pointer3,length);

                if (Avx2.IsSupported)
                {
                    Vector256<int> vresult = Vector256<int>.Zero;
                    while (i < length - lastIndexBlock )
                    {
                        vresult = Avx2.Add(Avx.LoadVector256(pointer1 + i), Avx.LoadVector256(pointer2 + i));
                        Avx.Store(pointer3 + i,vresult);
                        i += 4;
                    }
                }
                else if (Sse2.IsSupported)
                {
                    Vector128<int> vresult = Vector128<int>.Zero;
                    while (i < length - lastIndexBlock )
                    {
                        vresult = Sse2.Add(Sse2.LoadVector128(pointer1 + i), Sse2.LoadVector128(pointer2 + i));
                        Sse2.Store(pointer3 + i,vresult);
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
        
        public static unsafe Matrix<long> Add(Matrix<long> matrixA,Matrix<long> matrixB)
        {
            if(matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");
            
            int m = matrixA.Rows;
            int n = matrixA.Columns;
            int length = matrixA.Length;

            int i = 0;
            
            Matrix<long> matrix = new Matrix<long>(m,n);
            int lastIndexBlock = 8;

            fixed(long* pointer1 = matrixA.GetArray())
            fixed(long* pointer2 = matrixB.GetArray())
            fixed(long* pointer3 = matrix.GetArray())
            {
                Span<long> span1 = new Span<long>(pointer1,length);
                Span<long> span2 = new Span<long>(pointer2,length);
                Span<long> span3 = new Span<long>(pointer3,length);

                if (Avx2.IsSupported)
                {
                    Vector256<long> vresult = Vector256<long>.Zero;
                    while (i < length - lastIndexBlock )
                    {
                        vresult = Avx2.Add(Avx.LoadVector256(pointer1 + i), Avx.LoadVector256(pointer2 + i));
                        Avx.Store(pointer3 + i,vresult);
                        i += 4;
                    }
                }
                else if (Sse2.IsSupported)
                {
                    Vector128<long> vresult = Vector128<long>.Zero;
                    while (i < length - lastIndexBlock )
                    {
                        vresult = Sse2.Add(Sse2.LoadVector128(pointer1 + i), Sse2.LoadVector128(pointer2 + i));
                        Sse2.Store(pointer3 + i,vresult);
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
        
        public static unsafe Matrix<double> Add(Matrix<double> matrixA,Matrix<double> matrixB)
        {
            if(matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");
            
            int m = matrixA.Rows;
            int n = matrixA.Columns;
            int length = matrixA.Length;

            int i = 0;
            
            Matrix<double> matrix = new Matrix<double>(m,n);
            int lastIndexBlock = 8;

            fixed(double* pointer1 = matrixA.GetArray())
            fixed(double* pointer2 = matrixB.GetArray())
            fixed(double* pointer3 = matrix.GetArray())
            {
                Span<double> span1 = new Span<double>(pointer1,length);
                Span<double> span2 = new Span<double>(pointer2,length);
                Span<double> span3 = new Span<double>(pointer3,length);

                if (Avx2.IsSupported)
                {
                    Vector256<double> vresult = Vector256<double>.Zero;
                    while (i < length - lastIndexBlock )
                    {
                        vresult = Avx2.Add(Avx.LoadVector256(pointer1 + i), Avx.LoadVector256(pointer2 + i));
                        Avx.Store(pointer3 + i,vresult);
                        i += 4;
                    }
                }
                else if (Sse2.IsSupported)
                {
                    Vector128<double> vresult = Vector128<double>.Zero;
                    while (i < length - lastIndexBlock )
                    {
                        vresult = Sse2.Add(Sse2.LoadVector128(pointer1 + i), Sse2.LoadVector128(pointer2 + i));
                        Sse2.Store(pointer3 + i,vresult);
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
        
        public static unsafe Matrix<float> Add(Matrix<float> matrixA,Matrix<float> matrixB)
        {
            if(matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");
            
            int m = matrixA.Rows;
            int n = matrixA.Columns;
            int length = matrixA.Length;

            int i = 0;
            
            Matrix<float> matrix = new Matrix<float>(m,n);
            int lastIndexBlock = 8;

            fixed(float* pointer1 = matrixA.GetArray())
            fixed(float* pointer2 = matrixB.GetArray())
            fixed(float* pointer3 = matrix.GetArray())
            {
                Span<float> span1 = new Span<float>(pointer1,length);
                Span<float> span2 = new Span<float>(pointer2,length);
                Span<float> span3 = new Span<float>(pointer3,length);

                if (Avx2.IsSupported)
                {
                    Vector256<float> vresult = Vector256<float>.Zero;
                    while (i < length - lastIndexBlock )
                    {
                        vresult = Avx2.Add(Avx2.LoadVector256(pointer1 + i), Avx2.LoadVector256(pointer2 + i));
                        Avx2.Store(pointer3 + i,vresult);
                        i += 4;
                    }
                }
                else if (Sse2.IsSupported)
                {
                    Vector128<float> vresult = Vector128<float>.Zero;
                    while (i < length - lastIndexBlock )
                    {
                        vresult = Sse2.Add(Sse2.LoadVector128(pointer1 + i), Sse2.LoadVector128(pointer2 + i));
                        Sse2.Store(pointer3 + i,vresult);
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