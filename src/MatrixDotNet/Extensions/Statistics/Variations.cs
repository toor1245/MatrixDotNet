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
                arr[i] = MathExtension.Abs(MathExtension.Sub(xi[i],mean));
            }
            
            return arr;
        }
        
    }
}