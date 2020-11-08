using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Performance.Operations
{
    public static partial class Optimization
    {
        /// <summary>
        /// Subtract two matrices.
        /// </summary>
        /// <param name="matrixA">the matrix A.</param>
        /// <param name="matrixB">the matrix B.</param>
        /// <returns>create new matrix which gets from subtract two matrices.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe Matrix<int> Sub(Matrix<int> matrixA,Matrix<int> matrixB)
        {
            if(matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");
            
            int m = matrixA.Rows;
            int n = matrixA.Columns;
            int length = matrixA.Length;

            Matrix<int> matrix = new Matrix<int>(m,n);

            fixed(int* pointer1 = matrixA.GetArray())
            fixed(int* pointer2 = matrixB.GetArray())
            fixed(int* pointer3 = matrix.GetArray())
            {
                Span<int> span1 = new Span<int>(pointer1,length);
                Span<int> span2 = new Span<int>(pointer2,length);
                Span<int> span3 = new Span<int>(pointer3,length);
                for (int i = 0; i < length; i++)
                {
                    span3[i] = span2[i] - span1[i];
                }
            }
            return matrix;
        }
        
        
        /// <summary>
        /// Subtract two matrices.
        /// </summary>
        /// <param name="matrixA">the matrix A.</param>
        /// <param name="matrixB">the matrix B.</param>
        /// <returns>create new matrix which gets from subtract two matrices.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe Matrix<long> Sub(Matrix<long> matrixA,Matrix<long> matrixB)
        {
            if(matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");
            
            int m = matrixA.Rows;
            int n = matrixA.Columns;
            int length = matrixA.Length;

            Matrix<long> matrix = new Matrix<long>(m,n);

            fixed(long* pointer1 = matrixA.GetArray())
            fixed(long* pointer2 = matrixB.GetArray())
            fixed(long* pointer3 = matrix.GetArray())
            {
                Span<long> span1 = new Span<long>(pointer1,length);
                Span<long> span2 = new Span<long>(pointer2,length);
                Span<long> span3 = new Span<long>(pointer3,length);
                for (int i = 0; i < length; i++)
                {
                    span3[i] = span2[i] - span1[i];
                }
            }
            return matrix;
        }
        
        
        /// <summary>
        /// Subtract two matrices.
        /// </summary>
        /// <param name="matrixA">the matrix A.</param>
        /// <param name="matrixB">the matrix B.</param>
        /// <returns>create new matrix which gets from subtract two matrices.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe Matrix<double> Sub(Matrix<double> matrixA,Matrix<double> matrixB)
        {
            if(matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");
            
            int m = matrixA.Rows;
            int n = matrixA.Columns;
            int length = matrixA.Length;

            Matrix<double> matrix = new Matrix<double>(m,n);

            fixed(double* pointer1 = matrixA.GetArray())
            fixed(double* pointer2 = matrixB.GetArray())
            fixed(double* pointer3 = matrix.GetArray())
            {
                Span<double> span1 = new Span<double>(pointer1,length);
                Span<double> span2 = new Span<double>(pointer2,length);
                Span<double> span3 = new Span<double>(pointer3,length);
                for (int i = 0; i < length; i++)
                {
                    span3[i] = span2[i] - span1[i];
                }
            }
            return matrix;
        }
        
        
        /// <summary>
        /// Subtract two matrices.
        /// </summary>
        /// <param name="matrixA">the matrix A.</param>
        /// <param name="matrixB">the matrix B.</param>
        /// <returns>create new matrix which gets from subtract two matrices.</returns>
        /// <exception cref="MatrixDotNetException"></exception>
        public static unsafe Matrix<float> Sub(Matrix<float> matrixA,Matrix<float> matrixB)
        {
            if(matrixA.Rows != matrixB.Rows && matrixA.Columns != matrixB.Columns)
                throw new MatrixDotNetException("MatrixA");
            
            int m = matrixA.Rows;
            int n = matrixA.Columns;
            int length = matrixA.Length;

            Matrix<float> matrix = new Matrix<float>(m,n);

            fixed(float* pointer1 = matrixA.GetArray())
            fixed(float* pointer2 = matrixB.GetArray())
            fixed(float* pointer3 = matrix.GetArray())
            {
                var span1 = new Span<float>(pointer1,length);
                var span2 = new Span<float>(pointer2,length);
                var span3 = new Span<float>(pointer3,length);
                for (int i = 0; i < length; i++)
                {
                    span3[i] = span2[i] - span1[i];
                }
            }
            return matrix;
        }
    }
}