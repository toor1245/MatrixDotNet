using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        
        [FieldOffset(6)]
        internal fixed double _array[Size];

        [FieldOffset(52494)]
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
            return new MatrixAsFixedBuffer(matrix.GetMatrix());
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
        
        #endregion

        /// <summary>
        /// Init data of matrix.
        /// </summary>
        /// <param name="rows">the rows</param>
        /// <param name="columns">the columns</param>
        /// <exception cref="MatrixDotNetException">length matrix more than 6_561.</exception>
        #region .methods
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
            
            MatrixAsFixedBuffer matrix = new MatrixAsFixedBuffer(m,n);

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
            
            return matrix;
        }
        
        /// <summary>
        /// Gets column by row.
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public Span<double> GetColumn(int column)
        {
            int m = _rows;
            fixed (double* ptr = _array)
            {
                Span<double> span2 = new Span<double>(ptr,m);
                Span<double> span = new Span<double>(ptr,_length);
                for (int i = 0; i < m; i++)
                {
                    span2[i] = span[column + _columns * i];
                }
                return span2;
            }
        }

        public void SetColumn(int column,Span<double> data)
        {
            int m = _rows;
            fixed (double* ptr = _array)
            {
                Span<double> span2 = new Span<double>(ptr,_length);
                for (int i = 0; i < m; i++)
                {
                    span2[column + _columns * i] = data[i];
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