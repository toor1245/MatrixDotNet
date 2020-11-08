using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Math;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

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
        public bool IsSymmetric => this == this.Transpose();

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
        public unsafe T[] this[int i,State dimension]
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
                        Unsafe.CopyBlock(ptr1 + i * Columns,ptr2,(uint) (sizeof(T) * value.Length));
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
        
        public T this[int m, int n,State dimension]
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
                Unsafe.CopyBlock(ptr1,ptr2,(uint) (sizeof(T) * Length));
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
        /// Creates matrix with init constant value.
        /// </summary>
        /// <param name="row">row</param>
        /// <param name="col">col</param>
        /// <param name="value">constant</param>
        public Matrix(int row, int col, T value)
        {
            Rows = row;
            Columns = col;
            _Matrix = new T[row * col];
            Array.Fill(_Matrix,value);
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
        public static Matrix<T> operator +(Matrix<T> left, Matrix<T> right)
        {
            if (left.Rows != right.Rows || left.Columns != right.Columns)
            {
                throw new MatrixDotNetException(
                    $"matrix {nameof(left)} length: {left.Length} != matrix {nameof(right)}  length: {right.Length}");
            }
                
            var addFunc = MathGeneric<T>.GetAddFunc();

            Matrix<T> matrix = new Matrix<T>(left.Rows,right.Columns);

            for (int i = 0; i < left.Rows; i++)
            {
                for (int j = 0; j < left.Columns; j++)
                {
                    matrix[i, j] = addFunc(left[i,j], right[i,j]);
                }
            }

            return matrix;
        }
        
        public static Matrix<T> Plus(Matrix<T> left, Matrix<T> right)
        {
            if (left.Rows != right.Rows || left.Columns != right.Columns)
            {
                throw new MatrixDotNetException(
                    $"matrix {nameof(left)} length: {left.Length} != matrix {nameof(right)}  length: {right.Length}");
            }
            
            Matrix<T> matrix = new Matrix<T>(left.Rows,right.Columns);

            for (int i = 0; i < left.Rows; i++)
            {
                for (int j = 0; j < left.Columns; j++)
                {
                    matrix[i, j] = MathUnsafe<T>.Add(left[i,j], right[i,j]);
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
        public static Matrix<T> operator -(Matrix<T> left, Matrix<T> right)
        {
            if (left.Rows != right.Rows || left.Columns != right.Columns)
            {
                throw new MatrixDotNetException(
                    $"matrix {nameof(left)} length: {left.Length} != matrix {nameof(right)}  length: {right.Length}");
            }

            var subFunc = MathGeneric<T,T,T>.GetSubFunc();

            Matrix<T> matrix = new Matrix<T>(left.Rows,right.Columns);

            for (int i = 0; i < left.Rows; i++)
            {
                for (int j = 0; j < left.Columns; j++)
                {
                    matrix[i, j] = subFunc(left[i,j],right[i,j]);
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
        public static Matrix<T> operator *(Matrix<T> left, Matrix<T> right)
        {
            if (left.Columns != right.Rows)
            {
                throw new MatrixDotNetException(
                    $"matrix {nameof(left)} columns length must be equal matrix {nameof(right)} rows length");
            }

            var addFunc = MathGeneric<T,T,T>.GetAddFunc();
            var multiplyFunc = MathGeneric<T,T,T>.GetMultiplyFunc();

            Matrix<T> matrix = new Matrix<T>(left.Rows,right.Columns);

            for (int i = 0; i < left.Rows; i++)
            {
                for (int j = 0; j < right.Columns; j++)
                {
                    for (int k = 0; k < left.Columns; k++)
                    {
                        // matrix[i,j] = matrix[i,j] + left[i,k] * right[k,j]; 
                        matrix[i, j] = addFunc(matrix[i,j], multiplyFunc(left[i,k],right[k,j]));
                    }
                }
            }

            return matrix;
        } 
        
        /// <summary>
        /// Multiply operation matrix on digit right side.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="digit">digit.</param>
        /// <returns><see cref="Matrix{T}"/></returns>
        public static Matrix<T> operator *(Matrix<T> matrix, T digit)
        {
            var multiplyFunc = MathGeneric<T,T,T>.GetMultiplyFunc();
            Matrix<T> result = new Matrix<T>(matrix.Rows,matrix.Columns);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = multiplyFunc(matrix[i,j],digit);
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

            Matrix<T> result = new Matrix<T>(matrix.Rows,matrix.Columns);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = divideFunc(matrix[i,j],digit);
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

            Matrix<T> result = new Matrix<T>(matrix.Rows,matrix.Columns);
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    result[i, j] = divideFunc(digit,matrix[i,j]);
                }
            }

            return result;
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
                throw new MatrixDotNetException("not equals");
            }
            T[] res= new T[array.Length];
            for (int i = 0; i < matrix.Rows; i++)
            {
                T sum = default;
                for (int j = 0; j < matrix.Columns; j++)
                {
                    sum = MathGeneric<T,T,T>.Add(sum, MathGeneric<T,T,T>.Multiply(matrix[j, i], array[j]));
                }
                
                if(i == array.Length)
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
        public static unsafe T[] operator *(Matrix<T> matrix,T[] array)
        {
            if (matrix.Rows != array.Length)
            {
                throw new MatrixDotNetException("not equals");
            }

            int m = matrix.Rows;
            int n = 1;
            int K = matrix.Columns;
            
            T[] result = new T[m];
            fixed(T* ptr1 = matrix._Matrix)
            fixed(T* ptr2 = array)
            fixed(T* ptr3 = result)
            {
                Span<T> span1 = new Span<T>(ptr1,matrix.Length);
                
                for (int i = 0; i < m; i++)
                {
                    T* c = ptr3 + i * n;

                    for (int k = 0; k < K; k++)
                    {
                        T* b = ptr2 + k * n;
                        T a = span1[i * K + k];
                        for (int j = 0; j < n; j++)
                        {
                            c[j] = MathUnsafe<T>.Add(c[j],MathUnsafe<T>.Mul(a,b[j]));
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
        public static Vector<T> operator *(Vector<T> vector,Matrix<T> matrix)
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
        public static Vector<T> operator *(Matrix<T> matrix,Vector<T> vector)
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
            if(left is null || right is null)
                throw new NullReferenceException();
            
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
            return MatrixExtension.Output(this,builder);
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
            Matrix<T> res = new Matrix<T>(this.Rows,this.Columns);
            
            fixed (T* srcPtr = _Matrix)
            fixed (T* destPtr = res.GetArray())
            {
                Unsafe.CopyBlock(destPtr, srcPtr, (uint)(this.Length * sizeof(T)));
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
            return matrix.ToMatrix();
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

            Matrix<T> result = new Matrix<T>(matrix.Length, columns);

            fixed (T* dstPtr = result._Matrix)
            {
                for (int i = 0; i < matrix.Length; i++)
                {
                    fixed (T* srcPrt = matrix[i])
                    {
                        Unsafe.CopyBlock(dstPtr + i * result.Columns, srcPrt, (uint)(sizeof(T)*matrix[i].Length));
                    }
                }
            }
            return result;
        }

        public IEnumerator<T> GetEnumerator() =>
            new Enumerator(this);


        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            Matrix<T> matrix = obj as Matrix<T>;

            if (matrix is null || Rows != matrix.Rows || Columns != matrix.Columns)
                return false;

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    if (!this[i, j].Equals(matrix[i, j]))
                        return false;
                }
            }

            return true;
        }

        /// <inheritdoc />
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
            { }
        } 
        
        #endregion
    }
}