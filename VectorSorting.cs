using MatrixDotNet.Math;

namespace MatrixDotNet
{
    public static partial class VectorExtension 
    {
        #region Sorting
        
        public static void BubbleSort<T>(this Vector<T> vector) where T : unmanaged
        {
            for (int i = 0; i < vector.Length; i++)
            {
                for (int j = i + 1; j < vector.Length; j++)
                {
                    if (MathExtension.GreaterThan(vector[i],vector[j]))
                    {
                        T temp = vector[i];
                        vector[i] = vector[j];
                        vector[j] = temp;
                    }
                }
            }
        }
        
        private static Vector<T> QuickSort<T>(Vector<T> array, int minIndex, int maxIndex) where T : unmanaged
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }
        
        public static Vector<T> QuickSort<T>(this Vector<T> vector) where T : unmanaged
        {
            return QuickSort(vector, 0, vector.Length - 1);
        }
        
        #endregion

        private static int Partition<T>(Vector<T> array,int minIndex,int maxIndex) where T : unmanaged
        {
            var pivot = minIndex - 1;
            var x1 = array[pivot];
            var left = array[maxIndex];
            for (int i = minIndex; i < maxIndex; i++)
            {
                var right = array[i];
                if (MathExtension.GreaterThan(left,right))
                {
                    pivot++;
                    var x = x1;
                    Swap(ref x, ref right);
                }
            }
            
            pivot++;
            Swap(ref x1, ref left);
            return pivot;
        }
        
        private static void Swap<T>(ref T x, ref T y)
        {
            var t = x;
            x = y;
            y = t;
        }
        
    }
}