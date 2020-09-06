namespace MatrixDotNet.Extensions.Core.Extensions.Sorting
{
    /// <summary>
    /// Represents logic whole sorting algorithms.
    /// </summary>
    public interface ISorting
    {
        /// <summary>
        /// Sorts matrix by rows.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        void SortByRows(ref MatrixAsFixedBuffer matrix);
        
        /// <summary>
        /// Sorts matrix by column.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        void SortByColumns(ref MatrixAsFixedBuffer matrix);
        
        /// <summary>
        /// Sorts minor diagonal of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        void SortMinorDiagonal(ref MatrixAsFixedBuffer matrix);
        
        
        /// <summary>
        /// Sorts main diagonal of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        void SortMainDiagonal(ref MatrixAsFixedBuffer matrix);
        
        /// <summary>
        /// Sorts whole matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        void Sort(ref MatrixAsFixedBuffer matrix);
        
    }
}