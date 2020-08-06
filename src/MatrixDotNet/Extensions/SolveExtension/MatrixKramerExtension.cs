using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Determinant;

namespace MatrixDotNet.Extensions.SolveExtension
{
    public static partial class Solve
    {
        /// <summary>
        /// Gets determinant matrix by Kramer algorithm.
        /// </summary>
        /// <param name="matrix">matrix.</param>
        /// <param name="arr">array.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        /// <exception cref="MatrixDotNetException"></exception>
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
        
        public static long[] KramerSolve(this Matrix<long> matrix,long[] arr)
        {
            if (matrix.Rows != arr.Length)
                throw new MatrixDotNetException(
                    "Rows quantity matrix not equal array quantity",nameof(matrix),nameof(arr));
            
            long det = matrix.GetDeterminant();
            
            Matrix<long> temp = matrix.Clone() as Matrix<long>;
            
            long[] result = new long[matrix.Columns];
            
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
        
        public static int[] KramerSolve(this Matrix<int> matrix,int[] arr)
        {
            if (matrix.Rows != arr.Length)
                throw new MatrixDotNetException(
                    "Rows quantity matrix not equal array quantity",nameof(matrix),nameof(arr));
            
            int det = matrix.GetDeterminant();
            
            Matrix<int> temp = matrix.Clone() as Matrix<int>;
            
            int[] result = new int[matrix.Columns];
            
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
        
        public static double[] KramerSolve(this Matrix<double> matrix,double[] arr)
        {
            if (matrix.Rows != arr.Length)
                throw new MatrixDotNetException(
                    "Rows quantity matrix not equal array quantity",nameof(matrix),nameof(arr));
            
            double det = matrix.GetDeterminant();
            Matrix<double> temp = matrix.Clone() as Matrix<double>;
            double[] result = new double[matrix.Columns];
            
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