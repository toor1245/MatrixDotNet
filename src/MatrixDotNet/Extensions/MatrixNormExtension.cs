using System;
using System.Linq;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        #region LNorm
        public static double LNorm(this Matrix<double> matrix)
        {
            double[] max = new double[matrix.Rows];
            for (int i = 0; i < matrix.Rows; i++)
            {
                max[i] = Math.Abs(matrix[i].Sum());
            }

            return max.Max();
        }
        
        public static float LNorm(this Matrix<float> matrix)
        {
            float[] max = new float[matrix.Rows];
            for (int i = 0; i < matrix.Rows; i++)
            {
                max[i] = Math.Abs(matrix[i].Sum());
            }

            return max.Max();
        }

        public static long LNorm(this Matrix<long> matrix)
        {
            long[] max = new long[matrix.Rows];
            for (int i = 0; i < matrix.Rows; i++)
            {
                max[i] = Math.Abs(matrix[i].Sum());
            }
            
            return max.Max();
        }
        
        public static int LNorm(this Matrix<int> matrix)
        {
            int[] max = new int[matrix.Rows];
            for (int i = 0; i < matrix.Rows; i++)
            {
                max[i] = Math.Abs(matrix[i].Sum());
            }
            
            return max.Max();
        }
        
        #endregion

        #region MNorm
        
        public static double MNorm(this Matrix<double> matrix)
        {
            double[] max = new double[matrix.Columns];
            for (int i = 0; i < matrix.Columns; i++)
            {
                max[i] = Math.Abs(matrix[i,State.Column].Sum());
            }

            return max.Max();
        }
        
        public static float MNorm(this Matrix<float> matrix)
        {
            float[] max = new float[matrix.Columns];
            for (int i = 0; i < matrix.Columns; i++)
            {
                max[i] = Math.Abs(matrix[i,State.Column].Sum());
            }

            return max.Max();
        }
        
        public static long MNorm(this Matrix<long> matrix)
        {
            long[] max = new long[matrix.Columns];
            for (int i = 0; i < matrix.Columns; i++)
            {
                max[i] = Math.Abs(matrix[i,State.Column].Sum());
            }

            return max.Max();
        }
        
        public static int MNorm(this Matrix<int> matrix)
        {
            int[] max = new int[matrix.Columns];
            for (int i = 0; i < matrix.Columns; i++)
            {
                max[i] = Math.Abs(matrix[i,State.Column].Sum());
            }

            return max.Max();
        }

        #endregion
    }
}