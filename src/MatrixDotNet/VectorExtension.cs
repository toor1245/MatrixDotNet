using System;
using System.Collections.Generic;

namespace MatrixDotNet
{
    public static partial class VectorExtension
    {
        public static T Min<T>(this Vector<T> vector) where T : unmanaged
        {
            var comparer = Comparer<T>.Default;

            T min = vector[0];
            for (int i = 1; i < vector.Length; i++)
            {
                if (comparer.Compare(min, vector[i]) > 0)
                {
                    min = vector[i];
                }
            }

            return min;
        }

        public static T Max<T>(this Vector<T> vector) where T : unmanaged
        {
            var comparer = Comparer<T>.Default;

            T max = vector[0];
            for (int i = 1; i < vector.Length; i++)
            {
                if (comparer.Compare(max, vector[i]) < 0)
                {
                    max = vector[i];
                }
            }

            return max;
        }

        public static void Sort<T>(this Vector<T> vector) where T : unmanaged
        {
            Array.Sort(vector.Array);
        }
    }
}