using MatrixDotNet;
using Xunit;

namespace MatrixDotNetTests.VectorTests
{
    public class VectorOperationTest
    {
        [Fact]
        public void DotProductTest_MulTwoVector_AssertMustBeEqual()
        {
            // Arrange
            Vector<int> v1 = new[] { 1, 2, 21 };
            Vector<int> v2 = new[] { -3, 1, 53 };
            const int expected = 1112;
            
            // Act
            var actual = v1 * v2;
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void DotProductTest_MulTwoVectorWithNotPrimeSizeAndTestVectorization_AssertMustBeEqual()
        {
            // Arrange
            var v1 = new Vector<int>(16,1);
            var v2 = new Vector<int>(16,1);
            const int expected = 16;
            
            // Act
            var actual = v1 * v2;
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void MulTest_MulVectorOnConstant_AssertMustBeEqual()
        {
            // Arrange
            const int constant = 2;
            var v1 = new Vector<int>(16,1);
            var expected = new Vector<int>(16,constant) ;
            
            // Act
            var actual1 = constant * v1;
            var actual2 = v1 * constant;
            
            // Assert
            Assert.Equal(expected, actual1);
            Assert.Equal(expected, actual2);
        }
        
        [Theory]
        [InlineData(2,1,1)]
        [InlineData(1,2,-1)]
        public void SubTest_SubTwoVector_AssertMustBeEqual(int fill1,int fill2,int fill3)
        {
            // Arrange
            var v1 = new Vector<int>(16,fill1);
            var v2 = new Vector<int>(16,fill2);
            var expected = new Vector<int>(16,fill3);

            // Act
            var actual1 = v1 - v2;

            // Assert
            Assert.Equal(expected, actual1);
        }
        
        [Theory]
        [InlineData(2,1,3)]
        [InlineData(1,4,5)]
        public void AddTest_SubTwoVector_AssertMustBeEqual(int fill1,int fill2,int fill3)
        {
            // Arrange
            var v1 = new Vector<int>(16,fill1);
            var v2 = new Vector<int>(16,fill2);
            var expected = new Vector<int>(16,fill3);

            // Act
            var actual1 = v1 + v2;

            // Assert
            Assert.Equal(expected, actual1);
        }
        
    }
}