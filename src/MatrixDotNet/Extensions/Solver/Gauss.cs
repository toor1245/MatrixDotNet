using System;

namespace MatrixDotNet.Extensions.Solver
{
    public static partial class Solve
    {
        /// <summary>
        ///     Gets determinant matrix by Gauss`s algorithm.
        /// </summary>
        /// <param name="A1">the matrix A.</param>
        /// <param name="b1">the matrix B.</param>
        /// <returns></returns>
        public static double[] GaussSolve(this Matrix<double> A1, double[] b1)
        {
            // Input data
            var A = A1.Clone() as Matrix<double>;
            var b = new double[b1.Length];

            Array.Copy(b1, 0, b, 0, b.Length);


            // method of Gauss 
            var N = A.Rows;
            for (var p = 0; p < N; p++)
            {
                var max = p;
                for (var i = p + 1; i < N; i++)
                    if (System.Math.Abs(A[i, p]) > System.Math.Abs(A[max, p]))
                        max = i;
                var temp = A[p];
                A[p] = A[max];
                A[max] = temp;
                var t = b[p];
                b[p] = b[max];
                b[max] = t;

                if (System.Math.Abs(A[p][p]) <= 1e-10) return null;

                for (var i = p + 1; i < N; i++)
                {
                    var alpha = A[i, p] / A[p, p];
                    b[i] -= alpha * b[p];
                    for (var j = p; j < N; j++) A[i, j] -= alpha * A[p, j];
                }
            }


            // Result
            var x = new double[N];
            for (var i = N - 1; i >= 0; i--)
            {
                double sum = 0;
                for (var j = i + 1; j < N; j++) sum += A[i, j] * x[j];
                x[i] = (b[i] - sum) / A[i, i];
            }

            return x;
        }
    }
}