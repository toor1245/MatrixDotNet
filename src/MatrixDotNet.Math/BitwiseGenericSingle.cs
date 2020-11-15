using System;
using System.Linq.Expressions;

namespace MatrixDotNet.Math
{
    public class BitwiseGeneric<T1, TR>
    {
        #region Not

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