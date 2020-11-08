using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Text;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct MatrixAsFixedBuffer
    {
        #region .fields
        
        private const short Size = 6_561;
        
        [FieldOffset(0)]
        private byte _rows;
        
        [FieldOffset(1)]
        private byte _columns;
        
        [FieldOffset(2)]
        private int _length;
        
        [FieldOffset(5)]
        internal fixed double _array[Size];

        [FieldOffset(7)]
        private bool _isSquare;

        [FieldOffset(52495)]
        private bool _isPrime;
        
        #endregion
        
        #region .properties

        public byte Rows => _rows;
        public byte Columns => _columns;
        public int Length => _length;
        public bool IsPrime => _isPrime;
        public bool IsSquare => _isSquare;

        /// <summary>
        /// Gets data of matrix as span.
        /// </summary>
        public Span<double> Data
        {
            get
            {
                fixed (double* ptr = _array)
                {
                    return new Span<double>(ptr,_length);
                }
            }
        }
        
        #endregion

        #region .ctor
        
        /// <summary>
        /// Initialize empty matrix.
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="columns"></param>
        public MatrixAsFixedBuffer(byte rows,byte columns) : this()
        {
            Initialize(rows,columns);
        }
        
        /// <summary>
        /// init implicit of matrix.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>init matrix as fixed buffer</returns>
        public static implicit operator MatrixAsFixedBuffer(double[,] matrix)
        {
            return new MatrixAsFixedBuffer(matrix);
        }

        /// <summary>
        /// init implicit of matrix.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <returns>init matrix as fixed buffer</returns>
        public static implicit operator MatrixAsFixedBuffer(Matrix<double> matrix)
        {
            return new MatrixAsFixedBuffer(matrix.GetArray(),matrix.Rows,matrix.Columns);
        }
        
        /// <summary>
        /// Initialize matrix.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        public MatrixAsFixedBuffer(double[,] matrix) : this()
        {
            var m = matrix.GetLength(0);
            var n = matrix.GetLength(1);
            Initialize((byte)m,(byte)n);
            
            fixed (double* ptr = matrix)
            {
                var span = new Span<double>(ptr,m * n);
                var arr = Data;
                
                for (int i = 0; i < _length; i++)
                {
                    arr[i] = span[i];
                }
            }
        }

        /// <summary>
        /// Initialize matrix.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="m">number of rows of the matrix</param>
        /// <param name="n">number of columns of the matrix</param>
        public MatrixAsFixedBuffer(double[] matrix,int m,int n) : this()
        {
            Initialize((byte)m,(byte)n);
            
            fixed (double* ptr = matrix)
            {
                var span = new Span<double>(ptr,m * n);
                var arr = Data;
                
                for (int i = 0; i < _length; i++)
                {
                    arr[i] = span[i];
                }
            }
        }
        
        #endregion

        #region .methods
        
        /// <summary>
        /// Init data of matrix.
        /// </summary>
        /// <param name="rows">the rows</param>
        /// <param name="columns">the columns</param>
        /// <exception cref="MatrixDotNetException">length matrix more than 6_561.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Initialize(byte rows,byte columns)
        {
            if (rows * columns >= Size)
            {
                throw new MatrixDotNetException("Size must be less than 6_561!!!");
            }
            _rows = rows;
            _columns = columns;
            _length = rows * columns;
            _isSquare = rows == columns;
            _isPrime = (rows & 0b01) == 0 && (columns & 0b01) == 0;
        }

        /// <summary>
        /// Adds two matrices.
        /// </summary>
        /// <param name="left">the left matrix.</param>
        /// <param name="right">the right matrix.</param>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException">matrices are not equal</exception>
        public static MatrixAsFixedBuffer AddByRef(ref MatrixAsFixedBuffer left,ref MatrixAsFixedBuffer right)
        {
            var m = left._rows;
            var n = left._columns;

            if(m != right._rows || n != right._columns)
                throw new MatrixDotNetException("Not Equal");
            
            var matrix = new MatrixAsFixedBuffer(m,n);

            if (Avx2.IsSupported)
            {
                int length = left._length;
                fixed(double* ptr3 = matrix.Data)
                fixed(double* ptr1 = left._array)
                fixed(double* ptr2 = right._array)
                {
                    int i = 0;
                    
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
            {
                var a1 = left.Data;
                var a2 = right.Data;
                var a3 = matrix.Data;

                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int num = i + m * j;
                        a3[num] = a2[num] + a1[num];
                    }
                }
            }
            
            return matrix;
        }

        /// <summary>
        /// Subtracts two matrices via AVX(if supported) or unsafe.
        /// </summary>
        /// <param name="left">the matrix with fixed buffer.</param>
        /// <param name="right">the matrix with fixed buffer.</param>
        /// <returns>new matrix from subtract two matrices. </returns>
        /// <exception cref="MatrixDotNetException">matrices not equal by size.</exception>
        public static MatrixAsFixedBuffer SubByRef(ref MatrixAsFixedBuffer left,ref MatrixAsFixedBuffer right)
        {
            var m = left._rows;
            var n = left._columns;

            if(m != right._rows || n != right._columns)
                throw new MatrixDotNetException("Not Equal");
            
            var matrix = new MatrixAsFixedBuffer(m,n);

            if (Avx2.IsSupported)
            {
                int length = left._length;
                fixed(double* ptr3 = matrix.Data)
                fixed(double* ptr1 = left._array)
                fixed(double* ptr2 = right._array)
                {
                    int i = 0;
                    
                    // Adds two matrices via AVX.
                    while (i < length - Vector256<double>.Count)
                    {
                        var vector1 = Avx.LoadVector256(ptr1 + i);
                        var vector2 = Avx.LoadVector256(ptr2 + i);
                        Avx.Store(ptr3 + i, Avx.Subtract(vector1,vector2));
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
            {
                var a1 = left.Data;
                var a2 = right.Data;
                var a3 = matrix.Data;

                // Adds two matrices.
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int num = i + m * j;
                        a3[num] = a2[num] - a1[num];
                    }
                }
            }
            
            return matrix;
        }

        
        /// <summary>
        /// Multiplies two matrices with fixed buffer.
        /// </summary>
        /// <param name="left">the left matrix.</param>
        /// <param name="right">the right matrix.</param>
        /// <returns>new matrix from multiply of two matrices</returns>
        /// <exception cref="MatrixDotNetException">
        /// throws exception if length columns of left matrix not equal length rows of right matrix
        /// </exception>
        public static MatrixAsFixedBuffer MulByRef(ref MatrixAsFixedBuffer left,ref MatrixAsFixedBuffer right)
        {
            if(left.Columns != right.Rows)
                throw new MatrixDotNetException("");
            
#if OS_LINUX
            return MulMatrix(ref left,ref right);
#endif
            
#if OS_WINDOWS
            if(Avx2.IsSupported)
            {
                   
            }
            else
            {
                return MulMatrix(ref left,ref right);
            }
#endif
            return new MatrixAsFixedBuffer();
        }

        /// <summary>
        /// Multiply two matrices which support LINUX,WINDOWS,MAC.
        /// </summary>
        /// <param name="left">the left matrix</param>
        /// <param name="right">the right matrix.</param>
        /// <returns></returns>
        private static MatrixAsFixedBuffer MulMatrix(ref MatrixAsFixedBuffer left,ref MatrixAsFixedBuffer right)
        {
            var m = left.Rows;
            var n = right.Columns;
            var K = left.Columns;
            var len1 = left.Length;
            MatrixAsFixedBuffer matrix = new MatrixAsFixedBuffer(m,n);
            fixed(double* pointer1 = left._array)
            fixed(double* pointer2 = right._array)
            fixed(double* pointer3 = matrix.Data)
            {
                Span<double> span1 = new Span<double>(pointer1,len1);
                
                for (int i = 0; i < m; i++)
                {
                    double* c = pointer3 + i * n;

                    for (int k = 0; k < K; k++)
                    {
                        double* b = pointer2 + k * n;
                        double a = span1[i * K + k];
                        for (int j = 0; j < n; j++)
                        {
                            c[j] += a * b[j];
                        }
                    }
                }
                return matrix;
            }
        }
        
        /// <summary>
        /// Gets column by row.
        /// </summary>
        /// <param name="column">the column.</param>
        /// <returns></returns>
        public Span<double> GetColumn(int column)
        {
            int m = _rows;
            fixed (double* ptr = _array)
            {
                var span2 = new Span<double>(ptr,m);
                var span = new Span<double>(ptr,_length);
                for (int i = 0; i < m; i++)
                {
                    span2[i] = span[column + _columns * i];
                }
                return span2;
            }
        }

        /// <summary>
        /// Sets column by index of column matrix.
        /// </summary>
        /// <param name="column">the index.</param>
        /// <param name="data">the data.</param>
        public void SetColumn(int column,Span<double> data)
        {
            int m = _rows;
            fixed (double* ptr = _array)
            {
                var span2 = new Span<double>(ptr,_length);
                for (int i = 0; i < m; i++)
                {
                    span2[column * _columns + i] = data[i];
                }
            }
        }
        
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            fixed (double* ptr = _array)
            {
                var span1 = new Span<double>(ptr,_length);
                for (int i = 0; i < _rows; i++)
                {
                    var span = span1.Slice(i * _columns,_columns);
                    foreach (var t in span)
                    {
                        builder.Append(t + " ");
                    }

                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }
        
        #endregion
        
        #region .indexators
        
        /// <summary>
        /// Gets value by ref.
        /// </summary>
        /// <param name="i">the row.</param>
        /// <param name="j">the column.</param>
        public ref double this[int i, int j] => ref _array[i * _columns + j];

        /// <summary>
        /// Gets arr of matrix.
        /// </summary>
        /// <param name="i">the row</param>
        public Span<double> this[int i]
        {
            get
            {
                fixed (double* ptr = _array)
                {
                    return new Span<double>(ptr,_length).Slice(i * _columns,_columns);
                }
            }

            set
            {
                fixed (double* ptr = _array)
                {
                    var span = new Span<double>(ptr,_length).Slice(i * _columns,_columns);
                    for (int j = 0; j < span.Length; j++)
                    {
                        span[j] = value[j];
                    }
                }
            }
        }
        #endregion
    }
}