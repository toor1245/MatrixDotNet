using System;
using MatrixDotNet.Exceptions;

namespace MatrixDotNet.Extensions.Performance.Operations
{
    public static partial class Optimization
    {
        /// <summary>
        /// Add two matrices by elements.
        /// </summary>
        public static unsafe Matrix<int> Add(Matrix<int> matrixA,Matrix<int> matrixB)
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
                    span3[i] = span2[i] + span1[i];
                }
            }
            return matrix;
        }
        
        /// <summary>
        /// Add two matrices by elements.
        /// </summary>
        public static unsafe Matrix<float> Add(Matrix<float> matrixA,Matrix<float> matrixB)
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
                Span<float> span1 = new Span<float>(pointer1,length);
                Span<float> span2 = new Span<float>(pointer2,length);
                Span<float> span3 = new Span<float>(pointer3,length);
                for (int i = 0; i < length; i++)
                {
                    span3[i] = span2[i] + span1[i];
                }
            }
            return matrix;
        }
        
        /// <summary>
        /// Add two matrices by elements.
        /// </summary>
        public static unsafe Matrix<long> Add(Matrix<long> matrixA,Matrix<long> matrixB)
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
                    span3[i] = span2[i] + span1[i];
                }
            }
            return matrix;
        }
        
        /// <summary>
        /// Add two matrices by elements.
        /// </summary>
        public static unsafe Matrix<double> Add(Matrix<double> matrixA,Matrix<double> matrixB)
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
                    span3[i] = span2[i] + span1[i];
                }
            }
            return matrix;
        }
    }
}