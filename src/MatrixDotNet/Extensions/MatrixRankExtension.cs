using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Gets rank matrix.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException"></exception>
        internal static int GetRank<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException("matrix is null");

            Matrix<T> temp = matrix.Clone() as Matrix<T>;
            
            if(temp is null)
                throw new NullReferenceException("matrix is null");

            temp[0] = matrix[0];

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    for (int k = 1; k < matrix.Columns; k++)
                    {
                        T[] arr = matrix[k];
                        var element = MathExtension.Divide(arr[i],matrix[i, i]);
                        T[] arr2 = matrix[k].Mul(element);
                        temp[j] = Subtract(arr2,matrix[j]);
                    }
                }
            }
            return 0;
        }
        
        internal static int GetRank(this Matrix<double> matrix)
        {
            if (matrix is null)
                throw new NullReferenceException("matrix is null");

            Matrix<double> temp = matrix.Clone() as Matrix<double>;
            
            if(temp is null)
                throw new NullReferenceException("matrix is null");
            
            for (int k = 1; k < temp.Rows; k++)
            {
                
            }
            
            return 0;
        }

        private static T[] Mul<T>(this T[] arr,T num) where  T : unmanaged
        {
            T[] result = new T[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = MathExtension.Multiply(arr[i], num);
            }
            
            return result;
        }

        private static T[] Subtract<T>(T[] arr1, T[] arr2) where T : unmanaged
        {
            T[] result = new T[arr1.Length];
            for (int i = 0; i < arr1.Length; i++)
            {
                result[i] = MathExtension.Sub(arr1[i], arr2[i]);
            }

            return result;
        }
    }
}