using System.Collections;
using System.Collections.Generic;
using MatrixDotNet;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Builder;
using Xunit;

namespace MatrixDotNetTests.MatrixTests.MatrixExtensionTests
{
    public class WhenISetDiagonal
    {
        [Fact]
        public void ShouldThrowMatrixNotSquareException_WhenRowsNotEqualColumnsOfMatrix()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                { 1, 5, 8 },
                { 3, 5, 6 }
            };
            var array = new[] { 1, 2, 3 };

            // Act, Assert
            Assert.Throws<MatrixNotSquareException>(() => matrix.SetDiagonal(array));
        }

        [Theory]
        [ClassData(typeof(WhenISetDiagonalTestData))]
        public void ShouldThrowSizeNotEqualException_WhenSizeMatrixNotEqualLengthOfArray(Matrix<int> matrix, int[] array)
        {
            // Act, Assert
            Assert.Throws<SizeNotEqualException>(() => matrix.SetDiagonal(array));
        }

        private class WhenISetDiagonalTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { BuildMatrix.RandomInt(4, 4), new[] { 1, 2, 3, 4, 5 } };
                yield return new object[] { BuildMatrix.RandomInt(4, 4), new[] { 1, 2, 3 } };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}