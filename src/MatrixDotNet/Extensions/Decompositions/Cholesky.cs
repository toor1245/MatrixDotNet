using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.Extensions.Decompositions
{
    public static partial class Decomposition
    {
        public static void GetCholesky(this Matrix<double> matrix, out Matrix<double> lower,
            out Matrix<double> transpose)
        {
            if (!matrix.IsSquare) throw new MatrixNotSquareException();
            if (!matrix.IsSymmetric) throw new MatrixNotSymmetricException();

            var m = matrix.Rows;
            var n = matrix.Columns;

            lower = new Matrix<double>(m, n);

            // Decomposing a matrix  
            // into Lower Triangular 
            for (var i = 0; i < n; i++)
            for (var j = 0; j <= i; j++)
            {
                double sum = 0;

                // summation for diagnols 
                if (j == i)
                {
                    for (var k = 0; k < j; k++) sum += (int) System.Math.Pow(lower[j, k], 2);

                    lower[j, j] = (int) System.Math.Sqrt(matrix[j, j] - sum);
                }

                else
                {
                    // Evaluating L(i, j)  
                    // using L(j, j) 
                    for (var k = 0; k < j; k++) sum += lower[i, k] * lower[j, k];

                    lower[i, j] = (matrix[i, j] - sum) / lower[j, j];
                }
            }

            transpose = lower.Transpose();
        }

        public static void GetCholesky(this Matrix<float> matrix, out Matrix<float> lower, out Matrix<float> transpose)
        {
            if (!matrix.IsSquare) throw new MatrixNotSquareException();
            if (!matrix.IsSymmetric) throw new MatrixNotSymmetricException();

            var m = matrix.Rows;
            var n = matrix.Columns;

            lower = new Matrix<float>(m, n);

            // Decomposing a matrix  
            // into Lower Triangular 
            for (var i = 0; i < n; i++)
            for (var j = 0; j <= i; j++)
            {
                float sum = 0;

                // summation for diagnols 
                if (j == i)
                {
                    for (var k = 0; k < j; k++) sum += (int) System.Math.Pow(lower[j, k], 2);

                    lower[j, j] = (int) System.Math.Sqrt(matrix[j, j] - sum);
                }
                else
                {
                    // Evaluating L(i, j)  
                    // using L(j, j) 
                    for (var k = 0; k < j; k++) sum += lower[i, k] * lower[j, k];

                    lower[i, j] = (matrix[i, j] - sum) / lower[j, j];
                }
            }

            transpose = lower.Transpose();
        }

        public static void GetCholesky(this Matrix<decimal> matrix, out Matrix<decimal> lower,
            out Matrix<decimal> transpose)
        {
            if (!matrix.IsSquare) throw new MatrixNotSquareException();
            if (!matrix.IsSymmetric) throw new MatrixNotSymmetricException();

            var m = matrix.Rows;
            var n = matrix.Columns;

            lower = new Matrix<decimal>(m, n);

            // Decomposing a matrix  
            // into Lower Triangular 
            for (var i = 0; i < n; i++)
            for (var j = 0; j <= i; j++)
            {
                decimal sum = 0;

                // summation for diagnols 
                if (j == i)
                {
                    for (var k = 0; k < j; k++) sum += (int) System.Math.Pow((double) lower[j, k], 2);

                    lower[j, j] = (int) System.Math.Sqrt((double) (matrix[j, j] - sum));
                }

                else
                {
                    // Evaluating L(i, j)  
                    // using L(j, j) 
                    for (var k = 0; k < j; k++) sum += lower[i, k] * lower[j, k];

                    lower[i, j] = (matrix[i, j] - sum) / lower[j, j];
                }
            }

            transpose = lower.Transpose();
        }
    }
}