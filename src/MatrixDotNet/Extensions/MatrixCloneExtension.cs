namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        public static void Clone<T>(Matrix<T> matrix1,int dimension1, int start,Matrix<T> matrix2,int dimension2,int destinationIndex,int length) 
            where T : unmanaged
        {
            for (int i = start, k = destinationIndex; k < length; i++,k++)
            {
                matrix2[dimension2,k] = matrix1[dimension1,i];
            }
        }
        
    }
}