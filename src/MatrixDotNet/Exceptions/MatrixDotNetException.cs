using System;

namespace MatrixDotNet.Exceptions
{
    /// <summary>
    /// Represent exception for <see cref="Matrix{T}"/>
    /// </summary>
    public sealed class MatrixDotNetException : Exception
    {
        /// <summary>
        /// Exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="argument"></param>
        public MatrixDotNetException(string message, string argument) : base(message)
        {
            _message = message;
            Argument1 = argument;
        }
    
        /// <summary>
        /// Exception.
        /// </summary>
        /// <param name="message"></param>
        public MatrixDotNetException(string message) : base(message)
        {
            Console.ForegroundColor= ConsoleColor.Red;
            _message = message;
        }

        private string _message;
        /// <summary>
        /// Exception.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="argument1"></param>
        /// <param name="argument2"></param>
        public MatrixDotNetException(string message, string argument1,string argument2) : base(message)
        {
            _message = message;
            Argument1 = argument1;
            Argument2 = argument2;
        }

        /// <summary>
        /// Gets message.
        /// </summary>
        public override string Message
        {
            get 
            { 
                Console.ForegroundColor = ConsoleColor.Yellow;
                return _message;
            }
        }

        /// <summary>
        /// Gets argument.
        /// </summary>
        public string Argument1 { get; }
        
        /// <summary>
        /// Gets argument.
        /// </summary>
        public string Argument2 { get; }
    }
}