using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Statistics
{
    /// <summary>
    /// Represents store data such as matrix, tables.
    /// Share operations for all statistic classes.
    /// </summary>
    public abstract class ConfigStatistics<T> where T : unmanaged
    {
        protected Matrix<T> Matrix { get; }
        protected string[] ColumnNames { get; }
        protected int[] ColumnNumber { get; }
        private TableIntervals[] Tables { get; }

        /// <summary>
        /// Initialize matrix and tables.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="tables">the table.</param>
        /// <param name="index">the index which starts fill columns data.</param>
        protected ConfigStatistics(Matrix<T> matrix,TableIntervals[] tables,int index)
        {
            if (!MathExtension.IsFloatingPoint<T>()) 
                throw new ArgumentException("Matrix must be floating type."); 
            
            Matrix = matrix;
            Tables = tables;
            ColumnNames = new string[matrix.Columns];
            ColumnNumber = new int[matrix.Columns];
            
            for (int i = 0, k = index; i < Tables.Length; i++,k++)
            {
                ColumnNames[k] = Tables[i].ToString();
                ColumnNumber[k] = (int)Tables[i];
            }
        }
        

        /// <summary>
        /// Finds index of TableIntervals in column data.
        /// </summary>
        /// <param name="table">table which find in column data</param>
        /// <returns>Index in column table</returns>
        /// <exception cref="MatrixDotNetException">throws exception if not found index.</exception>
        protected int FindColumn(TableIntervals table)
        {
            var find = (int)table;
            
            for (int i = 0; i < ColumnNumber.Length; i++)
            {
                if (find == ColumnNumber[i])
                    return ColumnNumber[i];
            }
            
            throw new MatrixDotNetException("Value not found");
        }
        
    }
}