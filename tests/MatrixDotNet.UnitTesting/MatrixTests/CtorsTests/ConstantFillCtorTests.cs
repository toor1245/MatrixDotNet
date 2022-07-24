using System;
using MatrixDotNet;
using NUnit.Framework;

namespace MatrixDotNetTests.MatrixTests.CtorsTests
{
    [TestFixture]
    public class ConstantFillCtorTests
    {
        [TestCase(15, 15, (sbyte)-7)]
        [TestCase(15, 15, (byte)7)]
        [TestCase(15, 15, (short)-7)]
        [TestCase(5, 15, (ushort)7)]
        [TestCase(15, 15, -7L)]
        [TestCase(15, 1, 7UL)]
        [TestCase(15, 15, -7)]
        [TestCase(15, 125, 7U)]
        [TestCase(15, 15, 7.5)]
        [TestCase(15, 15, 7.5f)]
        public void FloatTest<T>(int rows, int cols, T value)
            where T : unmanaged
        {
            var arr = new T[rows * cols];
            Array.Fill(arr, value);

            var matrix = new Matrix<T>(rows, cols, value);

            Assert.AreEqual(matrix.GetArray(), arr);
        }
    }
}