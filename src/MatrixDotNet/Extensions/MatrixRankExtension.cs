using System;
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
        private static double GetRank<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (matrix is null)
                throw new MatrixDotNetException("matrix is null");
            
            var res = matrix.Clone() as Matrix<T>;
            
            if (res is null)
                throw new MatrixDotNetException("matrix is null");
            
            for (int i = 0; i < res.Rows; i++)
            {
                for (int j = i + 1; j < res.Columns; j++)
                { 
                    matrix[j, i] = default;
                }
            }
            
            return res.GetUnmanagedDeterminate();
        }
    }
}