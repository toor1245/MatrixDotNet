﻿using System;
using System.Runtime.CompilerServices;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Math;
#if NET5_0 || NETCOREAPP3_1
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Extensions.Performance.Simd.Handler;
#endif


namespace MatrixDotNet.Extensions.Conversion
{
    /// <summary>
    /// Represents converter which can change matrix.
    /// </summary>
    public static unsafe partial class MatrixConverter
    {
        /// <summary>
        /// Joins two matrix, matrix A rows must be equals matrix B rows.
        /// </summary>
        /// <param name="matrix1">The matrix A.</param>
        /// <param name="matrix2">The matrix B.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Joins two matrix</returns>
        /// <exception cref="MatrixDotNetException">
        /// Throws if matrix1.Rows != matrix2.Rows.
        /// </exception>
        public static Matrix<T> Concat<T>(this Matrix<T> matrix1, Matrix<T> matrix2)
            where T : unmanaged
        {
            int m = matrix1.Rows;
            int n = matrix1.Columns;
            int lenColumns = n + matrix2.Columns;

            if (m != matrix2.Rows)
            {
                throw new MatrixDotNetException("Rows must be equals");
            }

            var res = new Matrix<T>(m, lenColumns);

            for (var i = 0; i < m; i++)
            {
                for (int j = 0, k = 0; j < lenColumns; j++)
                {
                    if (j < n)
                    {
                        res[i, j] = matrix1[i, j];
                    }
                    else
                    {
                        res[i, k + n] = matrix2[i, k];
                        k++;
                    }
                }
            }

            return res;
        }

        /// <summary>
        /// Reduces column of matrix by index.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="column">The index of matrix which reduce column.</param>
        /// <typeparam name="T">Unmanaged type.</typeparam>
        /// <returns>A new matrix without the chosen column.</returns>
        public static unsafe Matrix<T> ReduceColumn<T>(this Matrix<T> matrix, uint column)
            where T : unmanaged
        {
            if (column >= matrix.Columns)
            {
                throw new IndexOutOfRangeException();
            }

            var newColumns = matrix.Columns - 1;
            var temp = new Matrix<T>(matrix.Rows, newColumns);

            fixed (T* ptr2 = temp.GetArray())
            fixed (T* ptr3 = matrix.GetArray())
            {
                int m = temp.Columns;
                uint len = (uint) m - column;
                for (int i = 0; i < temp.Rows; i++)
                {
                    T* src = ptr3 + i * matrix.Columns;
                    Unsafe.CopyBlock(ptr2 + i * m, src, (uint) (sizeof(T) * column));
                    Unsafe.CopyBlock(ptr2 + i * m + column, src + column + 1, (uint) (sizeof(T) * len));
                }
            }

            return temp;
        }

        /// <summary>
        /// Reduces row of matrix by index.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="row">index of matrix which reduce column.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>A new matrix without the chosen row.</returns>
        /// <exception cref="NullReferenceException">.</exception>
        public static unsafe Matrix<T> ReduceRow<T>(this Matrix<T> matrix, uint row)
            where T : unmanaged
        {
            if (row >= matrix.Rows)
                throw new IndexOutOfRangeException();

            var newRows = matrix.Rows - 1;
            var temp = new Matrix<T>(newRows, matrix.Columns);
            fixed (T* ptr2 = temp.GetArray())
            fixed (T* ptr3 = matrix.GetArray())
            {
                int m = temp.Columns;
                Array.Copy(matrix._Matrix, temp._Matrix, row * m);
                // finds difference len between whole matrix and length to index row.
                uint diff = (uint) (sizeof(T) * temp.Length - (sizeof(T) * row * m));
                Unsafe.CopyBlock(ptr2 + row * m, ptr3 + (row + 1) * m, diff);
            }

            return temp;
        }


        /// <summary>
        /// Add column of matrix by index.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="arr">the array.</param>
        /// <param name="column">column index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>A new matrix with new column.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static Matrix<T> AddColumn<T>(this Matrix<T> matrix, T[] arr, int column) where T : unmanaged
        {
            if (matrix.Rows != arr.Length)
            {
                string message = $"length {nameof(arr)}:{arr.Length} != {nameof(matrix.Rows)} of matrix:{matrix.Rows}";
                throw new MatrixDotNetException(message);
            }

            var m = matrix.Rows;
            var result = new Matrix<T>(m, matrix.Columns + 1);

            for (int i = 0; i < column; i++)
            {
                result[i, State.Column] = matrix[i, State.Column];
            }

            result[column, State.Column] = arr;

            for (int i = column + 1; i < result.Columns; i++)
            {
                result[i, State.Column] = matrix[i - 1, State.Column];
            }

            return result;
        }


        /// <summary>
        /// Returns new matrix with added row.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="array">the row for new matrix</param>
        /// <param name="row">index of row</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe Matrix<T> AddRow<T>(this Matrix<T> matrix, T[] array, int row)
            where T : unmanaged
        {
            if (matrix.Columns != array.Length)
            {
                var message =
                    $"length {nameof(array)}:{array.Length} != {nameof(matrix.Columns)} of matrix:{matrix.Columns}";
                throw new MatrixDotNetException(message);
            }

            var newRows = matrix.Rows + 1;
            var temp = new Matrix<T>(newRows, matrix.Columns);
            fixed (T* ptr1 = array)
            fixed (T* ptr2 = temp.GetArray())
            fixed (T* ptr3 = matrix.GetArray())
            {
                int m = temp.Columns;
                int aLength = array.Length;
                Array.Copy(matrix._Matrix, temp._Matrix, row * m);
                Unsafe.CopyBlock(ptr2 + row * m, ptr1, (uint) (sizeof(T) * aLength));
                // finds difference len between whole matrix and length to index row.
                int diff = sizeof(T) * temp.Length - (sizeof(T) * row * m + sizeof(T) * aLength);
                Unsafe.CopyBlock(ptr2 + (row + 1) * m, ptr3 + row * m, (uint) diff);
            }

            return temp;
        }

        /// <summary>
        /// Changes this matrix to identity matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Identity matrix.</returns>
        /// <exception cref="MatrixDotNetException">throws exception if matrix is not square</exception>
        public static void ToIdentityMatrix<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();

            if (!matrix.IsSquare)
                throw new MatrixDotNetException(
                    $"matrix is not square!!!\nRows: {matrix.Rows}\nColumns: {matrix.Columns}");

            for (var i = 0; i < matrix.Rows; i++)
            {
                for (var j = 0; j < matrix.Columns; j++)
                {
                    if (i == j)
                    {
                        matrix[i, j] = MathGeneric<T>.Increment(default);
                    }
                    else
                    {
                        matrix[i, j] = default;
                    }
                }
            }
        }

        /// <summary>
        /// Swap rows of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension1">the dimension 1</param>
        /// <param name="dimension2">the dimension 2</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <exception cref="MatrixDotNetException">throws exception if indexDimension1 equals indexDimension2 or matrix is null</exception>
        public static unsafe void SwapRows<T>(this Matrix<T> matrix, int dimension1, int dimension2) where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();

            int m = matrix.Rows;
            int n = matrix.Columns;

            int length = matrix.Length;

            fixed (T* ptr1 = matrix.GetArray())
            {
                Span<T> span = new Span<T>(ptr1, length);

                int index = dimension1 * n + m;
                int i = dimension1 * n;
                int j = dimension2 * n;

                while (i < index)
                {
                    var tmp = span[i];
                    span[i] = span[j];
                    span[j] = tmp;

                    i++;
                    j++;
                }
            }
        }

        /// <summary>
        /// Swap rows of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="indexDimension1">the dimension 1</param>
        /// <param name="indexDimension2">the dimension 2</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <exception cref="MatrixDotNetException">throws exception if indexDimension1 equals indexDimension2 or matrix is null</exception>
        public static void SwapColumns<T>(this Matrix<T> matrix, int indexDimension1, int indexDimension2)
            where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();

            var temp = matrix[indexDimension1, State.Column];
            matrix[indexDimension1, State.Column] = matrix[indexDimension2, State.Column];
            matrix[indexDimension2, State.Column] = temp;
        }


        /// <summary>
        /// Gets transport matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Transpose matrix</returns>
        public static Matrix<T> Transpose<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            var transport = new Matrix<T>(matrix.Columns, matrix.Rows);
            for (var i = 0; i < transport.Rows; i++)
            {
                for (var j = 0; j < transport.Columns; j++)
                {
                    transport[i, j] = matrix[j, i];
                }
            }

            return transport;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Matrix<T> Transpose8X8Avx2<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            var transpose = new Matrix<T>(matrix.Columns, matrix.Rows);

#if NETCOREAPP3_1 || NET5_0
            fixed (T* ptrM = matrix.GetArray())
            fixed (T* ptrT = transpose.GetArray())
            {
                if (Avx2.IsSupported)
                {
                    var ymm0 = IntrinsicsHandler<T>.Insert128Vector256(IntrinsicsHandler<T>.LoadVector256(ptrM + 0 * 8 + 0), IntrinsicsHandler<T>.LoadVector128(ptrM + 4 * 8 + 0), 0x1);
                    var ymm1 = IntrinsicsHandler<T>.Insert128Vector256(IntrinsicsHandler<T>.LoadVector256(ptrM + 1 * 8 + 0), IntrinsicsHandler<T>.LoadVector128(ptrM + 5 * 8 + 0), 0x1);
                    var ymm2 = IntrinsicsHandler<T>.Insert128Vector256(IntrinsicsHandler<T>.LoadVector256(ptrM + 2 * 8 + 0), IntrinsicsHandler<T>.LoadVector128(ptrM + 6 * 8 + 0), 0x1);
                    var ymm3 = IntrinsicsHandler<T>.Insert128Vector256(IntrinsicsHandler<T>.LoadVector256(ptrM + 3 * 8 + 0), IntrinsicsHandler<T>.LoadVector128(ptrM + 7 * 8 + 0), 0x1);
                    
                    var ymm4 = IntrinsicsHandler<T>.Insert128Vector256(IntrinsicsHandler<T>.LoadVector256(ptrM + 0 * 8 + 4), IntrinsicsHandler<T>.LoadVector128(ptrM + 4 * 8 + 4), 0x1);
                    var ymm5 = IntrinsicsHandler<T>.Insert128Vector256(IntrinsicsHandler<T>.LoadVector256(ptrM + 1 * 8 + 4), IntrinsicsHandler<T>.LoadVector128(ptrM + 5 * 8 + 4), 0x1);
                    var ymm6 = IntrinsicsHandler<T>.Insert128Vector256(IntrinsicsHandler<T>.LoadVector256(ptrM + 2 * 8 + 4), IntrinsicsHandler<T>.LoadVector128(ptrM + 6 * 8 + 4), 0x1);
                    var ymm7 = IntrinsicsHandler<T>.Insert128Vector256(IntrinsicsHandler<T>.LoadVector256(ptrM + 3 * 8 + 4), IntrinsicsHandler<T>.LoadVector128(ptrM + 7 * 8 + 4), 0x1);

                    var ymm8 = IntrinsicsHandler<T>.UnpackLowVector256(ymm0, ymm1);
                    var ymm9 = IntrinsicsHandler<T>.UnpackHighVector256(ymm0, ymm1);
                    var ymm10 = IntrinsicsHandler<T>.UnpackLowVector256(ymm2, ymm3);
                    var ymm11 = IntrinsicsHandler<T>.UnpackHighVector256(ymm2, ymm3);
                    
                    var ymm12 = IntrinsicsHandler<T>.UnpackLowVector256(ymm4, ymm5);
                    var ymm13 = IntrinsicsHandler<T>.UnpackHighVector256(ymm4, ymm5);
                    var ymm14 = IntrinsicsHandler<T>.UnpackLowVector256(ymm6, ymm7);
                    var ymm15 = IntrinsicsHandler<T>.UnpackHighVector256(ymm6, ymm7);

                    ymm0 = IntrinsicsHandler<T>.ShuffleVector256(ymm8, ymm10, 0x44);
                    ymm1 = IntrinsicsHandler<T>.ShuffleVector256(ymm8, ymm10, 0xEE);
                    ymm2 = IntrinsicsHandler<T>.ShuffleVector256(ymm9, ymm11, 0x44);
                    ymm3 = IntrinsicsHandler<T>.ShuffleVector256(ymm9, ymm11, 0xEE);
                    
                    ymm4 = IntrinsicsHandler<T>.ShuffleVector256(ymm12, ymm14, 0x44);
                    ymm5 = IntrinsicsHandler<T>.ShuffleVector256(ymm12, ymm14, 0xEE);
                    ymm6 = IntrinsicsHandler<T>.ShuffleVector256(ymm13, ymm15, 0x44);
                    ymm7 = IntrinsicsHandler<T>.ShuffleVector256(ymm13, ymm15, 0xEE);

                    IntrinsicsHandler<T>.StoreVector256(ptrT + 0 * 8, ymm0);
                    IntrinsicsHandler<T>.StoreVector256(ptrT + 1 * 8, ymm1);
                    IntrinsicsHandler<T>.StoreVector256(ptrT + 2 * 8, ymm2);
                    IntrinsicsHandler<T>.StoreVector256(ptrT + 3 * 8, ymm3);
                    
                    IntrinsicsHandler<T>.StoreVector256(ptrT + 4 * 8, ymm4);
                    IntrinsicsHandler<T>.StoreVector256(ptrT + 5 * 8, ymm5);
                    IntrinsicsHandler<T>.StoreVector256(ptrT + 6 * 8, ymm6);
                    IntrinsicsHandler<T>.StoreVector256(ptrT + 7 * 8, ymm7);
                }
            }
#endif
            
            return transpose;
        }

        /// <summary>
        /// Splits matrix by 4 parts.
        /// </summary>
        /// <param name="a">the matrix which want splits.</param>
        /// <param name="a11">the matrix a11.</param>
        /// <param name="a12">the matrix a12.</param>
        /// <param name="a21">the matrix a21.</param>
        /// <param name="a22">the matrix a22.</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <exception cref="MatrixDotNetException">
        ///  The matrix is not square.
        /// </exception>
        public static void SplitMatrix<T>(this Matrix<T> a, out Matrix<T> a11, out Matrix<T> a12, out Matrix<T> a21,
            out Matrix<T> a22)
            where T : unmanaged
        {
            if (!a.IsSquare)
                throw new MatrixDotNetException("Matrix is not square");

            var n = a.Rows >> 1;

            a11 = new Matrix<T>(n, n);
            a12 = new Matrix<T>(n, n);
            a21 = new Matrix<T>(n, n);
            a22 = new Matrix<T>(n, n);

            for (var i = 0; i < n; i++)
            {
                CopyTo(a, i, 0, a11, i, 0, n);
                CopyTo(a, i, n, a12, i, 0, n);
                CopyTo(a, i + n, 0, a21, i, 0, n);
                CopyTo(a, i + n, n, a22, i, 0, n);
            }
        }

        /// <summary>
        /// Collects square matrix.
        /// </summary>
        /// <param name="a11">the matrix a11</param>
        /// <param name="a12">the matrix a12</param>
        /// <param name="a21">the matrix a21</param>
        /// <param name="a22">the matrix a22</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>Collect matrix.</returns>
        public static Matrix<T> CollectMatrix<T>(Matrix<T> a11, Matrix<T> a12, Matrix<T> a21, Matrix<T> a22)
            where T : unmanaged
        {
            int n = a11.Rows;
            int sl = n << 1;
            Matrix<T> a = new Matrix<T>(sl, sl);
            for (int i = 0; i < n; i++)
            {
                CopyTo(a11, i, 0, a, i, 0, n);
                CopyTo(a12, i, 0, a, i, n, sl);
                CopyTo(a21, i, 0, a, i + n, 0, n);
                CopyTo(a22, i, 0, a, i + n, n, sl);
            }

            return a;
        }

        /// <summary>
        /// Reverse array
        /// </summary>
        /// <param name="array"></param>
        public static void Reverse(uint[] array)
        {
            int len = array.Length;
            if (len < 2)
            {
                return;
            }
#if NET5_0 || NETCOREAPP3_1
            int i = 0;
            if (Avx2.IsSupported)
            {
                int size = Vector256<uint>.Count;
                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }

                int lastIndexBlock = len - len % size;

                fixed (uint* ptr = &array[0])
                {
                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;
                        var vl = Avx.LoadVector256(leftPtr);
                        var vr = Avx.LoadVector256(rightPtr);
                        var va = Avx2.Shuffle(vr, 0x1b);
                        var vb = Avx2.Shuffle(vl, 0x1b);
                        Avx.Store(leftPtr, Avx2.Permute2x128(va, va, 0x67));
                        Avx.Store(rightPtr, Avx2.Permute2x128(vb, vb, 0x67));
                    }

                    if (i << 1 < len)
                    {
                        Array.Reverse(array, i, len - lastIndexBlock);
                    }
                }
            }
            else if (Sse2.IsSupported)
            {
                int size = Vector128<uint>.Count;

                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }

                int lastIndexBlock = len - len % size;

                fixed (uint* ptr = array)
                {
                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;

                        var vl = Sse2.LoadVector128(leftPtr);
                        var vr = Sse2.LoadVector128(rightPtr);

                        Sse2.Store(rightPtr, Sse2.Shuffle(vl, 0x1b));
                        Sse2.Store(leftPtr, Sse2.Shuffle(vr, 0x1b));
                    }
                }
                if (i << 1 < len)
                {
                    Array.Reverse(array, i, len - lastIndexBlock);
                }
            }
            else
#endif
            {
                Array.Reverse(array);
            }
        }

        /// <summary>
        /// Reverse array
        /// </summary>
        /// <param name="array"></param>
        public static void Reverse(int[] array)
        {
            int len = array.Length;
            if (len < 2)
            {
                return;
            }
#if NET5_0 || NETCOREAPP3_1
            int i = 0;
            if (Avx2.IsSupported)
            {
                int size = Vector256<int>.Count;
                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }

                int lastIndexBlock = len - len % size;

                fixed (int* ptr = &array[0])
                {
                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;
                        var vl = Avx.LoadVector256(leftPtr);
                        var vr = Avx.LoadVector256(rightPtr);
                        var va = Avx2.Shuffle(vr, 0x1b);
                        var vb = Avx2.Shuffle(vl, 0x1b);
                        Avx.Store(leftPtr, Avx2.Permute2x128(va, va, 0x67));
                        Avx.Store(rightPtr, Avx2.Permute2x128(vb, vb, 0x67));
                    }

                    if (i << 1 < len)
                    {
                        Array.Reverse(array, i, len - lastIndexBlock);
                    }
                }
            }
            else if (Sse2.IsSupported)
            {
                int size = Vector128<int>.Count;

                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }

                int lastIndexBlock = len - len % size;

                fixed (int* ptr = array)
                {
                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;

                        var vl = Sse2.LoadVector128(leftPtr);
                        var vr = Sse2.LoadVector128(rightPtr);

                        Sse2.Store(rightPtr, Sse2.Shuffle(vl, 0x1b));
                        Sse2.Store(leftPtr, Sse2.Shuffle(vr, 0x1b));
                    }
                }
                if (i << 1 < len)
                {
                    Array.Reverse(array, i, len - lastIndexBlock);
                }
            }
            else
#endif
            {
                Array.Reverse(array);
            }
        }

        /// <summary>
        /// Reverse array
        /// </summary>
        /// <param name="array"></param>
        public static void Reverse(ulong[] array)
        {
            Array.Reverse(array);
        }

        /// <summary>
        /// Reverse array
        /// </summary>
        /// <param name="array"></param>
        public static void Reverse(long[] array)
        {
            Array.Reverse(array);
        }

        /// <summary>
        /// Reverse array
        /// </summary>
        /// <param name="array"></param>
        public static void Reverse(byte[] array)
        {
            int len = array.Length;
            if (len < 2)
            {
                return;
            }
#if NET5_0 || NETCOREAPP3_1
            int i = 0;

            if (Avx2.IsSupported)
            {
                int size = Vector256<byte>.Count;
                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }
                int lastIndexBlock = len - len % size;

                fixed (byte* ptr = array)
                {
                    var mask = Vector256.Create(
                        31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16,
                        15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0).AsByte();

                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;
                        var vl = Avx.LoadVector256(leftPtr);
                        var vr = Avx.LoadVector256(rightPtr);
                        var va = Avx2.Shuffle(vl, mask);
                        var vb = Avx2.Shuffle(vr, mask);
                        Avx.Store(leftPtr, Avx2.Permute2x128(vb, vb, 0x27));
                        Avx.Store(rightPtr, Avx2.Permute2x128(va, va, 0x27));
                    }
                    if (i < len)
                    {
                        Array.Reverse(array, i, len - lastIndexBlock);
                    }
                }
            }
            else if (Ssse3.IsSupported)
            {
                int size = Vector128<byte>.Count;
                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }

                int lastIndexBlock = len - len % size;

                fixed (byte* ptr = array)
                {
                    var mask = Vector128.Create(15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0).AsByte();
                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;

                        var vl = Sse2.LoadVector128(leftPtr);
                        var vr = Sse2.LoadVector128(rightPtr);
                        Sse2.Store(rightPtr, Ssse3.Shuffle(vl, mask));
                        Sse2.Store(leftPtr, Ssse3.Shuffle(vr, mask));
                    }
                }
                if (i << 1 < len)
                {
                    Array.Reverse(array, i, len - lastIndexBlock);
                }
            }
            else
#endif
            {
                Array.Reverse(array);
            }
        }

        /// <summary>
        /// Reverse array
        /// </summary>
        /// <param name="array"></param>
        public static void Reverse(sbyte[] array)
        {
            int len = array.Length;
            if (len < 2)
            {
                return;
            }
#if NET5_0 || NETCOREAPP3_1
            int i = 0;

            if (Avx2.IsSupported)
            {
                int size = Vector256<sbyte>.Count;
                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }
                int lastIndexBlock = len - len % size;

                fixed (sbyte* ptr = array)
                {
                    var mask = Vector256.Create(
                        31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16,
                        15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0);

                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;
                        var vl = Avx.LoadVector256(leftPtr);
                        var vr = Avx.LoadVector256(rightPtr);
                        var va = Avx2.Shuffle(vl, mask);
                        var vb = Avx2.Shuffle(vr, mask);
                        Avx.Store(leftPtr, Avx2.Permute2x128(vb, vb, 0x27));
                        Avx.Store(rightPtr, Avx2.Permute2x128(va, va, 0x27));
                    }
                    if (i < len)
                    {
                        Array.Reverse(array, i, len - lastIndexBlock);
                    }
                }
            }
            else if (Ssse3.IsSupported)
            {
                int size = Vector128<sbyte>.Count;
                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }

                int lastIndexBlock = len - len % size;

                fixed (sbyte* ptr = array)
                {
                    var mask = Vector128.Create(15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0);
                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;

                        var vl = Sse2.LoadVector128(leftPtr);
                        var vr = Sse2.LoadVector128(rightPtr);
                        Sse2.Store(rightPtr, Ssse3.Shuffle(vl, mask));
                        Sse2.Store(leftPtr, Ssse3.Shuffle(vr, mask));
                    }
                }
                if (i << 1 < len)
                {
                    Array.Reverse(array, i, len - lastIndexBlock);
                }
            }
            else
#endif
            {
                Array.Reverse(array);
            }
        }

        /// <summary>
        /// Reverse array
        /// </summary>
        /// <param name="array"></param>
        public static void Reverse(float[] array)
        {
            int len = array.Length;
            if (len < 2)
            {
                return;
            }

#if NET5_0 || NETCOREAPP3_1
            int i = 0;
            if (Avx.IsSupported)
            {
                int size = Vector256<float>.Count;
                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }

                int lastIndexBlock = len - len % size;

                fixed (float* ptr = array)
                {
                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;
                        var vl = Avx.LoadVector256(leftPtr);
                        var vr = Avx.LoadVector256(rightPtr);
                        var va = Avx.Permute2x128(vr, vr, 0x67);
                        var vb = Avx.Permute2x128(vl, vl, 0x67);
                        Avx.Store(leftPtr, Avx.Permute(va, 0x1b));
                        Avx.Store(rightPtr, Avx.Permute(vb, 0x1b));
                    }
                    if (i << 1 < len)
                    {
                        Array.Reverse(array, i, len - lastIndexBlock);
                    }
                }
            }
            else if (Sse.IsSupported)
            {
                int size = Vector128<float>.Count;
                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }

                int lastIndexBlock = len - len % size;

                fixed (float* ptr = array)
                {
                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;

                        var vl = Sse.LoadVector128(leftPtr);
                        var vr = Sse.LoadVector128(rightPtr);
                        Sse.Store(rightPtr, Sse.Shuffle(vl, vl, 0x1b));
                        Sse.Store(leftPtr, Sse.Shuffle(vr, vr, 0x1b));
                    }
                }

                if (i << 1 < len)
                {
                    Array.Reverse(array, i, len - lastIndexBlock);
                }
            }
            else
#endif
            {
                Array.Reverse(array);
            }
        }

        /// <summary>
        /// Reverse array
        /// </summary>
        /// <param name="array"></param>
        public static void Reverse(double[] array)
        {
            int len = array.Length;
            if (len < 2)
            {
                return;
            }

#if NET5_0 || NETCOREAPP3_1
            int i = 0;
            if (Avx.IsSupported)
            {
                int size = Vector256<double>.Count;
                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }

                int lastIndexBlock = len - len % size;

                fixed (double* ptr = array)
                {
                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;
                        var vl = Avx.LoadVector256(leftPtr);
                        var vr = Avx.LoadVector256(rightPtr);
                        var va = Avx.Permute2x128(vr, vr, 0x67);
                        var vb = Avx.Permute2x128(vl, vl, 0x67);
                        Avx.Store(leftPtr, Avx.Permute(va, 0x5));
                        Avx.Store(rightPtr, Avx.Permute(vb, 0x5));
                    }
                    if (i << 2 < len)
                    {
                        Array.Reverse(array, i, len - lastIndexBlock);
                    }
                }
            }
            else if (Sse2.IsSupported)
            {
                int size = Vector128<double>.Count;
                if (len < size << 1)
                {
                    Array.Reverse(array, 0, array.Length);
                    return;
                }

                int lastIndexBlock = len - len % size;

                fixed (double* ptr = array)
                {
                    for (; i < lastIndexBlock >> 1; i += size)
                    {
                        var leftPtr = ptr + i;
                        var rightPtr = ptr + len - size - i;
                        var vl = Sse2.LoadVector128(leftPtr);
                        var vr = Sse2.LoadVector128(rightPtr);
                        Sse2.Store(rightPtr, Sse2.Shuffle(vl, vl, 0x65));
                        Sse2.Store(leftPtr, Sse2.Shuffle(vr, vr, 0x65));
                    }
                }
                if (i << 2 < len)
                {
                    Array.Reverse(array, i, len - lastIndexBlock);
                }
            }
            else
#endif
            {
                Array.Reverse(array);
            }
        }

        /// <summary>
        /// Reverse matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        public static void Reverse(Matrix<int> matrix)
        {
            Reverse(matrix.GetArray());
        }

        /// <summary>
        /// Reverse matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        public static void Reverse(Matrix<uint> matrix)
        {
            Reverse(matrix.GetArray());
        }

        /// <summary>
        /// Reverse matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        public static void Reverse(Matrix<long> matrix)
        {
            Reverse(matrix.GetArray());
        }

        /// <summary>
        /// Reverse matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        public static void Reverse(Matrix<ulong> matrix)
        {
            Reverse(matrix.GetArray());
        }

        /// <summary>
        /// Reverse matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        public static void Reverse(Matrix<float> matrix)
        {
            Reverse(matrix.GetArray());
        }

        /// <summary>
        /// Reverse matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        public static void Reverse(Matrix<double> matrix)
        {
            Reverse(matrix.GetArray());
        }

        /// <summary>
        /// Reverse matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        public static void Reverse(Matrix<byte> matrix)
        {
            Reverse(matrix.GetArray());
        }

        /// <summary>
        /// Reverse matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        public static void Reverse(Matrix<sbyte> matrix)
        {
            Reverse(matrix.GetArray());
        }
    }
}