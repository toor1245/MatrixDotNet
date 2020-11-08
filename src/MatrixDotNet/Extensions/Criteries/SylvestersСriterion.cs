using System.Collections.Generic;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Decompositions;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Criteries
{
    public static class Criterion
    {
        public static DefiniteType SylvestersCriterion<T>(Matrix<T> matrix) 
            where T : unmanaged
        {
            if (!matrix.IsSymmetric)
            {
                throw new MatrixDotNetException("the matrix is not symmetric");
            }
            List<int> forms = GetForm(matrix);
            bool isFirstNeg = forms[0] == -1;
            int count = 0;
            for (int i = 0; i < forms.Count; i++)
            {
                int element = (forms[i] - 1) >> 31;
                count += ~element & forms[i];
            }

            if (count == forms.Count)
            {
                return DefiniteType.Positive;
            }

            if (isFirstNeg && count <= 0)
            {
                return DefiniteType.Negative;
            }

            return DefiniteType.Alternating;
        }

        private static List<int> GetForm<T>(Matrix<T> matrix)
            where T : unmanaged
        {
            List<int> forms = new List<int>();
            matrix.GetLowerUpper(out var lower, out var upper);
            
            T lowerDet = MathExtension.Increment<T>(default);
            T upperDet = MathExtension.Increment<T>(default);

            for (int j = 0; j < matrix.Rows; j++)
            {
                lowerDet = MathExtension.Multiply(lowerDet, lower[j, j]);
                upperDet = MathExtension.Multiply(upperDet, upper[j, j]);
                var form = MathExtension.GreaterThanOrEqual(MathUnsafe<T>.Mul(lowerDet,upperDet),default);
                forms.Add(form ? 1 : -1);
            }
            return forms;
        }
    }
}