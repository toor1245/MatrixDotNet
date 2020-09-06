using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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
        public byte Rows;
        
        [FieldOffset(1)]
        public byte Columns;
        
        [FieldOffset(2)]
        public int Length;
        
        [FieldOffset(6)]
        private fixed double _array[Size];

        [FieldOffset(52494)]
        public bool IsSquare;

        [FieldOffset(52495)]
        public bool IsPrime;
        
        #endregion
        
        
        #region .properties
        
        /// <summary>
        /// Gets data of matrix as span.
        /// </summary>
        public Span<double> Data
        {
            get
            {
                fixed (double* ptr = _array)
                {
                    return new Span<double>(ptr,Length);
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

        public static implicit operator MatrixAsFixedBuffer(double[,] matrix)
        {
            return new MatrixAsFixedBuffer(matrix);
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
                
                for (int i = 0; i < Length; i++)
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
            Rows = rows;
            Columns = columns;
            Length = rows * columns;
            IsSquare = rows == columns;
            IsPrime = (rows & 0b01) == 0 && (columns & 0b01) == 0;
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
            var m = left.Rows;
            var n = left.Columns;
            
            if(m != right.Rows || n != right.Columns)
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
            int m = Rows;
            double* array = stackalloc double[m];
            fixed (double* ptr = _array)
            {
                Span<double> span = new Span<double>(ptr,Length);
                for (int i = 0; i < m; i++)
                {
                    array[i] = span[column + Columns * i];
                }
            }

            return new Span<double>(array,m);
        }
        
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            fixed (double* ptr = _array)
            {
                var span1 = new Span<double>(ptr,Length);
                for (int i = 0; i < Rows; i++)
                {
                    var span = span1.Slice(i * Columns,Columns);
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
        public ref double this[int i, int j] => ref _array[i + Columns * j];

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
                    return new Span<double>(ptr,Length).Slice(i * Columns,Columns);
                }
            }

            set
            {
                fixed (double* ptr = _array)
                {
                    var span = new Span<double>(ptr,Length).Slice(i * Columns,Columns);
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