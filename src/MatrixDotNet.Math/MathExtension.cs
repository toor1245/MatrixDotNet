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
        
        private static readonly ConcurrentDictionary<(Type, Type), Delegate> SubFuncCache = 
            new ConcurrentDictionary<(Type, Type), Delegate>();

        private static readonly ConcurrentDictionary<(Type, Type), Delegate> MultiplyFuncCache = 
            new ConcurrentDictionary<(Type, Type), Delegate>();

        private static readonly ConcurrentDictionary<(Type, Type), Delegate> DivideFuncCache = 
            new ConcurrentDictionary<(Type, Type), Delegate>();

        #region Add
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
        
        [Obsolete("Use Add functions instead")]
        public static T1 AddBy<T1, T2>(T1 left, T2 right)
        {
            return GetAddFunc<T1, T2, T1>()(left, right);
        }

        public static TR Add<TR, T1, T2>(T1 left, T2 right)
        {
            return GetAddFunc<T1, T2, TR>()(left, right);
        } 
        #endregion

        #region Substraction

        public static Func<T1, T2, TR> GetSubFunc<T1, T2, TR>()
        {
            var t1 = typeof(T1);
            var t2 = typeof(T2);

            if (SubFuncCache.TryGetValue((t1, t2), out var del))
                return del as Func<T1, T2, TR>;

            var leftPar = Expression.Parameter(t1, "left");
            var rightPar = Expression.Parameter(t2, "right");
            var body = Expression.Subtract(leftPar, rightPar);

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            SubFuncCache[(t1, t2)] = func;

            return func;
        }

        public static T1 Sub<T1, T2>(T1 left, T2 right)
        {
            return GetSubFunc<T1, T2, T1>()(left, right);
        }
        
        [Obsolete("Use Sub functions instead")]
        public static T1 SubBy<T1, T2>(T1 left, T2 right)
        {
            return GetSubFunc<T1, T2, T1>()(left, right);
        }

        public static TR Sub<TR, T1, T2>(T1 left, T2 right)
        {
            return GetSubFunc<T1, T2, TR>()(left, right);
        } 

        #endregion

        #region Multiply

        public static Func<T1, T2, TR> GetMultiplyFunc<T1, T2, TR>()
        {
            var t1 = typeof(T1);
            var t2 = typeof(T2);

            if (MultiplyFuncCache.TryGetValue((t1, t2), out var del))
                return del as Func<T1, T2, TR>;

            var leftPar = Expression.Parameter(t1, "left");
            var rightPar = Expression.Parameter(t2, "right");
            var body = Expression.Multiply(leftPar, rightPar);

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            MultiplyFuncCache[(t1, t2)] = func;

            return func;
        }

        public static T1 Multiply<T1, T2>(T1 left, T2 right)
        {
            return GetMultiplyFunc<T1, T2, T1>()(left, right);
        }

        [Obsolete("Use Multiply functions instead")]
        public static T1 MultiplyBy<T1, T2>(T1 left, T2 right)
        {
            return GetMultiplyFunc<T1, T2, T1>()(left, right);
        }

        public static TR Multiply<TR, T1, T2>(T1 left, T2 right)
        {
            return GetMultiplyFunc<T1, T2, TR>()(left, right);
        } 

        #endregion

        #region Divide
        public static Func<T1, T2, TR> GetDivideFunc<T1, T2, TR>()
        {
            var t1 = typeof(T1);
            var t2 = typeof(T2);

            if (DivideFuncCache.TryGetValue((t1, t2), out var del))
                return del as Func<T1, T2, TR>;

            var leftPar = Expression.Parameter(t1, "left");
            var rightPar = Expression.Parameter(t2, "right");
            var body = Expression.Divide(leftPar, rightPar);

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            DivideFuncCache[(t1, t2)] = func;

            return func;
        }

        public static T1 Divide<T1, T2>(T1 left, T2 right)
        {
            return GetDivideFunc<T1, T2, T1>()(left, right);
        }

        [Obsolete("Use Divide functions instead")]
        public static T1 DivideBy<T1, T2>(T1 left, T2 right)
        {
            return GetDivideFunc<T1, T2, T1>()(left, right);
        }

        public static TR Divide<TR, T1, T2>(T1 left, T2 right)
        {
            return GetDivideFunc<T1, T2, TR>()(left, right);
        }
        #endregion
        
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
        
        [Obsolete("bool shit = true;", true)]
        public static T Random<T>(int start,int end)
        {
            return default(T);
        }
        
        public static T Sqrt<T>(T arg) where T: unmanaged
        {
            var t = typeof(T);
            if (Cache.TryGetValue((t, nameof(Sqrt)),out var del))
                return del is Func<T,T> specificFunc
                    ? specificFunc(arg)
                    : throw new InvalidOperationException(nameof(Sqrt));
            
            var argPar = Expression.Parameter(t, nameof(arg));

            MethodInfo info = typeof(System.Math).GetMethod(nameof(Sqrt),new[]{argPar.Type});
            
            if(info is null)
                throw new InvalidOperationException(nameof(Sqrt));

            var call = Expression.Call(null, info,argPar);

            var func = Expression.Lambda<Func<T,T>>(call, argPar).Compile();

            Cache[(t, nameof(Sqrt))] = func;

            return func(arg);
        }
    }
}