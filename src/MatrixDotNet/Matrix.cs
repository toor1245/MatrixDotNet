using System;
using System.Linq;
using System.Text;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions;

namespace MatrixDotNet
{
    public class Matrix<T> : ICloneable
        where T : unmanaged
    {
        #region Properties

        internal T[,] _Matrix { get; private set; }

        public long Length => _Matrix.Length;

        public int Rows => _Matrix.GetLength(0);

        public int Columns => _Matrix.GetLength(1);

        #endregion

        #region Indexators

        public T this[uint i, uint j]
        {
            get
            {
                if (!IsRange(i, j))
                    throw new IndexOutOfRangeException();

                return _Matrix[i, j];
            }
            set
            {
                if (!IsRange(i, j))
                    throw new ArgumentException();

                _Matrix[i, j] = value;
            }
        }
        
        public T[] this[uint i]
        {
            get => this.GetRow(i);
            set
            {
                if (!IsRange(i))
                    throw new IndexOutOfRangeException();
                
                for (uint j = 0; j < Columns; j++)
                {
                    this[i, j] = value[j];
                }
            }
        }
        
        #endregion
        
        #region Ctor
        
        public Matrix(T[,] matrix)
        {
            _Matrix = new T[matrix.GetLength(0),matrix.GetLength(1)];
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    _Matrix[i,j] = matrix[i,j];
                }
            }
        }

        public Matrix(int row,int col)
        {
            _Matrix = new T[row,col];
        }
        
        #endregion

        #region OverLoad operator

        public static Matrix<T> operator +(Matrix<T> left, Matrix<T> right)
        {
            if (left.Length != right.Length)
            {
                throw new MatrixDotNetException(
                    $"matrix {nameof(left)} length: {left.Length} != matrix {nameof(right)}  length: {right.Length}");
            }
                
            Matrix<T> matrix = new Matrix<T>(left.Rows,right.Columns);

            for (uint i = 0; i < left.Rows; i++)
            {
                for (uint j = 0; j < left.Columns; j++)
                {
                    matrix[i, j] = MathExtension.Add(left[i,j],right[i,j]);
                }
            }

            return matrix;
        } 
        
        public static Matrix<T> operator -(Matrix<T> left, Matrix<T> right)
        {
            if (left.Length != right.Length)
            {
                throw new MatrixDotNetException(
                    $"matrix {nameof(left)} length: {left.Length} != matrix {nameof(right)}  length: {right.Length}");
            }

            Matrix<T> matrix = new Matrix<T>(left.Rows,right.Columns);

            for (uint i = 0; i < left.Rows; i++)
            {
                for (uint j = 0; j < left.Columns; j++)
                {
                    matrix[i, j] = MathExtension.Sub(left[i,j],right[i,j]);
                }
            }

            return matrix;
        } 
        
        public static Matrix<T> operator *(Matrix<T> left, Matrix<T> right)
        {
            if (left.Columns != right.Rows)
            {
                throw new MatrixDotNetException(
                    $"matrix {nameof(left)} columns length must be equal matrix {nameof(right)} rows length");
            }

            Matrix<T> matrix = new Matrix<T>(left.Rows,right.Columns);

            for (uint i = 0; i < left.Rows; i++)
            {
                for (uint j = 0; j < right.Columns; j++)
                {
                    for (uint k = 0; k < left.Columns; k++)
                    {
                        // matrix[i,j] += left[i,k] * right[k,j]; 
                        matrix[i, j] = MathExtension
                            .Add(matrix[i,j],MathExtension.Multiply(left[i,k],right[k,j]));
                    }
                }
            }

            return matrix;
        } 
        
        public static Matrix<T> operator *(Matrix<T> matrix, T digit)
        {
            
            for (uint i = 0; i < matrix.Rows; i++)
            {
                for (uint j = 0; j < matrix.Columns; j++)
                {
                    // matrix1[i,j] = matrix[i,j] * matrix
                    matrix[i, j] = MathExtension.Multiply(matrix[i,j],digit);
                }
            }
            
            return matrix;
        }
        
        public static Matrix<T> operator *(T digit, Matrix<T> matrix)
        {
            for (uint i = 0; i < matrix.Rows; i++)
            {
                for (uint j = 0; j < matrix.Columns; j++)
                {
                    // matrix1[i,j] = matrix[i,j] * matrix
                    matrix[i, j] = MathExtension.Multiply(matrix[i,j],digit);
                }
            }
            
            return matrix;
        }
        
        public static Matrix<T> operator /(Matrix<T> matrix, T digit)
        {
            for (uint i = 0; i < matrix.Rows; i++)
            {
                for (uint j = 0; j < matrix.Columns; j++)
                {
                    // matrix1[i,j] = matrix[i,j] * matrix
                    matrix[i, j] = MathExtension.Divide(matrix[i,j],digit);
                }
            }
            
            return matrix;
        }
        
        public static Matrix<T> operator /(T digit,Matrix<T> matrix)
        {
            for (uint i = 0; i < matrix.Rows; i++)
            {
                for (uint j = 0; j < matrix.Columns; j++)
                {
                    // matrix1[i,j] = matrix[i,j] * matrix
                    matrix[i, j] = MathExtension.Divide(matrix[i,j],digit);
                }
            }
            return matrix;
        }
        
        #endregion
        
        private bool IsRange(uint i,uint j)
        {
            if (i > _Matrix.GetLength(0) && j > _Matrix.GetLength(1))
                return false;
            
            return true;
        }

        private bool IsRange(uint i)
        {
            if (i > _Matrix.GetLength(0))
                return false;

            return true;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            
            for (uint i = 0; i < Rows; i++)
            {
                for (uint j = 0; j < Columns; j++)
                {
                    builder.Append(this[i,j] + " ");
                }

                builder.Append("\n");
            }
            
            return builder.ToString();
        }

        public object Clone()
        {
            Matrix<T> matrix = new Matrix<T>(Rows,Columns);
            for (uint i = 0; i < Rows; i++)
            {
                for (uint j = 0; j < Columns; j++)
                {
                    matrix[i, j] = this[i, j];
                }
            }

            return matrix;
        }
        
        public override bool Equals(object obj)
        {
            if(!(obj is Matrix<T>))
                throw new ArgumentException();
            
            var t = (Matrix<T>) obj;
            var count = 0;
            
            for (uint i = 0; i < t.Rows; i++)
            {
                for (uint j = 0; j < t.Columns; j++)
                {
                    if (this[i, j].Equals(t[i, j])) count++;
                }
            }
            
            return count == Length;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        
        public bool IsSquare()
        {
            return Rows == Columns;
        }
    }
}