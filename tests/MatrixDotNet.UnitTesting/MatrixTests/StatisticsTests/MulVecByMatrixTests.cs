using MatrixDotNet;
using Xunit;

namespace MatrixDotNetTests.MatrixTests.StatisticsTests
{
    public class MulVecByMatrixTests
    {
        [Fact]
        public void MulVecByMatrixTests_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {1,0,2,0},
                {0,3,0,4},
                {0,0,5,0},
                {6,0,0,7}
            };

            Vector<int> vec = new[]{2, 5, 1, 8};
            Vector<int> expected = new int[] {50, 15, 9, 76};
            
            // Act
            var actual = vec * matrix;
            
            // Assert
            Assert.Equal(expected,actual);
        }  
        
        [Fact]
        public void MulMatrixByVecTests_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {1,0,2,0},
                {0,3,0,4},
                {0,0,5,0},
                {6,0,0,7}
            };

            Vector<int> vec = new[]{2, 5, 1, 8};
            Vector<int> expected = new int[] {4, 47, 5, 68};
            
            // Act
            var actual = matrix * vec;
            
            // Assert
            Assert.Equal(expected,actual);
        }  
        
    }
}