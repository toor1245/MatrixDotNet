using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Statistics.TableSetup;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Statistics
{
    /// <summary>
    /// Represents calculations any variations operations.
    /// </summary>
    /// <typeparam name="T">unmanaged type.</typeparam>
    public sealed class Variations<T> : SetupVariations<T> where T : unmanaged
    {
        /// <summary>
        /// Gets standard deviation
        /// </summary>
        public T StandardDeviation => MathGeneric<T>.Sqrt(GetSampleDispersion());

        /// <summary>
        /// Gets coefficient of variations.
        /// </summary>
        /// <returns>Coefficient of variations</returns>
        public T Coefficient => MathGeneric<T>.Divide(StandardDeviation, GetSampleMeanByTable(TableVariations.Xi));

        /// <summary>
        /// Checks on uniform Coefficient of variations.
        /// </summary>
        public bool IsUniform => 0.30.CompareTo(Coefficient) < 0;

        /// <summary>
        /// Gets corrected standard deviation.
        /// </summary>
        /// <remarks>
        /// Finds by formula: sqrt(s^2).
        /// </remarks>
        public T CorrectedStandardDeviation => MathGeneric<T>.Sqrt(GetCorrectedDispersion());

        /// <summary>
        /// Initialize configuration.
        /// </summary>
        /// <param name="variations">configuration</param>
        public Variations(ConfigVariations<T> variations) : base(variations)
        {
            var length = variations.Variations.Length;
            var n = Matrix.Columns;

            if (length > n)
                throw new MatrixDotNetException("Length variations more than matrix columns.");

            if (length >= n) return;

            for (int i = length; i < n; i++)
            {
                ColumnNames[i] = TableIntervals.Column.ToString();
                ColumnNumber[i] = (int)TableIntervals.Column;
            }

        }

        /// <summary>
        /// Gets <c>mean</c> value by column table. 
        /// </summary>
        /// <param name="table">the table</param>
        /// <returns>mean value by column table.</returns>
        public T GetSampleMeanByTable(TableVariations table)
        {
            if (table != TableVariations.Column)
                throw new MatrixDotNetException("TableVariations.Column not allow");

            return Matrix.MeanByColumn(GetIndexColumn(table));
        }

        /// <summary>
        /// Gets modules of deviations from the mean.
        /// </summary>
        /// <returns>Modules of deviations from the mean.</returns>
        public T[] GetModulesDevMean()
        {
            T[] arr = new T[Matrix.Rows];

            T[] xi = Matrix[GetIndexColumn(TableVariations.Xi), State.Column];
            T mean = GetSampleMeanByTable(TableVariations.Xi);

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = MathGeneric<T>.Abs(MathUnsafe<T>.Sub(xi[i], mean));
            }

            return arr;
        }

        /// <summary>
        /// Gets mean linear deviation.
        /// </summary>
        /// <returns>mean linear deviation.</returns>
        public T GetMeanLinearDeviation()
        {
            T sum = default;
            T[] arr = GetModulesDevMean();
            for (int i = 0; i < Matrix.Rows; i++)
            {
                sum = MathUnsafe<T>.Add(sum, arr[i]);
            }

            return MathGeneric<T, int, T>.Divide(sum, Matrix.Rows);
        }

        /// <summary>
        /// Gets swing of variations.
        /// </summary>
        /// <returns></returns>
        public T GetRangeVariation()
        {
            var a = Matrix.Max();
            var b = Matrix.Min();
            return MathUnsafe<T>.Sub(a, b);
        }

        /// <summary>
        /// Gets range of variations by table column in matrix.
        /// </summary>
        /// <param name="table">the table.</param>
        /// <returns>range of variations by table column of matrix.</returns>
        public T GetRangeVariation(TableVariations table)
        {
            var a = Matrix.MaxByColumn(GetIndexColumn(table));
            var b = Matrix.MinByColumn(GetIndexColumn(table));
            return MathUnsafe<T>.Sub(a, b);
        }

        /// <summary>
        /// Gets range of variations by index column in matrix.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>range of variations by column index of matrix.</returns>
        public T GetRangeVariation(int index)
        {
            var a = Matrix.MaxByColumn(index);
            var b = Matrix.MinByColumn(index);
            return MathUnsafe<T>.Sub(a, b);
        }

        /// <summary>
        /// Gets sample dispersion of matrix.
        /// </summary>
        /// <returns></returns>
        public T GetSampleDispersion()
        {
            T mean = GetSampleMeanByTable(TableVariations.Xi);

            T[] xi = Matrix[GetIndexColumn(TableVariations.Xi), State.Column];
            T sum = default;

            for (int i = 0; i < Matrix.Rows; i++)
            {
                var operation = MathUnsafe<T>.Sub(xi[i], mean);
                sum = MathUnsafe<T>.Add(sum, MathUnsafe<T>.Mul(operation, operation));
            }

            return MathGeneric<T, int, T>.Divide(sum, Matrix.Rows);
        }

        /// <summary>
        /// Gets corrected dispersion.
        /// </summary>
        /// <remarks>
        /// Finds by formula: s^2 = (n / (n - 1)) * Dispersion(sample) 
        /// </remarks>
        public T GetCorrectedDispersion()
        {
            var n = Matrix.Rows;
            return MathGeneric<T, double, T>.Multiply(GetSampleDispersion(), n - 1 / n);
        }
    }
}