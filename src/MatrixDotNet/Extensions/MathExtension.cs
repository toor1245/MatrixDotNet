using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MatrixDotNet.Extensions
{
    internal static class MathExtension
    {
        private static readonly Dictionary<(Type type,string op),Delegate> Cache =
            new Dictionary<(Type type, string op), Delegate>();

        #region Arithmetic and Logic Op
        
        internal static T Add<T>(T left, T right)
            where T: unmanaged
        {
            // From Jhon Skeet article
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Add)),out var del))
                return del is Func<T,T,T> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(Add));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(t, nameof(right));
            var body = Expression.Add(leftPar, rightPar);
            
            var func = Expression.Lambda<Func<T, T, T>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(Add))] = func;

            return func(left, right);
        }
        
        internal static T Sub<T>(T left, T right)
            where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Sub)),out var del))
                return del is Func<T,T,T> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(Sub));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(t, nameof(right));
            var body = Expression.Subtract(leftPar, rightPar);
            
            var func = Expression.Lambda<Func<T, T, T>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(Sub))] = func;

            return func(left, right);
        }
        
        internal static T Multiply<T>(T left, T right) 
            where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Multiply)),out var del))
                return del is Func<T,T,T> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(Multiply));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(t, nameof(right));
            var body = Expression.Multiply(leftPar, rightPar);
            
            var func = Expression.Lambda<Func<T, T, T>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(Multiply))] = func;

            return func(left, right);
        }
        internal static T Divide<T>(T left, T right)
            where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Divide)),out var del))
                return del is Func<T,T,T> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(Divide));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(t, nameof(right));
            var body = Expression.Divide(leftPar, rightPar);
            
            var func = Expression.Lambda<Func<T, T, T>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(Divide))] = func;

            return func(left, right);
        }
        
        internal static bool GreaterThan<T>(T left, T right)
            where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(GreaterThan)),out var del))
                return del is Func<T,T,bool> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(GreaterThan));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(t, nameof(right));
            var body = Expression.GreaterThan(leftPar, rightPar);
            
            var func = Expression.Lambda<Func<T, T,bool>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(GreaterThan))] = func;

            return func(left, right);
        }
        
        #endregion

        #region Sum Op with Matrix
        
        public static T Sum<T>(this Matrix<T> matrix) 
            where T: unmanaged
        {
            T sum = default;
            for (int i = 0; i < matrix._Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix._Matrix.GetLength(1); j++)
                {
                    sum = Add<T>(sum,matrix[i,j]);
                }
            }
            return sum;
        }

        public static T Sum<T>(this Matrix<T> matrix, int row)
            where T: unmanaged
        {
            T sum = default;
            
            for (int i = 0; i < matrix._Matrix.GetLength(1); i++)
            {
                sum = Add(sum,matrix[row,i]);
            }

            return sum;
        }

        public static T[] SumRows<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            var array = new T[matrix._Matrix.GetLength(0)];
            
            for (int i = 0; i < matrix._Matrix.GetLength(0); i++)
            {
                T sum = default;
                
                for (int j = 0; j < matrix._Matrix.GetLength(1); j++)
                {
                    sum = Add(sum, matrix[i, j]); // sum = sum + matrix[i,j];
                }

                array[i] = sum;
            }

            return array;
        }
        
        public static T[] SumColumns<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            var array = new T[matrix._Matrix.GetLength(1)];
            
            for (int i = 0; i < matrix._Matrix.GetLength(1); i++)
            {
                T sum = default;
                
                for (int j = 0; j < matrix._Matrix.GetLength(0); j++)
                {
                    sum = Add(sum, matrix[j, i]);
                }

                array[i] = sum;
            }

            return array;
        }
        
        #endregion

        #region Max_Min Matrix

        public static T[] MaxRows<T>(this Matrix<T> matrix) 
            where T : unmanaged
        {
            T num = default;
            var max = new T[matrix._Matrix.GetLength(0)];

            for (int i = 0; i < matrix._Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix._Matrix.GetLength(1); j++)
                {
                    if (GreaterThan(matrix[i, j],num))
                    {
                        num = matrix[i, j];
                    }
                }

                max[i] = num;
                num = default;
            }

            return max;
        }
        
        public static T[] MaxColumns<T>(this Matrix<T> matrix) 
            where T : unmanaged
        {
            T num = default;
            var max = new T[matrix._Matrix.GetLength(1)];

            for (int i = 0; i < matrix._Matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix._Matrix.GetLength(0); j++)
                {
                    if (GreaterThan(matrix[j, i],num))
                    {
                        num = matrix[j, i];
                    }
                }
                max[i] = num;
                num = default;
            }

            return max;
        }
        
        public static T[] MinRows<T>(this Matrix<T> matrix) 
            where T : unmanaged
        {
            T num = default;
            var min = new T[matrix._Matrix.GetLength(0)];

            for (int i = 0; i < matrix._Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix._Matrix.GetLength(1); j++)
                {
                    if (!GreaterThan(matrix[i, j],num))
                    {
                        num = matrix[i, j];
                    }
                }

                min[i] = num;
                num = default;
            }

            return min;
        }
        
        public static T[] MinColumns<T>(this Matrix<T> matrix) 
            where T : unmanaged
        {
            T num = default;
            var min = new T[matrix._Matrix.GetLength(1)];

            for (int i = 0; i < matrix._Matrix.GetLength(1); i++)
            {
                for (int j = 0; j < matrix._Matrix.GetLength(0); j++)
                {
                    if (!GreaterThan(matrix[j, i],num))
                    {
                        num = matrix[j,i];
                    }
                }
                
                min[i] = num;
                num = default;
            }
            
            return min;
        }

        #endregion
    }
    
}