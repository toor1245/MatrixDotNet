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

        /// <summary>
        /// Gets low bound modal interval.
        /// </summary>
        public T LowBoundModalInterval => Matrix[_indexFrequency, 0];

        /// <summary>
        /// Gets previous accumulated frequency
        /// </summary>
        public T PreviousAccumulatedFrequency
        {
            get
            {
                if(_indexFrequency - 1 != -1)
                    return AccumulatedFrequency[_indexFrequency - 1];

                return default;
            }
        }
        
        /// <summary>
        /// Gets accumulated frequency.
        /// </summary>
        /// <returns>Accumulated frequency.</returns>
        public T[] AccumulatedFrequency => GetAccumulatedFrequency();

        /// <summary>
        /// Gets modal interval.
        /// </summary>
        public T ModalInterval { get; }

        /// <summary>
        /// Gets median interval.
        /// </summary>
        public T MedianInterval { get; }
        
        /// <summary>
        /// Gets volume of the statistical population
        /// </summary>
        public T VolumeStatisticalPopulation => Matrix.SumByColumn(ColumnIndex);
        

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

            ModalInterval = GetModalInterval();
            MedianInterval = GetMedianInterval();
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
            for (var i = 0; i < Matrix.Rows; i++)
            {
                upper = MathExtension.Add(upper,MathExtension.Multiply(Matrix[i,xi],Matrix[i,ni]));
            }

            return MathExtension.Divide(upper, VolumeStatisticalPopulation);
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
        private T GetModalInterval()
        {
            var upper = MathExtension.Sub(MaxFrequency, PreviousFrequency);
            var lower = MathExtension.Add(MathExtension.Sub(MaxFrequency, PreviousFrequency),
                MathExtension.Sub(MaxFrequency,NextFrequency));
            
            
            return MathExtension.Add(LowBoundModalInterval,
                MathExtension.Multiply(MathExtension.Divide(upper, lower), LengthModalInterval));
        }

        
        /// <summary>
        /// Gets MedianInterval
        /// </summary>
        /// <returns>Median interval.</returns>
        private T GetMedianInterval()
        {
            var upper = MathExtension.Sub(MathExtension.MultiplyBy(VolumeStatisticalPopulation, 0.5),
                PreviousAccumulatedFrequency);

            var lower = MaxFrequency;
            return MathExtension.Add(LowBoundModalInterval,
                MathExtension.Multiply(MathExtension.Divide(upper, lower), LengthModalInterval));
        }
        
        
        // calculate accumulated frequency.
        private T[] GetAccumulatedFrequency()
        {
            var accumulatedFreq = new T[Matrix.Rows];
            accumulatedFreq[0] = Matrix[0,ColumnIndex];
            for (int i = 0,k = 1; k < Matrix.Rows; i++,k++)
            {
                accumulatedFreq[k] = MathExtension.Add(accumulatedFreq[i],Matrix[k,ColumnIndex]);
            }

            return accumulatedFreq;
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