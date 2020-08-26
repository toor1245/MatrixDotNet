namespace MatrixDotNet.Extensions.Statistics
{
    /// <summary>
    /// Represents store data such as matrix, tables.
    /// Share variations operations.
    /// </summary>
    /// <typeparam name="T">unmanaged type.</typeparam>
    public abstract class SetupVariations<T> : Setup<T> where T : unmanaged
    {
        /// <summary>
        /// Gets table variations.
        /// </summary>
        protected TableVariations[] Variations { get; }

        /// <summary>
        /// Initialize settings for <c>Variations</c>
        /// </summary>
        /// <param name="variations">configuration.</param>
        protected SetupVariations(ConfigVariations<T> variations) : base(variations.Matrix)
        {
            Variations = variations.Variations;
        }
        
        /// <summary>
        /// Gets index column in matrix.
        /// </summary>
        /// <param name="tableVariations">the table</param>
        /// <returns>Index of column</returns>
        protected int GetIndexColumn(TableVariations tableVariations)
        {
            return FindColumn((int) tableVariations);
        }
    }
}