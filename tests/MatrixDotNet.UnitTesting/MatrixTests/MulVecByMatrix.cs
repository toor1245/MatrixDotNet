using MatrixDotNet;
using Xunit;

namespace MatrixDotNetTests.MatrixTests
{
    public class MulVecByMatrix
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

            MatrixDotNet.Vectorization.Vector<int> vec = new[]{2, 5, 1, 8};
            MatrixDotNet.Vectorization.Vector<int> expected = new int[] {50, 15, 9, 76};
            
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

            MatrixDotNet.Vectorization.Vector<int> vec = new[]{2, 5, 1, 8};
            MatrixDotNet.Vectorization.Vector<int> expected = new int[] {4, 47, 5, 68};
            
            // Act
            var actual = matrix * vec;
            
            // Assert
            Assert.Equal(expected,actual);
        }  
    }
}