using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Extensions.Conversion
{
    public readonly ref struct Converter
    {
        public static unsafe MatrixAsFixedBuffer AddRow(ref MatrixAsFixedBuffer matrix,double[] data,int index)
        {
            if (matrix.Columns != data.Length)
            {
                string message = $"length {nameof(data)}:{data.Length} != {nameof(matrix.Columns)} of matrix:{matrix.Columns}";
                throw new MatrixDotNetException(message);
            }
            
            fixed (double* arr = data)
            {
                byte n = matrix.Columns;
                Span<double> span3 = new Span<double>(arr,n);
                MatrixAsFixedBuffer matrixAsFixedBuffer = new MatrixAsFixedBuffer((byte)(matrix.Rows + 1),n);
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
    }
}