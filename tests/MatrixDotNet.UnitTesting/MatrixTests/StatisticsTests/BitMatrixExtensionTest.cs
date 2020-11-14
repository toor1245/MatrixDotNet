using System;
using MatrixDotNet;
using MatrixDotNet.Extensions.Statistics;
using Xunit;

namespace MatrixDotNetTests.MatrixTests.StatisticsTests
{
    public class BitMatrixExtensionTest
    {
        #region maximum tests
        
        #region default maximum
        
        [Fact]
        public void MaxTest_FindMaximumValue_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = 7;

            // Act
            int actual = matrixA.Max();

            // Assert
            Assert.Equal(expected,actual);
        }
        
        
        [Fact]
        public void MaxByRowTest_FindMaximumValueInDimensionZero_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = 3;

            // Act
            int actual = matrixA.MaxByRow(0);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MaxByColumnTest_FindMaximumValueInDimensionZero_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = 5;

            // Act
            int actual = matrixA.MaxByColumn(0);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MaxColumnsTest_FindMaximumValueInEachRow_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new int[,]
            {
                {1, 2,  3},
                {5, -6, 7},
                {-3, 4, 6},
                {1, 2,  3},
            };

            int[] expected = {5, 4, 7};

            // Act
            int[] actual = matrixA.MaxColumns();

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MaxRowsTest_FindMaximumValueInEachRow_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new int[,]
            {
                {1, 2, 3},
                {5, -6, 7},
                {-3, 4, 6}
            };

            int[] expected = {3, 7, 6};

            // Act
            int[] actual = matrixA.MaxRows();

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion
        
        #region bit maximum
        
        [Fact]
        public void BitMaxInt64Test_FindMaximumValue_AssertMustBeEqual()
        {
            // Arrange
            Matrix<long> matrixA = new long[,]
            {
                { 1,  2, 2131421413422353532 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            long expected = 2131421413422353532;

            // Act
            long actual = matrixA.BitMax();

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMaxInt32Test_FindMaximumValue_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = 7;

            // Act
            int actual = matrixA.BitMax();

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMaxInt16Test_FindMaximumValue_AssertMustBeEqual()
        {
            // Arrange
            Matrix<short> matrixA = new short[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { short.MaxValue, 4, 6 }
            };
            
            int expected = short.MaxValue;

            // Act
            int actual = matrixA.BitMax();

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMaxUInt8Test_FindMaximumValue_AssertMustBeEqual()
        {
            // Arrange
            Matrix<byte> matrixA = new byte[,]
            {
                { 1,  2, 3 },
                { 5, 1, 7 },
                { 251, 4, 6 }
            };
            
            int expected = 251;

            // Act
            int actual = matrixA.BitMax();

            // Assert
            Assert.Equal(expected,actual);
        }
        
        #endregion
        
        #region bit maximum by row
        
        [Fact]
        public void BitMaxByRowLongTest_FindMaximumValueInDimensionZero_AssertMustBeEqual()
        {
            // Arrange
            Matrix<long> matrixA = new long[,]
            {
                { 1,  2,  3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = 3;

            // Act
            long actual = matrixA.BitMaxByRow(0);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMaxByRowTest_FindMaximumValueInDimensionZero_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2,  3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = 3;

            // Act
            int actual = matrixA.BitMaxByRow(0);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMaxByRowInt16Test_FindMaximumValueInDimensionZero_AssertMustBeEqual()
        {
            // Arrange
            Matrix<short> matrixA = new short[,]
            {
                { 1,  2,  3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = 3;

            // Act
            int actual = matrixA.BitMaxByRow(0);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMaxByRowInt8Test_FindMaximumValueInDimensionZero_AssertMustBeEqual()
        {
            // Arrange
            Matrix<byte> matrixA = new byte[,]
            {
                { 1, 2, 3 },
                { 5, 6, 7 },
                { 3, 4, 6 }
            };
            
            int expected = 3;

            // Act
            int actual = matrixA.BitMaxByRow(0);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        #endregion
        
        #region bit maximum by column
        
        [Fact]
        public void BitMaxByColumnTest_FindMaximumValueInDimensionTwo_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = 7;

            // Act
            int actual = matrixA.BitMaxByColumn(2);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMaxByColumnTest2_FindMaximumValueInDimensionTwo_AssertMustBeEqual()
        {
            // Arrange
            Matrix<long> matrixA = new long[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            long expected = 7;

            // Act
            long actual = matrixA.BitMaxByColumn(2);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMaxByColumnInt16Test_FindMaximumValueInDimensionTwo_AssertMustBeEqual()
        {
            // Arrange
            Matrix<short> matrixA = new short[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = 7;

            // Act
            int actual = matrixA.BitMaxByColumn(2);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMaxByColumnInt8Test_FindMaximumValueInDimensionTwo_AssertMustBeEqual()
        {
            // Arrange
            Matrix<byte> matrixA = new byte[,]
            {
                { 1,  2, 3 },
                { 5, 1, 7 },
                { 3, 4, 6 }
            };
            
            int expected = 7;

            // Act
            int actual = matrixA.BitMaxByColumn(2);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        #endregion
        
        #endregion

        #region minimum tests
        
        #region default minimum
        
        [Fact]
        public void MinTest_FindMinimumValue_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = -6;

            // Act
            int actual = matrixA.BitMin();

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MinByColumnTest_FindMinimumValueInDimensionTwo_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = 3;

            // Act
            int actual = matrixA.MinByColumn(2);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MinByRowTest_FindMinimumValueInDimensionOne_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = -6;

            // Act
            int actual = matrixA.MinByRow(1);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        #endregion
        
        #region bit minimum by rows

        [Fact]
        public void BitMinInt32Test_FindMinimumValue_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = -6;

            // Act
            int actual = matrixA.BitMin();

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMinInt64Test_FindMinimumValue_AssertMustBeEqual()
        {
            // Arrange
            Matrix<long> matrixA = new long[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = -6;

            // Act
            long actual = matrixA.BitMin();

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMinInt16Test_FindMinimumValue_AssertMustBeEqual()
        {
            // Arrange
            Matrix<short> matrixA = new short[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, -9 }
            };
            
            int expected = -9;

            // Act
            int actual = matrixA.BitMin();

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMinUInt8Test_FindMinimumValue_AssertMustBeEqual()
        {
            // Arrange
            Matrix<byte> matrixA = new Byte[,]
            {
                { 1,  2, 3 },
                { 5, 2, 7 },
                { 1, 4, 0 }
            };
            
            int expected = 0;

            // Act
            int actual = matrixA.BitMin();

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMinByRowInt32Test_FindMinimumValueInDimensionOne_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = -6;

            // Act
            int actual = matrixA.BitMinByRow(1);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMinByRowInt64Test_FindMinimumValueInDimensionOne_AssertMustBeEqual()
        {
            // Arrange
            Matrix<long> matrixA = new long[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = -6;

            // Act
            long actual = matrixA.BitMinByRow(1);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMinByRowInt16Test_FindMinimumValueInDimensionOne_AssertMustBeEqual()
        {
            // Arrange
            Matrix<short> matrixA = new short[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = -6;

            // Act
            long actual = matrixA.BitMinByRow(1);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMinByRowInt8Test_FindMinimumValueInDimensionOne_AssertMustBeEqual()
        {
            // Arrange
            Matrix<byte> matrixA = new byte[,]
            {
                { 1,  2, 3 },
                { 5, 0, 7 },
                { 0, 4, 6 }
            };
            
            int expected = 0;

            // Act
            long actual = matrixA.BitMinByRow(1);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        #endregion
        
        #region bit minimum by column
        
        [Fact]
        public void BitMinByColumnInt32Test_FindMinimumValueInDimensionZero_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = -3;

            // Act
            int actual = matrixA.BitMinByColumn(0);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMinByColumnInt64Test_FindMinimumValueInDimensionZero_AssertMustBeEqual()
        {
            // Arrange
            Matrix<long> matrixA = new long[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = -3;

            // Act
            long actual = matrixA.BitMinByColumn(0);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMinByColumnInt16Test_FindMinimumValueInDimensionZero_AssertMustBeEqual()
        {
            // Arrange
            Matrix<short> matrixA = new short[,]
            {
                { 1,  2, 3 },
                { 5, -6, 7 },
                { -3, 4, 6 }
            };
            
            int expected = -3;

            // Act
            long actual = matrixA.BitMinByColumn(0);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void BitMinByColumnUInt8Test_FindMinimumValueInDimensionZero_AssertMustBeEqual()
        {
            // Arrange
            Matrix<byte> matrixA = new byte[,]
            {
                { 1,  2, 3 },
                { 5, 0, 7 },
                { 0, 4, 6 }
            };
            
            int expected = 0;

            // Act
            long actual = matrixA.BitMinByColumn(0);

            // Assert
            Assert.Equal(expected,actual);
        }
        
        #endregion
        
        #endregion
    }
}