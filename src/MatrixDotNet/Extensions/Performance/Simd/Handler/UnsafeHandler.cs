using System.Runtime.CompilerServices;

namespace MatrixDotNet.Extensions.Performance.Simd.Handler
{
    internal static unsafe class UnsafeHandler
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T* GetReference<T>(T[] array)
            where T : unmanaged
        {
            fixed (T* ptr = &array[0])
            {
                return ptr;
            }
        }
    }
}