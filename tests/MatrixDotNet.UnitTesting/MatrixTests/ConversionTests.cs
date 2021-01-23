using System;
using System.Linq;
using MatrixDotNet;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;
using Xunit;
using Xunit.Abstractions;

namespace MatrixDotNetTests.MatrixTests
{
    public class ConversionTests
    {
        private readonly ITestOutputHelper _output;
        public ConversionTests(ITestOutputHelper output)
        {
            _output = output;
        }
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

        #region Reverse
        
        [Fact] 
        public void ReverseTest_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new [,]
            {
                { 1,  2,  3,  4,  5,  6  },
                { 7,  8,  9,  10, 11, 12 },
                { 13, 14, 15, 16, 17, 18 },
                { 19, 20, 21, 22, 23, 24 },
                { 25, 26, 27, 28, 29, 30 },
                { 31, 32, 33, 34, 35, 36 }
            };
            Matrix<int> expected = new [,]
            {
                { 36, 35, 34, 33, 32, 31 },
                { 30, 29, 28, 27, 26, 25 },
                { 24, 23, 22, 21, 20, 19 },
                { 18, 17, 16, 15, 14, 13 },
                { 12, 11, 10, 9,  8,  7  },
                { 6,  5,  4,  3,  2,  1  }
            };

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
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
        [InlineData(15, 34)]
        [InlineData(16, 34)]
        [InlineData(7, 103)]
        [InlineData(9, 8)]
        public void ReverseInt32Test_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<int> matrixA = new Matrix<int>(m, n);
            Matrix<int> expected = new Matrix<int>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr2.Length - i - 1;
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
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
        public void ReverseSingleTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<float> matrixA = new Matrix<float>(m, n);
            Matrix<float> expected = new Matrix<float>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr2.Length - i - 1;
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
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
        public void ReverseDoubleTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<double> matrixA = new Matrix<double>(m, n);
            Matrix<double> expected = new Matrix<double>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr2.Length - i - 1;
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
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
        public void ReverseByteTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<byte> matrixA = new Matrix<byte>(m, n);
            Matrix<byte> expected = new Matrix<byte>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (byte i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (byte i = 0; i < arr2.Length; i++)
            {
                arr2[i] = (byte) (arr2.Length - i - 1);
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
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
        public void ReverseByteTest_CheckMask_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<byte> actual = BuildMatrix.RandomByte(m, n);
            Matrix<byte> matrixB = (Matrix<byte>) actual.Clone();
            var expected = matrixB.GetArray().Reverse();
            
            // Act
            MatrixConverter.Reverse(actual.GetArray());

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
        [InlineData(7, 103)]
        [InlineData(2, 103)]
        public void ReverseInt64Test_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<long> matrixA = new Matrix<long>(m, n);
            Matrix<long> expected = new Matrix<long>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr2.Length - i - 1;
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
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
        public void ReverseSByteTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<sbyte> matrixA = new Matrix<sbyte>(m, n);
            Matrix<sbyte> expected = new Matrix<sbyte>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (byte i = 0; i < arr1.Length; i++)
            {
                arr1[i] = (sbyte) i;
            }
            
            for (byte i = 0; i < arr2.Length; i++)
            {
                arr2[i] = (sbyte) (arr2.Length - i - 1);
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
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
        public void ReverseSByteTest_CheckMask_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<sbyte> actual = BuildMatrix.RandomSByte(m, n);
            Matrix<sbyte> matrixB = (Matrix<sbyte>) actual.Clone();
            var expected = matrixB.GetArray().Reverse();
            
            // Act
            MatrixConverter.Reverse(actual.GetArray());

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
        [InlineData(15, 34)]
        [InlineData(16, 34)]
        [InlineData(7, 103)]
        [InlineData(9, 8)]
        public void ReverseUInt32Test_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<uint> matrixA = new Matrix<uint>(m, n);
            Matrix<uint> expected = new Matrix<uint>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (uint i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (uint i = 0; i < arr2.Length; i++)
            {
                arr2[i] = (uint) (arr2.Length - i - 1);
            }
            
            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
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
        [InlineData(7, 103)]
        [InlineData(2, 103)]
        public void ReverseUInt64Test_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<ulong> matrixA = new Matrix<ulong>(m, n);
            Matrix<ulong> expected = new Matrix<ulong>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (uint i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (uint i = 0; i < arr2.Length; i++)
            {
                arr2[i] = (ulong) (arr2.Length - i - 1);
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
        }

        #endregion

        #region Transpose

        [Fact]
        public void TransposeFloatAvx2Test()
        {
            // Arrange
            var matrixA = new Matrix<float>(8, 8);
            for (int i = 0; i < matrixA.Length; i++)
            {
                matrixA.GetArray()[i] = i;
            }
            var expected = matrixA.Transpose();
            
            // Act
            var actual = matrixA.TransposeXVectorSize();
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        #endregion
    }
}