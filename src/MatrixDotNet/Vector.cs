﻿using System;
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

        public Vector(T[] array)
        {
            
            Length = array.Length;
            Array = new T[Length];
            for (int i = 0; i < Length; i++)
            {
                Array[i] = array[i];
            }
        }

        public T this[int i]
        {
            get => Array[i];
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

        private static T ScalarProduct(T[] a,T[] b)
        {
            CheckLength(a,b);
            T res = default;
            for (int i = 0; i < a.Length; i++)
            {
                res = MathExtension.Add(res,MathExtension.Multiply(a[i],b[i]));
            }
            return res;
        }

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
            for (int i = 0; i < vec.Length; i++)
            {
                if (!vec[i].Equals(this[i]))
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