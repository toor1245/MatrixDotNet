using System;

namespace MatrixDotNet.Exceptions
{
    public class NotSupportedTypeException : MatrixDotNetException
    {
        /// <summary>
        ///     Throws when not supported type of library.
        /// </summary>
        public NotSupportedTypeException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Throws when not supported type of library.
        /// </summary>
        public NotSupportedTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}