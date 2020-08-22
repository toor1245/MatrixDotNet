using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MatrixDotNet.Extensions.MathExpression
{
    internal static class MathExtension
    {
        private static readonly Dictionary<(Type type,string op),Delegate> Cache =
            new Dictionary<(Type type, string op), Delegate>();

        internal static bool IsFloatingPoint<T>()
        {
            return  typeof(T) == typeof(double) ||
                    typeof(T) == typeof(float)  ||
                    typeof(T) == typeof(decimal);
        } 

        #region Arithmetic and Logic Op
        
        internal static T Add<T>(T left, T right) where T: unmanaged
        {
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
        
        internal static T Sub<T>(T left, T right) where T: unmanaged
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
        
        internal static T Multiply<T>(T left, T right) where T: unmanaged
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
        
        internal static T Divide<T>(T left, T right) where T: unmanaged
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
        
        internal static T DivideBy<T,U>(T left, U right) 
            where T : unmanaged
            where U : unmanaged
        {
            var t = typeof(T);
            var u = typeof(U);
            if (Cache.TryGetValue((t, nameof(DivideBy)),out var del))
                return del is Func<T,U,T> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(DivideBy));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(u,nameof(right));
            var body = Expression.Divide(leftPar, Expression.Convert(Expression.Constant(right), t));
            
            var func = Expression.Lambda<Func<T,U,T>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(DivideBy))] = func;

            return func(left, right);
        }
        
        internal static bool GreaterThan<T>(T left, T right) where T: unmanaged
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
        
        internal static T Increment<T>(T left) where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Increment)),out var del))
                return del is Func<T,T> specificFunc
                    ? specificFunc(left)
                    : throw new InvalidOperationException(nameof(Increment));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var body = Expression.Increment(leftPar);
            
            var func = Expression.Lambda<Func<T,T>>(body, leftPar).Compile();

            Cache[(t, nameof(Increment))] = func;

            return func(left);
        }
        
        internal static T Abs<T>(T left) where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Abs)),out var del))
                return del is Func<T,T> specificFunc
                    ? specificFunc(left)
                    : throw new InvalidOperationException(nameof(Abs));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            MethodInfo info = typeof(Math).GetMethod("Abs", new[] {leftPar.Type});
            if (info == null)
                throw new InvalidOperationException(nameof(Abs));

            var call = Expression.Call(null, info, leftPar);

            var func = Expression.Lambda<Func<T,T>>(call, leftPar).Compile();

            Cache[(t, nameof(Abs))] = func;

            return func(left);
        }
        
        internal static T Negate<T>(T left) where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Negate)),out var del))
                return del is Func<T,T> specificFunc
                    ? specificFunc(left)
                    : throw new InvalidOperationException(nameof(Negate));
            
            var leftPar = Expression.Parameter(t, nameof(left));

            var negate = Expression.Negate(leftPar);

            var func = Expression.Lambda<Func<T,T>>(negate, leftPar).Compile();

            Cache[(t, nameof(Abs))] = func;

            return func(left);
        }
        
        internal static T Random<T>(int start,int end) where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Random)),out var del))
                return del is Func<int,int,T> specificFunc
                    ? specificFunc(start,end)
                    : throw new InvalidOperationException(nameof(Random));
            
            var startPar = Expression.Parameter(t, nameof(start));
            var endPar = Expression.Parameter(t, nameof(end));

            MethodInfo info = typeof(Random).GetMethod("Next", new[] {startPar.Type, endPar.Type});
            
            if(info is null)
                throw new InvalidOperationException(nameof(Random));

            var instance = Expression.New(typeof(Random));
            var call = Expression.Call(instance, info,startPar, endPar);

            var func = Expression.Lambda<Func<int,int,T>>(call,startPar,endPar).Compile();

            Cache[(t, nameof(Random))] = func;

            return func(start,end);
        }
        
        #endregion
    }
}