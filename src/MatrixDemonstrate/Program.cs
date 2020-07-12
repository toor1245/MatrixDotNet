using System;
using MatrixDotNet;

namespace MatrixDemonstrate
{
    public sealed class Program
    {
        static void Main(string[] args)
        {
            /*int[] arr =  {1,2,53,657};
            
            for (int i = 1; i < arr.Length; i++)
            {
                Swap(ref arr[i - 1], ref arr[i]);
            }

            foreach (var i in arr )
            {
                Console.WriteLine(i);
            }
            
            
            int[,] arrx =
            {
                {5,56,7},
                {3,6,3}
            };
            Matrix<int> matrix = new Matrix<int>(arrx);
            */
            
        }

        private static void Test()
        {
            long c = 0x80000000;
            long n = 10;
            bool test = Convert.ToBoolean(n >> 31);
            Console.WriteLine(test);
            n = c - n;
            Console.WriteLine(n);
        }
        
        private static void Swap(ref int x,ref int y)
        {

        }
    }
}