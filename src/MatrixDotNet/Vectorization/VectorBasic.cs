using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Math;

namespace MatrixDotNet.Vectorization
{
    public static partial class VectorExtension
    {
        /// <summary>
        /// Gets matrix after tensor product of two vectors
        /// </summary>
        /// <param name="va">the left vector</param>
        /// <param name="vb">the right vector(transpose)</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns><see cref="Matrix{T}"/></returns>
        /// <exception cref="MatrixDotNetException">left vector is not equal right</exception>
        public static unsafe Matrix<T> TensorProduct<T>(Vector<T> va, Vector<T> vb)
            where T : unmanaged
        {
            int n = va.Length;

            if (n != vb.Length)
            {
                throw new MatrixDotNetException("vector length is not equal");
            }

            int size = System.Numerics.Vector<T>.Count;
            var mr = new Matrix<T>(n, n);
            int lastIndexBlock = n - n % size;
            int j = 0;

            fixed (T* ptr = mr._Matrix)
            {
                for (int i = 0; i < mr.Rows; i++)
                {
                    for (j = 0; j < lastIndexBlock; j += size)
                    {
                        var vd = new System.Numerics.Vector<T>(vb.Array, j);
                        var vc = Vector.Multiply(va[i], vd);
                        var res = (T*) Unsafe.AsPointer(ref vc);
                        Unsafe.CopyBlock(ptr + i * mr.Columns + j, res, (uint) (sizeof(T) * size));
                    }
                }

                for (int i = 0; i < mr.Rows; i++)
                {
                    for (int k = j; k < n; k++)
                    {
                        mr[i, k] = MathUnsafe<T>.Mul(va[i], vb[k]);
                    }
                }
            }

            return mr;
        }

        /// <summary>
        /// Gets distance between two points.
        /// </summary>
        /// <param name="va">vector A</param>
        /// <param name="vb">vector B</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>new vector with distance between of two points</returns>
        /// <exception cref="MatrixDotNetException">length of vector A not equal length of vector B</exception>
        public static Vector<T> GetDistancePoint<T>(Vector<T> va, Vector<T> vb)
            where T : unmanaged
        {
            int len = va.Length;

            if (len != vb.Length)
            {
                throw new MatrixDotNetException("vectors are not equal");
            }

            Vector<T> vc = new Vector<T>(len);
            int i = 0;

#if OS_WINDOWS || OS_LINUX
            int size = System.Numerics.Vector<T>.Count;
            int lastIndexBlock = len - len % size;

            for (; i < lastIndexBlock; i += size)
            {
                var vectorA = new System.Numerics.Vector<T>(va.Array, i);
                var vectorB = new System.Numerics.Vector<T>(vb.Array, i);
                var vectorC = Vector.Subtract(vectorB, vectorA);
                vectorC.CopyTo(vc.Array, i);
            }
#endif

            for (; i < vc.Length; i++)
            {
                vc[i] = MathUnsafe<T>.Sub(vb[i], va[i]);
            }

            return vc;
        }


        /// <summary>
        /// Gets vector direct cosines
        /// </summary>
        /// <param name="va">vector A</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>direct cos's</returns>
        /// <exception cref="MatrixDotNetException">
        /// throw if data type is not floating type
        /// </exception>
        public static T[] GetDirectCos<T>(Vector<T> va)
            where T : unmanaged
        {
            if (!MathGeneric.IsFloatingPoint<T>())
            {
                throw new MatrixDotNetException("not supported type, must be floating data type");
            }

            int length = va.Length;
            T[] cos = new T[length];
            T mod = va.GetLengthVec();
            Array.Fill(cos, mod);

            int i = 0;
            int size = System.Numerics.Vector<T>.Count;
            int lastIndexBlock = length - length % size;

            for (; i < lastIndexBlock; i += size)
            {
                var vt = new System.Numerics.Vector<T>(va.Array, i);
                var vf = new System.Numerics.Vector<T>(cos);
                var vc = Vector.Divide(vt, vf);
                vc.CopyTo(cos, i);
            }

            for (; i < length; i++)
            {
                cos[i] = MathUnsafe<T>.Div(va[i], cos[i]);
            }

            return cos;
        }

        /// <summary>
        /// Gets vector direct cosines by coordinate points
        /// </summary>
        /// <param name="va">vector A</param>
        /// <param name="vb">vector B</param>
        /// <typeparam name="T">unmanaged type</typeparam>
        /// <returns>direct cos's</returns>
        /// <exception cref="MatrixDotNetException">
        /// throw if data type is not floating type
        /// </exception>
        public static T[] GetDirectCos<T>(Vector<T> va, Vector<T> vb)
            where T : unmanaged
        {
            if (va.Length != vb.Length)
            {
                throw new MatrixDotNetException("vectors are not equal");
            }

            if (!MathGeneric.IsFloatingPoint<T>())
            {
                throw new MatrixDotNetException("not supported type, must be floating data type");
            }

            int length = va.Length;
            var distance = GetDistancePoint(va, vb);
            T[] cos = new T[length];
            T mod = distance.GetLengthVec();
            Array.Fill(cos, mod);

            int i = 0;
            int size = System.Numerics.Vector<T>.Count;
            int lastIndexBlock = length - length % size;

            for (; i < lastIndexBlock; i += size)
            {
                var vt = new System.Numerics.Vector<T>(distance.Array, i);
                var vf = new System.Numerics.Vector<T>(cos);
                var vc = Vector.Divide(vt, vf);
                vc.CopyTo(cos, i);
            }

            for (; i < length; i++)
            {
                cos[i] = MathUnsafe<T>.Div(distance[i], cos[i]);
            }

            return cos;
        }
    }
}