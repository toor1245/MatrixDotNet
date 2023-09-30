using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using MatrixDotNet.Exceptions;
#if NETCOREAPP3_1_OR_GREATER
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace MatrixDotNet.Extensions.Performance
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct MatrixOnStack
    {
        private const short Size = 6_561;

        [FieldOffset(5)] internal fixed double _array[Size];

        [field: FieldOffset(0)] public byte Rows { get; private set; }

        [field: FieldOffset(1)] public byte Columns { get; private set; }

        [field: FieldOffset(2)] public int Length { get; private set; }

        [field: FieldOffset(52495)] public bool IsPrime { get; private set; }

        [field: FieldOffset(7)] public bool IsSquare { get; private set; }

        /// <summary>
        ///     Gets data of matrix as span.
        /// </summary>
        public Span<double> Data
        {
            get
            {
                fixed (double* ptr = _array)
                {
                    return new Span<double>(ptr, Length);
                }
            }
        }

        /// <summary>
        ///     Initialize empty matrix.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public MatrixOnStack(byte rows, byte columns) : this()
        {
            Initialize(rows, columns);
        }

        /// <summary>
        ///     init implicit of matrix.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>init matrix as fixed buffer</returns>
        public static implicit operator MatrixOnStack(double[,] matrix)
        {
            return new MatrixOnStack(matrix);
        }

        /// <summary>
        ///     init implicit of matrix.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>init matrix as fixed buffer</returns>
        public static implicit operator MatrixOnStack(Matrix<double> matrix)
        {
            return new MatrixOnStack(matrix.GetArray(), matrix.Rows, matrix.Columns);
        }

        /// <summary>
        ///     Initialize matrix.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        public MatrixOnStack(double[,] matrix) : this()
        {
            var m = matrix.GetLength(0);
            var n = matrix.GetLength(1);
            Initialize((byte) m, (byte) n);

            fixed (double* ptr = matrix)
            {
                var span = new Span<double>(ptr, m * n);
                var arr = Data;

                for (var i = 0; i < Length; i++)
                {
                    arr[i] = span[i];
                }
            }
        }

        /// <summary>
        ///     Initialize matrix.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="m">number of rows of the matrix</param>
        /// <param name="n">number of columns of the matrix</param>
        public MatrixOnStack(double[] matrix, int m, int n) : this()
        {
            Initialize((byte) m, (byte) n);

            fixed (double* ptr = matrix)
            {
                var span = new Span<double>(ptr, m * n);
                var arr = Data;

                for (var i = 0; i < Length; i++)
                {
                    arr[i] = span[i];
                }
            }
        }

        /// <summary>
        ///     Init data of matrix.
        /// </summary>
        /// <param name="rows">the rows</param>
        /// <param name="columns">the columns</param>
        /// <exception cref="MatrixDotNetException">length matrix more than 6_561.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Initialize(byte rows, byte columns)
        {
            if (rows * columns >= Size) throw new MatrixDotNetException("Size must be less than 6_561!!!");
            Rows = rows;
            Columns = columns;
            Length = rows * columns;
            IsSquare = rows == columns;
            IsPrime = (rows & 0b01) == 0 && (columns & 0b01) == 0;
        }

        /// <summary>
        ///     Adds two matrices.
        /// </summary>
        /// <param name="left">the left matrix.</param>
        /// <param name="right">the right matrix.</param>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException">matrices are not equal</exception>
        public static MatrixOnStack AddByRef(ref MatrixOnStack left, ref MatrixOnStack right)
        {
            var m = left.Rows;
            var n = left.Columns;

            if (m != right.Rows || n != right.Columns)
                throw new MatrixDotNetException("Not Equal");

            var matrix = new MatrixOnStack(m, n);

#if NETCOREAPP3_1_OR_GREATER
            if (Avx2.IsSupported)
            {
                var length = left.Length;
                fixed (double* ptr3 = matrix.Data)
                fixed (double* ptr1 = left._array)
                fixed (double* ptr2 = right._array)
                {
                    var i = 0;

                    // Adds two matrices via AVX.
                    while (i < length - Vector256<double>.Count)
                    {
                        var vector1 = Avx.LoadVector256(ptr1 + i);
                        Avx.Store(ptr3 + i, Avx.Add(vector1, Avx.LoadVector256(ptr2 + i)));
                        i += 4;
                    }

                    while (i < length)
                    {
                        matrix.Data[i] = left.Data[i] + right.Data[i];
                        i++;
                    }
                }
            }
            else
#endif
            {
                var a1 = left.Data;
                var a2 = right.Data;
                var a3 = matrix.Data;

                for (var i = 0; i < m; i++)
                {
                    for (var j = 0; j < n; j++)
                    {
                        var num = i + m * j;
                        a3[num] = a2[num] + a1[num];
                    }
                }
            }

            return matrix;
        }

        /// <summary>
        ///     Subtracts two matrices via AVX(if supported) or unsafe.
        /// </summary>
        /// <param name="left">the matrix with fixed buffer.</param>
        /// <param name="right">the matrix with fixed buffer.</param>
        /// <returns>new matrix from subtract two matrices. </returns>
        /// <exception cref="MatrixDotNetException">matrices not equal by size.</exception>
        public static MatrixOnStack SubByRef(ref MatrixOnStack left, ref MatrixOnStack right)
        {
            var m = left.Rows;
            var n = left.Columns;

            if (m != right.Rows || n != right.Columns)
            {
                throw new MatrixDotNetException("Not Equal");
            }

            var matrix = new MatrixOnStack(m, n);

#if NETCOREAPP3_1_OR_GREATER
            if (Avx2.IsSupported)
            {
                var length = left.Length;
                fixed (double* ptr3 = matrix.Data)
                fixed (double* ptr1 = left._array)
                fixed (double* ptr2 = right._array)
                {
                    var i = 0;

                    // Adds two matrices via AVX.
                    while (i < length - Vector256<double>.Count)
                    {
                        var vector1 = Avx.LoadVector256(ptr1 + i);
                        var vector2 = Avx.LoadVector256(ptr2 + i);
                        Avx.Store(ptr3 + i, Avx.Subtract(vector1, vector2));
                        i += 4;
                    }

                    while (i < length)
                    {
                        matrix.Data[i] = left.Data[i] - right.Data[i];
                        i++;
                    }
                }
            }
            else
#endif
            {
                var a1 = left.Data;
                var a2 = right.Data;
                var a3 = matrix.Data;

                // Adds two matrices.
                for (var i = 0; i < m; i++)
                {
                    for (var j = 0; j < n; j++)
                    {
                        var num = i + m * j;
                        a3[num] = a2[num] - a1[num];
                    }
                }
            }

            return matrix;
        }


        /// <summary>
        ///     Multiplies two matrices with fixed buffer.
        /// </summary>
        /// <param name="left">the left matrix.</param>
        /// <param name="right">the right matrix.</param>
        /// <returns>new matrix from multiply of two matrices</returns>
        /// <exception cref="MatrixDotNetException">
        ///     throws exception if length columns of left matrix not equal length rows of right matrix
        /// </exception>
        public static MatrixOnStack MulByRef(ref MatrixOnStack left, ref MatrixOnStack right)
        {
            if (left.Columns != right.Rows)
            {
                throw new MatrixDotNetException("columns of left matrix must be equal rows of right matrix");
            }

            return MulMatrix(ref left, ref right);
        }

        /// <summary>
        ///     Multiply two matrices which support LINUX,WINDOWS,MAC.
        /// </summary>
        /// <param name="left">the left matrix</param>
        /// <param name="right">the right matrix.</param>
        /// <returns></returns>
        private static MatrixOnStack MulMatrix(ref MatrixOnStack left, ref MatrixOnStack right)
        {
            var m = left.Rows;
            var n = right.Columns;
            var K = left.Columns;
            var len1 = left.Length;
            var matrix = new MatrixOnStack(m, n);
            fixed (double* pointer1 = left._array)
            fixed (double* pointer2 = right._array)
            fixed (double* pointer3 = matrix.Data)
            {
                var span1 = new Span<double>(pointer1, len1);

                for (var i = 0; i < m; i++)
                {
                    var c = pointer3 + i * n;

                    for (var k = 0; k < K; k++)
                    {
                        var b = pointer2 + k * n;
                        var a = span1[i * K + k];
                        for (var j = 0; j < n; j++) c[j] += a * b[j];
                    }
                }

                return matrix;
            }
        }

        /// <summary>
        ///     Gets column by row.
        /// </summary>
        /// <param name="column">the column.</param>
        /// <returns></returns>
        public Span<double> GetColumn(int column)
        {
            int m = Rows;
            fixed (double* ptr = _array)
            {
                var span2 = new Span<double>(ptr, m);
                var span = new Span<double>(ptr, Length);
                for (var i = 0; i < m; i++)
                {
                    span2[i] = span[column + Columns * i];
                }
                return span2;
            }
        }

        /// <summary>
        ///     Sets column by index of column matrix.
        /// </summary>
        /// <param name="column">the index.</param>
        /// <param name="data">the data.</param>
        public void SetColumn(int column, Span<double> data)
        {
            int m = Rows;
            fixed (double* ptr = _array)
            {
                var span2 = new Span<double>(ptr, Length);
                for (var i = 0; i < m; i++)
                {
                    span2[column * Columns + i] = data[i];
                }
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            fixed (double* ptr = _array)
            {
                var span1 = new Span<double>(ptr, Length);
                for (var i = 0; i < Rows; i++)
                {
                    var span = span1.Slice(i * Columns, Columns);
                    foreach (var t in span)
                    {
                        builder.Append(t);
                        builder.Append(" ");
                    }

                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }

        /// <summary>
        ///     Gets value by ref.
        /// </summary>
        /// <param name="i">the row.</param>
        /// <param name="j">the column.</param>
        public ref double this[int i, int j] => ref _array[i * Columns + j];

        /// <summary>
        ///     Gets arr of matrix.
        /// </summary>
        /// <param name="i">the row</param>
        public Span<double> this[int i]
        {
            get
            {
                fixed (double* ptr = _array)
                {
                    return new Span<double>(ptr, Length).Slice(i * Columns, Columns);
                }
            }

            set
            {
                fixed (double* ptr = _array)
                {
                    var span = new Span<double>(ptr, Length).Slice(i * Columns, Columns);
                    for (var j = 0; j < span.Length; j++)
                    {
                        span[j] = value[j];
                    }
                }
            }
        }
    }
}