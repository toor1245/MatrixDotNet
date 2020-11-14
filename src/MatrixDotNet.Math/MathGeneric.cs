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

        #region LeftShift

        private static Func<T1, T2, TR> LeftShiftFunc;

        public static Func<T1, T2, TR> GetLeftShiftFunc()
        {
            if (LeftShiftFunc != null)
                return LeftShiftFunc;

            var leftPar = Expression.Parameter(typeof(T1), "left");
            var rightPar = Expression.Parameter(typeof(T2), "right");

            var resultType = typeof(TR);
            var body = Expression.LeftShift(Expression.Convert(leftPar, resultType),
                Expression.Convert(rightPar, resultType));

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            LeftShiftFunc = func;

            return func;
        }

        public static TR LeftShift(T1 left, T2 right)
        {
            return GetLeftShiftFunc()(left, right);
        }

        #endregion

        #region RightShift

        private static Func<T1, T2, TR> RightShiftFunc;

        public static Func<T1, T2, TR> GetRightShiftFunc()
        {
            if (RightShiftFunc != null)
                return RightShiftFunc;

            var leftPar = Expression.Parameter(typeof(T1), "left");
            var rightPar = Expression.Parameter(typeof(T2), "right");

            var resultType = typeof(TR);
            var body = Expression.RightShift(Expression.Convert(leftPar, resultType),
                Expression.Convert(rightPar, resultType));

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            RightShiftFunc = func;

            return func;
        }

        public static TR RightShift(T1 left, T2 right)
        {
            return GetRightShiftFunc()(left, right);
        }

        #endregion

        #region Or

        private static Func<T1, T2, TR> OrFunc;

        public static Func<T1, T2, TR> GetOrFunc()
        {
            if (OrFunc != null)
                return OrFunc;

            var leftPar = Expression.Parameter(typeof(T1), "left");
            var rightPar = Expression.Parameter(typeof(T2), "right");

            var resultType = typeof(TR);
            var body = Expression.Or(Expression.Convert(leftPar, resultType), Expression.Convert(rightPar, resultType));

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            OrFunc = func;

            return func;
        }

        public static TR Or(T1 left, T2 right)
        {
            return GetOrFunc()(left, right);
        }

        #endregion

        #region And

        private static Func<T1, T2, TR> AndFunc;

        public static Func<T1, T2, TR> GetAndFunc()
        {
            if (AndFunc != null)
                return AndFunc;

            var leftPar = Expression.Parameter(typeof(T1), "left");
            var rightPar = Expression.Parameter(typeof(T2), "right");

            var resultType = typeof(TR);
            var body = Expression.And(Expression.Convert(leftPar, resultType),
                Expression.Convert(rightPar, resultType));

            var func = Expression.Lambda<Func<T1, T2, TR>>(body, leftPar, rightPar).Compile();

            AndFunc = func;

            return func;
        }

        public static TR And(T1 left, T2 right)
        {
            return GetAndFunc()(left, right);
        }

        #endregion

        #region And

        private static Func<T1, TR> NotFunc;

        public static Func<T1, TR> GetNotFunc()
        {
            if (NotFunc != null)
                return NotFunc;

            var leftPar = Expression.Parameter(typeof(T1), "left");
            var resultType = typeof(TR);
            var body = Expression.Not(Expression.Convert(leftPar, resultType));

            var func = Expression.Lambda<Func<T1, TR>>(body, leftPar).Compile();

            NotFunc = func;

            return func;
        }

        public static TR Not(T1 left)
        {
            return GetNotFunc()(left);
        }

        #endregion
    }
}