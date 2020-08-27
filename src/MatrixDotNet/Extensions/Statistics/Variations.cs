using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Statistics
{
    /// <summary>
    /// Represents calculations any variations operations.
    /// </summary>
    /// <typeparam name="T">unmanaged type.</typeparam>
    public sealed class Variations<T> : SetupVariations<T> where T : unmanaged
    {
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
                ColumnNumber[i] = (int) TableIntervals.Column;
            }

        }

        /// <summary>
        /// Gets <c>mean</c> value by column table. 
        /// </summary>
        /// <param name="table">the table</param>
        /// <returns>mean value by column table.</returns>
        public T GetSampleMeanByTable(TableVariations table)
        {
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
                arr[i] = MathExtension.Abs(MathExtension.Sub(xi[i], mean));
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
                sum = MathExtension.Add(sum, arr[i]);
            }

            return MathExtension.DivideBy(sum, Matrix.Rows);
        }

        /// <summary>
        /// Gets swing of variations.
        /// </summary>
        /// <returns></returns>
        public T GetRangeVariation()
        {
            var a = Matrix.Max();
            var b = Matrix.Min();
            return MathExtension.Sub(a, b);
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
            return MathExtension.Sub(a, b);
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
            return MathExtension.Sub(a, b);
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
                var operation = MathExtension.Sub(xi[i],mean);
                sum = MathExtension.Add(sum,MathExtension.Multiply(operation,operation));
            }

            return MathExtension.DivideBy(sum,Matrix.Rows);
        }
    }
}