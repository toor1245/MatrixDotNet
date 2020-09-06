using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Core.Extensions.Sorting
{
    public struct BubbleSortFixed : ISorting
    {
        public void SortByRows(ref MatrixAsFixedBuffer matrix)
        {
            int n = matrix.Columns;
            for (int i = 0; i < matrix.Rows; i++)
            {
                var span = matrix[i];
                
                for (int j = 0; j < span.Length; j++)
                {
                    for (int k = j + 1; k < span.Length; k++)
                    {
                        if (span[j] > span[k])
                        {
                            var temp = span[j];
                            span[j] = span[k];
                            span[k] = temp;
                        }
                    }
                }
            }
        }

        public void SortByColumns(ref MatrixAsFixedBuffer matrix)
        {
            int n = matrix.Columns;
            for (int i = 0; i < n; i++)
            {
                var span = matrix.GetColumn(i);
                for (int j = 0; j < span.Length; j++)
                {
                    for (int k = j + 1; k < span.Length; k++)
                    {
                        if (span[j] > span[k])
                        {
                            var temp = span[j];
                            span[j] = span[k];
                            span[k] = temp;
                        }
                    }
                }
            }
        }

        public void SortMinorDiagonal(ref MatrixAsFixedBuffer matrix)
        {
            if (!matrix.IsSquare)
                throw new MatrixDotNetException("Matrix is not square");
            
        }

        public void SortMainDiagonal(ref MatrixAsFixedBuffer matrix)
        {
            if (!matrix.IsSquare)
                throw new MatrixDotNetException("Matrix is not square");

            for (int i = 0; i < matrix.Rows; i++)
            {
                var slice = matrix.Data.Slice(i + matrix.Columns * i, 1);
                for (int j = i + 1; j < matrix.Rows; j++)
                {
                    var slice2 = matrix.Data.Slice(j + matrix.Columns * j, 1);
                    if (slice[0] > slice2[0])
                    {
                        var temp = slice[0];
                        slice[0] = slice2[0];
                        slice2[0] = temp;
                    }
                }
            }
        }
        
        public void Sort(ref MatrixAsFixedBuffer matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = i + 1; j < matrix.Length; j++)
                {
                    if (matrix.Data[i] > matrix.Data[j])
                    {
                        var temp = matrix.Data[i];
                        matrix.Data[i] = matrix.Data[j];
                        matrix.Data[j] = temp;
                    }   
                }
            }
        }
    }
}