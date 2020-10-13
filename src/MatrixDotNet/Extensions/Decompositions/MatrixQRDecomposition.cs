using System;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Math;

namespace MatrixDotNet.Extensions.Decompositions
{
    public static partial class Decomposition  
    {
        public static void QrDecomposition<T>(this Matrix<T> matrix,out Matrix<T> q,out Matrix<T> r) where T : unmanaged
        {
            q = ProcessGrammShmidtByColumns(matrix).GetNormByColumns();
            r = q.Transpose() * matrix;
        }
        
        public static Matrix<T> ProcessGrammShmidtByRows<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!MathExtension.IsFloatingPoint<T>())
            {
                throw new MatrixDotNetException("matrix must be floating point type such as " +
                                                "Matrix<double>, Matrix<decimal>, Matrix<float>");
            }
            
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("matrix is not square");
            }
            
            int m = matrix.Rows;
            
            Matrix<T> b = new Matrix<T>(m, matrix.Columns)
            {
                [0] = matrix[0]
            };
            
            for (int i = 1; i < m; i++)
            {
                Vector<T> ai = matrix[i];
                Vector<T> sum = new T[m];
                for (int j = 0; j < i; j++)
                {
                    Vector<T> bi = b[j];
                    T scalarProduct = ai * bi;
                    T biMul = bi * bi;
                    T ci = MathExtension.Divide(scalarProduct,biMul);
                    sum += ci * bi;
                }
                var res = ai - sum;
                b[i] = res.Array;
            }

            return b;
        }
        
        
        public static Matrix<T> ProcessGrammShmidtByColumns<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!MathExtension.IsFloatingPoint<T>())
            {
                throw new MatrixDotNetException("matrix must be floating point type such as " +
                                                "Matrix<double>, Matrix<decimal>, Matrix<float>");
            }
            
            if (!matrix.IsSquare)
            {
                throw new MatrixDotNetException("matrix is not square");
            }
            
            int m = matrix.Rows;
            int n = matrix.Columns;
            
            Matrix<T> b = new Matrix<T>(m, n)
            {
                [0, State.Column] = matrix[0, State.Column]
            };
            
            for (int i = 1; i < n; i++)
            {
                Vector<T> ai = matrix[i, State.Column];
                Vector<T> sum = new T[n];
                for (int j = 0; j < i; j++)
                {
                    Vector<T> bi = b[j, State.Column];
                    T scalarProduct = ai * bi;
                    T biMul = bi * bi;
                    T ci = MathExtension.Divide(scalarProduct,biMul);
                    sum += ci * bi;
                }
                var res = ai - sum;
                b[i, State.Column] = res.Array;
            }
            
            return b;
        }

        public static Matrix<T> GetNormByColumns<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!MathExtension.IsFloatingPoint<T>())
            {
                throw new MatrixDotNetException("matrix must be floating point type such as " +
                                                "Matrix<double>, Matrix<decimal>, Matrix<float>");
            }
            
            int m = matrix.Rows;
            int n = matrix.Columns;
            Matrix<T> orthogonal = new Matrix<T>(m,n); 
            for (int i = 0; i < n; i++)
            {
                Vector<T> vector = new Vector<T>(matrix[i,State.Column]);
                T val = vector.GetLengthVec();
                for (int j = 0; j < m; j++)
                {
                    orthogonal[j, i] = MathExtension.Divide(matrix[j, i], val);
                }
            }

            return orthogonal;
        }
        
        public static Matrix<T> GetNormByRows<T>(this Matrix<T> matrix) where T : unmanaged
        {
            if (!MathExtension.IsFloatingPoint<T>())
            {
                throw new MatrixDotNetException("matrix must be floating point type such as " +
                                                "Matrix<double>, Matrix<decimal>, Matrix<float>");
            }
            
            int m = matrix.Rows;
            int n = matrix.Columns;
            Matrix<T> orthogonal = new Matrix<T>(m,n); 
            for (int i = 0; i < m; i++)
            {
                Vector<T> vector = new Vector<T>(matrix[i]);
                T val = vector.GetLengthVec();
                for (int j = 0; j < n; j++)
                {
                    orthogonal[i,j] = MathExtension.Divide(matrix[i,j], val);
                }
            }

            return orthogonal;
        }
        
    }
}