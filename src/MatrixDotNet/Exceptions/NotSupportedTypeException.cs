namespace MatrixDotNet.Exceptions
{
    public class NotSupportedTypeException : MatrixDotNetException
    {
        /// <summary>
        /// Throws when not supported type of library.
        /// </summary>
        public NotSupportedTypeException() 
            : base("Not supported type.")
        {
            
        }
        
        /// <summary>
        /// Throws when not supported type of library.
        /// </summary>
        public NotSupportedTypeException(string message) 
            : base(message)
        {
            
        }
    }
}