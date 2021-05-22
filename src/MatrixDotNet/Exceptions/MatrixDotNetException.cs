using System;

namespace MatrixDotNet.Exceptions
{
    /// <summary>
    /// Represent exception for <see cref="Matrix{T}"/>
    /// </summary>
    public class MatrixDotNetException : Exception
    {
        /// <summary>
        /// Exception.
        /// </summary>
        /// <param name="message"></param>
        public MatrixDotNetException(string message) 
            : base(message)
        {

        }

        /// <summary>
        /// Exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public MatrixDotNetException(string message, Exception inner) 
            : base(message, inner)
        {

        }
    }
}