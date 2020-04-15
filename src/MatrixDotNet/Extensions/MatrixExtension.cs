using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions
{
    public static class MatrixExtension
    {
        public static T[] GetRow<T>(this Matrix<T> matrix,uint index) 
            where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();

            if (index > matrix._Matrix.GetLength(1))
                throw new IndexOutOfRangeException();
            
            T[] array = new T[matrix._Matrix.GetLength(1)];

            for (uint j = 0; j < matrix._Matrix.GetLength(1); j++)
            {
                array[j] = matrix[index,j];
            }
            
            return array;
        }
        
        public static T[] GetColumn<T>(this Matrix<T> matrix, uint index)
            where T : unmanaged
        {
            if(matrix is null)
                throw new ArgumentNullException();
            
            T[] array = new T[matrix._Matrix.GetLength(0)];
            
            if (index > matrix._Matrix.GetLength(0))
                throw new IndexOutOfRangeException();

            for (uint j = 0; j < matrix._Matrix.GetLength(0); j++)
            {
                array[j] = matrix[j,index];
            }
            
            return array;
        }

        public static Matrix<T> Transport<T>(this Matrix<T> matrix) 
            where T : unmanaged
        {
            Matrix<T> transport = new Matrix<T>(matrix.Columns,matrix.Rows);
            for (uint i = 0; i < transport.Rows; i++)
            {
                for (uint j = 0; j < transport.Columns; j++)
                {
                    transport[i, j] = matrix[j, i];
                }
            }

            return transport;

        }

        public static T[,] ToPrimitive<T>(this Matrix<T> matrix) 
            where T : unmanaged
        {
            T[,] matrix1 = new T[matrix.Rows,matrix.Columns];
            
            for (uint i = 0; i < matrix.Rows; i++)
            {
                for (uint j = 0; j < matrix.Columns; j++)
                {
                    matrix1[i, j] = matrix[i, j];
                }
            }
            return matrix1;
        }
        
        public static Matrix<T> ToMatrixDotNet<T>(this T[,] matrix) 
            where T : unmanaged
        {
            Matrix<T> matrix1 = new Matrix<T>(matrix.GetLength(0),matrix.GetLength(1));
            
            for (uint i = 0; i < matrix1.Rows; i++)
            {
                for (uint j = 0; j < matrix1.Columns; j++)
                {
                    matrix1[i, j] = matrix[i, j];
                }
            }
            return matrix1;
        }
        
        private static Matrix<T> GetMinor<T>( this Matrix<T> matrix,int n)
            where T : unmanaged
        {
            T[,] result = new T[matrix.Rows - 1, matrix.Rows - 1];

            for (uint i = 1; i < matrix.Rows; i++)
            {
                for (uint j = 0, col = 0; j < matrix.Columns; j++)
                {
                    if (j == n)
                        continue;
                    result[i - 1, col] = matrix[i, j];
                    col++;
                }
            }
            return result.ToMatrixDotNet();
        }

        public static double GetDeterminate<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            if (!matrix.IsSquare())
            {
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            double[,] temp = matrix.ToPrimitive() as double[,];
            
            if(temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }
            
            double sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<T> minr = GetMinor(matrix, i);
                result += sign * temp[0, i] * GetDeterminate(minr);
                sign = -sign;
            }

            return result;
        }

        public static double[] KramerSolve<T>(this Matrix<T> matrix,T[] arr)
            where T: unmanaged
        {
            if (matrix.Rows != arr.Length)
                 throw new MatrixDotNetException(
                     "Rows quantity matrix not equal array quantity",nameof(matrix),nameof(arr));
            
            double det = matrix.GetDeterminate();
            Matrix<T> temp;
            double[] result = new double[matrix.Columns];
            
            for (uint i = 0; i < matrix.Columns; i++)
            {
                temp = matrix.Clone() as Matrix<T>;
                for (uint j = 0; j < matrix.Rows; j++)
                {
                    temp[j, i] = arr[j];
                }
                result[i] = temp.GetDeterminate() / det;
            }
            return result;
        }
    }
}