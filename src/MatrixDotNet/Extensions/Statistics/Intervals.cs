using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Statistics.TableSetup;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Statistics
{
    /// <summary>
    /// Represents calculations any statistics operations where first two columns are <see cref="TableIntervals"/>.
    /// </summary>
    /// <typeparam name="T">unmanaged type</typeparam>
    public sealed class Intervals<T> : SetupIntervals<T> where T : unmanaged
    {
        #region .fields

        private int _indexFrequency;

        #endregion

        #region .properties

        private int Index => _indexFrequency;

        private int ColumnIndex => GetIndexColumn(TableIntervals.Ni);

        /// <summary>
        /// Gets max frequency.
        /// </summary>
        public T MaxFrequency => Matrix.MaxByColumn(ColumnIndex, out _indexFrequency);

        /// <summary>
        /// Gets Length modal interval.
        /// </summary>
        public T LengthModalInterval => MathUnsafe<T>.Sub(Matrix[_indexFrequency, 1], Matrix[_indexFrequency, 0]);

        /// <summary>
        /// Gets previous frequency before max frequency in column Ni.
        /// </summary>
        public T PreviousFrequency
        {
            get
            {
                if (_indexFrequency - 1 != -1)
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
                if (_indexFrequency + 1 < Matrix.Columns)
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
                if (_indexFrequency - 1 != -1)
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

        /// <summary>
        /// Initialize matrix with columns intervals.
        /// </summary>
        /// <param name="config">the configuration.</param>
        /// <exception cref="MatrixDotNetException">
        /// throws exception if columns of matrix less than 3,
        /// throws exception if length <see cref="ConfigIntervals{T}.Intervals"/> length more than matrix column. 
        /// </exception>
        public Intervals(ConfigIntervals<T> config) : base(config)
        {
            if (config.Matrix.Columns < 3)
                throw new MatrixDotNetException("Intervals matrix must be more or equal 3");

            if (config.Intervals.Length > config.Matrix.Columns - 2)
                throw new MatrixDotNetException(
                    "Too much columns for matrix. Note: first two column it is intervals.");

            ColumnNames[0] = TableIntervals.IntervalFirst.ToString();
            ColumnNames[1] = TableIntervals.IntervalSecond.ToString();
            ColumnNumber[0] = (int) TableIntervals.IntervalFirst;
            ColumnNumber[1] = (int) TableIntervals.IntervalSecond;

            if (ColumnNames.Length - 2 > Intervals.Length)
            {
                for (int i = Intervals.Length - 1; i < ColumnNumber.Length; i++)
                {
                    ColumnNames[i] = TableIntervals.Column.ToString();
                    ColumnNumber[i] = (int) TableIntervals.Column;
                }
            }

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
            var xi = GetIndexColumn(TableIntervals.Xi);
            var ni = GetIndexColumn(TableIntervals.Ni);
            T upper = default;
            for (var i = 0; i < Matrix.Rows; i++)
            {
                upper = MathUnsafe<T>.Add(upper, MathUnsafe<T>.Mul(Matrix[i, xi], Matrix[i, ni]));
            }

            return MathGeneric<T>.Divide(upper, VolumeStatisticalPopulation);
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
            var upper = MathUnsafe<T>.Sub(MaxFrequency, PreviousFrequency);
            var lower = MathUnsafe<T>.Add(MathUnsafe<T>.Sub(MaxFrequency, PreviousFrequency),
                MathUnsafe<T>.Sub(MaxFrequency, NextFrequency));


            return MathUnsafe<T>.Add(LowBoundModalInterval,
                MathUnsafe<T>.Mul(MathGeneric<T>.Divide(upper, lower), LengthModalInterval));
        }


        /// <summary>
        /// Gets MedianInterval
        /// </summary>
        /// <returns>Median interval.</returns>
        private T GetMedianInterval()
        {
            var upper = MathGeneric<double, T, T>.Sub(
                MathGeneric<T, double, double>.Multiply(VolumeStatisticalPopulation, 0.5),
                PreviousAccumulatedFrequency);

            var lower = MaxFrequency;
            return MathUnsafe<T>.Add(LowBoundModalInterval,
                MathUnsafe<T>.Mul(MathGeneric<T>.Divide(upper, lower), LengthModalInterval));
        }


        // calculate accumulated frequency.
        private T[] GetAccumulatedFrequency()
        {
            var accumulatedFreq = new T[Matrix.Rows];
            accumulatedFreq[0] = Matrix[0, ColumnIndex];
            for (int i = 0, k = 1; k < Matrix.Rows; i++, k++)
            {
                accumulatedFreq[k] = MathUnsafe<T>.Add(accumulatedFreq[i], Matrix[k, ColumnIndex]);
            }

            return accumulatedFreq;
        }

        #endregion
    }
}
