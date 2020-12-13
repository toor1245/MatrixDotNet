using System;
using System.Linq.Expressions;

namespace MatrixDotNet.Math
{
    public static class MathGeneric<T>
    {
        #region Add

        private static Func<T, T, T> AddFunc;

        public static Func<T, T, T> GetAddFunc()
        {
            if (AddFunc != null)
                return AddFunc;

            var leftPar = Expression.Parameter(typeof(T), "left");
            var rightPar = Expression.Parameter(typeof(T), "right");

            var body = Expression.Add(leftPar, rightPar);
            var func = Expression.Lambda<Func<T, T, T>>(body, leftPar, rightPar).Compile();
            AddFunc = func;

            return func;
        }

        public static T Add(T left, T right)
        {
            return GetAddFunc()(left, right);
        }

        #endregion

        #region Substraction

        private static Func<T, T, T> SubFunc;

        public static Func<T, T, T> GetSubFunc()
        {
            if (SubFunc != null)
                return SubFunc;

            var leftPar = Expression.Parameter(typeof(T), "left");
            var rightPar = Expression.Parameter(typeof(T), "right");

            var body = Expression.Subtract(leftPar, rightPar);
            var func = Expression.Lambda<Func<T, T, T>>(body, leftPar, rightPar).Compile();
            SubFunc = func;

            return func;
        }

        public static T Sub(T left, T right)
        {
            return GetSubFunc()(left, right);
        }

        #endregion

        #region Multiply

        private static Func<T, T, T> MultiplyFunc;

        public static Func<T, T, T> GetMultiplyFunc()
        {
            if (MultiplyFunc != null)
                return MultiplyFunc;

            var leftPar = Expression.Parameter(typeof(T), "left");
            var rightPar = Expression.Parameter(typeof(T), "right");


            var body = Expression.Multiply(leftPar, rightPar);
            var func = Expression.Lambda<Func<T, T, T>>(body, leftPar, rightPar).Compile();
            MultiplyFunc = func;

            return func;
        }

        public static T Multiply(T left, T right)
        {
            return GetMultiplyFunc()(left, right);
        }

        #endregion

        #region Divide

        private static Func<T, T, T> DivideFunc;

        public static Func<T, T, T> GetDivideFunc()
        {
            if (DivideFunc != null)
                return DivideFunc;

            var leftPar = Expression.Parameter(typeof(T), "left");
            var rightPar = Expression.Parameter(typeof(T), "right");

            var body = Expression.Divide(leftPar, rightPar);
            var func = Expression.Lambda<Func<T, T, T>>(body, leftPar, rightPar).Compile();
            DivideFunc = func;

            return func;
        }

        public static T Divide(T left, T right)
        {
            return GetDivideFunc()(left, right);
        }

        #endregion

        #region Increment

        private static Func<T, T> IncrementFunc;

        public static Func<T, T> GetIncrementFunc()
        {
            if (IncrementFunc != null)
                return IncrementFunc;

            var leftPar = Expression.Parameter(typeof(T), "value");
            var body = Expression.Increment(leftPar);
            var func = Expression.Lambda<Func<T, T>>(body, leftPar).Compile();
            IncrementFunc = func;

            return func;
        }

        public static T Increment(T left)
        {
            return GetIncrementFunc()(left);
        }

        #endregion

        #region Abs

        private static Func<T, T> AbsFunc;

        public static Func<T, T> GetAbsFunc()
        {
            if (AbsFunc != null)
                return AbsFunc;

            var leftPar = Expression.Parameter(typeof(T), "value");

            var info = typeof(System.Math).GetMethod("Abs", new[] { leftPar.Type });
            if (info == null)
                throw new InvalidOperationException(nameof(Abs));

            var call = Expression.Call(null, info, leftPar);
            var func = Expression.Lambda<Func<T, T>>(call, leftPar).Compile();
            AbsFunc = func;

            return func;
        }

        public static T Abs(T left)
        {
            return GetAbsFunc()(left);
        }

        #endregion

        #region Negate

        private static Func<T, T> NegateFunc;

        public static Func<T, T> GetNegateFunc()
        {
            if (NegateFunc != null)
                return NegateFunc;

            var leftPar = Expression.Parameter(typeof(T), "value");

            var negate = Expression.Negate(leftPar);

            var func = Expression.Lambda<Func<T, T>>(negate, leftPar).Compile();
            NegateFunc = func;

            return func;
        }

        public static T Negate(T left)
        {
            return GetNegateFunc()(left);
        }

        #endregion

        #region Sqrt

        private static Func<T, T> SqrtFunc;

        public static Func<T, T> Sqrt()
        {
            if (SqrtFunc != null)
                return SqrtFunc;

            var argPar = Expression.Parameter(typeof(T), "value");

            var info = typeof(System.Math).GetMethod(nameof(Sqrt), new[] { argPar.Type });

            if (info is null)
                throw new InvalidOperationException(nameof(Sqrt));

            var call = Expression.Call(null, info, argPar);

            var func = Expression.Lambda<Func<T, T>>(call, argPar).Compile();
            SqrtFunc = func;

            return func;
        }

        public static T Sqrt(T arg)
        {
            return Sqrt()(arg);
        }

        #endregion
    }
}