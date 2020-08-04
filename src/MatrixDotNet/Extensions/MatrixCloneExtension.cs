namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        public static void CopyTo<T>(Matrix<T> matrix1,int dimension1, int start,Matrix<T> matrix2,int dimension2,int destinationIndex,int length) 
            where T : unmanaged
        {
            for (int i = start, k = destinationIndex; k < length; i++,k++)
            {
                matrix2[dimension2,k] = matrix1[dimension1,i];
            }
        }
        
        public static void CopyTo<T>(State state,Matrix<T> matrix1,int dimension1, int start,Matrix<T> matrix2,int dimension2,int destinationIndex,int length) 
            where T : unmanaged
        {
            if (state == State.Row)
            {
                for (int i = start, k = destinationIndex; k < length; i++,k++)
                {
                    matrix2[dimension2,k] = matrix1[dimension1,i];
                }    
            }

            if (state == State.Column)
            {
                for (int i = start, k = destinationIndex; k < length; i++,k++)
                {
                    matrix2[k,dimension2] = matrix1[i,dimension1];
                }    
            }
        }
    }
}