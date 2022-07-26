using System;

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

        /// <summary>
        /// Throws when matrix is singular.
        /// </summary>
        public MatrixSingularException(Exception inner)
            : base("Matrix is singular", inner)
        {

        }
    }
}
