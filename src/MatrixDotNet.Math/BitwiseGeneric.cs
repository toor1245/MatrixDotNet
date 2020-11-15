using System;
using System.Linq.Expressions;

namespace MatrixDotNet.Math
{
    public static class BitwiseGeneric<T1, T2, TR>
    {
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
    }
}