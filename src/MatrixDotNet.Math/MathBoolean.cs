using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace MatrixDotNet.Math
{
    public static partial class MathExtension
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFloatingPoint<T>()
        {
            return  typeof(T) == typeof(double) ||
                    typeof(T) == typeof(float)  ||
                    typeof(T) == typeof(decimal);
        } 
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInteger<T>()
        {
            var type = typeof(T);
            return type == typeof(byte) ||
                   type == typeof(sbyte) ||
                   type == typeof(short) ||
                   type == typeof(ushort) ||
                   type == typeof(int) ||
                   type == typeof(uint) ||
                   type == typeof(long) ||
                   type == typeof(ulong);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSupported<T>()
        {
            return (typeof(T) == typeof(byte)) ||
                   (typeof(T) == typeof(sbyte)) ||
                   (typeof(T) == typeof(short)) ||
                   (typeof(T) == typeof(ushort)) ||
                   (typeof(T) == typeof(int)) ||
                   (typeof(T) == typeof(uint)) ||
                   (typeof(T) == typeof(long)) ||
                   (typeof(T) == typeof(ulong)) ||
                   (typeof(T) == typeof(float)) ||
                   (typeof(T) == typeof(double));
        }
        
        
        public static bool GreaterThan<T>(T left, T right) where T: unmanaged
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
        
        public static bool GreaterThanBy<T,U>(T left, U right) where T: unmanaged
        {
            var t = typeof(T);
            
            if (Cache.TryGetValue((t, nameof(GreaterThanBy)),out var del))
                return del is Func<T,U,bool> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(GreaterThanBy));
            
            var leftPar = Expression.Parameter(t, nameof(left)); // Left parameter.
            var rightPar = Expression.Parameter(t, nameof(right)); // Right parameter.
            
            // Compare value with converted to generic type of T.
            var body = Expression.GreaterThan(leftPar, Expression.Convert(Expression.Constant(right), t));
            
            var func = Expression.Lambda<Func<T, U, bool>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(GreaterThanBy))] = func;

            return func(left, right);
        }
    }
}