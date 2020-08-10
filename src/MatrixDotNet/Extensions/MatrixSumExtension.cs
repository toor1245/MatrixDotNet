using System;
using MatrixDotNet.Extensions.MathExpression;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        #region Sum T
        
        public static T Sum<T>(this Matrix<T> matrix) where T: unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            T sum = default;
            for (int i = 0; i < matrix._Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix._Matrix.GetLength(1); j++)
                {
                    sum = MathExtension.Add(sum,matrix[i,j]);
                }
            }
            
            return sum;
        }

        public static T SumByRow<T>(this Matrix<T> matrix, int dimension) where T: unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            T sum = default;
            
            for (int i = 0; i < matrix._Matrix.GetLength(1); i++)
            {
                sum = MathExtension.Add(sum,matrix[dimension,i]);
            }

            return sum;
        }
        
        public static T SumByColumn<T>(this Matrix<T> matrix, int dimension) where T: unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            T sum = default;
            
            for (int i = 0; i < matrix._Matrix.GetLength(0); i++)
            {
                sum = MathExtension.Add(sum,matrix[i,dimension]);
            }

            return sum;
        }

        public static T[] SumByRows<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            var array = new T[matrix._Matrix.GetLength(0)];
            
            for (int i = 0; i < matrix._Matrix.GetLength(0); i++)
            {
                T sum = default;
                
                for (int j = 0; j < matrix._Matrix.GetLength(1); j++)
                {
                    sum = MathExtension.Add(sum, matrix[i, j]); // sum = sum + matrix[i,j];
                }

                array[i] = sum;
            }

            return array;
        }
        
        public static T[] SumByColumns<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            var array = new T[matrix._Matrix.GetLength(1)];
            
            for (int i = 0; i < matrix._Matrix.GetLength(1); i++)
            {
                T sum = default;
                
                for (int j = 0; j < matrix._Matrix.GetLength(0); j++)
                {
                    sum = MathExtension.Add(sum, matrix[j, i]);
                }

                array[i] = sum;
            }

            return array;
        }

        public static T SumByDiagonal<T>(this Matrix<T> matrix) where T : unmanaged 
        {
            if(matrix is null)
                throw new NullReferenceException();

            return default;
        }

        #endregion
        
        #region Klein
        public static decimal KleinSum(this Matrix<decimal> matrix)
        {
            decimal sum = 0;
            decimal cs = 0;
            decimal ccs = 0;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    var t = sum + matrix[i,j];
                    decimal error;
                    if (Math.Abs(sum) >= matrix[i,j])
                    {
                        error = (sum - t) + matrix[i,j];
                    }
                    else
                    {
                        error = (matrix[i,j] - t) + sum;
                    }

                    sum = t;
                    t = cs + cs;
                    decimal error2;
                    if (Math.Abs(cs) >= Math.Abs(error))
                    {
                        error2 = (error - t) + cs;
                    }
                    else
                    {
                        error2 = (cs - t) + error;
                    }

                    cs = t;
                    ccs += error2;
                }
            }
            return sum + cs + ccs;
        }
        
        public static decimal KleinSum(this Matrix<decimal> matrix, int dimension)
        {
            decimal sum = 0;
            decimal cs = 0;
            decimal ccs = 0;
            for (int j = 0; j < matrix.Columns; j++)
            {
                var t = sum + matrix[dimension,j];
                decimal error;
                if (Math.Abs(sum) >= matrix[dimension,j])
                {
                    error = (sum - t) + matrix[dimension,j];
                }
                else
                {
                    error = (matrix[dimension,j] - t) + sum;
                }

                sum = t;
                t = cs + cs;
                decimal error2;
                if (Math.Abs(cs) >= Math.Abs(error))
                {
                    error2 = (error - t) + cs;
                }
                else
                {
                    error2 = (cs - t) + error;
                }

                cs = t;
                ccs += error2;
            }
            return sum + cs + ccs;
        }
        
        public static double KleinSum(this Matrix<double> matrix)
        {
            double sum = 0;
            double cs = 0;
            double ccs = 0;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    var t = sum + matrix[i,j];
                    double error;
                    if (Math.Abs(sum) >= matrix[i,j])
                    {
                        error = (sum - t) + matrix[i,j];
                    }
                    else
                    {
                        error = (matrix[i,j] - t) + sum;
                    }

                    sum = t;
                    t = cs + cs;
                    double error2;
                    if (Math.Abs(cs) >= Math.Abs(error))
                    {
                        error2 = (error - t) + cs;
                    }
                    else
                    {
                        error2 = (cs - t) + error;
                    }

                    cs = t;
                    ccs += error2;
                }
            }
            return sum + cs + ccs;
        }
        
        public static double KleinSum(this Matrix<double> matrix, int dimension)
        {
            double sum = 0;
            double cs = 0;
            double ccs = 0;
            for (int j = 0; j < matrix.Columns; j++)
            {
                var t = sum + matrix[dimension,j];
                double error;
                if (Math.Abs(sum) >= matrix[dimension,j])
                {
                    error = (sum - t) + matrix[dimension,j];
                }
                else
                {
                    error = (matrix[dimension,j] - t) + sum;
                }

                sum = t;
                t = cs + cs;
                double error2;
                if (Math.Abs(cs) >= Math.Abs(error))
                {
                    error2 = (error - t) + cs;
                }
                else
                {
                    error2 = (cs - t) + error;
                }

                cs = t;
                ccs += error2;
            }
            return sum + cs + ccs;
        }
        
        public static float KleinSum(this Matrix<float> matrix)
        {
            float sum = 0;
            float cs = 0;
            float ccs = 0;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    float t = sum + matrix[i,j];
                    float error;
                    if (Math.Abs(sum) >= matrix[i,j])
                    {
                        error = (sum - t) + matrix[i,j];
                    }
                    else
                    {
                        error = (matrix[i,j] - t) + sum;
                    }

                    sum = t;
                    t = cs + cs;
                    float error2;
                    if (Math.Abs(cs) >= Math.Abs(error))
                    {
                        error2 = (error - t) + cs;
                    }
                    else
                    {
                        error2 = (cs - t) + error;
                    }

                    cs = t;
                    ccs += error2;
                }
            }
            return sum + cs + ccs;
        }
        
        public static float KleinSum(this Matrix<float> matrix, int dimension)
        {
            float sum = 0;
            float cs = 0;
            float ccs = 0;
            for (int j = 0; j < matrix.Columns; j++)
            {
                var t = sum + matrix[dimension,j];
                float error;
                if (Math.Abs(sum) >= matrix[dimension,j])
                {
                    error = (sum - t) + matrix[dimension,j];
                }
                else
                {
                    error = (matrix[dimension,j] - t) + sum;
                }

                sum = t;
                t = cs + cs;
                float error2;
                if (Math.Abs(cs) >= Math.Abs(error))
                {
                    error2 = (error - t) + cs;
                }
                else
                {
                    error2 = (cs - t) + error;
                }

                cs = t;
                ccs += error2;
            }
            return sum + cs + ccs;
        }
        
        #endregion

        #region Kahan
        public static double KahanSum(this Matrix<double> matrix,int dimension)
        {
            double sum = 0;
            double error = 0;
            for (int i = 0; i < matrix.Columns; i++)
            {
                double y = matrix[dimension, i] - error;
                double t = sum + y;
                error = (t - sum) - matrix[dimension, i];
                sum = t;
            }

            return sum;
        }

        public static double KahanSum(this Matrix<double> matrix)
        {
            double sum = 0;
            double error = 0;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    double y = matrix[i,j] - error;
                    double t = sum + y;
                    error = (t - sum) - matrix[i,j];
                    sum = t;
                }
            }
            return sum;
        }
        
        public static float KahanSum(this Matrix<float> matrix)
        {
            float sum = 0f;
            float error = 0f;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    float y = matrix[i,j] - error;
                    float t = sum + y;
                    error = (t - sum) - matrix[i,j];
                    sum = t;
                }
            }
            return sum;
        }
        
        public static float KahanSum(this Matrix<float> matrix,int dimension)
        {
            float sum = 0;
            float error = 0;
            for (int i = 0; i < matrix.Columns; i++)
            {
                float y = matrix[dimension, i] - error;
                float t = sum + y;
                error = (t - sum) - matrix[dimension, i];
                sum = t;
            }

            return sum;
        }
        
        
        #endregion
    }
}