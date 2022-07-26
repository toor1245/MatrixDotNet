using System.Threading.Tasks;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.Extensions.Performance
{
    public static partial class Optimization
    {
        public static async ValueTask<Matrix<T>> MultiplyStrassenAsync<T>(Matrix<T> a, Matrix<T> b)
            where T : unmanaged
        {
            if (a.Rows > 5000)
            {
                throw new MatrixDotNetException("Matrix is much big size your CPU will be on 100%");
            }

            return await MultiplyStrassenParallel(a, b);
        }

        private static ValueTask<Matrix<T>> MultiplyStrassenParallel<T>(Matrix<T> a, Matrix<T> b)
            where T : unmanaged
        {
            if (a.Rows <= 64)
            {
                return new ValueTask<Matrix<T>>(a * b);
            }

            a.SplitMatrix(out var a11, out var a12, out var a21, out var a22);
            b.SplitMatrix(out var b11, out var b12, out var b21, out var b22);

            var p1 = Task.Run(() => MultiplyStrassen(a11 + a22, b11 + b22));
            var p2 = Task.Run(() => MultiplyStrassen(a21 + a22, b11));
            var p3 = Task.Run(() => MultiplyStrassen(a11, b12 - b22));
            var p4 = Task.Run(() => MultiplyStrassen(a22, b21 - b11));
            var p5 = Task.Run(() => MultiplyStrassen(a11 + a12, b22));
            var p6 = Task.Run(() => MultiplyStrassen(a21 - a11, b11 + b12));
            var p7 = MultiplyStrassen(a12 - a22, b21 + b22);

            Task.WhenAll(p1, p2, p3, p4, p5, p6);

            var c11 = p1.Result + p4.Result - p5.Result + p7;
            var c12 = p3.Result + p5.Result;
            var c21 = p2.Result + p4.Result;
            var c22 = p1.Result + p3.Result - p2.Result + p6.Result;

            return new ValueTask<Matrix<T>>(MatrixConverter.CollectMatrix(c11, c12, c21, c22));
        }

        public static Matrix<T> MultiplyStrassen<T>(Matrix<T> a, Matrix<T> b)
            where T : unmanaged
        {
            if (a.Rows <= 64)
            {
                return a * b;
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
