using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Conversion;
using System;
using System.Threading.Tasks;

namespace MatrixDotNet.Extensions.Performance.Operations
{
    public static partial class Optimization
    {
        public static unsafe Matrix<int> Multiply(Matrix<int> matrixA, Matrix<int> matrixB)
        {
            int m = matrixA.Rows;
            int n = matrixB.Columns;
            int K = matrixA.Columns;
            int len1 = matrixA.Length;

            Matrix<int> matrix = new Matrix<int>(m, n);

            fixed (int* pointer1 = matrixA.GetArray())
            fixed (int* pointer2 = matrixB.GetArray())
            fixed (int* pointer3 = matrix.GetArray())
            {
                Span<int> span1 = new Span<int>(pointer1, len1);

                for (int i = 0; i < m; i++)
                {
                    int* c = pointer3 + i * n;

                    for (int k = 0; k < K; k++)
                    {
                        int* b = pointer2 + k * n;
                        int a = span1[i * K + k];
                        for (int j = 0; j < n; j++)
                        {
                            c[j] += a * b[j];
                        }
                    }
                }
            }

            return matrix;
        }

        public static unsafe Matrix<double> Multiply(Matrix<double> matrixA, Matrix<double> matrixB)
        {
            int m = matrixA.Rows;
            int n = matrixB.Columns;
            int K = matrixA.Columns;
            int len1 = matrixA.Length;

            Matrix<double> matrix = new Matrix<double>(m, n);

            fixed (double* pointer1 = matrixA.GetArray())
            fixed (double* pointer2 = matrixB.GetArray())
            fixed (double* pointer3 = matrix.GetArray())
            {
                Span<int> span1 = new Span<int>(pointer1, len1);

                for (int i = 0; i < m; i++)
                {
                    double* c = pointer3 + i * n;

                    for (int k = 0; k < K; k++)
                    {
                        double* b = pointer2 + k * n;
                        int a = span1[i * K + k];
                        for (int j = 0; j < n; j++)
                        {
                            c[j] += a * b[j];
                        }
                    }
                }
            }

            return matrix;
        }

        public static async Task<Matrix<int>> MultiplyStrassenAsync(Matrix<int> a, Matrix<int> b)
        {
            if (a.Rows > 5000)
                throw new MatrixDotNetException("Matrix is much big size your CPU will be on 100%");

            return await MultiplyStrassen(a, b);
        }

        private static async Task<Matrix<int>> MultiplyStrassen(Matrix<int> a, Matrix<int> b)
        {
            if (a.Rows <= 1024)
            {
                return Multiply(a, b);
            }

            a.SplitMatrix(out var a11, out var a12, out var a21, out var a22);
            b.SplitMatrix(out var b11, out var b12, out var b21, out var b22);

            Task<Matrix<int>> t1 = Task.Run(() => MultiplyStrassen(Optimization.Add(a11, a22), Optimization.Add(b11, b22)));
            Task<Matrix<int>> t2 = Task.Run(() => MultiplyStrassen(Optimization.Add(a21, a22), b11));
            Task<Matrix<int>> t3 = Task.Run(() => MultiplyStrassen(a11, Optimization.Sub(b12, b22)));
            Task<Matrix<int>> t4 = Task.Run(() => MultiplyStrassen(a22, Optimization.Sub(b21, b11)));
            Task<Matrix<int>> t5 = Task.Run(() => MultiplyStrassen(Optimization.Add(a11, a12), b22));
            Task<Matrix<int>> t6 = Task.Run(() => MultiplyStrassen(Optimization.Sub(a21, a11), Optimization.Add(b11, b12)));
            Task<Matrix<int>> t7 = Task.Run(() => MultiplyStrassen(Optimization.Sub(a12, a22), Optimization.Add(b21, b22)));

            Matrix<int> p1 = await t1;
            Matrix<int> p2 = await t2;
            Matrix<int> p3 = await t3;
            Matrix<int> p4 = await t4;
            Matrix<int> p5 = await t5;
            Matrix<int> p6 = await t6;
            Matrix<int> p7 = await t7;


            var c11 = p1 + p4 - p5 + p7;
            var c12 = Optimization.Add(p3, p5);
            var c21 = Optimization.Add(p2, p4);
            var c22 = p1 + p3 - p2 + p6;

            return MatrixConverter.CollectMatrix(c11, c12, c21, c22);
        }


        public static Matrix<double> MultiplyStrassen(Matrix<double> a, Matrix<double> b)
        {
            if (a.Rows <= 1024)
            {
                return Multiply(a, b);
            }

            a.SplitMatrix(out var a11, out var a12, out var a21, out var a22);
            b.SplitMatrix(out var b11, out var b12, out var b21, out var b22);

            var p1 = MultiplyStrassen(a11 + a22, b11 + b22);
            var p2 = MultiplyStrassen(a21 + a22, b11);
            var p3 = MultiplyStrassen(a11, b12 - b22);
            var p4 = MultiplyStrassen(a22, b21 - b11);
            var p5 = MultiplyStrassen(a11 + a12, b22);
            var p6 = MultiplyStrassen(a21 - a22, b11 + b12);
            var p7 = MultiplyStrassen(a12 - a22, b21 + b22);

            var c11 = p1 + p4 - p5 + p7;
            var c12 = p3 + p5;
            var c21 = p2 + p4;
            var c22 = p1 + p3 - p2 + p6;

            return MatrixConverter.CollectMatrix(c11, c12, c21, c22);
        }
    }
}