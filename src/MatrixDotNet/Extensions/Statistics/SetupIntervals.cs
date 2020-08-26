using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Statistics
{
    /// <summary>
    /// Represents store data such as matrix, tables.
    /// Share interval operations.
    /// </summary>
    public abstract class SetupIntervals<T> : Setup<T>  where T : unmanaged
    {
        /// <summary>
        /// Gets table intervals.
        /// </summary>
        protected TableIntervals[] Intervals { get; }
        
        /// <summary>
        /// Initialize settings for <c>Intervals</c>
        /// </summary>
        /// <param name="config">configuration</param>
        protected SetupIntervals(ConfigIntervals<T> config) : base(config.Matrix)
        {
            Intervals = config.Intervals;
            
            for (int i = 0, k = 2; i < Intervals.Length; i++,k++)
            {
                ColumnNames[k] = Intervals[i].ToString();
                ColumnNumber[k] = (int)Intervals[i];
            }
            
        }

        /// <summary>
        /// Gets index column in matrix.
        /// </summary>
        /// <param name="tableIntervals">the table</param>
        /// <returns></returns>
        protected int GetIndexColumn(TableIntervals tableIntervals)
        {
            return FindColumn((int) tableIntervals);
        }
        
        
        /// <summary>
        /// Checks matrix on correct intervals. 
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        protected bool IsCorrectInterval()
        {
            var first = GetIndexColumn(TableIntervals.IntervalFirst);
            var second = GetIndexColumn(TableIntervals.IntervalSecond);

            for (int i = 0; i < Matrix.Rows; i++)
            {
                if (MathExtension.GreaterThan(Matrix[i,first], Matrix[i,second]))
                    return false;

            }
            return true;
        }
    }
}