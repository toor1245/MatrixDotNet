using System;
using System.Linq.Expressions;

namespace MatrixDotNet.Math
{
    public static class MathGeneric<T1, T2, TR>
    {
        #region Add

        private static Func<T1, T2, TR> AddFunc;

        public static Func<T1, T2, TR> GetAddFunc()
        {
            if (AddFunc != null)
                return AddFunc;

            var leftPar = Expression.Parameter(typeof(T1), "left");
            var rightPar = Expression.Parameter(typeof(T2), "right");

            var resultType = typeof(TR);
            var body = Expression.Add(Expression.Convert(leftPar, resultType),
                Expression.Convert(rightPar, resultType));

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            AddFunc = func;

            return func;
        }

        public static TR Add(T1 left, T2 right)
        {
            return GetAddFunc()(left, right);
        }

        #endregion

        #region Substraction

        private static Func<T1, T2, TR> SubFunc;

        public static Func<T1, T2, TR> GetSubFunc()
        {
            if (SubFunc != null)
                return SubFunc;

            var leftPar = Expression.Parameter(typeof(T1), "left");
            var rightPar = Expression.Parameter(typeof(T2), "right");

            var resultType = typeof(TR);
            var body = Expression.Subtract(Expression.Convert(leftPar, resultType),
                Expression.Convert(rightPar, resultType));

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            SubFunc = func;

            return func;
        }

        public static TR Sub(T1 left, T2 right)
        {
            return GetSubFunc()(left, right);
        }

        #endregion

        #region Multiply

        private static Func<T1, T2, TR> MultiplyFunc;

        public static Func<T1, T2, TR> GetMultiplyFunc()
        {
            if (MultiplyFunc != null)
                return MultiplyFunc;

            var leftPar = Expression.Parameter(typeof(T1), "left");
            var rightPar = Expression.Parameter(typeof(T2), "right");

            var resultType = typeof(TR);
            var body = Expression.Multiply(Expression.Convert(leftPar, resultType),
                Expression.Convert(rightPar, resultType));

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            MultiplyFunc = func;

            return func;
        }

        public static TR Multiply(T1 left, T2 right)
        {
            return GetMultiplyFunc()(left, right);
        }

        #endregion

        #region Divide

        private static Func<T1, T2, TR> DivideFunc;

        public static Func<T1, T2, TR> GetDivideFunc()
        {
            if (DivideFunc != null)
                return DivideFunc;

            var leftPar = Expression.Parameter(typeof(T1), "left");
            var rightPar = Expression.Parameter(typeof(T2), "right");

            var resultType = typeof(TR);
            var body = Expression.Divide(Expression.Convert(leftPar, resultType),
                Expression.Convert(rightPar, resultType));

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            DivideFunc = func;

            return func;
        }

        public static TR Divide(T1 left, T2 right)
        {
            return GetDivideFunc()(left, right);
        }

        #endregion
    }
}