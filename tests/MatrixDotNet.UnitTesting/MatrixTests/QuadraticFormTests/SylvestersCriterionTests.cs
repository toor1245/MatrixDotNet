using System;
using MatrixDotNet;
using MatrixDotNet.Extensions.QudraticForm;
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
            var expected = Form.Alternating;
            
            // Act 
            var actual = QuadraticForm.SylvestersCriterion(matrix);
            
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
            var expected = Form.Positive;
            
            // Act 
            var actual = QuadraticForm.SylvestersCriterion(matrix);
            
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
            var expected = Form.Negative;
            
            // Act 
            var actual = QuadraticForm.SylvestersCriterion(matrix);
            
            // Assert
            Assert.Equal(expected,actual);
        }
    }
}