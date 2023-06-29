namespace MatrixDotNet.Extensions.Statistics.TableSetup
{
    public interface IConfig<T> where T : unmanaged
    {
        /// <summary>
        /// Gets matrix.
        /// </summary>
        Matrix<T> Matrix { get; }
    }
}
