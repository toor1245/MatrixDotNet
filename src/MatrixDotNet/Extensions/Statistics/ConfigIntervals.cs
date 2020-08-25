namespace MatrixDotNet.Extensions.Statistics
{
    /// <summary>
    /// Represents configuration for <c>Intervals</c>
    /// </summary>
    /// <typeparam name="T">unmanaged type</typeparam>
    public class ConfigIntervals<T> where T : unmanaged
    {
        /// <summary>
        /// Gets intervals for matrix columns.
        /// </summary>
        public TableIntervals[] Intervals { get; }

        /// <summary>
        /// Gets matrix.
        /// </summary>
        public Matrix<T> Matrix { get;}

        
        /// <summary>
        /// Initialize matrix and table.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="intervals">the intervals.</param>
        public ConfigIntervals(Matrix<T> matrix,TableIntervals[] intervals)
        {
            Intervals = intervals;
            Matrix = matrix;
        }
    }
}