namespace MatrixDotNet.Exceptions
{
    public class MatrixNotSquareException : MatrixDotNetException
    {
        /// <summary>
        /// Throws when matrix is not square.
        /// </summary>
        public MatrixNotSquareException()
            : base("Matrix is not square.")
        {

        }
    }
}