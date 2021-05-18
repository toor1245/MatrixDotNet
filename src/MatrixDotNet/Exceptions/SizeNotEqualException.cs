namespace MatrixDotNet.Exceptions
{
    public class SizeNotEqualException : MatrixDotNetException
    {
        /// <summary>
        /// Throws when size of array A is not equal B.
        /// </summary>
        public SizeNotEqualException(string message) 
            : base(message)
        {
            
        }
        
        /// <summary>
        /// Throws when size of array A is not equal B.
        /// </summary>
        public SizeNotEqualException() 
            : base("Size of array A is not equal B")
        {
            
        }
    }
}