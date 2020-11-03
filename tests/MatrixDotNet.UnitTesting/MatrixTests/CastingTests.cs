using MatrixDotNet;
using NUnit.Framework;
using System;

namespace MatrixDotNetTests.MatrixTests
{
    [TestFixture]
    class CastingTests
    {
        [Test]
        public void FromJaggedArray_HappyPath()
        {
            Matrix<int> expected = new Matrix<int>(new [,]
            {
                { 15, 67, 97 },
                { 98, 1, 7 },
                { 5, 69, 9 }
            });

            int[][] array = {
                new []{ 15, 67, 97 },
                new []{ 98, 1, 7 },
                new []{ 5, 69, 9 }
            };

            Matrix<int> matrix = array;

            Assert.AreEqual(expected, matrix);
        }

        [Test]
        public void FromJaggedArray_DifferentLength()
        {
            Matrix<int> expected = new Matrix<int>(new [,]
            {
                { 15, 67, 0 },
                { 98, 0, 0 },
                { 5, 69, 9 }
            });

            int[][] array = {
                new []{ 15, 67 },
                new []{ 98 },
                new []{ 5, 69, 9}
            };

            Matrix<int> matrix = array;

            Assert.AreEqual(expected, matrix);
        }

        [Test]
        public void FromJaggedArray_NullSubArray()
        {
            int[][] array =
            {
                new[] {15, 67, 97},
                null,
                new[] {5, 69}
            };

            Assert.Catch<NullReferenceException>(() =>
            {
                Matrix<int> matrix = array;
            });
        }

        [Test]
        public void FromJaggedArray_EmptyArray()
        {
            int[][] array = {};

            Assert.Catch<Exception>(() =>
            {
                Matrix<int> matrix = array;
            });
        }
    }
}
