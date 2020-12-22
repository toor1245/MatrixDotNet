using System;
using System.Linq.Expressions;

namespace MatrixDotNet.Math
{
    public static partial class MathExtension
    {
        public static TResult Cast<TSource, TResult>(TSource arg)
        {
            var t = typeof(TSource);
            var u = typeof(TResult);

            if (Cache.TryGetValue((t, nameof(Cast)), out var del))
                return del is Func<TSource, TResult> specificFunc
                    ? specificFunc(arg)
                    : throw new InvalidOperationException(nameof(Cast));

            var body = Expression.Convert(Expression.Constant(arg), u);

            var parameter = Expression.Parameter(t, nameof(arg));
            var func = Expression.Lambda<Func<TSource, TResult>>(body, parameter).Compile();

            Cache[(t, nameof(Cast))] = func;

            return func(arg);
        }
    }
}