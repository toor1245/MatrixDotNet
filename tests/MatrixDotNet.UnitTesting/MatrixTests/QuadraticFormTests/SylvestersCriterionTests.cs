using MatrixDotNet;
using MatrixDotNet.Extensions.Criteries;
using Xunit;

namespace MatrixDotNetTests.MatrixTests.QuadraticFormTests
{
    public class SylvestersCriterionTests
    {
        [Fact]
        public void SylvesterCriterionTest_AssertMustBe_AlternatingQuadraticForm()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                { 3, 2,  0 },
                { 2, -2, 1 },
                { 0, 1, -1 }
                
            };
            var expected = DefiniteType.Alternating;
            
            // Act 
            var actual = Criterion.SylvestersCriterion(matrix);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void SylvesterCriterionTest_AssertMustBe_PositiveQuadraticForm()
        {
            // Arrange
            Matrix<double> matrix = new[,]
            {
                {  1,  -1  },
                { -1,  1.5 }
                
            };
            var expected = DefiniteType.Positive;
            
            // Act 
            var actual = Criterion.SylvestersCriterion(matrix);
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void SylvesterCriterionTest_AssertMustBe_NegativeQuadraticForm()
        {
            // Arrange
            Matrix<double> matrix = new double[,]
            {
                {  0,  2  },
                {  2,  -1 }
                
            };
            var expected = DefiniteType.Negative;
            
            // Act 
            var actual = Criterion.SylvestersCriterion(matrix);
            
            // Assert
            Assert.Equal(expected,actual);
        }
    }
}