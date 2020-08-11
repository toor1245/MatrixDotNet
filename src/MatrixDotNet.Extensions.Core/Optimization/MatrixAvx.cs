using System;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace MatrixDotNet.Extensions.Core.Optimization
{
    public class MatrixAvx
    {
        public Vector<int>[] _matrix;
        private int dimension = -1;

        public MatrixAvx(int length)
        {
            _matrix = new Vector<int>[length - (length % 8)];
        }
        public void Add(Vector<int> matrix)
        {
            _matrix[dimension++] = matrix;
        }

        public int this[int index]
        {
            get
            {
                unsafe
                {
                    fixed (Vector<int>* pointer = _matrix )
                    {
                        Span<int> span = new Span<int>(pointer,_matrix.Length);
                        return span[index];
                    }
                }
            }
        }
    }
}