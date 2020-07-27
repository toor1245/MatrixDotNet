namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        public static void BubbleSort<T>(this Matrix<T> matrix) where T : unmanaged
        {
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    for (int k = 0; k < matrix.Columns - 1; k++)
                    {
                        if (MathExtension.GreaterThan(matrix[i,k],matrix[i,k + 1]))
                        {
                            T temp = matrix[i, k];
                            matrix[i, k] = matrix[i, k + 1];
                            matrix[i, k + 1] = temp;
                        }
                    }
                }
            }
        }
    }
}