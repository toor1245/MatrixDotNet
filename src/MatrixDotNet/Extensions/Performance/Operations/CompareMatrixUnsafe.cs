using MatrixDotNet.Exceptions;
using System;

namespace MatrixDotNet.Extensions.Performance.Operations
{
    public static partial class Optimization
    {
        public static unsafe bool Equals(Matrix<int> matrixA, Matrix<int> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");

            int length = matrixA.Length;
            fixed (int* pointer1 = matrixA.GetArray())
            fixed (int* pointer2 = matrixB.GetArray())
            {
                Span<int> span1 = new Span<int>(pointer1, length);
                Span<int> span2 = new Span<int>(pointer2, length);
                for (int i = 0; i < length; i++)
                {
                    if (span1[i] != span2[i]) return false;
                }
            }

            return true;
        }

        public static unsafe bool Equals(Matrix<float> matrixA, Matrix<float> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");

            int length = matrixA.Length;
            fixed (float* pointer1 = matrixA.GetArray())
            fixed (float* pointer2 = matrixB.GetArray())
            {
                var span1 = new Span<float>(pointer1, length);
                var span2 = new Span<float>(pointer2, length);
                for (int i = 0; i < length; i++)
                {
                    if (System.Math.Abs(span1[i] - span2[i]) > 0.0001) return false;
                }
            }

            return true;
        }

        public static unsafe bool Equals(Matrix<long> matrixA, Matrix<long> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");

            int length = matrixA.Length;
            fixed (long* pointer1 = matrixA.GetArray())
            fixed (long* pointer2 = matrixB.GetArray())
            {
                var span1 = new Span<long>(pointer1, length);
                var span2 = new Span<long>(pointer2, length);
                for (int i = 0; i < length; i++)
                {
                    if (span1[i] != span2[i]) return false;
                }
            }

            return true;
        }

        public static unsafe bool Equals(Matrix<double> matrixA, Matrix<double> matrixB)
        {
            if (matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");

            int length = matrixA.Length;
            fixed (double* pointer1 = matrixA.GetArray())
            fixed (double* pointer2 = matrixB.GetArray())
            {
                var span1 = new Span<double>(pointer1, length);
                var span2 = new Span<double>(pointer2, length);
                for (int i = 0; i < length; i++)
                {
                    if (System.Math.Abs(span1[i] - span2[i]) > 0.0001) return false;
                }
            }

            return true;
        }


    }
}