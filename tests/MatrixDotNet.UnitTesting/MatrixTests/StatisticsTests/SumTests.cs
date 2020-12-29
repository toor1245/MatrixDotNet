using System;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using Xunit;

namespace MatrixDotNetTests.MatrixTests.StatisticsTests
{
    public class SumTests
    {
        #region Sum

        [Fact]
        public void SumTest_SumAllElementsOfMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {2, 3, 4, 5, 6},
                {2, 9, 2, 8, 1},
                {3, 4, 7, 3, 1}
            };

            // Act
            int expected = 60;
            int actual = matrix.Sum();
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void SumByRowTest_SumAllElementsInDimensionZeroOfMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {2, 3, 4, 5, 6},
                {2, 9, 2, 8, 1},
                {3, 4, 7, 3, 1}
            };

            // Act
            int expected = 20;
            int actual = matrix.SumByRow(0);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void SumByColumnTest_SumAllElementsInDimensionOneByColumnOfMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {2, 3, 4, 5, 6},
                {2, 9, 2, 8, 1},
                {3, 4, 7, 3, 1}
            };

            // Act
            int expected = 16;
            int actual = matrix.SumByColumn(1);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void SumByRowsTest_SumAllElementsInDimensionsByRowsOfMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {2, 3, 4, 5, 6},
                {2, 9, 2, 8, 1},
                {3, 4, 7, 3, 1}
            };

            // Act
            int[] expected = {20,22,18};
            int[] actual = matrix.SumByRows();
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void SumByColumnsTest_SumAllElementsInDimensionsByColumnsOfMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {2, 3, 4, 5, 6},
                {2, 9, 2, 8, 1},
                {3, 4, 7, 3, 1}
            };

            // Act
            int[] expected = {7,16,13,16,8};
            int[] actual = matrix.SumByColumns();
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        #endregion
        
        #region Kahan
        [Fact]
        public void KahanSumDoubleTest_SumAllElementsMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[10, 10];
            matrix[0, 0] = Math.Pow(10, 16);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 1; j < matrix.Columns; j++)
                {
                    matrix[i, j] = 1;
                }
            }

            // Act
            var expected = Math.Pow(10, 16) + 80;
            var actual = matrix.GetKahanSum();
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        
        [Fact]
        public void SumDoubleTest_SumAllElementsMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[10, 10];
            matrix[0, 0] = Math.Pow(10, 16);

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 1; j < matrix.Columns; j++)
                {
                    matrix[i, j] = 1;
                }
            }

            // Act
            var expected = Math.Pow(10, 16);
            var actual = matrix.Sum();
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void SumFloatKahanKlainTest_SumAllElementsMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<float> matrix = new float[100, 100];
            matrix[0, 0] = (float)Math.Pow(10,8);
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 1; j < matrix.Columns; j++)
                {
                    matrix[i,j] = 1;
                }
            }

            // Act
            float actual = matrix.Sum();
            float actual2 = matrix.GetKahanSum();
            float actual3 = matrix.GetKleinSum();
            
            // Assert
            Assert.Equal(actual,actual2);
            Assert.NotEqual(actual,actual3);
        }
        #endregion

        #region Klain
        
        [Fact]
        public void SumFloatKahanGetKlainTest_SumAllElementsMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<float> matrix = new Matrix<float>(100,100,1);
            matrix[0, 0] = (float)Math.Pow(10,8);
            

            // Act
            float actual = matrix.Sum();
            float actual3 = matrix.GetKleinSum();
            
            // Assert
            Assert.NotEqual(actual,actual3);
        }
        
        [Fact]
        public void GetSumKahanByRows_And_GetKlainByRowsTest_SumAllElementsMatrixByColumns_AssertMustBeEqual()
        {
            // Arrange
            Matrix<float> matrix = new Matrix<float>(100, 100, 1)
            {
                [0, 0] = (float) Math.Pow(10, 8)
            };
            
            // Act
            float actual = matrix.SumByColumn(0);
            float actual3 = matrix.GetKleinSum(0,State.Column);

            // Assert
            Assert.NotEqual(actual,actual3);
        }
        #endregion
    }
}