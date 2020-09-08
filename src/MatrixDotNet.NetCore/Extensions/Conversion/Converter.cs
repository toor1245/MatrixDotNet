using System;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Core.Optimization;

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

        public static unsafe void SwapRows(ref MatrixAsFixedBuffer matrix, int from, int to)
        {
            if (Avx.IsSupported)
            {
                int i = 0;
                int n = matrix.Columns;
                fixed (double* ptr1 = matrix[from])
                fixed (double* ptr2 = matrix[to])
                {

                    while (i < matrix.Columns - Vector256<double>.Count)
                    {
                        var vector2 = Avx.LoadVector256(ptr2 + i);
                        var vector1 = Avx.LoadVector256(ptr1 + i);
                        Avx.Store(ptr2 + i, vector1);
                        Avx.Store(ptr1 + i, vector2);
                        i += 4;
                    }

                    while (i < matrix.Columns)
                    {
                        var temp = matrix[from,i];
                        matrix[from,i] = matrix[to,i];
                        matrix[to,i] = temp;
                        i++;
                    }
                    
                }
            }
            else
            {

                fixed (double* ptr1 = matrix._array)
                {
                    Span<double> span = new Span<double>(ptr1,matrix.Length);
                    int m = matrix.Rows;
                    int n = matrix.Columns;
                    var index = from * n + m;
                    var i = from * n;
                    var j = to * n;

                    while (i < index)
                    {
                        var tmp = span[i];
                        span[i] = span[j];
                        span[j] = tmp;
                        i++; j++;
                    }
                }
            }
        }
    }
}