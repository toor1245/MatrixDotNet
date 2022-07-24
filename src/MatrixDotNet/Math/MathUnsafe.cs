using System;
using System.Runtime.CompilerServices;

namespace MatrixDotNet.Math
{
    public static class MathUnsafe<T> where T : unmanaged
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                var sum = Unsafe.As<T, int>(ref left) + Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(double))
            {
                var sum = Unsafe.As<T, double>(ref left) + Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref sum);
            }

            if (typeof(T) == typeof(float))
            {
                var sum = Unsafe.As<T, float>(ref left) + Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref sum);
            }

            if (typeof(T) == typeof(decimal))
            {
                var sum = Unsafe.As<T, decimal>(ref left) + Unsafe.As<T, decimal>(ref right);
                return Unsafe.As<decimal, T>(ref sum);
            }

            if (typeof(T) == typeof(long))
            {
                var sum = Unsafe.As<T, long>(ref left) + Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }

            if (typeof(T) == typeof(short))
            {
                var sum = Unsafe.As<T, short>(ref left) + Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(byte))
            {
                var sum = Unsafe.As<T, byte>(ref left) + Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(ulong))
            {
                var sum = Unsafe.As<T, ulong>(ref left) + Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }

            if (typeof(T) == typeof(uint))
            {
                var sum = Unsafe.As<T, uint>(ref left) + Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sub(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                var sum = Unsafe.As<T, int>(ref left) - Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(double))
            {
                var sum = Unsafe.As<T, double>(ref left) - Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref sum);
            }

            if (typeof(T) == typeof(float))
            {
                var sum = Unsafe.As<T, float>(ref left) - Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref sum);
            }

            if (typeof(T) == typeof(decimal))
            {
                var sum = Unsafe.As<T, decimal>(ref left) - Unsafe.As<T, decimal>(ref right);
                return Unsafe.As<decimal, T>(ref sum);
            }

            if (typeof(T) == typeof(long))
            {
                var sum = Unsafe.As<T, long>(ref left) - Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }

            if (typeof(T) == typeof(short))
            {
                var sum = Unsafe.As<T, short>(ref left) - Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(byte))
            {
                var sum = Unsafe.As<T, byte>(ref left) - Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(ulong))
            {
                var sum = Unsafe.As<T, ulong>(ref left) - Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }

            if (typeof(T) == typeof(uint))
            {
                var sum = Unsafe.As<T, uint>(ref left) - Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Mul(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                var sum = Unsafe.As<T, int>(ref left) * Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(double))
            {
                var sum = Unsafe.As<T, double>(ref left) * Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref sum);
            }

            if (typeof(T) == typeof(float))
            {
                var sum = Unsafe.As<T, float>(ref left) * Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref sum);
            }

            if (typeof(T) == typeof(decimal))
            {
                var sum = Unsafe.As<T, decimal>(ref left) * Unsafe.As<T, decimal>(ref right);
                return Unsafe.As<decimal, T>(ref sum);
            }

            if (typeof(T) == typeof(long))
            {
                var sum = Unsafe.As<T, long>(ref left) * Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }

            if (typeof(T) == typeof(short))
            {
                var sum = Unsafe.As<T, short>(ref left) * Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(byte))
            {
                var sum = Unsafe.As<T, byte>(ref left) * Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(ulong))
            {
                var sum = Unsafe.As<T, ulong>(ref left) * Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }

            if (typeof(T) == typeof(uint))
            {
                var sum = Unsafe.As<T, uint>(ref left) * Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Div(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                var sum = Unsafe.As<T, int>(ref left) / Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(double))
            {
                var sum = Unsafe.As<T, double>(ref left) / Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref sum);
            }

            if (typeof(T) == typeof(float))
            {
                var sum = Unsafe.As<T, float>(ref left) / Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref sum);
            }

            if (typeof(T) == typeof(decimal))
            {
                var sum = Unsafe.As<T, decimal>(ref left) / Unsafe.As<T, decimal>(ref right);
                return Unsafe.As<decimal, T>(ref sum);
            }

            if (typeof(T) == typeof(long))
            {
                var sum = Unsafe.As<T, long>(ref left) / Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }

            if (typeof(T) == typeof(short))
            {
                var sum = Unsafe.As<T, short>(ref left) / Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(byte))
            {
                var sum = Unsafe.As<T, byte>(ref left) / Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(ulong))
            {
                var sum = Unsafe.As<T, ulong>(ref left) / Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }

            if (typeof(T) == typeof(uint))
            {
                var sum = Unsafe.As<T, uint>(ref left) / Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Mod(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                var sum = Unsafe.As<T, int>(ref left) % Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(double))
            {
                var sum = Unsafe.As<T, double>(ref left) % Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref sum);
            }

            if (typeof(T) == typeof(float))
            {
                var sum = Unsafe.As<T, float>(ref left) % Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref sum);
            }

            if (typeof(T) == typeof(decimal))
            {
                var sum = Unsafe.As<T, decimal>(ref left) % Unsafe.As<T, decimal>(ref right);
                return Unsafe.As<decimal, T>(ref sum);
            }

            if (typeof(T) == typeof(long))
            {
                var sum = Unsafe.As<T, long>(ref left) % Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }

            if (typeof(T) == typeof(short))
            {
                var sum = Unsafe.As<T, short>(ref left) % Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(byte))
            {
                var sum = Unsafe.As<T, byte>(ref left) % Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(ulong))
            {
                var sum = Unsafe.As<T, ulong>(ref left) % Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }

            if (typeof(T) == typeof(uint))
            {
                var sum = Unsafe.As<T, uint>(ref left) % Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ShiftLeft(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                var sum = Unsafe.As<T, int>(ref left) << Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(long))
            {
                var sum = Unsafe.As<T, long>(ref left) << Unsafe.As<T, int>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }

            if (typeof(T) == typeof(short))
            {
                var sum = Unsafe.As<T, short>(ref left) << Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(byte))
            {
                var sum = Unsafe.As<T, byte>(ref left) << Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(ulong))
            {
                var sum = Unsafe.As<T, ulong>(ref left) << Unsafe.As<T, int>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }

            if (typeof(T) == typeof(uint))
            {
                var sum = Unsafe.As<T, uint>(ref left) << Unsafe.As<T, int>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ShiftRight(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                var sum = Unsafe.As<T, int>(ref left) >> Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(long))
            {
                var sum = Unsafe.As<T, long>(ref left) >> Unsafe.As<T, int>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }

            if (typeof(T) == typeof(short))
            {
                var sum = Unsafe.As<T, short>(ref left) >> Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(byte))
            {
                var sum = Unsafe.As<T, byte>(ref left) >> Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }

            if (typeof(T) == typeof(ulong))
            {
                var sum = Unsafe.As<T, ulong>(ref left) >> Unsafe.As<T, int>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }

            if (typeof(T) == typeof(uint))
            {
                var sum = Unsafe.As<T, uint>(ref left) >> Unsafe.As<T, int>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Abs(T arg)
        {
            if (typeof(T) == typeof(int))
            {
                var abs = Unsafe.As<T, int>(ref arg);
                var source = System.Math.Abs(abs);
                return Unsafe.As<int, T>(ref source);
            }

            if (typeof(T) == typeof(double))
            {
                var abs = Unsafe.As<T, double>(ref arg);
                var source = System.Math.Abs(abs);
                return Unsafe.As<double, T>(ref source);
            }

            if (typeof(T) == typeof(float))
            {
                var abs = Unsafe.As<T, float>(ref arg);
                var source = System.Math.Abs(abs);
                return Unsafe.As<float, T>(ref source);
            }

            if (typeof(T) == typeof(decimal))
            {
                var abs = Unsafe.As<T, decimal>(ref arg);
                var source = System.Math.Abs(abs);
                return Unsafe.As<decimal, T>(ref source);
            }

            if (typeof(T) == typeof(long))
            {
                var abs = Unsafe.As<T, long>(ref arg);
                var source = System.Math.Abs(abs);
                return Unsafe.As<long, T>(ref source);
            }

            if (typeof(T) == typeof(short))
            {
                var abs = Unsafe.As<T, short>(ref arg);
                var source = System.Math.Abs(abs);
                return Unsafe.As<short, T>(ref source);
            }

            if (typeof(T) == typeof(byte))
            {
                var abs = Unsafe.As<T, byte>(ref arg);
                int source = System.Math.Abs(abs);
                return Unsafe.As<int, T>(ref source);
            }

            throw new NotSupportedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Negate(T arg)
        {
            if (typeof(T) == typeof(int))
            {
                var negate = -Unsafe.As<T, int>(ref arg);
                return Unsafe.As<int, T>(ref negate);
            }

            if (typeof(T) == typeof(double))
            {
                var negate = -Unsafe.As<T, double>(ref arg);
                return Unsafe.As<double, T>(ref negate);
            }

            if (typeof(T) == typeof(float))
            {
                var negate = -Unsafe.As<T, float>(ref arg);
                return Unsafe.As<float, T>(ref negate);
            }

            if (typeof(T) == typeof(decimal))
            {
                var negate = -Unsafe.As<T, decimal>(ref arg);
                return Unsafe.As<decimal, T>(ref negate);
            }

            if (typeof(T) == typeof(long))
            {
                var negate = -Unsafe.As<T, long>(ref arg);
                return Unsafe.As<long, T>(ref negate);
            }

            if (typeof(T) == typeof(short))
            {
                var negate = (short) -Unsafe.As<T, short>(ref arg);
                return Unsafe.As<short, T>(ref negate);
            }


            throw new NotSupportedException();
        }
    }
}