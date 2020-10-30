using System;
using MatrixDotNet;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Matrix<double> a = new double[,]
            {
                {12, -51, 4},
                {6, 167, -68},
                {-4, 24, -41}
            };

            Vector<Double> vector = a[0];
            vector.Sort();
            Console.WriteLine(vector);
        }
    }
}