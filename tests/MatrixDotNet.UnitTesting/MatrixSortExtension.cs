using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Sorting;
using Xunit;

namespace MatrixDotNetTests
{
    public class MatrixSortExtension
    {
        [Fact]
        public void BubbleSortByRow_SortElementsOfMatrixAlgorithmBubbleSort_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {2, 3, 4, 5, 6},
                {2, 9, 2, 8, 1},
                {3, 4, 7, 3, 1}
            };
            
            Matrix<int> expected = new[,]
            {
                {2, 3, 4, 5, 6},
                {1, 2, 2, 8, 9},
                {1, 3, 3, 4, 7}
            };
            
            // Act
            //matrix.BubbleSortByRows();
            
            // Assert
            Assert.Equal(expected,matrix);
        }
        
        [Fact]
        public void BubbleSortByColumns_SortElementsOfMatrixAlgorithmBubbleSort_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {2, 3, 4, 5, 6},
                {2, 9, 2, 8, 1},
                {3, 4, 7, 3, 1}
            };
            
            Matrix<int> expected = new[,]
            {
                {2, 3, 2, 3, 1},
                {2, 4, 4, 5, 1},
                {3, 9, 7, 8, 6}
            };
            
            // Act
            // matrix.BubbleSortByColumn();
            
            // Assert
            Assert.Equal(expected,matrix);
        }
    }
}