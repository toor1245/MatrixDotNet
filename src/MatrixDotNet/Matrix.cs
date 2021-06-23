using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions;
using MatrixDotNet.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
#if NET5_0 || NETCOREAPP3_1
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Extensions.Performance.Simd.Handler;
#endif

namespace MatrixDotNet
{
    /// <summary>
    /// Represents math matrix.
    /// </summary>
    /// <typeparam name="T">integral type.</typeparam>
    [Serializable]
    public sealed class Matrix<T> : ICloneable, IEnumerable<T>
        where T : unmanaged
    {
        #region properties

        /// <summary>
        /// Gets matrix.
        /// </summary>
        internal T[] _Matrix { get; private set; }

        public T[] GetArray()
        {
            return _Matrix;
        }

        /// <summary>
        /// Gets length matrix.
        /// </summary>
        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _Matrix.Length;
        }

        /// <summary>
        /// Gets length row of matrix.
        /// </summary>
        public int Rows { get; private set; }

        /// <summary>
        /// Gets length columns of matrix.
        /// </summary>
        public int Columns { get; private set; }

        /// <summary>
        /// Checks square matrix.
        /// </summary>
        public bool IsSquare
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Rows == Columns;
        }

        /// <summary>
        /// Checks for pairing of matrix.
        /// </summary>
        public bool IsPrime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (Rows & 0b01) == 0 && (Columns & 0b01) == 0;
        }

        /// <summary>
        /// Checks for symmetric matrix.
        /// </summary>
        public bool IsSymmetric
        {
            get
            {
                if (!IsSquare)
                {
                    throw new MatrixDotNetException("matrix is not square");
                }

                var comparer = Comparer<T>.Default;

                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if (comparer.Compare(this[j, i], this[i, j]) != 0)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        #endregion

        #region indexators

        /// <summary>
        /// Gets element matrix.
        /// </summary>
        /// <param name="i">the index by rows.</param>
        /// <param name="j">the index by columns.</param>
        /// <exception cref="IndexOutOfRangeException">
        /// Throws if index out of range
        /// </exception>
        public T this[int i, int j]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _Matrix[i * Columns + j];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _Matrix[i * Columns + j] = value;
        }

        public ref T GetByRef(int i, int j)
        {
            return ref _Matrix[i * Columns + j];
        }

        /// <summary>
        /// Gets or sets array by row.
        /// </summary>
        /// <param name="i">the row</param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public T[] this[int i]
        {
            get => this.GetRow(i);
            set
            {
                for (int j = 0; j < Columns; j++)
                {
                    this[i, j] = value[j];
                }
            }
        }

        /// <summary>
        /// Gets or sets array by rows or columns.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="dimension"></param>
        /// <exception cref="IndexOutOfRangeException"></exception>
        public unsafe T[] this[int i, State dimension]
        {
            get => dimension == State.Row ? this.GetRow(i) : this.GetColumn(i);
            set
            {
                if (dimension == State.Row)
                {
                    if (i >= Rows)
                        throw new IndexOutOfRangeException();

                    fixed (T* ptr1 = _Matrix)
                    fixed (T* ptr2 = value)
                    {
                        Unsafe.CopyBlock(ptr1 + i * Columns, ptr2, (uint) (sizeof(T) * value.Length));
                    }
                }
                else if (dimension == State.Column)
                {
                    for (int j = 0; j < Rows; j++)
                    {
                        this[j, i] = value[j];
                    }
                }
            }
        }

        public T this[int m, int n, State dimension]
        {
            get => dimension == State.Row ? this[m, n] : this[n, m];
            set
            {
                if (dimension == State.Row)
                {
                    this[m, n] = value;
                }
                else
                {
                    this[n, m] = value;
                }
            }
        }

        #endregion

        #region .ctor

        /// <summary>
        /// Initialize matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        public unsafe Matrix(T[,] matrix)
        {
            Rows = matrix.GetLength(0);
            Columns = matrix.GetLength(1);

            _Matrix = new T[Rows * Columns];
            fixed (T* ptr1 = _Matrix)
            fixed (T* ptr2 = matrix)
            {
                Unsafe.CopyBlock(ptr1, ptr2, (uint) (sizeof(T) * Length));
            }
        }


        /// <summary>
        /// Creates matrix.
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="col">col</param>
        public Matrix(int row, int col)
        {
            Rows = row;
            Columns = col;
            _Matrix = new T[row * col];
        }

        /// <summary>
        /// Creates matrix.
        /// </summary>
        /// <param name="array">array</param>
        /// <param name="row">row</param>
        /// <param name="col">col</param>
        internal unsafe Matrix(T[] array, int row, int col)
        {
            Rows = row;
            Columns = col;
            _Matrix = new T[row * col];
            fixed (T* ptr1 = _Matrix)
            fixed (T* ptr2 = array)
            {
                Unsafe.CopyBlock(ptr1, ptr2, (uint) (sizeof(T) * Length));
            }
        }

        /// <summary>
        /// Creates matrix with init constant value.
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="col">col</param>
        /// <param name="value">constant</param>
        public unsafe Matrix(int row, int col, T value)
        {
            Rows = row;
            Columns = col;
            _Matrix = new T[row * col];

#if NET5_0 || NETCOREAPP3_1

            if (Avx.IsSupported)
            {
                var vector = IntrinsicsHandler<T>.CreateVector256(value);
                int i = 0;
                int length = _Matrix.Length;
                fixed (T* ptr = _Matrix)
                {
                    int size = Vector256<T>.Count;
                    int lastIndexBlock = length - length % size;
                    for (; i < lastIndexBlock; i += size)
                    {
                        IntrinsicsHandler<T>.StoreVector256(ptr + i, vector);
                    }
                }
                for (; i < length; i++)
                {
                    _Matrix[i] = value;
                }
            }
            else if (Sse.IsSupported)
            {
                var vector = IntrinsicsHandler<T>.CreateVector128(value);
                int i = 0;
                int length = _Matrix.Length;
                fixed (T* ptr = _Matrix)
                {
                    int size = Vector128<T>.Count;
                    int lastIndexBlock = length - length % size;
                    for (; i < lastIndexBlock; i += size)
                    {
                        IntrinsicsHandler<T>.StoreVector128(ptr + i, vector);
                    }
                }
                for (; i < length; i++)
                {
                    _Matrix[i] = value;
                }
            }
            else
#endif
            {
                Array.Fill(_Matrix, value);
            }
        }

        #endregion

        #region operators

        /// <summary>
        /// Add operation of two matrix.
        /// </summary>
        /// <param name="left">left matrix.</param>
        /// <param name="right">right matrix.</param>
        /// <returns><see cref="Matrix{T}"/></returns>
        /// <exception cref="MatrixDotNetException">
        /// Length of two matrix not equal.
        /// </exception>
        public static unsafe Matrix<T> operator +(Matrix<T> left, Matrix<T> right)
        {
            if (left.Rows != right.Rows || left.Columns != right.Columns)
            {
                throw new MatrixDotNetException(ExceptionArgument.MatricesLengthAreNotEqual);
            }

            int m = left.Rows;
            int n = right.Columns;
            int length = left.Length;

            Matrix<T> matrix = new Matrix<T>(m, n);

            fixed (T* ptr1 = left.GetArray())
            fixed (T* ptr2 = right.GetArray())
            fixed (T* ptr3 = matrix.GetArray())
            {
                Span<T> span1 = new Span<T>(ptr1, length);
                Span<T> span2 = new Span<T>(ptr2, length);
                Span<T> span3 = new Span<T>(ptr3, length);
                int i = 0;

#if NET5_0 || NETCOREAPP3_1
                if (Avx2.IsSupported && typeof(T) != typeof(decimal))
                {
                    int size = Vector256<T>.Count;
                    int lastIndexBlock = length - length % size;
                    for (; i < lastIndexBlock; i += size)
                    {
                        var va = IntrinsicsHandler<T>.LoadVector256(ptr1 + i);
                        var vb = IntrinsicsHandler<T>.LoadVector256(ptr2 + i);
                        IntrinsicsHandler<T>.StoreVector256(ptr3 + i, IntrinsicsHandler<T>.AddVector256(va, vb));
                    }
                }
                else if (Sse2.IsSupported && typeof(T) != typeof(decimal))
                {
                    int size = Vector128<T>.Count;
                    int lastIndexBlock = length - length % size;
                    for (; i < lastIndexBlock; i += size)
                    {
                        var va = IntrinsicsHandler<T>.LoadVector128(ptr1 + i);
                        var vb = IntrinsicsHandler<T>.LoadVector128(ptr2 + i);
                        IntrinsicsHandler<T>.StoreVector128(ptr3 + i, IntrinsicsHandler<T>.AddVector128(va, vb));
                    }
                }
#endif
                for (; i < length; i++)
                {
                    span3[i] = MathUnsafe<T>.Add(span2[i], span1[i]);
                }
            }
            return matrix;
        }

        /// <summary>
        /// Subtract operation of two matrix.
        /// </summary>
        /// <param name="left">left matrix.</param>
        /// <param name="right">right matrix.</param>
        /// <returns><see cref="Matrix{T}"/>.</returns>
        /// <exception cref="MatrixDotNetException">
        /// Length of two matrix not equal.
        /// </exception>
        public static unsafe Matrix<T> operator -(Matrix<T> left, Matrix<T> right)
        {
            if (left.Rows != right.Rows || left.Columns != right.Columns)
            {
                throw new MatrixDotNetException(ExceptionArgument.MatricesLengthAreNotEqual);
            }

            int m = left.Rows;
            int n = right.Columns;
            int length = left.Length;

            Matrix<T> matrix = new Matrix<T>(m, n);

            fixed (T* ptr1 = left.GetArray())
            fixed (T* ptr2 = right.GetArray())
            fixed (T* ptr3 = matrix.GetArray())
            {
                Span<T> span1 = new Span<T>(ptr1, length);
                Span<T> span2 = new Span<T>(ptr2, length);
                Span<T> span3 = new Span<T>(ptr3, length);
                int i = 0;

#if NET5_0 || NETCOREAPP3_1
                if (Avx2.IsSupported && typeof(T) != typeof(decimal))
                {
                    int size = Vector256<T>.Count;
                    int lastIndexBlock = length - length % size;
                    for (; i < lastIndexBlock; i += size)
                    {
                        var va = IntrinsicsHandler<T>.LoadVector256(ptr1 + i);
                        var vb = IntrinsicsHandler<T>.LoadVector256(ptr2 + i);
                        IntrinsicsHandler<T>.StoreVector256(ptr3 + i, IntrinsicsHandler<T>.SubtractVector256(va, vb));
                    }
                }
                else if (Sse2.IsSupported && typeof(T) != typeof(decimal))
                {
                    int size = Vector128<T>.Count;
                    int lastIndexBlock = length - length % size;
                    for (; i < lastIndexBlock; i += size)
                    {
                        var va = IntrinsicsHandler<T>.LoadVector128(ptr1 + i);
                        var vb = IntrinsicsHandler<T>.LoadVector128(ptr2 + i);
                        IntrinsicsHandler<T>.StoreVector128(ptr3 + i, IntrinsicsHandler<T>.SubtractVector128(va, vb));
                    }
                }
#endif
                for (; i < length; i++)
                {
                    span3[i] = MathUnsafe<T>.Sub(span1[i], span2[i]);
                }
            }
            return matrix;
        }

        /// <summary>
        /// Multiply operation of two matrix.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe Matrix<T> operator *(Matrix<T> left, Matrix<T> right)
        {
            if (left.Columns != right.Rows)
            {
                throw new SizeNotEqualException(ExceptionArgument.MatricesMultiplySize);
            }

            var m = left.Rows;
            var n = right.Columns;
            var l = left.Columns;
            var length = m * n;
            var matrixC = new Matrix<T>(m, n);

            fixed (T* ptrA = left.GetArray())
            fixed (T* ptrB = right.GetArray())
            fixed (T* ptrC = matrixC.GetArray())
            {
                var span1 = new Span<T>(ptrA, length);

#if NET5_0 || NETCOREAPP3_1
                if (IntrinsicsHandler<T>.CanMultiply(m))
                {
                    var size = Vector256<T>.Count;
                    for (int i = 0; i < m; i++)
                    {
                        var c = ptrC + i * n;
                        for (int k = 0; k < l; k++)
                        {
                            var b = ptrB + k * n;
                            var va = IntrinsicsHandler<T>.CreateVector256(span1[i * l + k]);
                            for (int j = 0; j < n; j += size << 1)
                            {
                                var vb1 = IntrinsicsHandler<T>.LoadVector256(b + j);
                                var vb2 = IntrinsicsHandler<T>.LoadVector256(b + j + size);

                                var vc1 = IntrinsicsHandler<T>.LoadVector256(c + j);
                                var vc2 = IntrinsicsHandler<T>.LoadVector256(c + j + size);

                                IntrinsicsHandler<T>.StoreVector256(c + j + 0,
                                    IntrinsicsHandler<T>.MultiplyAddVector256(va, vb1, vc1));
                                IntrinsicsHandler<T>.StoreVector256(c + j + size,
                                    IntrinsicsHandler<T>.MultiplyAddVector256(va, vb2, vc2));
                            }
                        }
                    }
                }
                else
#endif
                {
                    for (int i = 0; i < m; i++)
                    {
                        var c = ptrC + i * n;
                        for (int k = 0; k < l; k++)
                        {
                            var b = ptrB + k * n;
                            var a = span1[i * l + k];
                            for (int j = 0; j < n; j++)
                            {
                                c[j] = MathUnsafe<T>.Add(c[j], MathUnsafe<T>.Mul(a, b[j]));
                            }
                        }
                    }
                }
            }
            return matrixC;
        }

        /// <summary>
        /// Multiply operation matrix on digit right side.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="digit">digit.</param>
        /// <returns><see cref="Matrix{T}"/></returns>
        public static Matrix<T> operator *(Matrix<T> matrix, T digit)
        {
            var multiplyFunc = MathGeneric<T, T, T>.GetMultiplyFunc();
            Matrix<T> result = new Matrix<T>(matrix.Rows, matrix.Columns);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = multiplyFunc(matrix[i, j], digit);
                }
            }

            return result;
        }

        /// <summary>
        /// Multiply operation matrix on digit left side.
        /// </summary>
        /// <param name="digit">digit</param>
        /// <param name="matrix">matrix</param>
        /// <returns><see cref="Matrix{T}"/></returns>
        public static Matrix<T> operator *(T digit, Matrix<T> matrix)
        {
            return matrix * digit;
        }

        /// <summary>
        /// Divide operation matrix on digit right side.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="digit"></param>
        /// <returns></returns>
        public static Matrix<T> operator /(Matrix<T> matrix, T digit)
        {
            var divideFunc = MathGeneric<T>.GetDivideFunc();

            Matrix<T> result = new Matrix<T>(matrix.Rows, matrix.Columns);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = divideFunc(matrix[i, j], digit);
                }
            }

            return result;
        }

        /// <summary>
        /// Divide operation matrix on digit left side.
        /// </summary>
        /// <param name="digit"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Matrix<T> operator /(T digit, Matrix<T> matrix)
        {
            var divideFunc = MathGeneric<T>.GetDivideFunc();
            Matrix<T> result = new Matrix<T>(matrix.Rows, matrix.Columns);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = divideFunc(digit, matrix[i, j]);
                }
            }

            return result;
        }

#if NET5_0 || NETCOREAPP3_1
        /// <summary>
        /// Negate matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        public static unsafe Matrix<short> Negate(Matrix<short> matrix)
        {
            if (Avx2.IsSupported)
            {
                var negate = new Matrix<short>(matrix._Matrix, matrix.Rows, matrix.Columns);
                int size = Vector256<short>.Count;
                int len = matrix.Length;
                int lastIndexBlock = len - len % size;
                int i = 0;
                var setOne = IntrinsicsHandler<short>.SetAllBits256;
                Span<short> negateSpan = negate._Matrix;
                Span<short> matrixSpan = matrix._Matrix;
                fixed (short* ptr1 = negateSpan)
                fixed (short* ptr2 = matrixSpan)
                {
                    for (; i < lastIndexBlock; i += size)
                    {
                        Avx.Store(ptr1 + i, Avx2.Sign(Avx.LoadVector256(ptr2 + i), setOne));
                    }
                }

                ref var negValue = ref Unsafe.Add(ref MemoryMarshal.GetReference(negateSpan), i);
                for (; i < matrix.Length; i++)
                {
                    negValue = MathUnsafe<short>.Negate(negValue);
                    negValue = ref Unsafe.Add(ref negValue, 1);
                }
                return negate;
            }
            else if (Ssse3.IsSupported)
            {
                var negate = new Matrix<short>(matrix._Matrix, matrix.Rows, matrix.Columns);
                int size = Vector128<short>.Count;
                int len = matrix.Length;
                int lastIndexBlock = len - len % size;
                int i = 0;
                var setOne = IntrinsicsHandler<short>.SetAllBits128;
                Span<short> negateSpan = negate._Matrix;
                Span<short> matrixSpan = matrix._Matrix;
                fixed (short* ptr1 = negateSpan)
                fixed (short* ptr2 = matrixSpan)
                {
                    for (; i < lastIndexBlock; i += size)
                    {
                        Sse2.Store(ptr1 + i, Ssse3.Sign(Sse2.LoadVector128(ptr2 + i), setOne));
                    }
                }

                ref var negValue = ref Unsafe.Add(ref MemoryMarshal.GetReference(negateSpan), i);
                for (; i < matrix.Length; i++)
                {
                    negValue = (short) -negValue;
                    negValue = ref Unsafe.Add(ref negValue, 1);
                }
                return negate;
            }
            return -matrix;
        }

        /// <summary>
        /// Negate matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        public static unsafe Matrix<sbyte> Negate(Matrix<sbyte> matrix)
        {
            if (Avx2.IsSupported)
            {
                var negate = new Matrix<sbyte>(matrix._Matrix, matrix.Rows, matrix.Columns);
                int size = Vector256<sbyte>.Count;
                int len = matrix.Length;
                int lastIndexBlock = len - len % size;
                int i = 0;
                var setOne = IntrinsicsHandler<sbyte>.SetAllBits256;
                Span<sbyte> negateSpan = negate._Matrix;
                Span<sbyte> matrixSpan = matrix._Matrix;
                fixed (sbyte* ptr1 = negateSpan)
                fixed (sbyte* ptr2 = matrixSpan)
                {
                    for (; i < lastIndexBlock; i += size)
                    {
                        Avx.Store(ptr1 + i, Avx2.Sign(Avx.LoadVector256(ptr2 + i), setOne));
                    }
                }

                ref var negValue = ref Unsafe.Add(ref MemoryMarshal.GetReference(negateSpan), i);
                for (; i < matrix.Length; i++)
                {
                    negValue = (sbyte) -negValue;
                    negValue = ref Unsafe.Add(ref negValue, 1);
                }
                return negate;
            }
            else if (Ssse3.IsSupported)
            {
                var negate = new Matrix<sbyte>(matrix._Matrix, matrix.Rows, matrix.Columns);
                int size = Vector128<sbyte>.Count;
                int len = matrix.Length;
                int lastIndexBlock = len - len % size;
                int i = 0;
                var setOne = IntrinsicsHandler<sbyte>.SetAllBits128;
                Span<sbyte> negateSpan = negate._Matrix;
                Span<sbyte> matrixSpan = matrix._Matrix;
                fixed (sbyte* ptr1 = negateSpan)
                fixed (sbyte* ptr2 = matrixSpan)
                {
                    for (; i < lastIndexBlock; i += size)
                    {
                        Sse2.Store(ptr1 + i, Ssse3.Sign(Sse2.LoadVector128(ptr2 + i), setOne));
                    }
                }

                ref var negValue = ref Unsafe.Add(ref MemoryMarshal.GetReference(negateSpan), i);
                for (; i < matrix.Length; i++)
                {
                    negValue = (sbyte) -negValue;
                    negValue = ref Unsafe.Add(ref negValue, 1);
                }

                return negate;
            }
            return -matrix;
        }

        /// <summary>
        /// Negate matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        public static unsafe Matrix<int> Negate(Matrix<int> matrix)
        {
            if (Avx2.IsSupported)
            {
                var negate = new Matrix<int>(matrix._Matrix, matrix.Rows, matrix.Columns);
                int size = Vector256<int>.Count;
                int len = matrix.Length;
                int lastIndexBlock = len - len % size;
                int i = 0;
                var setOne = IntrinsicsHandler<int>.SetAllBits256;
                Span<int> negateSpan = negate._Matrix;
                fixed (int* ptr1 = negateSpan)
                fixed (int* ptr2 = matrix._Matrix)
                {
                    for (; i < lastIndexBlock; i += size)
                    {
                        Avx.Store(ptr1 + i, Avx2.Sign(Avx.LoadVector256(ptr2 + i), setOne));
                    }
                }

                ref var negValue = ref Unsafe.Add(ref MemoryMarshal.GetReference(negateSpan), i);
                for (; i < matrix.Length; i++)
                {
                    negValue = -negValue;
                    negValue = ref Unsafe.Add(ref negValue, 1);
                }

                return negate;
            }
            else if (Ssse3.IsSupported)
            {
                var negate = new Matrix<int>(matrix._Matrix, matrix.Rows, matrix.Columns);
                int size = Vector128<int>.Count;
                int len = matrix.Length;
                int lastIndexBlock = len - len % size;
                int i = 0;
                var setOne = IntrinsicsHandler<int>.SetAllBits128;
                Span<int> negateSpan = negate._Matrix;
                fixed (int* ptr1 = negateSpan)
                fixed (int* ptr2 = matrix._Matrix)
                {
                    for (; i < lastIndexBlock; i += size)
                    {
                        Sse2.Store(ptr1 + i, Ssse3.Sign(Sse2.LoadVector128(ptr2 + i), setOne));
                    }
                }

                ref var negValue = ref Unsafe.Add(ref MemoryMarshal.GetReference(negateSpan), i);
                for (; i < matrix.Length; i++)
                {
                    negValue = -negValue;
                    negValue = ref Unsafe.Add(ref negValue, 1);
                }
                return negate;
            }
            return -matrix;
        }
#endif

        /// <summary>
        /// Returns negate matrix.
        /// </summary>
        /// <param name="matrix">matrix</param>
        /// <returns>Negate matrix</returns>
        public static Matrix<T> operator -(Matrix<T> matrix)
        {
            var negate = new Matrix<T>(matrix._Matrix, matrix.Rows, matrix.Columns);
            Span<T> negateSpan = negate._Matrix;
            ref T negValue = ref Unsafe.Add(ref MemoryMarshal.GetReference(negateSpan), 0);
            for (int i = 0; i < matrix.Length; i++)
            {
                negValue = MathUnsafe<T>.Negate(negValue);
                negValue = ref Unsafe.Add(ref negValue, 1);
            }

            return negate;
        }

        /// <summary>
        /// Returns vector sum of each multiply element of row.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="array">array.</param>
        /// <returns>sum of each multiply element of row.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static T[] operator *(T[] array, Matrix<T> matrix)
        {
            if (array.Length != matrix.Columns)
            {
                throw new SizeNotEqualException(ExceptionArgument.ColumnOfMatrixIsNotEqualSizeOfVector);
            }

            T[] res = new T[array.Length];

            for (int i = 0; i < matrix.Rows; i++)
            {
                T sum = default;
                for (int j = 0; j < matrix.Columns; j++)
                {
                    sum = MathGeneric<T, T, T>.Add(sum, MathGeneric<T, T, T>.Multiply(matrix[j, i], array[j]));
                }

                if (i == array.Length)
                    break;

                res[i] = sum;
            }

            return res;
        }


        /// <summary>
        /// Returns vector sum of each multiply element of row.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="array">array.</param>
        /// <returns>sum of each multiply element of row.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe T[] operator *(Matrix<T> matrix, T[] array)
        {
            if (matrix.Rows != array.Length)
            {
                throw new SizeNotEqualException(ExceptionArgument.RowSizeOfMatrixIsNotEqualSizeOfVector);
            }

            int m = matrix.Rows;
            int n = 1;
            int K = matrix.Columns;

            T[] result = new T[m];
            fixed (T* ptr1 = matrix._Matrix)
            fixed (T* ptr2 = array)
            fixed (T* ptr3 = result)
            {
                Span<T> span1 = new Span<T>(ptr1, matrix.Length);

                for (int i = 0; i < m; i++)
                {
                    T* c = ptr3 + i * n;

                    for (int k = 0; k < K; k++)
                    {
                        T* b = ptr2 + k * n;
                        T a = span1[i * K + k];
                        for (int j = 0; j < n; j++)
                        {
                            c[j] = MathUnsafe<T>.Add(c[j], MathUnsafe<T>.Mul(a, b[j]));
                        }
                    }
                }

                return result;
            }
        }

        /// <summary>
        /// Returns vector sum of each multiply element of row.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="vector">vector.</param>
        /// <returns>sum of each multiply element of row.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static Vectorization.Vector<T> operator *(Vectorization.Vector<T> vector, Matrix<T> matrix)
        {
            return vector.Array * matrix;
        }

        /// <summary>
        /// Returns vector sum of each multiply element of row.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="vector">vector.</param>
        /// <returns>sum of each multiply element of row.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static Vectorization.Vector<T> operator *(Matrix<T> matrix, Vectorization.Vector<T> vector)
        {
            return matrix * vector.Array;
        }

        /// <summary>
        /// Compares all values left matrix with right matrix.
        /// Returns true if left matrix full equals right matrix.
        /// </summary>
        /// <param name="left">matrix A</param>
        /// <param name="right">matrix B</param>
        /// <returns><see cref="Boolean"/></returns>
        /// <exception cref="NullReferenceException">Throws if left or right matrix are null.</exception>
        public static bool operator ==(Matrix<T> left, Matrix<T> right)
        {
            if (left is null || right is null)
            {
                throw new NullReferenceException();
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Compares all values left matrix with right matrix.
        /// Returns true if minimum one element of left matrix not equals right matrix.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Matrix<T> left, Matrix<T> right)
        {
            return !(left == right);
        }

        #endregion

        #region methods

        /// <summary>
        /// <inheritdoc cref="object.ToString"/>
        /// </summary>
        /// <returns><see cref="string"/></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            return MatrixExtension.Output(this, builder);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Clones matrix.
        /// </summary>
        /// <returns>object.</returns>
        public unsafe object Clone()
        {
            var res = new Matrix<T>(Rows, Columns);

            fixed (T* srcPtr = _Matrix)
            fixed (T* destPtr = res.GetArray())
            {
                Unsafe.CopyBlock(destPtr, srcPtr, (uint) (Length * sizeof(T)));
            }

            return res;
        }

        /// <summary>
        /// Implicit assign matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static implicit operator Matrix<T>(T[,] matrix)
        {
            return new Matrix<T>(matrix);
        }

        /// <summary>
        /// Implicit assign matrix.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static unsafe implicit operator Matrix<T>(T[][] matrix)
        {
            var columns = matrix[0].Length;
            for (int i = 1; i < matrix.Length; i++)
            {
                var prefetch = matrix[i].Length;
                columns = prefetch & ((columns - prefetch) >> 31) | columns & (~(columns - prefetch) >> 31);
            }

            var result = new Matrix<T>(matrix.Length, columns);

            fixed (T* dstPtr = result._Matrix)
            {
                for (int i = 0; i < matrix.Length; i++)
                {
                    fixed (T* srcPrt = matrix[i])
                    {
                        Unsafe.CopyBlock(dstPtr + i * result.Columns, srcPrt, (uint) (sizeof(T) * matrix[i].Length));
                    }
                }
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator() =>
            new Enumerator(this);


        public override bool Equals(object obj)
        {
            var matrix = obj as Matrix<T>;

            if (matrix is null || Rows != matrix.Rows || Columns != matrix.Columns)
            {
                return false;
            }

            if (matrix._Matrix == _Matrix)
            {
                return true;
            }

            int i = 0;

            for (; i < matrix.Length - Vector<T>.Count; i += Vector<T>.Count)
            {
                var vectorA = new Vector<T>(GetArray(), i);
                var vectorB = new Vector<T>(matrix.GetArray(), i);
                bool vector = Vector.EqualsAll(vectorA, vectorB);

                if (!vector)
                {
                    return false;
                }
            }

            for (; i < matrix._Matrix.Length; i++)
            {
                if (!_Matrix[i].Equals(matrix._Matrix[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _Matrix.GetHashCode();
        }

        #endregion

        #region enumerator

        /// <summary>
        /// Represents implementations IEnumerator.
        /// </summary>
        public struct Enumerator : IEnumerator<T>
        {
            private int _position;

            private readonly Matrix<T> _matrix;

            internal Enumerator(Matrix<T> matrix)
            {
                _position = -1;

                _matrix = matrix;
            }

            public bool MoveNext()
            {
                ++_position;

                return (_position >= 0 && _position < _matrix.Length);
            }

            public void Reset()
            {
                _position = -1;
            }

            public T Current => _matrix._Matrix[_position];

            object IEnumerator.Current => Current;

            /// <summary>
            /// Isn't used here
            /// </summary>
            public void Dispose()
            {
            }
        }

        #endregion
    }
}