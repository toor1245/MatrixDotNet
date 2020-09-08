using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Extensions.Conversion
{
    /// <summary>
    /// Represents conversion operations for matrix with fixed buffer size.
    /// </summary>
    public readonly ref struct Converter
    {
        /// <summary>
        /// Adds row for matrix with fixed buffer size.
        /// </summary>
        /// <param name="matrix">the matrix</param>
        /// <param name="data">the data which assign by index</param>
        /// <param name="index">the row index</param>
        /// <returns>Matrix with new row.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe MatrixAsFixedBuffer AddRow(ref MatrixAsFixedBuffer matrix,double[] data,int index)
        {
            if (matrix.Columns != data.Length)
            {
                var message = $"length {nameof(data)}:{data.Length} != {nameof(matrix.Columns)} of matrix:{matrix.Columns}";
                throw new MatrixDotNetException(message);
            }
            
            fixed (double* arr = data)
            {
                var n = matrix.Columns;
                var span3 = new Span<double>(arr,n);
                var matrixAsFixedBuffer = new MatrixAsFixedBuffer((byte)(matrix.Rows + 1),n);
                
                for (int i = 0; i < index; i++)
                {
                    matrixAsFixedBuffer[i] = matrix[i];
                }
                
                matrixAsFixedBuffer[index] = span3;
                
                for (int i = index + 1; i < matrixAsFixedBuffer.Rows; i++)
                {
                    matrixAsFixedBuffer[i] = matrix[i - 1];
                }

                return matrixAsFixedBuffer;
            }
        }
        
        /// <summary>
        /// Adds column for matrix with fixed buffer size.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="data">the data.</param>
        /// <param name="index">the column index.</param>
        /// <returns>Matrix with new column.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe MatrixAsFixedBuffer AddColumn(ref MatrixAsFixedBuffer matrix,double[] data,int index)
        {
            if (matrix.Rows != data.Length)
            {
                string message = $"length {nameof(data)}:{data.Length} != {nameof(matrix.Rows)} of matrix:{matrix.Rows}";
                throw new MatrixDotNetException(message);
            }

            fixed (double* arr = data)
            {
                var m = matrix.Rows;
                var span3 = new Span<double>(arr,m);
                var matrixAsFixedBuffer = new MatrixAsFixedBuffer(m,(byte)(matrix.Columns + 1));
                
                for (int i = 0; i < index; i++)
                {
                    matrixAsFixedBuffer.SetColumn(i,matrix.GetColumn(i));
                }
                
                matrixAsFixedBuffer.SetColumn(index,span3);
                
                for (int i = index + 1; i < matrixAsFixedBuffer.Columns; i++)
                {
                    matrixAsFixedBuffer.SetColumn(i,matrix.GetColumn(i - 1));
                }

                return matrixAsFixedBuffer;
            }
        }

        public static unsafe  void SwapRows(ref MatrixAsFixedBuffer matrix, int from, int to)
        {

            int i = 0;
            int size = Vector256<double>.Count;
            fixed (double* ptr1 = matrix[from])
            fixed(double* ptr2 = matrix[to])
            {
                while (i < matrix.Columns)
                {
                    var vector1 = Avx.LoadVector256(ptr1 + i);
                    var vector2 = Avx.LoadVector256(ptr2 + i);
                    Avx.Store(ptr2,vector1);
                    Avx.Store(ptr1,vector2);
                    i += 4;
                }

            }

        }
    }
}