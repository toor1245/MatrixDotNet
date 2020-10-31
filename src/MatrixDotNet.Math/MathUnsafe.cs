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
                int sum = Unsafe.As<T, int>(ref left) + Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(double))
            {
                double sum = Unsafe.As<T, double>(ref left) + Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref sum);
            }
            if (typeof(T) == typeof(float))
            {
                float sum = Unsafe.As<T, float>(ref left) + Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref sum);
            }
            if (typeof(T) == typeof(decimal))
            {
                decimal sum = Unsafe.As<T, decimal>(ref left) + Unsafe.As<T, decimal>(ref right);
                return Unsafe.As<decimal, T>(ref sum);
            }
            if (typeof(T) == typeof(long))
            {
                long sum = Unsafe.As<T,long>(ref left) + Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }
            if (typeof(T) == typeof(short))
            {
                int sum = Unsafe.As<T,short>(ref left) + Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(byte))
            {
                int sum = Unsafe.As<T,byte>(ref left) + Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(ulong))
            {
                ulong sum = Unsafe.As<T,ulong>(ref left) + Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }
            if (typeof(T) == typeof(uint))
            {
                uint sum = Unsafe.As<T,uint>(ref left) + Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sub(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                int sum = Unsafe.As<T, int>(ref left) - Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(double))
            {
                double sum = Unsafe.As<T, double>(ref left) - Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref sum);
            }
            if (typeof(T) == typeof(float))
            {
                float sum = Unsafe.As<T, float>(ref left) - Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref sum);
            }
            if (typeof(T) == typeof(decimal))
            {
                decimal sum = Unsafe.As<T, decimal>(ref left) - Unsafe.As<T, decimal>(ref right);
                return Unsafe.As<decimal, T>(ref sum);
            }
            if (typeof(T) == typeof(long))
            {
                long sum = Unsafe.As<T,long>(ref left) - Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }
            if (typeof(T) == typeof(short))
            {
                int sum = Unsafe.As<T,short>(ref left) - Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(byte))
            {
                int sum = Unsafe.As<T,byte>(ref left) - Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(ulong))
            {
                ulong sum = Unsafe.As<T,ulong>(ref left) - Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }
            if (typeof(T) == typeof(uint))
            {
                uint sum = Unsafe.As<T,uint>(ref left) - Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Mul(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                int sum = Unsafe.As<T, int>(ref left) * Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(double))
            {
                double sum = Unsafe.As<T, double>(ref left) * Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref sum);
            }
            if (typeof(T) == typeof(float))
            {
                float sum = Unsafe.As<T, float>(ref left) * Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref sum);
            }
            if (typeof(T) == typeof(decimal))
            {
                decimal sum = Unsafe.As<T, decimal>(ref left) * Unsafe.As<T, decimal>(ref right);
                return Unsafe.As<decimal, T>(ref sum);
            }
            if (typeof(T) == typeof(long))
            {
                long sum = Unsafe.As<T,long>(ref left) * Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }
            if (typeof(T) == typeof(short))
            {
                int sum = Unsafe.As<T,short>(ref left) * Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(byte))
            {
                int sum = Unsafe.As<T,byte>(ref left) * Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(ulong))
            {
                ulong sum = Unsafe.As<T,ulong>(ref left) * Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }
            if (typeof(T) == typeof(uint))
            {
                uint sum = Unsafe.As<T,uint>(ref left) * Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Div(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                int sum = Unsafe.As<T, int>(ref left) / Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(double))
            {
                double sum = Unsafe.As<T, double>(ref left) / Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref sum);
            }
            if (typeof(T) == typeof(float))
            {
                float sum = Unsafe.As<T, float>(ref left) / Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref sum);
            }
            if (typeof(T) == typeof(decimal))
            {
                decimal sum = Unsafe.As<T, decimal>(ref left) / Unsafe.As<T, decimal>(ref right);
                return Unsafe.As<decimal, T>(ref sum);
            }
            if (typeof(T) == typeof(long))
            {
                long sum = Unsafe.As<T,long>(ref left) / Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }
            if (typeof(T) == typeof(short))
            {
                int sum = Unsafe.As<T,short>(ref left) / Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(byte))
            {
                int sum = Unsafe.As<T,byte>(ref left) / Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(ulong))
            {
                ulong sum = Unsafe.As<T,ulong>(ref left) / Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }
            if (typeof(T) == typeof(uint))
            {
                uint sum = Unsafe.As<T,uint>(ref left) / Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }
            
            throw new NotSupportedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Mod(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                int sum = Unsafe.As<T, int>(ref left) % Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(double))
            {
                double sum = Unsafe.As<T, double>(ref left) % Unsafe.As<T, double>(ref right);
                return Unsafe.As<double, T>(ref sum);
            }
            if (typeof(T) == typeof(float))
            {
                float sum = Unsafe.As<T, float>(ref left) % Unsafe.As<T, float>(ref right);
                return Unsafe.As<float, T>(ref sum);
            }
            if (typeof(T) == typeof(decimal))
            {
                decimal sum = Unsafe.As<T, decimal>(ref left) % Unsafe.As<T, decimal>(ref right);
                return Unsafe.As<decimal, T>(ref sum);
            }
            if (typeof(T) == typeof(long))
            {
                long sum = Unsafe.As<T,long>(ref left) % Unsafe.As<T, long>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }
            if (typeof(T) == typeof(short))
            {
                int sum = Unsafe.As<T,short>(ref left) % Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(byte))
            {
                int sum = Unsafe.As<T,byte>(ref left) % Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(ulong))
            {
                ulong sum = Unsafe.As<T,ulong>(ref left) % Unsafe.As<T, ulong>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }
            if (typeof(T) == typeof(uint))
            {
                uint sum = Unsafe.As<T,uint>(ref left) % Unsafe.As<T, uint>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ShiftLeft(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                int sum = Unsafe.As<T, int>(ref left) << Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(long))
            {
                long sum = Unsafe.As<T,long>(ref left) << Unsafe.As<T, int>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }
            if (typeof(T) == typeof(short))
            {
                int sum = Unsafe.As<T,short>(ref left) << Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(byte))
            {
                int sum = Unsafe.As<T,byte>(ref left) << Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(ulong))
            {
                ulong sum = Unsafe.As<T,ulong>(ref left) << Unsafe.As<T, int>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }
            if (typeof(T) == typeof(uint))
            {
                uint sum = Unsafe.As<T,uint>(ref left) << Unsafe.As<T, int>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ShiftRight(T left, T right)
        {
            if (typeof(T) == typeof(int))
            {
                int sum = Unsafe.As<T, int>(ref left) >> Unsafe.As<T, int>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(long))
            {
                long sum = Unsafe.As<T,long>(ref left) >> Unsafe.As<T, int>(ref right);
                return Unsafe.As<long, T>(ref sum);
            }
            if (typeof(T) == typeof(short))
            {
                int sum = Unsafe.As<T,short>(ref left) >> Unsafe.As<T, short>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(byte))
            {
                int sum = Unsafe.As<T,byte>(ref left) >> Unsafe.As<T, byte>(ref right);
                return Unsafe.As<int, T>(ref sum);
            }
            if (typeof(T) == typeof(ulong))
            {
                ulong sum = Unsafe.As<T,ulong>(ref left) >> Unsafe.As<T, int>(ref right);
                return Unsafe.As<ulong, T>(ref sum);
            }
            if (typeof(T) == typeof(uint))
            {
                uint sum = Unsafe.As<T,uint>(ref left) >> Unsafe.As<T, int>(ref right);
                return Unsafe.As<uint, T>(ref sum);
            }

            throw new NotSupportedException();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Abs(T arg)
        {
            if (typeof(T) == typeof(int))
            {
                int abs = Unsafe.As<T, int>(ref arg);
                int source = System.Math.Abs(abs);
                return Unsafe.As<int,T>(ref source);
            }
            if (typeof(T) == typeof(double))
            {
                double abs = Unsafe.As<T, double>(ref arg);
                double source = System.Math.Abs(abs);
                return Unsafe.As<double,T>(ref source);
            }
            if (typeof(T) == typeof(float))
            {
                float abs = Unsafe.As<T, float>(ref arg);
                float source = System.Math.Abs(abs);
                return Unsafe.As<float,T>(ref source);
            }
            if (typeof(T) == typeof(decimal))
            {
                decimal abs = Unsafe.As<T, decimal>(ref arg);
                decimal source = System.Math.Abs(abs);
                return Unsafe.As<decimal,T>(ref source);
            }
            if (typeof(T) == typeof(long))
            {
                long abs = Unsafe.As<T, long>(ref arg);
                long source = System.Math.Abs(abs);
                return Unsafe.As<long,T>(ref source);
            }
            if (typeof(T) == typeof(short))
            {
                short abs = Unsafe.As<T, short>(ref arg);
                short source = System.Math.Abs(abs);
                return Unsafe.As<short,T>(ref source);
            }
            if (typeof(T) == typeof(byte))
            {
                byte abs = Unsafe.As<T, byte>(ref arg);
                int source = System.Math.Abs(abs);
                return Unsafe.As<int,T>(ref source);
            }
            throw new NotSupportedException();
        }
    }
}