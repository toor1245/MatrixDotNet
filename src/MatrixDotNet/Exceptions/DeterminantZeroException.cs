namespace MatrixDotNet.Exceptions
{
    public class DeterminantZeroException : MatrixDotNetException
    {
        /// <summary>
        /// Throws when determinant is zero.
        /// </summary>
        public DeterminantZeroException()
            : base("Determinant is zero.")
        {

        }
    }
}