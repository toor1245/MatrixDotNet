using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Statistics
{
    public class ConfigIntervals<T> : ConfigStatistics<T> where T : unmanaged
    {
        private string[] ColumnNames { get; }
        private Table[] _tables;
        

        public ConfigIntervals(Matrix<T> matrix,Table[] columns,string description = "") : base(matrix,columns,description,columns.Length - 2)
        {
            if(matrix.Columns < 3)
                throw new MatrixDotNetException("Intervals matrix must be more or equal 3");
            
            if(columns.Length != matrix.Columns)
                throw new MatrixDotNetException($"{nameof(columns)}: " +
                                                $"{columns.Length} length after intervals (2)" +
                                                $" != matrix columns: {matrix.Length}");
            
            _tables = columns;
            
            ColumnNames = new string[columns.Length];
            
            ColumnNames[0] = "Intervals";
            ColumnNames[1] = "Intervals";

            
            for (int i = 0,k = 2; k < _tables.Length; i++,k++)
            {
                ColumnNames[k] = _tables[i].ToString();
            }
        }

        public T IntervalRowMean()
        {
            int xi = FindColumn(Table.Xi) - 1;
            int ni = FindColumn(Table.Ni) - 1;
            T upper = default;
            T sumNi = default;
            for (int i = 0; i < _matrix.Rows; i++)
            {
                T temp = _matrix[ni, i];
                sumNi = MathExtension.Add(sumNi, temp);
                upper = MathExtension.Add(upper, MathExtension.Multiply(_matrix[xi, i],temp));
            }

            return MathExtension.Divide(upper, sumNi);
        }
        
        public int FindColumn(Table table)
        {
            int count = 0;
            int find = (int)table;
            for (int i = 0; i < ColumnNames.Length; i++)
            {
                count += (((((int)_tables[i] & find) - find) >> 31) ^ i) & i;
                Console.WriteLine(count);
            }
            return count;
        }
        
    }
}