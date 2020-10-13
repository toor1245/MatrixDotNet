using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using BenchmarkDotNet.Attributes;
using Samples.Samples;

namespace Samples.Vectorization
{
    public class VmaskmovBench
    {
        public const int N = 10001;
        public double[] A = new double[N];
        public double[] B = new double[N];
        public double[] C = new double[N];
        public double[] D = new double[N];
        public double[] E = new double[N];
        
        [GlobalSetup]
        public void Setup()
        {
            for (int i = 0; i < A.Length; i++)
            {
                if ((i & 0b1) == 0 )
                {
                    A[i] = i;
                }
                else
                {
                    A[i] = -i;
                }

                B[i] = 0;
                E[i] = 1;
                C[i] = 2;
                D[i] = 3;
                
            }
        }

        [Benchmark]
        public void WithoutVMaskMov()
        {
            Setup();
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] > 0)
                {
                    B[i] = E[i] * D[i];
                }
                else
                {
                    B[i] = E[i] * C[i];
                }
            }
        }
        
        [Benchmark]
        public unsafe void VMaskMov()
        {
            Setup();
            fixed (double* ptrA = A)
            fixed (double* ptrB = B)
            fixed (double* ptrC = C)
            fixed (double* ptrD = D)
            fixed (double* ptrE = E)
            {
                var source = new Span<double>(ptrA, N);
                int i = 0;
                var ymm8 = Vector256<double>.Zero; // 0 0 0 0
                var ymm9 = Avx.Compare(ymm8, ymm8, FloatComparisonMode.OrderedNonSignaling);

                while (i < source.Length - 4)
                {
                    var ymm1 = Avx.LoadVector256(ptrA + i);
                    var ymm2 = Avx.Compare(ymm8,ymm1, FloatComparisonMode.OrderedGreaterThanSignaling);
                    var ymm4 = Avx.MaskLoad(ptrC + i,ymm2);
                    ymm2 = Avx.Xor(ymm9,ymm2);
                    var ymm5 = Avx.MaskLoad(ptrD + i,ymm2);
                    ymm4 = Avx.Or(ymm4,ymm5);
                    ymm4 = Avx.Multiply(ymm4, Avx.LoadVector256(ptrE + i));
                    Avx.Store(ptrB + i,ymm4);
                    i += 4;
                }
                
                var spanB = new Span<double>(ptrB,B.Length);
                var spanC = new Span<double>(ptrC,C.Length);
                var spanD = new Span<double>(ptrD,D.Length);
                var spanE = new Span<double>(ptrE,E.Length);
                
                while (i < source.Length)
                {
                    if (source[i] > 0)
                    {
                        spanB[i] = spanE[i] * spanD[i];
                    }
                    else
                    {
                        spanB[i] = spanE[i] * spanC[i];
                    }
                    i++;
                }
            }
        }
    }
}