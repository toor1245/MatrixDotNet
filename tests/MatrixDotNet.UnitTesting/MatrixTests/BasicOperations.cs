using System;
using System.Threading.Tasks;
using MatrixDotNet;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Performance;
using MatrixDotNet.Extensions.Performance.Simd;
using MatrixDotNet.Vectorization;
using Xunit;

namespace MatrixDotNetTests.MatrixTests
{
    public class BasicOperations
    {
        #region Multiply
        
        [Fact]
        public void MatrixMultiplyTest_MultiplyTwoMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<float> matrixA = new float[,]
            {
                {1, 5, 8},
                {3, 5, 6},
            };

            Matrix<float> matrixB = new float[,]
            {
                {1, 2, 4},
                {3, 4, 6},
                {4, 8, 9},
            };
            
            Matrix<float> expected = new float[,]
            {
                {48,86,106},
                {42,74,96}
            };
            
            // Act
            Matrix<float> actual = matrixA * matrixB;

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MatrixMultiplyDoubleTest_MultiplyTwoMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrixA = new double[,]
            {
                {1, 5, 8},
                {3, 5, 6},
            };

            Matrix<double> matrixB = new double[,]
            {
                {1, 2, 4},
                {3, 4, 6},
                {4, 8, 9},
            };
            
            Matrix<double> expected = new double[,]
            {
                {48,86,106},
                {42,74,96}
            };
            
            // Act
            Matrix<double> actual = matrixA * matrixB;

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MatrixMultiplySimdDoubleTest_MultiplyTwoMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrixA = new Matrix<double>(16, 16, 1);
            Matrix<double> expected = new Matrix<double>(16, 16, 16);

            // Act
            Matrix<double> actual = matrixA * matrixA;

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(16)]
        [InlineData(32)]
        [InlineData(48)]
        [InlineData(64)]
        [InlineData(80)]
        [InlineData(96)]
        [InlineData(128)]
        [InlineData(144)]
        public void MatrixMultiplyIntegerSimdTest_MultiplyTwoMatrix_AssertMustBeEqual(int size)
        {
            // Arrange
            Matrix<int> matrixA = new Matrix<int>(size, size, 1);
            Matrix<int> expected = new Matrix<int>(size, size, size);

            // Act
            Matrix<int> actual = matrixA * matrixA;

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(16)]
        [InlineData(32)]
        [InlineData(48)]
        [InlineData(64)]
        [InlineData(80)]
        [InlineData(96)]
        [InlineData(128)]
        [InlineData(144)]
        public void MatrixMultiplySimdInt32Test_MultiplyTwoMatrix_AssertMustBeEqual(int size)
        {
            // Arrange
            Matrix<int> matrixA = new Matrix<int>(size, size, 1);
            Matrix<int> expected = new Matrix<int>(size, size, size);

            // Act
            Matrix<int> actual = matrixA * matrixA;

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(16)]
        [InlineData(32)]
        [InlineData(48)]
        [InlineData(64)]
        [InlineData(80)]
        [InlineData(96)]
        [InlineData(128)]
        [InlineData(144)]
        public void MatrixMultiplySimdUInt32Test_MultiplyTwoMatrix_AssertMustBeEqual(int size)
        {
            // Arrange
            var matrixA = new Matrix<uint>(size, size, 1);
            var expected = new Matrix<uint>(size, size, (uint) size);

            // Act
            var actual = matrixA * matrixA;

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(16)]
        [InlineData(32)]
        [InlineData(48)]
        [InlineData(64)]
        [InlineData(80)]
        [InlineData(96)]
        [InlineData(128)]
        [InlineData(144)]
        public void MatrixMultiplySimdTest_MultiplyTwoMatrix_AssertMustBeEqual(int size)
        {
            // Arrange
            Matrix<float> matrixA = new Matrix<float>(size, size, 1);
            Matrix<float> expected = new Matrix<float>(size, size, size);

            // Act
            Matrix<float> actual = matrixA * matrixA;

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void MatrixMultiplyTest_MultiplyTwoMatrix_AssertMustBeThrowsMatrixDotNetException()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
                {4, 8, 9},
            };

            Matrix<int> matrixB = new[,]
            {
                {1, 2, 4},
                {3, 4, 6},
            };
            
            // Assert, Act
            Assert.Throws<MatrixDotNetException>(() => matrixA * matrixB);
        }

        #endregion
        
        #region Strassen
        
        [Fact]
        public void StrassenTest_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = BuildMatrix.RandomInt(1024,1024,1, 2);
            Matrix<int> matrixB = BuildMatrix.RandomInt(1024,1024,1, 2);
            Matrix<int> expected = matrixA * matrixB;

            // Act
            var actual = Optimization.MultiplyStrassen(matrixA, matrixB);
            
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact] 
        public async Task StrassenParallelTest_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = BuildMatrix.RandomInt(1024,1024,1, 2);
            Matrix<int> matrixB = BuildMatrix.RandomInt(1024,1024,1, 2);
            Matrix<int> expected = matrixA * matrixB;

            // Act
            var actual = await Optimization.MultiplyStrassenAsync(matrixA, matrixB);
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        #endregion
        
        #region Multiply on constant
        
        [Fact]
        public void MultiplyOpMatrixOnConstTest_MatrixMultiplyToConstantRightSide_AssertMustBeTrue()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
            };
            
            Matrix<int> expected = new[,]
            {
                {2, 10, 16},
                {6, 10, 12},
            };
            
            // Act
            var actual = matrixA * 2; 
            

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MultiplyOpMatrixOnConstTest_MatrixMultiplyToConstantLeftSide_AssertMustBeTrue()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
            };
            
            Matrix<int> expected = new[,]
            {
                {2, 10, 16},
                {6, 10, 12},
            };
            
            // Act
            var actual = 2 * matrixA; 
            

            // Assert
            Assert.Equal(expected,actual);
        }
        
        #endregion

        #region Multiply on vector
        
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
        
        [Fact]
        public void MultiplyMatrixOnVector_AssertMustBeThrowsMatrixDotNetException()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {4, 5, 10},
            };

            int[] vector = { 1, 2, 4, 6 };
            
            // Assert, Assert
            Assert.Throws<MatrixDotNetException>(() => vector * matrixA);
            Assert.Throws<MatrixDotNetException>(() => matrixA * vector);
        }
        
        #endregion

        #region Addition
        
        [Fact]
        public void AddOpMatrixTest_SumTwoMatrix_AssertMustBeThrowsMatrixDotNetException()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
                {4, 8, 9},
            };

            Matrix<int> matrixB = new[,]
            {
                {1, 2, 4},
                {3, 4, 6},
            };
            
            // Assert, Act
            Assert.Throws<MatrixDotNetException>(() => matrixA + matrixB);
        }
        
        [Fact]
        public void AddOpMatrixTest_SumTwoMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<decimal> matrixA = new Decimal[,]
            {
                {1, 5, 8},
                {3, 5, 6},
                {4, 8, 9},
            };

            Matrix<decimal> matrixB = new decimal[,]
            {
                {1, 2, 4},
                {3, 4, 6},
                {4, 8, 9},
            };
            
            Matrix<decimal> expected = new decimal[,]
            {
                { 2, 7, 12 },
                { 6, 9, 12 },
                { 8, 16,18 }
            };
            
            // Act
            var actual = matrixA + matrixB;
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void AddOpMatrixSimdTest_SumTwoMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
                {4, 8, 9},
            };

            Matrix<int> matrixB = new[,]
            {
                {1, 2, 4},
                {3, 4, 6},
                {4, 8, 9},
            };
            
            Matrix<int> expected = new[,]
            {
                { 2, 7, 12 },
                { 6, 9, 12 },
                { 8, 16,18 }
            };
            
            // Act
            var actual = matrixA + matrixB;
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        #endregion

        #region Subtract
        
        [Fact]
        public void SubtractOpMatrixTest_SubTwoMatrices_AssertMustBeEqual()
        {
            // Arrange
            Matrix<decimal> matrixA = new decimal[,]
            {
                {1, 5, 8},
                {3, 5, 6},
                {4, 8, 9},
            };

            Matrix<decimal> matrixB = new decimal[,]
            {
                {1, 2, 4},
                {3, 4, 6},
                {4, 8, 9},
            };
            
            Matrix<decimal> expected = new decimal[,]
            {
                { 0, 3, 4 },
                { 0, 1, 0 },
                { 0, 0, 0 }
            };
            
            // Act
            var actual = matrixA - matrixB;
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void SubtractOpMatrixTest_SumTwoMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
                {4, 8, 9},
            };

            Matrix<int> matrixB = new[,]
            {
                {1, 2, 4},
                {3, 4, 6},
                {4, 8, 9},
            };
            
            Matrix<int> expected = new [,]
            {
                { 0, 3, 4 },
                { 0, 1, 0 },
                { 0, 0, 0 }
            };
            
            // Act
            var actual = matrixA - matrixB;
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void SubtractOpMatrixTest_SumTwoMatrix_AssertMustBeThrowsMatrixDotNetException()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
            };

            Matrix<int> matrixB = new[,]
            {
                {1, 2, 4},
                {3, 4, 6},
                {4, 8, 9},
            };
            
            // Assert, Act
            Assert.Throws<MatrixDotNetException>(() => matrixA - matrixB);
        }
        
        #endregion

        #region Divide
        
        [Fact]
        public void DivideOpMatrixOnConstTest_MatrixDivideToConstantLeftSide_AssertMustBeTrue()
        {
            // Arrange
            Matrix<double> matrixA = new double[,]
            {
                {1, 5, 8},
                {4, 5, 10},
            };
            
            const int k = 20;
            Matrix<double> expected = new[,]
            {
                {20, 4, 2.5},
                {5,  4, 2},
            };
            
            // Act
            var actual = k / matrixA; 
            

            // Assert
            Assert.Equal(expected,actual);
        }

        [Fact]
        public void DivideOpMatrixOnConstTest_MatrixDivideToConstantRightSide_AssertMustBeTrue()
        {
            // Arrange
            Matrix<double> matrixA = new[,]
            {
                {2, 4, 8.0},
                {6, 3, 1},
            };
            
            const int k = 2;
            Matrix<double> expected = new[,]
            {
                {1,  2,  4  },
                {3, 1.5, 0.5},
            };
            
            // Act
            var actual = matrixA / k; 
            

            // Assert
            Assert.Equal(expected,actual);
        }
        
        #endregion
    }
}