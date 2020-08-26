namespace MatrixDotNet.Extensions.Statistics
{
    /// <summary>
    /// Represents configuration for <c>Variations</c>
    /// </summary>
    /// <typeparam name="T">unmanaged type</typeparam>
    public class ConfigVariations<T> : IConfig<T> where T : unmanaged
    {
        /// <summary>
        /// <inheritdoc cref="IConfig{T}.Matrix"/>
        /// </summary>
        public Matrix<T> Matrix { get; }

        /// <summary>
        /// Gets Variations.
        /// </summary>
        public TableVariations[] Variations { get; }
        
        /// <summary>
        /// Initialize matrix and table for variations.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="variations">the variations</param>
        public ConfigVariations(Matrix<T> matrix,TableVariations[] variations)
        {
            Matrix = matrix;
            Variations = variations;
        }
    }
}