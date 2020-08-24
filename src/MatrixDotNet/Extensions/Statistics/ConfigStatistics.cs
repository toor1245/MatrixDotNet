using System;

namespace MatrixDotNet.Extensions.Statistics
{
    public abstract class ConfigStatistics<T> where T : unmanaged
    {
        protected Matrix<T> Matrix { get; }
        protected string[] ColumnNames { get; }
        protected int[] ColumnNumber { get; }
        private TableIntervals[] Tables { get; }

        protected ConfigStatistics(Matrix<T> matrix,TableIntervals[] tables,int index)
        {
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
        
        protected int FindColumn(TableIntervals table)
        {
            var find = (int)table;
            
            for (int i = 0; i < ColumnNumber.Length; i++)
            {
                if (find == ColumnNumber[i])
                    return ColumnNumber[i];
            }

            return -1;
        }
        
    }
}