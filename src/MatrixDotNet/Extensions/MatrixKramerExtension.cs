using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension  
    {
        public static long[] KramerSolve(this Matrix<long> matrix,long[] arr)
        {
            if (matrix.Rows != arr.Length)
                throw new MatrixDotNetException(
                    "Rows quantity matrix not equal array quantity",nameof(matrix),nameof(arr));
            
            long det = matrix.GetDeterminate();
            
            Matrix<long> temp = matrix.Clone() as Matrix<long>;
            
            long[] result = new long[matrix.Columns];
            
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    if (temp != null) temp[j, i] = arr[j];
                }
                result[i] = temp.GetDeterminate() / det;
            }
            return result;
        }
        
        public static int[] KramerSolve(this Matrix<int> matrix,int[] arr)
        {
            if (matrix.Rows != arr.Length)
                throw new MatrixDotNetException(
                    "Rows quantity matrix not equal array quantity",nameof(matrix),nameof(arr));
            
            int det = matrix.GetDeterminate();
            
            Matrix<int> temp = matrix.Clone() as Matrix<int>;
            
            int[] result = new int[matrix.Columns];
            
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    if (temp != null) temp[j, i] = arr[j];
                }
                result[i] = temp.GetDeterminate() / det;
            }
            return result;
        }
        
        public static double[] KramerSolve(this Matrix<double> matrix,double[] arr)
        {
            if (matrix.Rows != arr.Length)
                throw new MatrixDotNetException(
                    "Rows quantity matrix not equal array quantity",nameof(matrix),nameof(arr));
            
            double det = matrix.GetDeterminate();
            Matrix<double> temp = matrix.Clone() as Matrix<double>;
            double[] result = new double[matrix.Columns];
            
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    if (temp != null) temp[j, i] = arr[j];
                }
                result[i] = temp.GetDeterminate() / det;
            }
            return result;
        }
        
        public static float[] KramerSolve(this Matrix<float> matrix,float[] arr)
        {
            if (matrix.Rows != arr.Length)
                throw new MatrixDotNetException(
                    "Rows quantity matrix not equal array quantity",nameof(matrix),nameof(arr));
            
            float det = matrix.GetDeterminate();
            
            Matrix<float> temp = matrix.Clone() as Matrix<float>;
            
            float[] result = new float[matrix.Columns];
            
            for (int i = 0; i < matrix.Columns; i++)
            {
                for (int j = 0; j < matrix.Rows; j++)
                {
                    if (temp != null) temp[j, i] = arr[j];
                }
                result[i] = temp.GetDeterminate() / det;
            }
            return result;
        }
    }
}