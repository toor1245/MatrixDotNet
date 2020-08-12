using System;
using System.Runtime.InteropServices;
using MatrixDotNet;
using MatrixDotNet.Extensions;
using MatrixDotNet.Extensions.Core.Optimization;

namespace Samples
{
    class Program
    {
        static unsafe void Main(string[] args)
        {
            Matrix<int> matrix1 = new Matrix<int>(3,3);
            Matrix<int> matrix2 = new Matrix<int>(3,3);

            for (int i = 0; i < matrix1.Rows; i++)
            {
                for (int j = 0; j < matrix1.Columns; j++)
                {
                    matrix1[i, j] = i + j + 1;
                    matrix2[i, j] = i + j - 1;
                }
            }

            Matrix<int> matrix3 = Optimization.Multiply(matrix1,matrix2);
            
            matrix1.Pretty();
            matrix2.Pretty();
            matrix3.Pretty();
            
        }
    }
}