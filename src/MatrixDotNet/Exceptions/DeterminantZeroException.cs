using System;

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

        /// <summary>
        /// Throws when determinant is zero.
        /// </summary>
        public DeterminantZeroException(Exception inner)
            : base("Determinant is zero.", inner)
        {

        }
    }
}
