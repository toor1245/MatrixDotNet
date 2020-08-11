using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace MatrixDotNet.Extensions.Core.Optimization
{
    public static partial class Optimization
    {
        public static unsafe MatrixAvx MultiplyAvx(Matrix<int> matrixA, int value)
        {
            int length = matrixA.Length;

            MatrixAvx matrix = new MatrixAvx(6);
            
            int size = 8;
            fixed(int* pointer1 = matrixA.GetMatrix())
            {
                Span<int> span = new Span<int>(pointer1,length);
                Vector<int> mul = new Vector<int>(value);
                int i = 0;
                
                while (i < length - size)
                {
                    Vector<int> vector = new Vector<int>(span.Slice(i,8));
                    vector = Vector.Multiply(vector, mul);
                    matrix.Add(vector);
                    i += 4;
                }
            }

            return matrix;
        }
        
        public static unsafe Matrix<int> MultiplyAvx(Matrix<int> matrixA,Matrix<int> matrixB)
        {
            Matrix<int> matrix = new Matrix<int>(matrixA.Rows,matrixB.Columns);
            int length = matrix.Length;
            
            fixed(int* pointer1 = matrixA.GetMatrix())
            fixed(int* pointer2 = matrixB.GetMatrix())
            {
                Span<int> span1 = new Span<int>(pointer1,matrixA.Length);
                Span<int> span2 = new Span<int>(pointer2,matrixB.Length);
                Span<int> span3 = new Span<int>(pointer2,matrix.Length);
            }

            return matrix;
        }
    }
}