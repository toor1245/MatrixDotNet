using System;

namespace MatrixDotNet
{
    public partial class Vector<T> where T : unmanaged
    {
        public static bool operator ==(Vector<T> a,Vector<T> b)
        {
            if (a is null || b is null)
                throw new NullReferenceException();

            return Equals(a,b);
        }


        public static bool operator !=(Vector<T> a, Vector<T> b)
        {
            return !(a == b);
        }

        public static Vector<T> operator +(Vector<T> a,Vector<T> b)
        {
            return Add(a.Array,b.Array);
        }
        
        public static Vector<T> operator +(Vector<T> a,T[] b)
        {
            return Add(a.Array,b);
        }
        
        public static Vector<T> operator +(T[] a,Vector<T> b)
        {
            return Add(a,b.Array);
        }
        
        public static Vector<T> operator -(Vector<T> a,Vector<T> b)
        {
            return Sub(a.Array,b.Array);
        }
        
        public static Vector<T> operator -(Vector<T> a,T[] b)
        {
            return Sub(a.Array,b);
        }
        
        public static Vector<T> operator -(T[] a,Vector<T> b)
        {
            return Sub(a,b.Array);
        }
        
        public static Vector<T> operator *(Vector<T> a,T b)
        {
            return Mul(b,a.Array);
        }
        
        public static Vector<T> operator *(T a,Vector<T> b)
        {
            return Mul(a,b.Array);
        }

        public static T operator *(Vector<T> a,Vector<T> b)
        {
            return ScalarProduct(a.Array,b.Array);
        }
        
        public static implicit operator Vector<T>(T[] arr)
        {
            return new Vector<T>(arr);
        }
    }
}