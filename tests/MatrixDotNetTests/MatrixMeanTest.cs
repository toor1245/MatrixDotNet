using MatrixDotNet;
using MatrixDotNet.Extensions.Statistics;
using Xunit;

namespace MatrixDotNetTests
{
    public class MatrixMeanTest
    {
        [Fact]
        public void MeanTest_GetsMeanWholeMatrix3x3_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                { 4, 3, -3 },
                { 2, 4, 4  },
                {-1, 0, -5 }
            };
            
            
            var expected = 8d / matrix.Length;
            
            // Act
            var actual = matrix.Mean();
            
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MeanByRowTest_GetsMeanFirstIndexByRowMatrix3x3_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                {4, 12, -16},
                {2, 4, 4},
                {-16, -43, 98}
            };
            
            double expected = 10d / matrix.Rows;
            
            // Act
            var actual = matrix.MeanByRow(1);
            
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MeanByColumnTest_GetsMeanZeroIndexByRowMatrix3x3_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                { 4, 12, -16},
                { 2, 4, 4},
                {-16, -43, 98}
            };
            
            double expected = -10d / matrix.Columns;
            
            // Act
            var actual = matrix.MeanByColumn(0);
            
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MeanByRowsTest_GetsMeanByRowsMatrix3x3_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                { 4, 3, -3 },
                { 2, 4, 4  },
                {-1, 0, -5 }
            };

            var n = matrix.Columns;
            double[] expected = {4d / n,10d / n,-6d / n };
            
            // Act
            var actual = matrix.MeanByRows();
            
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MeanByColumnsTest_GetsMeanByColumnsMatrix3x3_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                { 4, 3, -3 },
                { 2, 4, 4  },
                {-1, 0, -5 }
            };

            var n = matrix.Rows;
            
            double[] expected = {5d / n, 7d / n,-4d / n };
            
            // Act
            var actual = matrix.MeanByColumns();
            
            
            // Assert
            Assert.Equal(expected,actual);
        }
    }
}