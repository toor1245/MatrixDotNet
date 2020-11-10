using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Math;


namespace MatrixDotNet.Extensions.Statistics.TableSetup
{
    /// <summary>
    /// Represents store data such as matrix, tables.
    /// Share operations for all statistic classes.
    /// </summary>
    public abstract class Setup<T> where T : unmanaged
    {
        protected Matrix<T> Matrix { get; }
        protected string[] ColumnNames { get; }
        protected int[] ColumnNumber { get; }

        /// <summary>
        /// Initialize 
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <exception cref="ArgumentException"></exception>
        protected Setup(Matrix<T> matrix)
        {
            if (!MathGeneric.IsFloatingPoint<T>()) 
                throw new ArgumentException("Matrix must be floating type.");
            
            ColumnNames = new string[matrix.Columns];
            ColumnNumber = new int[matrix.Columns];
            
            Matrix = matrix;
            
        }
        
        /// <summary>
        /// Finds column index in matrix.
        /// </summary>
        /// <param name="nameIndex">index of column</param>
        /// <returns>index column</returns>
        /// <exception cref="MatrixDotNetException">Index not found.</exception>
        protected int FindColumn(int nameIndex)
        {
            var find = nameIndex;
            
            for (int i = 0; i < ColumnNumber.Length; i++)
            {
                if (find == ColumnNumber[i])
                    return i;
            }
            
            throw new MatrixDotNetException("Not Found value");
        }
    }
}