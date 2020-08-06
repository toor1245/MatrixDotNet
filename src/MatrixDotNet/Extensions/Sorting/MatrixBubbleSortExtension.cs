using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions.Sorting
{
    public static partial class Sort
    {
        /// <summary>
        /// Sorts matrix by rows.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        public static void BubbleSortByRows<T>(this Matrix<T> matrix) where T : unmanaged
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

        /// <summary>
        /// Sorts matrix by column.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        public static void BubbleSortByColumn<T>(this Matrix<T> matrix) where T : unmanaged
        {
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    for (int k = 0; k < matrix.Rows - 1; k++)
                    {
                        if (MathExtension.GreaterThan(matrix[k,i],matrix[k + 1,i]))
                        {
                            T temp = matrix[k,i];
                            matrix[k,i] = matrix[k + 1,i];
                            matrix[k + 1,i] = temp;
                        }
                    }
                }
            }
        }
        
        /// <summary>
        /// Sorts main diagonal of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        public static void BubbleSortMainDiagonal<T>(this Matrix<T> matrix) where T : unmanaged
        {
            int x = matrix.Rows;
            int y = matrix.Columns;
            int c = x & ((x - y) >> 31) | y & (~(x - y) >> 31);
            
            for (int i = 0; i < x; i++)
            {
                for (int k = i + 1; k < c; k++)
                {
                    if (MathExtension.GreaterThan(matrix[i,i],matrix[k,k]))
                    {
                        T temp = matrix[i,i];
                        matrix[i,i] = matrix[k,k];
                        matrix[k,k] = temp;
                    }
                }
            }
        }
        
        /// <summary>
        /// Sorts main diagonal of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        public static void BubbleSortMinorDiagonal<T>(this Matrix<T> matrix) where T : unmanaged
        {
            int x = matrix.Rows;
            int y = matrix.Columns;
            int c = x & ((x - y) >> 31) | y & (~(x - y) >> 31);
            
            for (int l = 0; l < x; l++)
            {
                for (int i = 0, k = y - 1; i < x; i++,k--)
                {
                    for (int j = i + 1; j < c; j++)
                    {
                        if (MathExtension.GreaterThan(matrix[i,k],matrix[j,k - 1]))
                        {
                            T temp = matrix[i,k];
                            matrix[i,k] = matrix[j,k - 1];
                            matrix[j,k - 1] = temp;
                        }    
                    }
                }
            }
        }
        
        /// <summary>
        /// Sorts whole matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        public static void BubbleSort<T>(this Matrix<T> matrix) where T : unmanaged
        {

            for (int j = 0; j < matrix.Length; j++)
            {
                for (int k = 0; k < matrix.Rows; k++)
                {
                    for (int l = 0; l < matrix.Columns; l++)
                    {
                        if (l + 1 == matrix.Columns && k + 1 == matrix.Rows) continue;

                        if (l + 1 == matrix.Columns && k + 1 != matrix.Rows &&
                            MathExtension.GreaterThan(matrix[k, l], matrix[k + 1, 0]))
                        {
                            var temp = matrix[k, l];
                            matrix[k, l] = matrix[k + 1, 0];
                            matrix[k + 1, 0] = temp;
                        }
                        else if (l + 1 != matrix.Columns)
                        {
                            if (MathExtension.GreaterThan(matrix[k, l], matrix[k, l + 1]))
                            {
                                var temp = matrix[k, l];
                                matrix[k, l] = matrix[k, l + 1];
                                matrix[k, l + 1] = temp;
                            }
                        }
                    }
                }
            }
        }
    }
}