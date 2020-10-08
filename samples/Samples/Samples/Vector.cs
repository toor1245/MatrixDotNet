﻿using System.Text;

 namespace Samples
{
    public class Vector
    {
        public int[] Array { get; }
        public int Length { get; }

        public Vector(int n)
        {
            Array = new int[n];
            Length = n;
            for (int i = 0; i < n; i++)
            {
                Array[i] = 1;
            }
        }

        public Vector(int[] array)
        {
            
            Length = array.Length;
            Array = new int[Length];
            for (int i = 0; i < Length; i++)
            {
                Array[i] = array[i];
            }
        }

        public int this[int i]
        {
            get => Array[i];
            set => Array[i] = value;
        }

        public static Vector operator +(Vector A,Vector B)
        {
            return Add(A.Array,B.Array);
        }
        
        public static Vector operator +(Vector A,int[] B)
        {
            return Add(A.Array,B);
        }
        
        public static Vector operator +(int[] B,Vector A)
        {
            return Add(A.Array,B);
        }

        private static Vector Add(int[] A, int[] B)
        {
            Vector C = new Vector(A.Length);
            for (int i = 0; i < C.Length; i++)
            {
                C[i] = A[i] + B[i];
            }

            return C;
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