using System;
using MatrixDotNet.Exceptions;
using MathExtension = MatrixDotNet.Math.MathExtension;

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
        public static T Sum<T>(this Matrix<T> matrix) 
            where T: unmanaged
        {
            if(matrix is null)
                throw new NullReferenceException();
            
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
        public static T SumByRow<T>(this Matrix<T> matrix, int dimension) 
            where T: unmanaged
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
        
        /// <summary>
        /// Gets sum by column of matrix.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <param name="dimension">column index.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns>Sum column by index</returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T SumByColumn<T>(this Matrix<T> matrix, int dimension)
            where T: unmanaged
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

        /// <summary>
        /// Gets array of sum rows.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T[] SumByRows<T>(this Matrix<T> matrix)
            where T : unmanaged
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
        
        /// <summary>
        /// Gets array of sum columns.
        /// </summary>
        /// <param name="matrix">the matrix.</param>
        /// <typeparam name="T">unmanaged type.</typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T[] SumByColumns<T>(this Matrix<T> matrix)
            where T : unmanaged
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

        
        /// <summary>
        /// Gets sum by diagonal.
        /// </summary>
        /// <param name="matrix"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static T SumByDiagonal<T>(this Matrix<T> matrix) 
            where T : unmanaged
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

        public static T GetKleinSum<T>(this Matrix<T> matrix) 
            where T : unmanaged
        {
            if (!MathExtension.IsFloatingPoint<T>())
            {
                throw new MatrixDotNetException($"{typeof(T)} is not supported type must be floating type");
            }
            
            T sum = default;
            T cs = default;
            T ccs = default;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    T t = MathExtension.Add(sum,matrix[i,j]);
                    T error;
                    
                    if (MathExtension.GreaterThanOrEqual(MathExtension.Abs(sum),matrix[i,j]))
                    {
                        error = MathExtension.Add(MathExtension.Sub(sum,t),matrix[i,j]);
                    }
                    else
                    {
                        error = MathExtension.Add(MathExtension.Sub(matrix[i,j],t),sum);
                    }
                    
                    sum = t;
                    t = MathExtension.Add(cs,cs);
                    T error2;
                    
                    if (MathExtension.GreaterThanOrEqual(MathExtension.Abs(cs),error))
                    {
                        error2 = MathExtension.Add(MathExtension.Sub(error,t),cs);
                    }
                    else
                    {
                        error2 = MathExtension.Add(MathExtension.Sub(cs,t),error);
                    }

                    cs = t;
                    ccs = MathExtension.Add(ccs,error2);
                }
            }
            return MathExtension.Add(MathExtension.Add(sum,cs),ccs);
        }

        public static T GetKleinSum<T>(this Matrix<T> matrix, int dimension, State state = State.Row) 
            where T : unmanaged
        {
            if (!MathExtension.IsFloatingPoint<T>())
            {
                throw new MatrixDotNetException($"{typeof(T)} is not supported type must be floating type");
            }

            return state == State.Row ? GetKleinSumByRows(matrix,dimension) : GetKleinSumByColumns(matrix,dimension);
        }
        
        private static T GetKleinSumByRows<T>(this Matrix<T> matrix, int dimension) 
            where T : unmanaged
        {
            T sum = default;
            T cs = default;
            T ccs = default;
            
            for (int j = 0; j < matrix.Columns; j++)
            {
                T t = MathExtension.Add(sum,matrix[dimension,j]);
                T error;
                
                if (MathExtension.GreaterThanOrEqual(MathExtension.Abs(sum),matrix[dimension,j]))
                {
                    error = MathExtension.Add(MathExtension.Sub(sum,t),matrix[dimension,j]);
                }
                else
                {
                    error = MathExtension.Add(MathExtension.Sub(matrix[dimension,j],t),sum);
                }
                
                sum = t;
                t = MathExtension.Add(cs,cs);
                T error2;
                
                if (MathExtension.GreaterThanOrEqual(MathExtension.Abs(cs),error))
                {
                    error2 = MathExtension.Add(MathExtension.Sub(error,t),cs);
                }
                else
                {
                    error2 = MathExtension.Add(MathExtension.Sub(cs,t),error);
                }

                cs = t;
                ccs = MathExtension.Add(ccs,error2);
            }
            return MathExtension.Add(MathExtension.Add(sum,cs),ccs);
        }
        
        private static T GetKleinSumByColumns<T>(this Matrix<T> matrix, int dimension) 
            where T : unmanaged
        {
            T sum = default;
            T cs = default;
            T ccs = default;
            
            for (int j = 0; j < matrix.Rows; j++)
            {
                T t = MathExtension.Add(sum,matrix[j,dimension]);
                T error;
                
                if (MathExtension.GreaterThanOrEqual(MathExtension.Abs(sum),matrix[j,dimension]))
                {
                    error = MathExtension.Add(MathExtension.Sub(sum,t),matrix[j,dimension]);
                }
                else
                {
                    error = MathExtension.Add(MathExtension.Sub(matrix[j,dimension],t),sum);
                }
                
                sum = t;
                t = MathExtension.Add(cs,cs);
                T error2;
                
                if (MathExtension.GreaterThanOrEqual(MathExtension.Abs(cs),error))
                {
                    error2 = MathExtension.Add(MathExtension.Sub(error,t),cs);
                }
                else
                {
                    error2 = MathExtension.Add(MathExtension.Sub(cs,t),error);
                }

                cs = t;
                ccs = MathExtension.Add(ccs,error2);
            }
            return MathExtension.Add(MathExtension.Add(sum,cs),ccs);
        }
        

        #endregion

        #region Kahan
        
        public static T GetKahanSum<T>(this Matrix<T> matrix) 
            where T : unmanaged
        {
            T sum = default;
            T error = default;
            for (int i = 0; i < matrix.Rows; i++)
            {
                for (int j = 0; j < matrix.Columns; j++)
                {
                    T y = MathExtension.Sub(matrix[i, j], error);
                    T t = MathExtension.Add(sum, y);
                    error = MathExtension.Sub(MathExtension.Sub(t, sum), matrix[i, j]);
                    sum = t;
                }
            }

            return sum;
        }

        public static T GetKahanSum<T>(this Matrix<T> matrix, int dimension,State state = State.Row) 
            where T : unmanaged
        {
            T sum = default;
            T error = default;
            if (state == State.Row)
            {
                for (int i = 0; i < matrix.Columns; i++)
                {
                    T y = MathExtension.Sub(matrix[dimension, i], error);
                    T t = MathExtension.Add(sum, y);
                    error = MathExtension.Sub(MathExtension.Sub(t, sum), matrix[dimension, i]);
                    sum = t;
                }
            }
            else
            {
                for (int i = 0; i < matrix.Rows; i++)
                {
                    T y = MathExtension.Sub(matrix[i,dimension], error);
                    T t = MathExtension.Add(sum, y);
                    error = MathExtension.Sub(MathExtension.Sub(t, sum), matrix[i,dimension]);
                    sum = t;
                }
            }

            return sum;
        }

        #endregion
    }
}