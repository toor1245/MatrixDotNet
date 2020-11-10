using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace MatrixDotNet.Math
{
    public static partial class MathExtension
    {
        private static readonly ConcurrentDictionary<(Type type,string op),Delegate> Cache = 
            new ConcurrentDictionary<(Type type, string op), Delegate>();
        
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
        
        [Obsolete("Use Comparer<T>.Default instead")]
        public static bool Equal<T>(T left, T right) where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Equal)),out var del))
                return del is Func<T,T,bool> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(Equal));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(t, nameof(right));
            
            var body = Expression.Equal(leftPar, rightPar);
            
            var func = Expression.Lambda<Func<T, T,bool>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(Equal))] = func;

            return func(left, right);
        }
        
        [Obsolete("Use Comparer<T>.Default instead")]
        public static bool NotEqual<T>(T left, T right) where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(NotEqual)),out var del))
                return del is Func<T,T,bool> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(NotEqual));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(t, nameof(right));
            
            var body = Expression.NotEqual(leftPar, rightPar);
            
            var func = Expression.Lambda<Func<T, T,bool>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(NotEqual))] = func;

            return func(left, right);
        }
        
        [Obsolete("Use Comparer<T>.Default instead")]
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
        
        [Obsolete("Use Comparer<T>.Default instead")]
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
        
        [Obsolete("Use Comparer<T>.Default instead")]
        public static bool GreaterThanOrEqual<T>(T left, T right) where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(GreaterThanOrEqual)),out var del))
                return del is Func<T,T,bool> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(GreaterThanOrEqual));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(t, nameof(right));
            
            var body = Expression.GreaterThanOrEqual(leftPar, rightPar);
            
            var func = Expression.Lambda<Func<T, T,bool>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(GreaterThanOrEqual))] = func;

            return func(left, right);
        }
    }
}