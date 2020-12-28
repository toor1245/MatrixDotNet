using System;
using MatrixDotNet;
using MatrixDotNet.Extensions.Conversion;
using Xunit;

namespace MatrixDotNetTests.MatrixTests
{
    public class MatrixConversionTest
    {
        #region ReduceColumn
        
        [Fact]
        public void ReduceColumnTest_ReduceFirstColumn_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {2, 3, 4, 5, 6},
                {2, 9, 2, 8, 1},
                {3, 4, 7, 3, 1}
            };
            
            Matrix<int> expected =  new[,]
            {
                { 3, 4, 5, 6},
                { 9, 2, 8, 1},
                { 4, 7, 3, 1}
            };
            
            // Act 
            var actual = matrix.ReduceColumn(0);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void ReduceColumnTest_ReduceLastColumn_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {2, 3, 4, 5, 6},
                {2, 9, 2, 8, 1},
                {3, 4, 7, 3, 1}
            };
            
            Matrix<int> expected =  new[,]
            {
                {2, 3, 4, 5,},
                {2, 9, 2, 8},
                {3, 4, 7, 3}
            };
            
            // Act 
            var actual = matrix.ReduceColumn(4);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void ReduceColumnTest_ReduceTwoColumnWhereColumnsLessThanRows_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 2, 3, 4 },
                { 2, 9, 2 },
                { 3, 4, 7 },
                { 3, 4, 7 }
            };
            
            Matrix<int> expected =  new[,]
            {
                { 2,  4 },
                { 2,  2 },
                { 3,  7 },
                { 3,  7 }
            };
            
            // Act 
            var actual = matrix.ReduceColumn(1);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void ReduceColumnTest_ReduceTwoColumnWhereColumnsMoreThanRows_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 2, 3, 4,8 },
                { 2, 9, 2,9 },
                { 3, 4, 7,10 }
            };
            
            Matrix<int> expected =  new[,]
            {
                { 2, 4, 8 },
                { 2, 2, 9 },
                { 3, 7, 10 }
            };
            
            // Act 
            var actual = matrix.ReduceColumn(1);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void ReduceColumnTest_ThrowsNullReferenceException_AssertMustThrowsNullReferenceException()
        {
            // Arrange
            Matrix<int> matrix = null;
            
            // Assert Act 
            Assert.Throws<NullReferenceException>(() => matrix.ReduceColumn(0));
        }
        
        #endregion
        
        #region ReduceRow
        
        [Fact]
        public void ReduceRowTest_ReduceFirstRow_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 2, 3, 4, 5, 6 },
                { 2, 9, 2, 8, 1 },
                { 3, 4, 7, 3, 1 }
            };
            
            Matrix<int> expected = new[,]
            {
                {2, 9, 2, 8, 1},
                {3, 4, 7, 3, 1}
            };
            
            // Act 
            var actual = matrix.ReduceRow(0);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void ReduceRowTest_ReduceLastRow_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {2, 3, 4, 5, 6},
                {2, 9, 2, 8, 1},
                {3, 4, 7, 3, 1}
            };
            
            Matrix<int> expected =  new[,]
            {
                {2, 3, 4, 5, 6},
                {2, 9, 2, 8, 1},
            };
            
            // Act 
            var actual = matrix.ReduceRow(2);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void ReduceRowTest_ReduceTwoColumnWhereRowsMoreThanColumns_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 1, 3, 4 },
                { 2, 9, 2 },
                { 3, 4, 7 },
                { 3, 4, 7 }
            };
            
            Matrix<int> expected =  new[,]
            {
                { 1, 3, 4 },
                { 3, 4, 7 },
                { 3, 4, 7 }
            };
            
            // Act 
            var actual = matrix.ReduceRow(1);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void ReduceRowTest_ReduceTwoRowWhereRowsLessThanRows_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 2, 3, 4,8 },
                { 2, 9, 2,9 },
                { 3, 4, 7,10 }
            };
            
            Matrix<int> expected =  new[,]
            {
                { 2, 3, 4,8 },
                { 3, 4, 7,10 }
            };
            
            // Act 
            var actual = matrix.ReduceRow(1);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void ReduceRowTest_ThrowsNullReferenceException_AssertMustThrowsNullReferenceException()
        {
            // Arrange
            Matrix<int> matrix = null;
            
            // Assert Act 
            Assert.Throws<NullReferenceException>(() => matrix.ReduceRow(0));
        }
        
        #endregion

        #region AddColumn

        [Fact]
        public void AddColumnTest_AddFirstColumn_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 2, 3, 4, 5, 6},
                { 2, 9, 2, 8, 1},
                { 3, 4, 7, 3, 1}
            };
            
            int[] arr = {1, 2, 3};
            
            Matrix<int> expected =  new[,]
            {
                { 1, 2, 3, 4, 5, 6},
                { 2, 2, 9, 2, 8, 1},
                { 3, 3, 4, 7, 3, 1}
            };
            
            // Act 
            var actual = matrix.AddColumn(arr,0);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void AddColumnTest_AddLastColumn_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 2, 3, 4, 5, 6},
                { 2, 9, 2, 8, 1},
                { 3, 4, 7, 3, 1}
            };
            
            int[] arr = {1, 2, 3};
            
            Matrix<int> expected =  new[,]
            {
                { 2, 3, 4, 5, 6, 1},
                { 2, 9, 2, 8, 1, 2},
                { 3, 4, 7, 3, 1, 3}
            };
            
            // Act 
            var actual = matrix.AddColumn(arr,5);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void AddColumnTest_AddIndexThreeColumn_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 2, 3, 4},
                { 2, 9, 2},
                { 3, 4, 7}
            };
            
            int[] arr = {1, 2, 3};
            
            Matrix<int> expected =  new[,]
            {
                { 2, 3, 1, 4},
                { 2, 9, 2, 2},
                { 3, 4, 3, 7}
            };
            
            // Act 
            var actual = matrix.AddColumn(arr,2);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void AddColumnTest_ThrowsNullReferenceException_AssertMustThrowsNullReferenceException()
        {
            // Arrange
            Matrix<int> matrix = null;
            
            // Assert Act 
            Assert.Throws<NullReferenceException>(() => matrix.AddColumn(new int[]{1,2,3},0));
        }

        #endregion

        #region AddRow
        
        [Fact]
        public void AddRowTest_AddFirstIndexRow_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 2, 3, 4, 5, 6},
                { 2, 9, 2, 8, 1},
                { 3, 4, 7, 3, 1}
            };
            
            int[] arr = {1, 2, 3, 4, 5};
            
            Matrix<int> expected =  new[,]
            {
                { 1, 2, 3, 4, 5},
                { 2, 3, 4, 5, 6},
                { 2, 9, 2, 8, 1},
                { 3, 4, 7, 3, 1}
            };
            
            // Act 
            var actual = matrix.AddRow(arr,0);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void AddRowTest_AddLastIndexOfRow_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 2, 3, 4, 5, 6},
                { 2, 9, 2, 8, 1},
                { 3, 4, 7, 3, 1}
            };
            
            int[] arr = {1, 2, 3, 4 ,5};
            
            Matrix<int> expected =  new[,]
            {
                
                { 2, 3, 4, 5, 6},
                { 2, 9, 2, 8, 1},
                { 3, 4, 7, 3, 1},
                { 1, 2, 3, 4 ,5}
            };
            
            // Act 
            var actual = matrix.AddRow(arr,3);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        
        [Fact]
        public void AddRowTest_AddIndexTwoRow_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 2, 3, 4, 5, 6},
                { 2, 9, 2, 8, 1},
                { 3, 4, 7, 3, 1}
            };
            
            int[] arr = { 1, 2, 3, 4 ,5};
            
            Matrix<int> expected =  new[,]
            {
                
                { 2, 3, 4, 5, 6},
                { 2, 9, 2, 8, 1},
                { 1, 2, 3, 4 ,5},
                { 3, 4, 7, 3, 1},
            };
            
            // Act 
            var actual = matrix.AddRow(arr,2);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void AddRowTest_ThrowsNullReferenceException_AssertMustThrowsNullReferenceException()
        {
            // Arrange
            Matrix<int> matrix = null;
            
            // Assert Act 
            Assert.Throws<NullReferenceException>(() => matrix.AddRow(new int[]{1,2,3},0));
        }
        #endregion

        #region Negate
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        [InlineData(7, 103)]
        [InlineData(2, 103)]
        public void NegateMatrixTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<int> matrixA = new Matrix<int>(m, n, 1);
            Matrix<int> expected = new Matrix<int>(m, n, -1);

            // Act
            var actual = Matrix<int>.Negate(matrixA);

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        [InlineData(7, 14)]
        [InlineData(2, 32)]
        public void NegateMatrixSByteTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<sbyte> matrixA = new Matrix<sbyte>(m, n, 1);
            Matrix<sbyte> expected = new Matrix<sbyte>(m, n, -1);

            // Act
            var actual = Matrix<sbyte>.Negate(matrixA);

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        [InlineData(7, 14)]
        [InlineData(2, 32)]
        public void NegateMatrixShortTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<short> matrixA = new Matrix<short>(m, n, 1);
            Matrix<short> expected = new Matrix<short>(m, n, -1);

            // Act
            var actual = Matrix<short>.Negate(matrixA);

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        [InlineData(7, 14)]
        [InlineData(2, 32)]
        public void NegateTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<short> matrixA = new Matrix<short>(m, n, 1);
            Matrix<short> expected = new Matrix<short>(m, n, -1);

            // Act
            var actual = -matrixA;

            // Assert
            Assert.Equal(expected, actual);
        }
        
        #endregion
    }
}