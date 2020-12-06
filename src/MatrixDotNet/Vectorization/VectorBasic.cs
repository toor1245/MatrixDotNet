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
    }
}