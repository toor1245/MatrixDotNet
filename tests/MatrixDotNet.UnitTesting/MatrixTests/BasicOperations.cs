using System.Threading.Tasks;
using MatrixDotNet;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions;
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
            
            Matrix<int> expected = new [,]
            {
                {48,86,106},
                {42,74,96}
            };
            
            // Act
            Matrix<int> actual = matrixA * matrixB;

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Theory]
        [InlineData(16)]
        [InlineData(32)]
        [InlineData(48)]
        [InlineData(64)]
        [InlineData(128)]
        public void MatrixMulTest16_MultiplyTwoMatrix_AssertMustBeEqual(int n)
        {
            // Arrange
            Matrix<float> matrixA = BuildMatrix.RandomFloat(n, n, -10, 10);
            Matrix<float> matrixB = BuildMatrix.RandomFloat(n, n, -10, 10);
            Matrix<float> expected = matrixA * matrixB;

            // Act
            Matrix<float> actual = Simd.BlockMultiply(matrixA, matrixB);

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

        #region Sum

        [Fact]
        public void SumTest_CheckDouble_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrixA = new double[,]
            {
                {1, 5, 8},
                {4, 5, 10},
            };
            
            double expected = 33;

            // Act
            var actual = matrixA.Sum();
            

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void SumTest_CheckDoubleWith_Simd_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrixA = new double[,]
            {
                {1, 5, 8},
                {4, 5, 10},
                {1, 5, 8},
                {4, 5, 10},
                {1, 5, 8},
                {4, 5, 10},
            };
            
            double expected = 99;

            // Act
            var actual = matrixA.Sum();
            
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void SumTest_CheckFloat_AssertMustBeEqual()
        {
            // Arrange
            Matrix<float> matrixA = new float[,]
            {
                {1, 5, 8},
                {4, 5, 10},
            };
            
            float expected = 33;

            // Act
            var actual = matrixA.Sum();
            

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void SumTest_CheckFloatWith_Simd_AssertMustBeEqual()
        {
            // Arrange
            Matrix<float> matrixA = new float[,]
            {
                {1, 5, 8},
                {4, 5, 10},
                {1, 5, 8},
                {4, 5, 10},
                {1, 5, 8},
                {4, 5, 10},
            };
            
            float expected = 99;

            // Act
            var actual = matrixA.Sum();
            
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void SumTest_CheckInt_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {4, 5, 10},
            };
            
            int expected = 33;

            // Act
            var actual = matrixA.Sum();
            

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void SumTest_CheckIntWith_Simd_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {4, 5, 10},
                {1, 5, 8},
                {4, 5, 10},
                {1, 5, 8},
                {4, 5, 10},
            };
            
            int expected = 99;

            // Act
            var actual = matrixA.Sum();
            
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void SumTest_CheckShort_AssertMustBeEqual()
        {
            // Arrange
            Matrix<short> matrixA = new short[,]
            {
                {1, 5, 8},
                {4, 5, 10},
            };
            
            short expected = 33;

            // Act
            var actual = matrixA.Sum();
            

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void SumTest_CheckShortWith_Simd_AssertMustBeEqual()
        {
            // Arrange
            Matrix<short> matrixA = new short[,]
            {
                {1, 5, 8},
                {4, 5, 10},
                {1, 5, 8},
                {4, 5, 10},
                {1, 5, 8},
                {4, 5, 10},
            };
            
            short expected = 99;

            // Act
            var actual = matrixA.Sum();
            
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        #endregion
    }
}