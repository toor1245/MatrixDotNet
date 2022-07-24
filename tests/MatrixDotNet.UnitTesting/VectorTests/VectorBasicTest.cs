using MatrixDotNet.Exceptions;
using MatrixDotNet.Vectorization;
using Xunit;

namespace MatrixDotNetTests.VectorTests
{
    public class VectorBasicTest
    {
        #region Ctor

        [Fact]
        public void CtorTest_ChecksLengthOfVectorAfterInit_AssertMustBeEqual()
        {
            // Arrange
            const int expected = 10;

            // Act
            var actual = new Vector<int>(expected);
            var actual2 = new Vector<int>(expected, 1);

            // Assert
            Assert.Equal(expected, actual.Length);
            Assert.Equal(expected, actual2.Length);
        }

        #endregion

        #region Equals

        [Fact]
        public void EqualsTest_ChecksTwoVectorsByReference_AssertMustBeEqual()
        {
            // Arrange
            var v1 = new Vector<int>(16, 1);
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
            var v1 = new Vector<int>(16, 1);
            var v2 = new Vector<int>(16, 2);
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
            var v1 = new Vector<int>(16, 1);
            var v2 = new Vector<int>(15, 1);
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
            Vector<double> v1 = new[] { 1.32, 2, .324, 23.23 };
            Vector<double> v2 = new[] { 1.32, 2, .324, 23.23 };
            const bool expected = true;

            // Act
            var actual = v1 == v2;

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion

        #region GetDistancePoint

        [Fact]
        public void GetDistancePointVectorTest_AssertMustBeEqual()
        {
            // Arrange
            Vector<int> v1 = new[] { 4, 2, 1, 1, 2, 3, 4, 6, 9 };
            Vector<int> v2 = new[] { 4, 2, 4, 1, 3, 4, 5, 7, 11 };
            Vector<int> expected = new[] { 0, 0, 3, 0, 1, 1, 1, 1, 2 };

            // Act
            var actual = VectorExtension.GetDistancePoint(v1, v2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDistancePointTest_AssertMustBeEqual()
        {
            // Arrange
            Vector<int> v1 = new[] { 4, 2, 1 };
            Vector<int> v2 = new[] { 4, 2, 4 };
            Vector<int> expected = new[] { 0, 0, 3 };

            // Act
            var actual = VectorExtension.GetDistancePoint(v1, v2);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDistancePointTest_ThrowsMatrixDotNetException()
        {
            // Arrange
            Vector<int> v1 = new[] { 4, 2, 1, 5 };
            Vector<int> v2 = new[] { 4, 2, 4 };

            // Act Assert
            Assert.Throws<SizeNotEqualException>(() => VectorExtension.GetDistancePoint(v1, v2));
        }

        #endregion

        #region GetDirectCos

        [Fact]
        public void GetDirectCosTest_AssertMustBeEqual()
        {
            // Arrange
            Vector<double> va = new double[] { 1, 2, 3 };
            Vector<double> expected = new[] { 0.2672612419124244, 0.5345224838248488, 0.8017837257372732 };

            // Act
            var actual = VectorExtension.GetDirectCos(va);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetDirectCosTest2_AssertMustBeEqual()
        {
            // Arrange
            Vector<double> va = new double[] { 1, 2, 3 };
            Vector<double> vb = new double[] { 4, 5, 6 };
            Vector<double> expected = new[] { 0.5773502691896257, 0.5773502691896257, 0.5773502691896257 };

            // Act
            var actual = VectorExtension.GetDirectCos(va, vb);

            // Assert
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}