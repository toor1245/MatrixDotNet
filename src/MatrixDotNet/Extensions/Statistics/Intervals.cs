using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Statistics
{
    /// <summary>
    /// Represents calculations any statistics operations where first two columns are <see cref="TableIntervals"/>.
    /// </summary>
    /// <typeparam name="T">unmanaged type</typeparam>
    public sealed class Intervals<T> : ConfigStatistics<T> where T : unmanaged
    {
        /// <summary>Creates intervals matrix.</summary>
        /// <para>First two columns initialize <see cref="TableIntervals"/>!!!</para>
        /// <param name="matrix">the matrix.</param>
        /// <param name="columns">the names of columns which must be less than matrix columns on 2 unit.</param>
        /// <exception cref="MatrixDotNetException">
        /// throws exception if matrix columns less than 3 and
        /// columns of matrix more than columns of table on 2 unit.
        /// </exception>
        public Intervals(Matrix<T> matrix,TableIntervals[] columns) : base(matrix,columns,2)
        {
            if(matrix.Columns < 3)
                throw new MatrixDotNetException("Intervals matrix must be more or equal 3");

            if (columns.Length > matrix.Columns - 2)
                throw new MatrixDotNetException("Too much columns for matrix. Note: first two column it is intervals.");
            

            ColumnNames[0] = TableIntervals.IntervalFirst.ToString();
            ColumnNames[1] = TableIntervals.IntervalSecond.ToString();
            ColumnNumber[0] = (int)TableIntervals.IntervalFirst;
            ColumnNumber[1] = (int) TableIntervals.IntervalSecond;
        }

        /// <summary>
        /// Gets interval row mean value.
        /// </summary>
        /// <returns>interval row mean value.</returns>
        public T GetIntervalRowMean()
        {
            var xi = FindColumn(TableIntervals.Xi);
            var ni = FindColumn(TableIntervals.Ni);
            T upper = default;
            T sumNi = default;
            for (var i = 0; i < Matrix.Rows; i++)
            {
                var temp = Matrix[i,ni];
                sumNi = MathExtension.Add(sumNi,temp);
                upper = MathExtension.Add(upper,MathExtension.Multiply(Matrix[i,xi],temp));
            }

            return MathExtension.Divide(upper, sumNi);
        }
        
    }
}