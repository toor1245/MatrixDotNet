using MatrixDotNet;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using NUnit.Framework;

namespace MatrixDotNetTests.MatrixTests.MatrixExtensionTests
{
    [TestFixture(typeof(int))]
    [TestFixture(typeof(double))]
    [TestFixture(typeof(float))]
    [TestFixture(typeof(decimal))]
    public class IsIdentity<T>
        where T : unmanaged
    {
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public void IsIdentity_Success(int rows, int columns)
        {
            // Arrange
            var matrix = BuildMatrix.CreateIdentityMatrix<T>(rows, columns);

            // Act
            var isIdentity = matrix.IsIdentity();

            // Assert
            Assert.IsTrue(isIdentity);
        }

        [Test]
        public void IsIdentity_ThrowsMatrixNotSquareException()
        {
            // Arrange
            const int rows = 3;
            const int columns = 4;
            var matrix = new Matrix<T>(rows, columns);

            // Act & Assert
            Assert.Throws<MatrixNotSquareException>(() => matrix.IsIdentity());
        }
    }
}
