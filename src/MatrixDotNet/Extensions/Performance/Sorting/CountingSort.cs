using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Sorting;
using MatrixDotNet.Extensions.Statistics;
using MathExtension = MatrixDotNet.Math.MathExtension;

namespace MatrixDotNet.Extensions.Core.Optimization.Unsafe.Sorting
{
    public struct CountingSort<T> : ISort<T> where T : unmanaged
    {
        public void SortByRows(Matrix<T> matrix)
        {
            throw new System.NotImplementedException();
        }

        public void SortByColumns(Matrix<T> matrix)
        {
            throw new System.NotImplementedException();
        }

        public void SortMinorDiagonal(Matrix<T> matrix)
        {
            throw new System.NotImplementedException();
        }

        public void SortMainDiagonal(Matrix<T> matrix)
        {
            throw new System.NotImplementedException();
        }

        public void Sort(Matrix<T> matrix)
        {
            if(!MathExtension.IsInteger<T>())
                throw new MatrixDotNetException("Counting sort only supported for integer types");

            T max = matrix.Max();
            
            var c = new T[MathExtension.Cast<T,int>(MathExtension.Increment(max))];
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    MathExtension.Increment(c[MathExtension.Cast<T,int>(matrix[i, j])]);
                }
            }

            int index1 = 0;
            int index2 = 0;
            
            for (int i = 0; i < c.Length; i++)
            {
                index1 = 0;
                for (int j = 0; MathExtension.GreaterThanBy(c[i],j); j++)
                {
                    index1++;
                    for (int k = 0; MathExtension.GreaterThanBy(c[j],k); k++)
                    {
                        matrix[index1,index2] = MathExtension.Cast<int,T>(MathExtension.Add(i,j));
                        index2++;
                    }

                    index2 = 0;
                }
            }
        }
    }
}