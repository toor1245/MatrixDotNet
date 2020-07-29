using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        public static Matrix<T> Concat<T>(this Matrix<T> matrix1,Matrix<T> matrix2) 
            where T : unmanaged
        {
            if (matrix1.Rows != matrix2.Rows)
                throw new MatrixDotNetException("Rows must be equals");
            
            Matrix<T> res = new Matrix<T>(matrix1.Rows,matrix1.Columns + matrix2.Columns);
            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0, k = 0; j < matrix1.Columns + matrix2.Columns; j++)
                {
                    if (j < matrix1.Columns)
                    {
                        res[i, j] = matrix1[i, j];
                    }
                    else
                    {
                        res[i, k + matrix1.Columns ] = matrix2[i, k];
                        k++;
                    }
                }
            }
            return res;
        }
    }
}