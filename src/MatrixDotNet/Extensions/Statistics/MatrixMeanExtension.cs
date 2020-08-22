using System;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Statistics
{
    public static partial class MatrixExtension
    {
        /// <summary>
        /// Gets mean value whole matrix.  
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>mean value.</returns>
        /// <exception cref="ArgumentException">matrix is not: float, double, decimal.</exception>
        public static T Mean<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!MathExtension.IsFloatingPoint<T>())
                throw new ArgumentException($"{matrix.GetType()} is not floating type.");
            
            
            return MathExtension.DivideBy(matrix.Sum(),matrix.Length);
        }

        
        /// <summary>
        /// Gets mean value by row.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="index">the row index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>mean value by row </returns>
        /// <exception cref="ArgumentException">matrix is not: float, double, decimal.</exception>
        public static T MeanByRow<T>(this Matrix<T> matrix,int index) where T : unmanaged
        {
            if (!MathExtension.IsFloatingPoint<T>())
                throw new ArgumentException($"{matrix.GetType()} is not floating type.");

            return MathExtension.DivideBy(matrix.SumByRow(index), matrix.Columns);
        }

        /// <summary>
        /// Gets mean value by column.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="index">the column index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>mean value by column.</returns>
        public static T MeanByColumn<T>(this Matrix<T> matrix, int index) where T : unmanaged
        {
            if (!MathExtension.IsFloatingPoint<T>())
                throw new ArgumentException($"{matrix.GetType()} is not floating type.");

            return MathExtension.DivideBy(matrix.SumByColumn(index), matrix.Rows);
        }
        
        /// <summary>
        /// Gets mean value by each row.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>mean value by each row.</returns>
        public static T[] MeanByRows<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!MathExtension.IsFloatingPoint<T>())
                throw new ArgumentException($"{matrix.GetType()} is not floating type.");

            var rows = matrix.Rows;
            var columns = matrix.Columns;
            var arr = new T[rows];
            
            for (int i = 0; i < rows; i++)
            {
                arr[i] = MathExtension.DivideBy(matrix.SumByRow(i), columns); 
            }

            return arr;
        }

        
        /// <summary>
        /// Gets mean value by each row.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>mean value by each row.</returns>
        public static T[] MeanByColumns<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!MathExtension.IsFloatingPoint<T>())
                throw new ArgumentException($"{matrix.GetType()} is not floating type.");
            
            var rows = matrix.Rows;
            var columns = matrix.Columns;
            var arr = new T[columns];
            
            for (int i = 0; i < columns; i++)
            {
                arr[i] = MathExtension.DivideBy(matrix.SumByColumn(i), rows); 
            }
            
            return arr;
        }
    }
}