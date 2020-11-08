using System.Collections.Generic;
using MatrixDotNet.Extensions.Decompositions;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.QudraticForm
{
    public static partial class QudraticForm
    {
        public static Form SylvestersCriterion<T>(Matrix<T> matrix) 
            where T : unmanaged
        {
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
                return Form.Positive;
            }

            if (isFirstNeg && count <= 0)
            {
                return Form.Negative;
            }

            return Form.Alternating;
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