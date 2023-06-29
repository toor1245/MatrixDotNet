using MatrixDotNet;
using MatrixDotNet.Extensions.Sorting;
using Xunit;

namespace MatrixDotNetTests.MatrixTests
{
    public class SortTests
    {
        [Fact]
        public void SortMatrix()
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
                {1, 1, 2, 2, 2},
                {3, 3, 3, 4, 4},
                {5, 6, 7, 8, 9}
            };

            // Act
            matrix.Sort();

            // Assert
            Assert.Equal(expected, matrix);
        }

        [Fact]
        public void SortMatrixByRows()
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
            matrix.SortByRows();

            // Assert
            Assert.Equal(expected, matrix);
        }

        [Fact]
        public void SortMatrixByColumns()
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
            matrix.SortByColumns();

            // Assert
            Assert.Equal(expected, matrix);
        }
    }
}