using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        public static int GetDeterminant(this Matrix<int> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            
            var temp = matrix.ToPrimitive();
            
            if(temp == null)
                throw new NullReferenceException();
            
            if(temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }
            
            int sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<int> minor = GetMinor(matrix, i);
                result += sign * temp[0, i] * minor.GetDeterminant();
                sign = -sign;
            }

            return result;
        }
        
        public static long GetDeterminant(this Matrix<long> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            var temp = matrix.ToPrimitive();
            
            if(temp == null)
                throw new NullReferenceException();
            
            if(temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }
            
            long sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<long> minor = GetMinor(matrix, i);
                result += sign * temp[0, i] * minor.GetDeterminant();
                sign = -sign;
            }

            return result;
        }
        
        public static float GetDeterminant(this Matrix<float> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            var temp = matrix.ToPrimitive();
            
            if(temp == null)
                throw new NullReferenceException();
            
            if(temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }
            
            float sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<float> minor = GetMinor(matrix, i);
                result += sign * temp[0, i] * minor.GetDeterminant();
                sign = -sign;
            }

            return result;
        }
        
        public static double GetDeterminant(this Matrix<double> matrix)
        {
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("the matrix is not square",nameof(matrix));
            }
            
            var temp = matrix.ToPrimitive();
            
            if(temp == null)
                throw new NullReferenceException();
            
            if(temp.Length == 4)
            {
                return temp[0, 0] * temp[1, 1] - temp[0, 1] * temp[1, 0];
            }
            
            double sign = 1, result = 0;

            for (int i = 0; i < temp.GetLength(1); i++)
            {
                Matrix<double> minor = GetMinor(matrix, i);
                result += sign * temp[0, i] * minor.GetDeterminant();
                sign = -sign;
            }
            return result;
        }
    }
}