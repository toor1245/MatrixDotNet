using System;
using System.Collections.Generic;
using MatrixDotNet.Extensions.Conversion;

namespace MatrixDotNet.Vectorization
{
    public static partial class VectorExtension
    {
        /// <summary>
        /// Gets minimal element of vector.
        /// </summary>
        /// <param name="vector">vector</param>
        /// <typeparam name="T">unmanaged type</typeparam>
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

        /// <summary>
        /// Gets minimal element of vector.
        /// </summary>
        /// <param name="vector">vector</param>
        /// <typeparam name="T">unmanaged type</typeparam>
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

        /// <summary>
        /// Sorts vector.
        /// </summary>
        /// <param name="vector">vector</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        public static void Sort<T>(this Vector<T> vector) where T : unmanaged
        {
            Array.Sort(vector.Array);
        }

        /// <summary>
        /// Reverse vector.
        /// </summary>
        /// <param name="vector">vector</param>
        public static void Reverse(Vector<int> vector)
        {
            MatrixConverter.Reverse(vector.Array);
        }

        /// <summary>
        /// Reverse vector.
        /// </summary>
        /// <param name="vector">vector</param>
        public static void Reverse(Vector<uint> vector)
        {
            MatrixConverter.Reverse(vector.Array);
        }

        /// <summary>
        /// Reverse vector.
        /// </summary>
        /// <param name="vector">vector</param>
        public static void Reverse(Vector<long> vector)
        {
            MatrixConverter.Reverse(vector.Array);
        }

        /// <summary>
        /// Reverse vector.
        /// </summary>
        /// <param name="vector">vector</param>
        public static void Reverse(Vector<ulong> vector)
        {
            MatrixConverter.Reverse(vector.Array);
        }

        /// <summary>
        /// Reverse vector.
        /// </summary>
        /// <param name="vector">vector</param>
        public static void Reverse(Vector<double> vector)
        {
            MatrixConverter.Reverse(vector.Array);
        }

        /// <summary>
        /// Reverse vector.
        /// </summary>
        /// <param name="vector">vector</param>
        public static void Reverse(Vector<float> vector)
        {
            MatrixConverter.Reverse(vector.Array);
        }

        /// <summary>
        /// Reverse vector.
        /// </summary>
        /// <param name="vector">vector</param>
        public static void Reverse(Vector<byte> vector)
        {
            MatrixConverter.Reverse(vector.Array);
        }

        /// <summary>
        /// Reverse vector.
        /// </summary>
        /// <param name="vector">vector</param>
        public static void Reverse(Vector<sbyte> vector)
        {
            MatrixConverter.Reverse(vector.Array);
        }
    }
}
