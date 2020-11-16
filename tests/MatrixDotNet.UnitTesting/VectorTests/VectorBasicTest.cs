using MatrixDotNet;
using Xunit;

namespace MatrixDotNetTests.VectorTests
{
    public class VectorBasicTest
    {
        [Fact]
        public void CtorTest_ChecksLengthOfVectorAfterInit_AssertMustBeEqual()
        {
            // Arrange
            const int expected = 10;

            // Act
            var actual = new Vector<int>(expected);
            var actual2 = new Vector<int>(expected,1);
            
            // Assert
            Assert.Equal(expected, actual.Length);
            Assert.Equal(expected, actual2.Length);
        }
        
        [Fact]
        public void EqualsTest_ChecksTwoVectorsByReference_AssertMustBeEqual()
        {
            // Arrange
            var v1 = new Vector<int>(16,1);
            var v2 = v1;
            const bool expected = true;
            
            // Act
            var actual = v1 == v2;
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void EqualsTest_ChecksTwoVectors_AssertMustBeEqual()
        {
            // Arrange
            var v1 = new Vector<int>(16,1);
            var v2 = new Vector<int>(16,2);
            const bool expected = false;
            
            // Act
            var actual = v1 == v2;
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void EqualsTest_ChecksTwoVectorsByLength_AssertMustBeNotEqual()
        {
            // Arrange
            var v1 = new Vector<int>(16,1);
            var v2 = new Vector<int>(15,1);
            const bool expected = true;
            
            // Act
            var actual = v1 == v2;
            
            // Assert
            Assert.NotEqual(expected, actual);
        }
        
        [Fact]
        public void EqualsTest_ChecksTwoVectorsOnFloatType_AssertMustBeEqual()
        {
            // Arrange
            Vector<double> v1 = new[] {1.32, 2, .324, 23.23 };
            Vector<double> v2 = new[] {1.32, 2, .324, 23.23 };
            const bool expected = true;
            
            // Act
            var actual = v1 == v2;
            
            // Assert
            Assert.Equal(expected, actual);
        }
    }
}