using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Gets rank matrix.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException"></exception>
        private static int GetRank<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (matrix is null)
                throw new MatrixDotNetException("matrix is null");

            Matrix<T> temp = matrix.Clone() as Matrix<T>;

            int rank = 0;
            for (int i = 0, k = 0; i < matrix.Rows; i++)
            {
                for (int j = i + 1; j < matrix.Columns; j++)
                {
                    var element = matrix[i, 0];
                    T[] arr = matrix[k].Mul(element);
                }
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
    }
}