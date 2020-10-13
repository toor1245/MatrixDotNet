namespace MatrixDotNet.Extensions.Core.Simd
{
    public static partial class Simd
    {
        /*private static unsafe Matrix<int> Multiply(Matrix<int> matrixA,Matrix<int> matrixB)
        {
            Matrix<int> matrix = new Matrix<int>(matrixA.Rows,matrixB.Columns);
            int m = matrixA.Rows;
            int n = matrixB.Columns;
            int K = matrixA.Columns;
            

            fixed(int* ptr1 = matrixA.GetMatrix())
            fixed(int* ptr2 = matrixB.GetMatrix())
            fixed(int* ptr3 = matrix.GetMatrix())
            {
                int i = 0, j = 0,k = 0;
                for (; i < m; i += 8)
                {
                    var c = Avx.LoadVector256(ptr3 + i);
                    for (; j < n; j += 8)
                    {
                        var b = Avx.LoadVector256(&matrix[i,j]);
                        for (; k < K; k += 8)
                        {
                            
                        }
                    }
                }
            }

            return matrix;
        }
        */
    }
}