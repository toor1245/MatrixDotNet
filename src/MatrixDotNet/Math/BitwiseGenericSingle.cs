using System;
using System.Linq.Expressions;

namespace MatrixDotNet.Math
{
    public class BitwiseGeneric<T>
    {
        #region Not

        private static Func<T, T> NotFunc;

        public static Func<T, T> GetNotFunc()
        {
            if (NotFunc != null)
                return NotFunc;

            var leftPar = Expression.Parameter(typeof(T), "left");
            var body = Expression.Not(leftPar);

            var func = Expression.Lambda<Func<T, T>>(body, leftPar).Compile();
            NotFunc = func;

            return func;
        }

        public static T Not(T left)
        {
            return GetNotFunc()(left);
        }

        #endregion
    }
}
