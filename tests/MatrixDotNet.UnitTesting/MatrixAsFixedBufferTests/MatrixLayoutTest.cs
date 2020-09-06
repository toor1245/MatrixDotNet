using MatrixDotNet.Extensions.Core;
using Xunit;


namespace MatrixDotNetTests.MatrixAsFixedBufferTests
{
    
    public class MatrixLayoutTest
    {
        [Fact]
        public void Print()
        {
            ObjectLayoutInspector.TypeLayout.PrintLayout<MatrixAsFixedBuffer>();
        }
    }
}