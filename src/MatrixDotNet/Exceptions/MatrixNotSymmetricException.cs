using System;

namespace MatrixDotNet.Exceptions
{
    public class MatrixNotSymmetricException : MatrixDotNetException
    {
        /// <summary>
        /// Throws when matrix is not symmetric.
        /// </summary>
        public MatrixNotSymmetricException()
            : base("Matrix is not symmetric")
        {

        }

        /// <summary>
        /// Throws when matrix is not symmetric.
        /// </summary>
        public MatrixNotSymmetricException(Exception inner)
            : base("Matrix is not symmetric", inner)
        {

        }
    }
}
