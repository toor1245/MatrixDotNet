namespace MatrixDotNet.Extensions.Statistics
{
    public sealed class Variations<T> : SetupVariations<T> where T : unmanaged
    {
        
        public Variations(ConfigVariations<T> variations) : base(variations)
        {
            
        }
    }
}