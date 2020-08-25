namespace MatrixDotNet.Extensions.Statistics
{
    public abstract class SetupVariations<T> : Setup<T> where T : unmanaged
    {
        
        protected SetupVariations(Matrix<T> matrix) : base(matrix)
        {
            
        }
    }
}