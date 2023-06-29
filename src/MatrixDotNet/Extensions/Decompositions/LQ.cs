using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.Extensions.Decompositions
{
    public partial class Decomposition
    {
        public static void LqDecomposition<T>(this Matrix<T> matrix, out Matrix<T> l, out Matrix<T> q)
            where T : unmanaged
        {
            q = ProcessGrammShmidtByRows(matrix).GetNormByRows();
            l = matrix * q.Transpose();
        }
    }
}
