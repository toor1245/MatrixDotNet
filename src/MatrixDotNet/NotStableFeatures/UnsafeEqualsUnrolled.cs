using System.Runtime.InteropServices;
using MatrixDotNet.Math;

namespace MatrixDotNet.NotStableFeatures
{
    public class UnsafeEqualsUnrolled
    {
        [DllImport("msvcrt.dll", CallingConvention=CallingConvention.Cdecl)]
        static extern int memcmp(int[] b1, int[] b2, long count);

        public static bool ByteArrayCompare(int[] b1, int[] b2)
        {
            // Validate buffers are the same length.
            // This also ensures that the count does not exceed the length of either buffer.  
            return b1.Length == b2.Length && memcmp(b1, b2, b1.Length * sizeof(int)) == 0;
        }
        
        public static unsafe bool EqualBytesLongUnrolled<T>(T[] data1, T[] data2) where T : unmanaged
        {
            if (data1.Length != data2.Length)
                return false;

            fixed (T* bytes1 = data1, bytes2 = data2) {
                int len = data1.Length;
                int rem = len % (sizeof(long) * 16);
                long* b1 = (long*)bytes1;
                long* b2 = (long*)bytes2;
                long* e1 = (long*)(bytes1 + len - rem);

                while (b1 < e1) {
                    if (*(b1) != *(b2) || *(b1 + 1) != *(b2 + 1) || 
                        *(b1 + 2) != *(b2 + 2) || *(b1 + 3) != *(b2 + 3) ||
                        *(b1 + 4) != *(b2 + 4) || *(b1 + 5) != *(b2 + 5) || 
                        *(b1 + 6) != *(b2 + 6) || *(b1 + 7) != *(b2 + 7) ||
                        *(b1 + 8) != *(b2 + 8) || *(b1 + 9) != *(b2 + 9) || 
                        *(b1 + 10) != *(b2 + 10) || *(b1 + 11) != *(b2 + 11) ||
                        *(b1 + 12) != *(b2 + 12) || *(b1 + 13) != *(b2 + 13) || 
                        *(b1 + 14) != *(b2 + 14) || *(b1 + 15) != *(b2 + 15))
                        return false;
                    b1 += 16;
                    b2 += 16;
                }

                for (int i = 0; i < rem; i++)
                    if (MathExtension.NotEqual(data1 [len - 1 - i],data2 [len - 1 - i]))
                        return false;

                return true;
            }
        }
    }
}