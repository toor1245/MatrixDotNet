namespace MatrixDotNet.Exceptions
{
    public class MatrixSingularException : MatrixDotNetException
    {
        /// <summary>
        /// Throws when matrix is singular.
        /// </summary>
        public MatrixSingularException()
            : base("Matrix is singular")
        {

        }
    }
}