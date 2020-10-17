namespace MatrixDotNet.Extensions.Sorting
{
    /// <summary>
    /// Represents logic whole sorting algorithms.
    /// </summary>
    /// <typeparam name="T">unmanaged type.</typeparam>
    public interface ISort<T> where T : unmanaged
    {
        /// <summary>
        /// Sorts matrix by rows.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        void SortByRows(Matrix<T> matrix);
        
        /// <summary>
        /// Sorts matrix by column.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        void SortByColumns(Matrix<T> matrix);
        
        /// <summary>
        /// Sorts minor diagonal of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        void SortMinorDiagonal(Matrix<T> matrix);
        
        
        /// <summary>
        /// Sorts main diagonal of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        void SortMainDiagonal(Matrix<T> matrix);
        
        /// <summary>
        /// Sorts whole matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        void Sort(Matrix<T> matrix);
        
    }
}