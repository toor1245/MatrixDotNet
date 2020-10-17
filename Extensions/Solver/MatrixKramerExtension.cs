using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Determinants;

namespace MatrixDotNet.Extensions.Solver
{
    public static partial class Solve
    {
        /// <summary>
        /// Gets determinant matrix by Kramer algorithm.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="arr">array.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Gets array x.</returns>
        /// <exception cref="MatrixDotNetException">
        /// array length not equal matrix rows.
        /// </exception>
        public static double[] KramerSolve<T>(this Matrix<T> matrix,T[] arr) where T: unmanaged
        {
            if (matrix.Rows != arr.Length)
                throw new MatrixDotNetException(
                    "Rows quantity matrix not equal array quantity",nameof(matrix),nameof(arr));
            
            double det = matrix.GetDoubleDeterminant();
            Matrix<T> temp;
            double[] result = new double[matrix.Columns];
            
            for (int i = 0; i < matrix.Columns; i++)
            {
                temp = matrix.Clone() as Matrix<T>;
                for (int j = 0; j < matrix.Rows; j++)
                {
                    if (temp != null) temp[j, i] = arr[j];
                }
                result[i] = temp.GetDoubleDeterminant() / det;
            }
            return result;
        }

        
        /// <summary>
        /// Gets determinant matrix by Kramer algorithm.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="arr">array.</param>
        /// <returns>Gets array x.</returns>
        /// <exception cref="MatrixDotNetException">
        /// array length not equal matrix rows.
        /// </exception>
        public static double[] KramerSolve(this Matrix<double> matrix,double[] arr)
        {
            if (matrix.Rows != arr.Length)
                throw new MatrixDotNetException(
                    "Rows quantity matrix not equal array quantity",nameof(matrix),nameof(arr));
            
            double det = matrix.GetMinorDeterminant();
            Matrix<double> temp = matrix.Clone() as Matrix<double>;
            double[] result = new double[matrix.Columns];
            
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    if (temp != null) temp[j, i] = arr[j];
                }
                result[i] = temp.GetMinorDeterminant() / det;
            }
            return result;
        }
        
        /// <summary>
        /// Gets determinant matrix by Kramer algorithm.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="arr">array.</param>
        /// <returns>Gets array x.</returns>
        /// <exception cref="MatrixDotNetException">
        /// array length not equal matrix rows.
        /// </exception>
        public static float[] KramerSolve(this Matrix<float> matrix,float[] arr)
        {
            if (matrix.Rows != arr.Length)
                throw new MatrixDotNetException(
                    "Rows quantity matrix not equal array quantity",nameof(matrix),nameof(arr));
            
            float det = matrix.GetDeterminant();
            
            Matrix<float> temp = matrix.Clone() as Matrix<float>;
            
            float[] result = new float[matrix.Columns];
            
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    if (temp != null) temp[j, i] = arr[j];
                }
                
                result[i] = temp.GetDeterminant() / det;
            }
            return result;
        }
    }
}