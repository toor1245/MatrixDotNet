using System;

namespace MatrixDotNet.Extensions.SolveExtension
{
    public static partial class Solve 
    {
        /// <summary>
        /// Gets determinant matrix by Gauss`s algorithm.
        /// </summary>
        /// <param name="A1">the matrix A.</param>
        /// <param name="b1">the matrix B.</param>
        /// <returns></returns>
        public static double[] Gauss(this Matrix<double> A1, double[] b1) {

            // Input data
            Matrix<double> A = A1.Clone() as Matrix<double>;
            double[] b = new double[b1.Length];
            
            Array.Copy(b1, 0, b, 0, b.Length);


            // method of Gauss 
            int N  = A.Rows;
            for (int p = 0; p < N; p++) {

                int max = p;
                for (int i = p + 1; i < N; i++) {
                    if (Math.Abs(A[i,p]) > Math.Abs(A[max,p])) {
                        max = i;
                    }
                }
                double[] temp = A[p]; A[p] = A[max]; A[max] = temp;
                double   t    = b[p]; b[p] = b[max]; b[max] = t;

                if (Math.Abs(A[p][p]) <= 1e-10) {
                    return null;
                }

                for (int i = p + 1; i < N; i++) {
                    double alpha = A[i,p] / A[p,p];
                    b[i] -= alpha * b[p];
                    for (int j = p; j < N; j++) {
                        A[i,j] -= alpha * A[p,j];
                    }
                }
            }
            

            // Result
            double[] x = new double[N];
            for (int i = N - 1;i >= 0 ; i--) {
                double sum = 0;
                for (int j = i + 1; j < N; j++) {
                    sum += A[i,j] * x[j];
                }
                x[i] = (b[i] - sum) / A[i,i];
            }
            return x;
        }
    }
}