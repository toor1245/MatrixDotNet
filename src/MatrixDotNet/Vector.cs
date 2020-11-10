using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Math;

namespace MatrixDotNet
{
    public partial class Vector<T> where T : unmanaged
    {
        public T[] Array { get; }
        
        /// <summary>
        /// Gets length of array.
        /// </summary>
        public int Length { get; }

        public Vector(int n)
        {
            Array = new T[n];
            Length = n;
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
                Unsafe.CopyBlock(ptr1,ptr2,(uint) (sizeof(T) * Length));
            }
        }

        /// <summary>
        /// Initialize Vector and fill vector of specify value.
        /// </summary>
        /// <param name="length">the length of array</param>
        /// <param name="fill">fill vector of specify value</param>
        public Vector(int length,T fill)
        {
            Length = length;
            Array = new T[Length];
            System.Array.Fill(Array,fill);
        }

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
                sum = MathExtension.Add(sum, MathExtension.Multiply(this[i],this[i]));
            }
            return MathExtension.Sqrt(sum);
        }
        
        private static Vector<T> Add(T[] a, T[] b)
        {
            CheckLength(a,b);
            Vector<T> c = new Vector<T>(a.Length);
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = MathExtension.Add(a[i],b[i]);
            }

            return c;
        }
        
        private static Vector<T> Mul(T val, T[] b)
        {
            Vector<T> c = new Vector<T>(b.Length);
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = MathExtension.Multiply(val,b[i]);
            }

            return c;
        }
        
        private static Vector<T> Sub(T[] a, T[] b)
        {
            CheckLength(a,b);
            Vector<T> c = new Vector<T>(a.Length);
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = MathExtension.Sub(a[i],b[i]);
            }

            return c;
        }

        private static T DotProduct(T[] a,T[] b)
        {
            CheckLength(a,b);
            T res = default;
            int size = System.Numerics.Vector<T>.Count;
            int i = 0;
            for (; i < a.Length - size; i += size)
            {
                System.Numerics.Vector<T> va = new System.Numerics.Vector<T>(a,i);
                System.Numerics.Vector<T> vb = new System.Numerics.Vector<T>(b,i);
                res = MathUnsafe<T>.Add(res,Vector.Dot(va, vb));
            }

            for (; i < a.Length; i++)
            {
                res = MathUnsafe<T>.Add(res, MathUnsafe<T>.Mul(a[i],b[i]));
            }
            return res;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void CheckLength(T[] a,T[] b)
        {
            if(a.Length != b.Length)
                throw new MatrixDotNetException("a length not equals b length");
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector<T>))
                throw new InvalidCastException("object is not Vector<T>");
            
            var vec = (Vector<T>)obj;

            if (vec.Array == Array)
            {
                return true;
            }
            
            int i = 0;
            
            for (; i < vec.Length - System.Numerics.Vector<T>.Count; i += System.Numerics.Vector<T>.Count)
            {
                var vectorA = new System.Numerics.Vector<T>(Array,i);
                var vectorB = new System.Numerics.Vector<T>(vec.Array,i);
                bool vector = Vector.EqualsAll(vectorA,vectorB);
                if (!vector)
                {
                    return false;
                }
            }

            for (; i < vec.Length; i++)
            {
                if (!Array[i].Equals(vec.Array[i]))
                {
                    return false;
                }
            }
            
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
            StringBuilder builder = new StringBuilder();
            
            for (int i = 0; i < Array.Length; i++)
            {
                builder.Append(Array[i] + " ");
            }

            return builder.ToString();
        }
    }
}