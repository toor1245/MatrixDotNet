using MatrixDotNet;
using MatrixDotNet.Extensions.Determinants;
using Xunit;

namespace MatrixDotNetTests.MatrixTests
{
    public class DeterminantTest
    {
        [Fact]
        public void CholeskyDeterminant_GetsDeterminantMatrix3x3_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                {4, 12, -16},
                {12, 37, -43},
                {-16, -43, 98}
            };

            var expected = 36;
            
            // Act
            var actual = matrix.GetCholeskyDeterminant();
            
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void LUDeterminant_GetsDeterminantMatrix3x3_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                {4, 12, -16},
                {12, 37, -43},
                {-16, -43, 98}
            };

            var expected = 36;
            
            // Act
            var actual = matrix.GetLowerUpperDeterminant();
            
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void LUPDeterminant_GetsDeterminantMatrix3x3_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                {4, 12, -16},
                {12, 37, -43},
                {-16, -43, 98}
            };

            var expected = 36;
            
            // Act
            var actual = matrix.GetLowerUpperDeterminant();
            
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void ShurDeterminant_GetsDeterminantMatrix3x3_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                {4, 12, -16,5},
                {12, 37, -43,6},
                {-16, -43, 98,10},
                {3,5,2,1}
            };

            var expected = -11595;
            
            // Act
            var actual = matrix.GetLowerUpperPermutationDeterminant();
            
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MinorDeterminant_GetsDeterminantMatrix3x3_AssertMustBeEqual()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                {4, 12, -16},
                {12, 37, -43},
                {-16, -43, 98}
            };

            var expected = 36;

            // Act
            var actual = matrix.GetDeterminant();


            // Assert
            Assert.Equal(expected, actual);
        }
    }
}