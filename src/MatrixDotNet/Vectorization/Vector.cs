using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Performance.Simd.Handler;
using MatrixDotNet.Math;
#if NET5_0 || NETCOREAPP3_1
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
#endif

namespace MatrixDotNet.Vectorization
{
    public partial class Vector<T> where T : unmanaged
    {
        /// <summary>
        ///     Creates empty array with <c>n</c> length.
        /// </summary>
        /// <param name="n">the length of vector</param>
        public Vector(int n)
        {
            Length = n;
            Array = new T[Length];
        }

        /// <summary>
        ///     Assign array
        /// </summary>
        public unsafe Vector(T[] array)
        {
            Length = array.Length;
            Array = new T[Length];
            fixed (T* ptr1 = Array)
            fixed (T* ptr2 = array)
            {
                Unsafe.CopyBlock(ptr1, ptr2, (uint) (sizeof(T) * Length));
            }
        }

        /// <summary>
        ///     Initialize Vector and fill vector of specify value.
        /// </summary>
        /// <param name="length">the length of array</param>
        /// <param name="value">fill vector of specify value</param>
        public unsafe Vector(int length, T value)
        {
            Length = length;
            Array = new T[Length];

#if NET5_0 || NETCOREAPP3_1
            if (Avx.IsSupported)
            {
                var vector = IntrinsicsHandler<T>.CreateVector256(value);
                var i = 0;
                fixed (T* ptr = Array)
                {
                    var size = Vector256<T>.Count;
                    var lastIndexBlock = length - length % size;
                    for (; i < lastIndexBlock; i += size) IntrinsicsHandler<T>.StoreVector256(ptr + i, vector);
                }

                for (; i < length; i++) Array[i] = value;
            }
            else if (Sse.IsSupported)
            {
                var vector = IntrinsicsHandler<T>.CreateVector128(value);
                var i = 0;
                fixed (T* ptr = Array)
                {
                    var size = Vector128<T>.Count;
                    var lastIndexBlock = length - length % size;
                    for (; i < lastIndexBlock; i += size) IntrinsicsHandler<T>.StoreVector128(ptr + i, vector);
                }

                for (; i < length; i++) Array[i] = value;
            }
            else
#endif
            {
                System.Array.Fill(Array, value);
            }
        }

        /// <summary>
        ///     Gets array of <c>Vector</c>.
        /// </summary>
        public T[] Array { get; }

        /// <summary>
        ///     Gets length of array.
        /// </summary>
        public int Length { get; }

        /// <summary>
        ///     Gets element of vector.
        /// </summary>
        /// <param name="i">the index of vector</param>
        public T this[int i]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Array[i];

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => Array[i] = value;
        }

        /// <summary>
        ///     Fill Vector with specified value.
        /// </summary>
        /// <param name="value">value to fill vector</param>
        public unsafe void Fill(T value)
        {
#if NET5_0 || NETCOREAPP3_1

            if (Avx.IsSupported)
            {
                var vector = IntrinsicsHandler<T>.CreateVector256(value);
                var i = 0;
                var length = Array.Length;
                fixed (T* ptr = Array)
                {
                    var size = Vector256<T>.Count;
                    var lastIndexBlock = length - length % size;
                    for (; i < lastIndexBlock; i += size) IntrinsicsHandler<T>.StoreVector256(ptr + i, vector);
                }

                for (; i < length; i++) Array[i] = value;
            }
            else if (Sse.IsSupported)
            {
                var vector = IntrinsicsHandler<T>.CreateVector128(value);
                var i = 0;
                var length = Array.Length;
                fixed (T* ptr = Array)
                {
                    var size = Vector128<T>.Count;
                    var lastIndexBlock = length - length % size;
                    for (; i < lastIndexBlock; i += size) IntrinsicsHandler<T>.StoreVector128(ptr + i, vector);
                }

                for (; i < length; i++) Array[i] = value;
            }
            else
#endif
            {
                System.Array.Fill(Array, value);
            }
        }

        /// <summary>
        ///     Gets length of vector
        /// </summary>
        public T GetLengthVec()
        {
            T sum = default;
            for (var i = 0; i < Length; i++) sum = MathUnsafe<T>.Add(sum, MathUnsafe<T>.Mul(this[i], this[i]));

            return MathGeneric<T>.Sqrt(sum);
        }

        /// <summary>
        ///     Adds two vectors.
        /// </summary>
        /// <param name="a">the left vector</param>
        /// <param name="b">the right vector</param>
        /// <returns>new vector after addition of two vectors</returns>
        private static Vector<T> Add(T[] a, T[] b)
        {
            CheckLength(a, b);
            var vc = new Vector<T>(a.Length);

            for (var i = 0; i < vc.Length; i++) vc[i] = MathUnsafe<T>.Add(a[i], b[i]);

            return vc;
        }

        /// <summary>
        ///     Represents multiplication of value on vector.
        /// </summary>
        /// <param name="val">the left vector</param>
        /// <param name="b">the right vector</param>
        /// <returns>new vector after multiply constant on vector.</returns>
        private static Vector<T> Mul(T val, T[] b)
        {
            var vc = new Vector<T>(b.Length);

            for (var i = 0; i < vc.Length; i++) vc[i] = MathUnsafe<T>.Mul(val, b[i]);

            return vc;
        }

        /// <summary>
        ///     Represents subtraction of two vectors.
        /// </summary>
        /// <param name="a">the left vector</param>
        /// <param name="b">the right vector</param>
        /// <returns>new vector after subtraction of two vectors</returns>
        private static Vector<T> Sub(T[] a, T[] b)
        {
            CheckLength(a, b);
            var vc = new Vector<T>(a.Length);

            for (var i = 0; i < vc.Length; i++) vc[i] = MathUnsafe<T>.Sub(a[i], b[i]);

            return vc;
        }


        /// <summary>
        ///     Represents defines dot(scalar) product.
        /// </summary>
        /// <param name="a">the left vector</param>
        /// <param name="b">the right vector</param>
        /// <returns>new vector after multiplication of two vectors</returns>
        private static T DotProduct(T[] a, T[] b)
        {
            CheckLength(a, b);

            T res = default;
            var size = System.Numerics.Vector<T>.Count;
            var i = 0;
            var lastIndexBlock = a.Length - a.Length % size;

            for (; i < lastIndexBlock; i += size)
            {
                var va = new System.Numerics.Vector<T>(a, i);
                var vb = new System.Numerics.Vector<T>(b, i);
                res = MathUnsafe<T>.Add(res, Vector.Dot(va, vb));
            }

            for (; i < a.Length; i++) res = MathUnsafe<T>.Add(res, MathUnsafe<T>.Mul(a[i], b[i]));

            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckLength(T[] a, T[] b)
        {
            if (a.Length != b.Length) throw new MatrixDotNetException(ExceptionArgument.VectorLength);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector<T>)) throw new InvalidCastException("object is not Vector<T>");

            var vec = (Vector<T>) obj;

            if (vec.Array.Length != Length) return false;

            if (vec.Array == Array) return true;

            var i = 0;
            var size = System.Numerics.Vector<T>.Count;
            var lastIndexBlock = vec.Length - vec.Length % size;

            for (; i < lastIndexBlock; i += size)
            {
                var vectorA = new System.Numerics.Vector<T>(Array, i);
                var vectorB = new System.Numerics.Vector<T>(vec.Array, i);
                var equal = Vector.EqualsAll(vectorA, vectorB);
                if (!equal) return false;
            }

            var cmp = Comparer<T>.Default;

            for (; i < vec.Length; i++)
                if (cmp.Compare(Array[i], vec.Array[i]) != 0)
                    return false;

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Array != null ? Array.GetHashCode() : 0) * 397) ^ Length;
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("<");

            for (var i = 0; i < Array.Length - 1; i++) builder.Append($"{Array[i]},");

            builder.Append(Array[^1]);
            builder.Append(">");
            builder.AppendLine();

            return builder.ToString();
        }
    }
}