using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace MatrixDotNet.Math
{

    public static partial class MathExtension
    {
        private static readonly Dictionary<(Type, Type, Type), Delegate> AddFuncCache =
            new Dictionary<(Type, Type, Type), Delegate>();

        private static readonly ConcurrentDictionary<(Type, Type, Type), Delegate> SubFuncCache =
            new ConcurrentDictionary<(Type, Type, Type), Delegate>();

        private static readonly ConcurrentDictionary<(Type, Type, Type), Delegate> MultiplyFuncCache =
            new ConcurrentDictionary<(Type, Type, Type), Delegate>();

        private static readonly ConcurrentDictionary<(Type, Type, Type), Delegate> DivideFuncCache =
            new ConcurrentDictionary<(Type, Type, Type), Delegate>();

        private static readonly ConcurrentDictionary<Type, Delegate> IncrementFuncCache =
            new ConcurrentDictionary<Type, Delegate>();

        private static readonly ConcurrentDictionary<Type, Delegate> AbsFuncCache =
            new ConcurrentDictionary<Type, Delegate>();

        private static readonly ConcurrentDictionary<Type, Delegate> NegateFuncCache =
            new ConcurrentDictionary<Type, Delegate>();

        private static readonly ConcurrentDictionary<Type, Delegate> SqrtFuncCache =
            new ConcurrentDictionary<Type, Delegate>();

        #region Add
        public static Func<T1, T2, TR> GetAddFunc<T1, T2, TR>()
        {
            var t1 = typeof(T1);
            var t2 = typeof(T2);
            var tr = typeof(TR);

            if (AddFuncCache.TryGetValue((t1, t2, tr), out var del))
                return del as Func<T1, T2, TR>;

            var leftPar = Expression.Parameter(t1, "left");
            var rightPar = Expression.Parameter(t2, "right");
            var body = Expression.Add(leftPar, rightPar);

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            AddFuncCache[(t1, t2, tr)] = func;

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

        public static TR Add<T1, T2, TR>(T1 left, T2 right)
        {
            return GetAddFunc<T1, T2, TR>()(left, right);
        }
        #endregion

        #region Substraction

        public static Func<T1, T2, TR> GetSubFunc<T1, T2, TR>()
        {
            var t1 = typeof(T1);
            var t2 = typeof(T2);
            var tr = typeof(TR);

            if (SubFuncCache.TryGetValue((t1, t2, tr), out var del))
                return del as Func<T1, T2, TR>;

            var leftPar = Expression.Parameter(t1, "left");
            var rightPar = Expression.Parameter(t2, "right");
            var body = Expression.Subtract(leftPar, rightPar);

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            SubFuncCache[(t1, t2, tr)] = func;

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

        public static TR Sub<T1, T2, TR>(T1 left, T2 right)
        {
            return GetSubFunc<T1, T2, TR>()(left, right);
        }

        #endregion

        #region Multiply

        public static Func<T1, T2, TR> GetMultiplyFunc<T1, T2, TR>()
        {
            var t1 = typeof(T1);
            var t2 = typeof(T2);
            var tr = typeof(TR);

            if (MultiplyFuncCache.TryGetValue((t1, t2, tr), out var del))
                return del as Func<T1, T2, TR>;

            var leftPar = Expression.Parameter(t1, "left");
            var rightPar = Expression.Parameter(t2, "right");
            var body = Expression.Multiply(leftPar, rightPar);

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            MultiplyFuncCache[(t1, t2, tr)] = func;

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

        public static TR Multiply<T1, T2, TR>(T1 left, T2 right)
        {
            return GetMultiplyFunc<T1, T2, TR>()(left, right);
        }

        #endregion

        #region Divide
        public static Func<T1, T2, TR> GetDivideFunc<T1, T2, TR>()
        {
            var t1 = typeof(T1);
            var t2 = typeof(T2);
            var tr = typeof(TR);

            if (DivideFuncCache.TryGetValue((t1, t2, tr), out var del))
                return del as Func<T1, T2, TR>;

            var leftPar = Expression.Parameter(t1, "left");
            var rightPar = Expression.Parameter(t2, "right");
            var body = Expression.Divide(leftPar, rightPar);

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            DivideFuncCache[(t1, t2, tr)] = func;

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

        public static TR Divide<T1, T2, TR>(T1 left, T2 right)
        {
            return GetDivideFunc<T1, T2, TR>()(left, right);
        }
        #endregion

        #region Increment
        public static Func<T, T> GetIncrementFunc<T>()
        {
            var t = typeof(T);
            if (IncrementFuncCache.TryGetValue(t, out var del))
                return del as Func<T, T>;

            var leftPar = Expression.Parameter(t, "value");
            var body = Expression.Increment(leftPar);

            var func = Expression.Lambda<Func<T, T>>(body, leftPar).Compile();

            IncrementFuncCache[t] = func;

            return func;
        }

        public static T Increment<T>(T left)
        {
            return GetIncrementFunc<T>()(left);
        }

        #endregion

        #region Abs

        public static Func<T, T> GetAbsFunc<T>()
        {
            var t = typeof(T);
            if (AbsFuncCache.TryGetValue(t, out var del))
                return del as Func<T, T>;

            var leftPar = Expression.Parameter(t, "value");
            MethodInfo info = typeof(System.Math).GetMethod("Abs", new[] { leftPar.Type });
            if (info == null)
                throw new InvalidOperationException(nameof(Abs));

            var call = Expression.Call(null, info, leftPar);

            var func = Expression.Lambda<Func<T, T>>(call, leftPar).Compile();

            AbsFuncCache[t] = func;

            return func;
        }

        public static T Abs<T>(T left)
        {
            return GetAbsFunc<T>()(left);
        }

        #endregion

        #region Negate
        public static Func<T, T> GetNegateFunc<T>()
        {
            var t = typeof(T);
            if (NegateFuncCache.TryGetValue(t, out var del))
                return del as Func<T, T>;

            var leftPar = Expression.Parameter(t, "value");

            var negate = Expression.Negate(leftPar);

            var func = Expression.Lambda<Func<T, T>>(negate, leftPar).Compile();

            NegateFuncCache[t] = func;

            return func;
        }

        public static T Negate<T>(T left)
        {
            return GetNegateFunc<T>()(left);
        }
        #endregion

        #region Sqrt
        public static Func<T, T> Sqrt<T>()
        {
            var t = typeof(T);
            if (SqrtFuncCache.TryGetValue(t, out var del))
                return del as Func<T, T>;

            var argPar = Expression.Parameter(t, "value");

            MethodInfo info = typeof(System.Math).GetMethod(nameof(Sqrt), new[] { argPar.Type });

            if (info is null)
                throw new InvalidOperationException(nameof(Sqrt));

            var call = Expression.Call(null, info, argPar);

            var func = Expression.Lambda<Func<T, T>>(call, argPar).Compile();

            SqrtFuncCache[t] = func;

            return func;
        }

        public static T Sqrt<T>(T arg)
        {
            return Sqrt<T>()(arg);
        }
        #endregion

        [Obsolete("bool shit = true;", true)]
        public static T Random<T>(int start, int end)
        {
            return default(T);
        }
    }
}