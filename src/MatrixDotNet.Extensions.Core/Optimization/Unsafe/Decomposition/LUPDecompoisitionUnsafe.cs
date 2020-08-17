using System;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe.Builder;
using MatrixDotNet.Extensions.Core.Optimization.Unsafe.Conversion;

namespace MatrixDotNet.Extensions.Core.Optimization.Unsafe.Decomposition
{
    public static partial class UnsafeDecomposition
    {
        internal static int _exchanges;
            
        public static unsafe void GetLowerUpperPermutation(this Matrix<double> matrix,out Matrix<double> lower,out Matrix<double> upper,out Matrix<double> matrixP)
        {
            if (matrix is null)
                throw new NullReferenceException();

            _exchanges = 0;
            
            int n = matrix.Rows;
            int m = matrix.Columns;
            lower = matrix.CreateIdentityMatrix();
            upper = matrix.CloneObject();
            // load to P identity matrix.
            matrixP = lower.CloneObject();
            fixed(double* ptrU = upper.GetMatrix())
            fixed(double* ptrL = lower.GetMatrix())
            {
                for (int i = 0; i < n - 1; i++)
                {
                    int index = i;
                    double max = *(ptrU + index * m);
                    double* mx = ptrU + i * m;
                    double* test1 = ptrU + i * n;
                    for (int j = i + 1; j < n; j++)
                    {
                        double current = test1[j];
                        if (Math.Abs(current) > Math.Abs(max))
                        {
                            max = current;
                            index = j;
                        }
                    }
                    
                    if(Math.Abs(max) < 0.0001) continue;
                    
                    if (index != i)
                    {
                        upper.SwapRows(i, index);
                        matrixP.SwapRows(i, index);
                        _exchanges++;

                    }
                    
                    for (int j = i + 1; j < n; j++)
                    {
                        double* test2 = ptrU + j * m;
                        double div = test2[i] / mx[i];
                        *(ptrL + j * m) = div;
                        test2 = ptrU + j * n;

                        for (int k = i;  k < n; k++)
                        {
                            test2[k] = test2[k] - mx[k] * div;
                        }
                    }
                }
            }
        }
        
        public static unsafe void GetLowerUpperPermutation(this Matrix<float> matrix,out Matrix<float> lower,out Matrix<float> upper,out Matrix<float> matrixP)
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            int n = matrix.Rows;
            int m = matrix.Columns;
            lower = matrix.CreateIdentityMatrix();
            upper = matrix.CloneObject();

            _exchanges = 0;
            
            // load to P identity matrix.
            matrixP = lower.CloneObject();
            fixed(float* ptrU = upper.GetMatrix())
            fixed(float* ptrL = lower.GetMatrix())
            {
                for (int i = 0; i < n - 1; i++)
                {
                    int index = i;
                    double max = *(ptrU + index * m);
                    float* mx = ptrU + i * m;
                    float* test1 = ptrU + i * n;
                    for (int j = i + 1; j < n; j++)
                    {
                        float current = test1[j];
                        if (Math.Abs(current) > Math.Abs(max))
                        {
                            max = current;
                            index = j;
                        }
                    }
                    
                    if(Math.Abs(max) < 0.0001) continue;
                        
                    if (index != i)
                    {
                        upper.SwapRows(i, index);
                        matrixP.SwapRows(i, index);
                        _exchanges++;
                    }
                    
                    for (int j = i + 1; j < n; j++)
                    {
                        float* test2 = ptrU + j * m;
                        float div = test2[i] / mx[i];
                        *(ptrL + j * m) = div;
                        test2 = ptrU + j * n;

                        for (int k = i;  k < n; k++)
                        {
                            test2[k] = test2[k] - mx[k] * div;
                        }
                    }
                }
            }
        }
    }
}