using System;

namespace MatrixDotNet.Exceptions
{
    public class MatrixDotNetException : Exception
    {
        public MatrixDotNetException(string message, string argument) : base(message)
        {
            Message = message;
            Argument1 = argument;
        }
    
        public MatrixDotNetException(string message) : base(message)
        {
            Message = message;
        }
    
        public MatrixDotNetException(string message, string argument1,string argument2) : base(message)
        {
            Message = message;
            Argument1 = argument1;
            Argument2 = argument2;
        }
        
        
        public string Message { get; }
        public string Argument1 { get; }
        public string Argument2 { get; }
    }
}