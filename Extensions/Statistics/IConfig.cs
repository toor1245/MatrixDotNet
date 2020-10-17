namespace MatrixDotNet.Extensions.Statistics
{
    public interface IConfig<T> where T : unmanaged
    {
        /// <summary>
        /// Gets matrix.
        /// </summary>
        public Matrix<T> Matrix { get; }
    }
}