using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Decompositions
{
    public static partial class Decomposition 
    {
        public static void ShurDecomposition(this Matrix<double> matrix, 
            out Matrix<double> orthogonal,
            out Matrix<double> upper,
            out Matrix<double> ortTranspose)
        {
            if (!matrix.IsSquare)
                throw new MatrixDotNetException("matrix is not square");

            orthogonal = matrix.ProcessGrammShmidtByColumns().GetNormByColumns();
            ortTranspose = orthogonal.Transpose();
            upper = matrix * orthogonal;
        }
        
        public static bool IsIdentity<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            if (!MathExtension.IsSupported<T>())
                throw new MatrixDotNetException("Not Supported data type");

            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    if (i == j)
                    {
                        if (MathExtension.NotEqual(matrix[i,j],MathExtension.Increment<T>(default)))
                        {
                            return false;
                        }
                    }
                    else if(MathExtension.NotEqual(matrix[i,j],default))
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        public static Matrix<T> GetQuasiTriangular<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!matrix.IsSquare)
                throw new MatrixDotNetException("matrix is not square");
            
            var quasi = new Matrix<T>(matrix.Rows,matrix.Columns);
            for (int i = 0; i < matrix.Rows; i++)
            {
                quasi[i, i] = matrix[i, i];
            }

            return quasi;
        }
    }
}