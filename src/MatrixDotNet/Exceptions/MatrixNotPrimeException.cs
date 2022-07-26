using System;

namespace MatrixDotNet.Exceptions
{
    public class MatrixNotPrimeException : MatrixDotNetException
    {
        /// <summary>
        /// Throws when matrix is not prime.
        /// </summary>
        public MatrixNotPrimeException()
            : base("Matrix is not prime.")
        {

        }

        /// <summary>
        /// Throws when matrix is not prime.
        /// </summary>
        public MatrixNotPrimeException(Exception inner)
            : base("Matrix is not prime.", inner)
        {

        }
    }
}
