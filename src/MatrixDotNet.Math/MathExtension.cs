using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace MatrixDotNet.Math
{
    public static partial class MathExtension
    {
        private static readonly ConcurrentDictionary<(Type type,string op),Delegate> Cache = 
            new ConcurrentDictionary<(Type type, string op), Delegate>();
        
        private static readonly ConcurrentDictionary<(Type, Type), Delegate> AddFuncCache = 
            new ConcurrentDictionary<(Type, Type), Delegate>();

        public static Func<T1, T2, TR> GetAddFunc<T1, T2, TR>()
        {
            var t1 = typeof(T1);
            var t2 = typeof(T2);

            if (AddFuncCache.TryGetValue((t1, t2), out var del))
                return del as Func<T1, T2, TR>;

            var leftPar = Expression.Parameter(t1, "left");
            var rightPar = Expression.Parameter(t2, "right");
            var body = Expression.Add(leftPar, rightPar);
            
            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            AddFuncCache[(t1, t2)] = func;

            return func;
        }

        public static T1 Add<T1, T2>(T1 left, T2 right)
        {
            return GetAddFunc<T1, T2, T1>()(left, right);
        }

        public static T1 AddBy<T1, T2>(T1 left, T2 right)
        {
            return GetAddFunc<T1, T2, T1>()(left, right);
        }

        public static TR Add<TR, T1, T2>(T1 left, T2 right)
        {
            return GetAddFunc<T1, T2, TR>()(left, right);
        }
        
        public static T Sub<T>(T left, T right) where T: unmanaged
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
        
        public static T SubBy<T,U>(T left, U right) 
            where T : unmanaged
            where U : unmanaged
        {
            var t = typeof(T);
            var u = typeof(U);
            if (Cache.TryGetValue((t, nameof(SubBy)),out var del))
                return del is Func<T,U,T> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(SubBy));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(u,nameof(right));
            var body = Expression.Subtract(leftPar, Expression.Convert(Expression.Constant(right), t));
            
            var func = Expression.Lambda<Func<T,U,T>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(SubBy))] = func;

            return func(left, right);
        }
        
        public static T Multiply<T>(T left, T right) where T: unmanaged
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
        
        public static T Divide<T>(T left, T right) where T: unmanaged
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
        
        public static T DivideBy<T,U>(T left, U right) 
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
        
        public static T MultiplyBy<T,U>(T left, U right) 
            where T : unmanaged
            where U : unmanaged
        {
            var t = typeof(T);
            var u = typeof(U);
            if (Cache.TryGetValue((t, nameof(MultiplyBy)),out var del))
                return del is Func<T,U,T> specificFunc
                    ? specificFunc(left, right)
                    : throw new InvalidOperationException(nameof(MultiplyBy));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            var rightPar = Expression.Parameter(u,nameof(right));
            var body = Expression.Multiply(leftPar, Expression.Convert(Expression.Constant(right), t));
            
            var func = Expression.Lambda<Func<T,U,T>>(body, leftPar, rightPar).Compile();

            Cache[(t, nameof(MultiplyBy))] = func;

            return func(left, right);
        }
        
        public static T Increment<T>(T left) where T: unmanaged
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
        
        public static T Abs<T>(T left) where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Abs)),out var del))
                return del is Func<T,T> specificFunc
                    ? specificFunc(left)
                    : throw new InvalidOperationException(nameof(Abs));
            
            var leftPar = Expression.Parameter(t, nameof(left));
            MethodInfo info = typeof(System.Math).GetMethod("Abs", new[] {leftPar.Type});
            if (info == null)
                throw new InvalidOperationException(nameof(Abs));

            var call = Expression.Call(null, info, leftPar);

            var func = Expression.Lambda<Func<T,T>>(call, leftPar).Compile();

            Cache[(t, nameof(Abs))] = func;

            return func(left);
        }
        
        public static T Negate<T>(T left) where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Negate)),out var del))
                return del is Func<T,T> specificFunc
                    ? specificFunc(left)
                    : throw new InvalidOperationException(nameof(Negate));
            
            var leftPar = Expression.Parameter(t, nameof(left));

            var negate = Expression.Negate(leftPar);

            var func = Expression.Lambda<Func<T,T>>(negate, leftPar).Compile();

            Cache[(t, nameof(Negate))] = func;

            return func(left);
        }
        
        public static T Random<T>(int start,int end) where T: unmanaged
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
        
        public static T Sqrt<T>(T arg) where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Sqrt)),out var del))
                return del is Func<T,T> specificFunc
                    ? specificFunc(arg)
                    : throw new InvalidOperationException(nameof(Random));
            
            var argPar = Expression.Parameter(t, nameof(arg));

            MethodInfo info = typeof(System.Math).GetMethod("Sqrt",new[]{argPar.Type});
            
            if(info is null)
                throw new InvalidOperationException(nameof(Sqrt));

            var call = Expression.Call(null, info,argPar);

            var func = Expression.Lambda<Func<T,T>>(call,argPar).Compile();

            Cache[(t, nameof(Random))] = func;

            return func(arg);
        }
    }
}