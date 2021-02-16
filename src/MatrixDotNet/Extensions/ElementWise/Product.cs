using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.ElementWise
{
    public unsafe class ElementWise
    {
        /// <summary>
        /// Gets Hadamard product(Schur product) of two matrices.
        /// </summary>
        /// <exception cref="MatrixDotNetException">
        /// Size of two matrices are not equal.
        /// </exception>
        public static Matrix<T> HadamardProduct<T>(Matrix<T> left, Matrix<T> right)
            where T : unmanaged
        {
            if (left.Rows != right.Rows || left.Columns != right.Columns)
            {
                throw new MatrixDotNetException("Size of two matrices are not equal");
            }

            var matrix = new Matrix<T>(left.Rows, right.Columns);

            fixed (T* matrixPtr = matrix._Matrix)
            fixed (T* leftPtr = left._Matrix)
            fixed (T* rightPtr = right._Matrix)
            {
                var leftSpan = new Span<T>(leftPtr, matrix.Length);
                var rightSpan = new Span<T>(rightPtr, matrix.Length);
                var matrixSpan = new Span<T>(matrixPtr, matrix.Length);

                for (int i = 0; i < matrix.Length; i++)
                {
                    matrixSpan[i] = MathUnsafe<T>.Mul(leftSpan[i], rightSpan[i]);
                }
            }

            return matrix;
        }
    }
}