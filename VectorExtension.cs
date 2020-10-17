using MatrixDotNet.Math;

namespace MatrixDotNet
{
    public static partial class VectorExtension
    {
        public static T Min<T>(this Vector<T> vector) where T : unmanaged 
        {
            T min = vector[0];
            for (int i = 0; i < vector.Length; i++)
            {
                if (MathExtension.GreaterThan(min,vector[i]))
                {
                    min = vector[i];
                }
            }

            return min;
        }
        
        public static T Max<T>(this Vector<T> vector) where T : unmanaged
        {
            T max = vector[0];
            for (int i = 0; i < vector.Length; i++)
            {
                if (MathExtension.GreaterThan(vector[i],max))
                {
                    max = vector[i];
                }
            }

            return max;
        }
    }
}