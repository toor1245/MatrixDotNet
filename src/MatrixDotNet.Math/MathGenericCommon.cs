using System.Runtime.CompilerServices;

namespace MatrixDotNet.Math
{
    public static class MathGeneric
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsFloatingPoint<T>()
        {
            return typeof(T) == typeof(double) ||
                    typeof(T) == typeof(float) ||
                    typeof(T) == typeof(decimal);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsInteger<T>()
        {
            var type = typeof(T);
            return type == typeof(byte) ||
                   type == typeof(sbyte) ||
                   type == typeof(short) ||
                   type == typeof(ushort) ||
                   type == typeof(int) ||
                   type == typeof(uint) ||
                   type == typeof(long) ||
                   type == typeof(ulong);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSupported<T>()
        {
            return (typeof(T) == typeof(byte)) ||
                   (typeof(T) == typeof(sbyte)) ||
                   (typeof(T) == typeof(short)) ||
                   (typeof(T) == typeof(ushort)) ||
                   (typeof(T) == typeof(int)) ||
                   (typeof(T) == typeof(uint)) ||
                   (typeof(T) == typeof(long)) ||
                   (typeof(T) == typeof(ulong)) ||
                   (typeof(T) == typeof(float)) ||
                   (typeof(T) == typeof(double));
        }
    }
}
