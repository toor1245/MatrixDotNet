using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Math;

namespace MatrixDotNet
{
    public partial class Vector<T> where T : unmanaged
    {
        /// <summary>
        /// Gets array of <c>Vector</c>.
        /// </summary>
        public T[] Array { get; }

        /// <summary>
        /// Gets length of array.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Creates empty array with <c>n</c> length.
        /// </summary>
        /// <param name="n">the length of vector</param>
        public Vector(int n)
        {
            Length = n;
            Array = new T[Length];
        }

        /// <summary>
        /// Assign array
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
        /// Initialize Vector and fill vector of specify value.
        /// </summary>
        /// <param name="length">the length of array</param>
        /// <param name="fill">fill vector of specify value</param>
        public Vector(int length, T fill)
        {
            Length = length;
            Array = new T[Length];
            System.Array.Fill(Array, fill);
        }

        /// <summary>
        /// Gets element of vector.
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
        /// Gets length of vector
        /// </summary>
        public T GetLengthVec()
        {
            T sum = default;
            for (int i = 0; i < Length; i++)
            {
                sum = MathUnsafe<T>.Add(sum, MathUnsafe<T>.Mul(this[i], this[i]));
            }

            return MathGeneric<T>.Sqrt(sum);
        }

        /// <summary>
        /// Adds two vectors.
        /// </summary>
        /// <param name="a">the left vector</param>
        /// <param name="b">the right vector</param>
        /// <returns>new vector after addition of two vectors</returns>
        private static Vector<T> Add(T[] a, T[] b)
        {
            CheckLength(a, b);
            Vector<T> vc = new Vector<T>(a.Length);

            for (int i = 0; i < vc.Length; i++)
            {
                vc[i] = MathUnsafe<T>.Add(a[i], b[i]);
            }

            return vc;
        }

        /// <summary>
        /// Represents multiplication of value on vector.
        /// </summary>
        /// <param name="val">the left vector</param>
        /// <param name="b">the right vector</param>
        /// <returns>new vector after multiply constant on vector.</returns>
        private static Vector<T> Mul(T val, T[] b)
        {
            Vector<T> vc = new Vector<T>(b.Length);

            for (int i = 0; i < vc.Length; i++)
            {
                vc[i] = MathUnsafe<T>.Mul(val, b[i]);
            }

            return vc;
        }

        /// <summary>
        /// Represents subtraction of two vectors.
        /// </summary>
        /// <param name="a">the left vector</param>
        /// <param name="b">the right vector</param>
        /// <returns>new vector after subtraction of two vectors</returns>
        private static Vector<T> Sub(T[] a, T[] b)
        {
            CheckLength(a, b);
            var vc = new Vector<T>(a.Length);

            for (int i = 0; i < vc.Length; i++)
            {
                vc[i] = MathUnsafe<T>.Sub(a[i], b[i]);
            }

            return vc;
        }


        /// <summary>
        /// Represents defines dot(scalar) product.
        /// </summary>
        /// <param name="a">the left vector</param>
        /// <param name="b">the right vector</param>
        /// <returns>new vector after multiplication of two vectors</returns>
        private static T DotProduct(T[] a, T[] b)
        {
            CheckLength(a, b);

            T res = default;
            int size = System.Numerics.Vector<T>.Count;
            int i = 0;
            int lastIndexBlock = a.Length - a.Length % size;

            for (; i < lastIndexBlock; i += size)
            {
                var va = new System.Numerics.Vector<T>(a, i);
                var vb = new System.Numerics.Vector<T>(b, i);
                res = MathUnsafe<T>.Add(res, Vector.Dot(va, vb));
            }

            for (; i < a.Length; i++)
            {
                res = MathUnsafe<T>.Add(res, MathUnsafe<T>.Mul(a[i], b[i]));
            }

            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckLength(T[] a, T[] b)
        {
            if (a.Length != b.Length)
            {
                throw new MatrixDotNetException("a length not equals b length");
            }
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector<T>))
            {
                throw new InvalidCastException("object is not Vector<T>");
            }

            var vec = (Vector<T>) obj;

            if (vec.Array.Length != Length)
            {
                return false;
            }

            if (vec.Array == Array)
            {
                return true;
            }

            int i = 0;
            int size = System.Numerics.Vector<T>.Count;
            int lastIndexBlock = vec.Length - vec.Length % size;

            for (; i < lastIndexBlock; i += size)
            {
                var vectorA = new System.Numerics.Vector<T>(Array, i);
                var vectorB = new System.Numerics.Vector<T>(vec.Array, i);
                bool equal = Vector.EqualsAll(vectorA, vectorB);
                if (!equal)
                {
                    return false;
                }
            }

            var cmp = Comparer<T>.Default;

            for (; i < vec.Length; i++)
            {
                if (cmp.Compare(Array[i], vec.Array[i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((Array != null ? Array.GetHashCode() : 0) * 397) ^ Length;
            }
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < Array.Length; i++)
            {
                builder.Append(Array[i] + " ");
            }

            return builder.ToString();
        }
    }
}