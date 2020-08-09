using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Decomposition
{
    public static partial class Decomposition  
    {
        public static void QRDecomposition<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            if(!matrix.IsSquare)
                throw new MatrixDotNetException("matrix is not square");
            
            
        }

        public static void GramShmidtProcess()
        {
            
        }
    }
}