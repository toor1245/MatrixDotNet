using System;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions
{
    public static partial class MatrixExtension
    {
        #region Sum T
        
        /// <summary>
        /// Summation matrix. 
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Sum whole of matrix.</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T Sum<T>(this Matrix<T> matrix) where T: unmanaged
        {
            T sum = default;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    sum = MathExtension.Add(sum,matrix[i,j]);
                }
            }
            
            return sum;
        }
        
        /// <summary>
        /// Gets sum by row of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">row index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Sum row by index</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T SumByRow<T>(this Matrix<T> matrix, int dimension) where T: unmanaged
        {
            T sum = default;
            
            for (int i = 0; i < matrix.Columns; i++)
            {
                sum = MathExtension.Add(sum,matrix[dimension,i]);
            }

            return sum;
        }
        
        /// <summary>
        /// Gets sum by column of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">column index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Sum column by index</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T SumByColumn<T>(this Matrix<T> matrix, int dimension) where T: unmanaged
        {
            T sum = default;
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                sum = MathExtension.Add(sum,matrix[i,dimension]);
            }

            return sum;
        }

        /// <summary>
        /// Gets array of sum rows.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T[] SumByRows<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            var array = new T[matrix.Rows];
            
            for (int i = 0; i < matrix.Rows; i++)
            {
                T sum = default;
                
                for (int j = 0; j < matrix.Columns; j++)
                {
                    sum = MathExtension.Add(sum, matrix[i, j]); // sum = sum + matrix[i,j];
                }

                array[i] = sum;
            }

            return array;
        }
        
        /// <summary>
        /// Gets array of sum columns.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T[] SumByColumns<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
            var array = new T[matrix.Columns];
            
            for (int i = 0; i < matrix.Columns; i++)
            {
                T sum = default;
                
                for (int j = 0; j < matrix.Rows; j++)
                {
                    sum = MathExtension.Add(sum, matrix[j, i]);
                }

                array[i] = sum;
            }

            return array;
        }

        
        /// <summary>
        /// Gets sum by diagonal.
        /// </summary>
        /// <param name="matrix"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T SumByDiagonal<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (matrix is null)
                throw new NullReferenceException();
            
            T sum = default;

            for (int i = 0; i < matrix.Rows; i++)
            {
                sum = MathExtension.Add(sum, matrix[i, i]);
            }

            return sum;
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
                    if (System.Math.Abs(sum) >= matrix[i,j])
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
                    if (System.Math.Abs(cs) >= System.Math.Abs(error))
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
                if (System.Math.Abs(sum) >= matrix[dimension,j])
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
                if (System.Math.Abs(cs) >= System.Math.Abs(error))
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
                    if (System.Math.Abs(sum) >= matrix[i,j])
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
                    if (System.Math.Abs(cs) >= System.Math.Abs(error))
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
                if (System.Math.Abs(sum) >= matrix[dimension,j])
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
                if (System.Math.Abs(cs) >= System.Math.Abs(error))
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
                    if (System.Math.Abs(sum) >= matrix[i,j])
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
                    if (System.Math.Abs(cs) >= System.Math.Abs(error))
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
                if (System.Math.Abs(sum) >= matrix[dimension,j])
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
                if (System.Math.Abs(cs) >= System.Math.Abs(error))
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