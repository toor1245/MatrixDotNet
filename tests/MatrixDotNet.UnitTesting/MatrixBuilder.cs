using MatrixDotNet;
using MatrixDotNet.Extensions.Builder;
using Xunit;

namespace MatrixDotNetTests
{
    public class MatrixBuilder
    {
        [Fact]
        public void BuildTest_CreateMatrixByExpressionXMulXPlusY_AssertMustBeEqual()
        {
            // Arrange
            int[] arr = {1, 2, 3, 4};
            
            Matrix<int> expected =  new[,]
            {
                { 1, 4, 9, 16},
            };
            
            // Act 
            var actual = BuildMatrix.Build(1, 4, (x, y) => x * x, arr);
            
            // Assert
            Assert.Equal(expected,actual);
        }
    }
}