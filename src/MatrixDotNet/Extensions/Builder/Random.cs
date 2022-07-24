using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Builder
{
    public static partial class BuildMatrix
    {
        /// <summary>
        ///     Builds matrix by expression;
        /// </summary>
        /// <param name="row">row length of matrix.</param>
        /// <param name="column">column length of matrix.</param>
        /// <param name="expression">expression.</param>
        /// <param name="arg1">argument1.</param>
        /// <param name="arg2">argument2.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Creates Matrix by formula.</returns>
        /// <exception cref="MatrixDotNetException">
        ///     throws exception if arg1 or arg2 length not equal Max(row,column).
        /// </exception>
        public static Matrix<T> Build<T>(int row, int column, Func<T, T, T> expression, T[] arg1, T[] arg2)
            where T : unmanaged
        {
            var max = (column & ((row - column) >> 31)) | (row & (~(row - column) >> 31));
            if (arg1.Length != max || arg2.Length != max)
                throw new MatrixDotNetException(
                    $"array length error:\n arr1: {arg1.Length}\n arr2: {arg2.Length}\n  not equal length dimension {max} of matrix");

            var matrix = new Matrix<T>(row, column);
            for (var i = 0; i < row; i++)
            for (var j = 0; j < column; j++)
                matrix[i, j] = expression(arg1[i], arg2[j]);

            return matrix;
        }

        /// <summary>
        ///     Builds matrix by expression;
        /// </summary>
        /// <param name="row">row length of matrix.</param>
        /// <param name="column">column length of matrix.</param>
        /// <param name="expression">expression.</param>
        /// <param name="arg1">argument1.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Creates Matrix by formula.</returns>
        /// <exception cref="MatrixDotNetException">
        ///     throws exception if arg1 length not equal Max(row,column).
        /// </exception>
        public static Matrix<T> Build<T>(int row, int column, Func<T, T> expression, T[] arg1)
            where T : unmanaged
        {
            var max = (column & ((row - column) >> 31)) | (row & (~(row - column) >> 31));
            if (arg1.Length != max)
                throw new MatrixDotNetException(
                    $"array length error:\n arr1: {arg1.Length}\n not equal length dimension {max} of matrix");

            var matrix = new Matrix<T>(row, column);
            for (var i = 0; i < row; i++)
            for (var j = 0; j < column; j++)
                matrix[i, j] = expression(arg1[j]);

            return matrix;
        }

        /// <summary>
        ///     Builds matrix by expression;
        /// </summary>
        /// <param name="row">row length of matrix.</param>
        /// <param name="column">column length of matrix.</param>
        /// <param name="expression">expression.</param>
        /// <param name="arg1">argument1.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Creates Matrix by formula.</returns>
        /// <exception cref="MatrixDotNetException">
        ///     throws exception if arg1 length not equal Max(row,column).
        /// </exception>
        public static Matrix<T> Build<T>(int row, int column, Func<T, T, T> expression, T[] arg1)
            where T : unmanaged
        {
            var max = (column & ((row - column) >> 31)) | (row & (~(row - column) >> 31));
            if (arg1.Length != max)
                throw new MatrixDotNetException(
                    $"array length error:\n arr1: {arg1.Length}\n not equal length dimension {max} of matrix");

            var matrix = new Matrix<T>(row, column);
            for (var i = 0; i < row; i++)
            for (var j = 0; j < column; j++)
                matrix[i, j] = expression(arg1[j], arg1[j]);

            return matrix;
        }

        /// <summary>
        ///     Build random matrix.
        /// </summary>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <param name="row">row length of matrix</param>
        /// <param name="column">column length of matrix</param>
        /// <returns>Random matrix</returns>
        public static unsafe Matrix<T> BuildRandom<T>(int row, int column)
            where T : unmanaged
        {
            var matrix = new Matrix<T>(row, column);
            var random = new Random();

            fixed (T* p = matrix._Matrix)
            {
                var a = (byte*) p;
                var s = new Span<byte>(a, matrix._Matrix.Length * sizeof(T));
                random.NextBytes(s);
            }

            return matrix;
        }

        /// <summary>
        ///     Generates random matrix.
        /// </summary>
        /// <param name="row">number of rows</param>
        /// <param name="column">number of columns</param>
        /// <param name="startRandom">start value</param>
        /// <param name="endRandom">end value</param>
        /// <returns>Random matrix</returns>
        public static Matrix<int> RandomInt(int row, int column, int startRandom = int.MinValue,
            int endRandom = int.MaxValue)
        {
            var matrix = new Matrix<int>(row, column);
            var random = new Random();
            for (var i = 0; i < matrix._Matrix.Length; i++) matrix._Matrix[i] = random.Next(startRandom, endRandom);

            return matrix;
        }

        /// <summary>
        ///     Generates random matrix.
        /// </summary>
        /// <param name="row">number of rows</param>
        /// <param name="column">number of columns</param>
        /// <param name="startRandom">start value</param>
        /// <param name="endRandom">end value</param>
        /// <returns>Random matrix</returns>
        public static Matrix<long> RandomLong(int row, int column, int startRandom = int.MinValue,
            int endRandom = int.MaxValue)
        {
            var matrix = new Matrix<long>(row, column);
            var random = new Random();
            for (var i = 0; i < matrix._Matrix.Length; i++) matrix._Matrix[i] = random.Next(startRandom, endRandom);

            return matrix;
        }

        /// <summary>
        ///     Generates random matrix.
        /// </summary>
        /// <param name="row">number of rows</param>
        /// <param name="column">number of columns</param>
        /// <param name="startRandom">start value</param>
        /// <param name="endRandom">end value</param>
        /// <returns>Random matrix</returns>
        public static Matrix<byte> RandomByte(int row, int column, byte startRandom = byte.MinValue,
            byte endRandom = byte.MaxValue)
        {
            var matrix = new Matrix<byte>(row, column);
            var random = new Random();

            for (var i = 0; i < matrix._Matrix.Length; i++)
                matrix._Matrix[i] = (byte) random.Next(startRandom, endRandom);

            return matrix;
        }

        /// <summary>
        ///     Generates random matrix.
        /// </summary>
        /// <param name="row">number of rows</param>
        /// <param name="column">number of columns</param>
        /// <param name="startRandom">start value</param>
        /// <param name="endRandom">end value</param>
        /// <returns>Random matrix</returns>
        public static Matrix<sbyte> RandomSByte(int row, int column, sbyte startRandom = sbyte.MinValue,
            sbyte endRandom = sbyte.MaxValue)
        {
            var matrix = new Matrix<sbyte>(row, column);
            var random = new Random();

            for (var i = 0; i < matrix._Matrix.Length; i++)
                matrix._Matrix[i] = (sbyte) random.Next(startRandom, endRandom);

            return matrix;
        }

        /// <summary>
        ///     Generates random matrix.
        /// </summary>
        /// <param name="row">number of rows</param>
        /// <param name="column">number of columns</param>
        /// <param name="startRandom">start value</param>
        /// <param name="endRandom">end value</param>
        /// <returns>Random matrix</returns>
        public static Matrix<short> RandomShort(int row, int column, short startRandom = short.MinValue,
            short endRandom = short.MaxValue)
        {
            var matrix = new Matrix<short>(row, column);
            var random = new Random();
            for (var i = 0; i < row; i++)
            for (var j = 0; j < column; j++)
                matrix[i, j] = (short) random.Next(startRandom, endRandom);

            return matrix;
        }

        /// <summary>
        ///     Generates random matrix.
        /// </summary>
        /// <param name="row">number of rows</param>
        /// <param name="column">number of columns</param>
        /// <param name="startRandom">start value</param>
        /// <param name="endRandom">end value</param>
        /// <returns>Random matrix</returns>
        public static Matrix<double> RandomDouble(int row, int column, int startRandom = int.MinValue,
            int endRandom = int.MaxValue)
        {
            var matrix = new Matrix<double>(row, column);
            var random = new Random();

            for (var i = 0; i < matrix._Matrix.Length; i++) matrix._Matrix[i] = random.Next(startRandom, endRandom);

            return matrix;
        }

        /// <summary>
        ///     Generates random matrix.
        /// </summary>
        /// <param name="row">number of rows</param>
        /// <param name="column">number of columns</param>
        /// <param name="startRandom">start value</param>
        /// <param name="endRandom">end value</param>
        /// <returns>Random matrix</returns>
        public static Matrix<float> RandomFloat(int row, int column, int startRandom = int.MinValue,
            int endRandom = int.MaxValue)
        {
            var matrix = new Matrix<float>(row, column);
            var random = new Random();

            for (var i = 0; i < matrix._Matrix.Length; i++) matrix._Matrix[i] = random.Next(startRandom, endRandom);

            return matrix;
        }
    }
}