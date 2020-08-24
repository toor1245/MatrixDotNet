using System;
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
        #region .fields
        
        
        private int _indexFrequency;
        
        
        #endregion
        
        #region .properties
        
        private int Index => _indexFrequency;

        private int ColumnIndex => FindColumn(TableIntervals.Ni);
        
        /// <summary>
        /// Gets max frequency.
        /// </summary>
        public T MaxFrequency => Matrix.MaxByColumn(ColumnIndex,out _indexFrequency);
        
        /// <summary>
        /// Gets Length modal interval.
        /// </summary>
        public T LengthModalInterval => MathExtension.Sub(Matrix[_indexFrequency,1], Matrix[_indexFrequency,0]);

        /// <summary>
        /// Gets previous frequency before max frequency in column Ni.
        /// </summary>
        public T PreviousFrequency
        {
            get
            {
                if(_indexFrequency - 1 != -1)
                    return Matrix[_indexFrequency - 1, ColumnIndex];

                return default;
            }
        }

        /// <summary>
        /// Gets next frequency after max frequency in column Ni. 
        /// </summary>
        public T NextFrequency
        {
            get
            {
                if(_indexFrequency + 1 < Matrix.Columns)
                    return Matrix[_indexFrequency + 1, ColumnIndex];

                return default;
            }
        }

        public T LowBoundModalInterval => Matrix[_indexFrequency, 0];

        #endregion
        
        #region .ctor
        
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
            ColumnNumber[1] = (int)TableIntervals.IntervalSecond;
            
            if (!IsCorrectInterval())
            {
                throw new MatrixDotNetException("Not correct intervals in second interval contains " +
                                                "value which less first interval");
            }
        }
        
        #endregion
        
        #region .methods
        
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

        /// <summary>
        /// Gets modal interval.
        /// </summary>
        /// <example>
        ///                        MaxFreq - PrevFreq
        ///  M0 = x0 + -------------------------------------------- * h
        ///            (MaxFreq - PrevFreq) + (MaxFreq -  NextFreq)
        /// </example>
        /// <returns>modal interval.</returns>
        public T GetModalInterval()
        {
            T upper = MathExtension.Sub(MaxFrequency, PreviousFrequency);
            T lower = MathExtension.Add(MathExtension.Sub(MaxFrequency, PreviousFrequency),
                MathExtension.Sub(MaxFrequency,NextFrequency));
            
            
            return MathExtension.Add(LowBoundModalInterval,
                MathExtension.Multiply(MathExtension.Divide(upper, lower), LengthModalInterval));
        }
        
        /// <summary>
        /// Checks matrix on correct intervals. 
        /// </summary>
        /// <returns><see cref="Boolean"/></returns>
        private bool IsCorrectInterval()
        {
            var first = FindColumn(TableIntervals.IntervalFirst);
            var second = FindColumn(TableIntervals.IntervalSecond);

                for (int i = 0; i < Matrix.Rows; i++)
                {
                    if (MathExtension.GreaterThan(Matrix[i,first], Matrix[i,second]))
                        return false;

                }
                
            return true;
        }
        
        #endregion
    }
}