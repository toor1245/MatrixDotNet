using System;
using System.Collections.Generic;

namespace MatrixDotNet.Extensions.Sorting
{
    public static class MatrixSortExtension
    {
        public static void Sort<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            Array.Sort(matrix._Matrix);
        }

        public static void Sort<T>(this Matrix<T> matrix, IComparer<T> comparer)
            where T : unmanaged
        {
            Array.Sort(matrix._Matrix, comparer);
        }

        public static void SortByRows<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            for (int i = 0; i < matrix.Rows; i++)
            {
                Array.Sort(matrix._Matrix, i*matrix.Columns, matrix.Columns);
            }
        }

        public static void SortByRows<T>(this Matrix<T> matrix, IComparer<T> comparer)
            where T : unmanaged
        {
            for (int i = 0; i < matrix.Rows; i++)
            {
                Array.Sort(matrix._Matrix, i*matrix.Columns, matrix.Columns, comparer);
            }
        }

        public static void SortByColumns<T>(this Matrix<T> matrix)
            where T : unmanaged
        {
            matrix.SortByColumns(Comparer<T>.Default);
        }

        public static void SortByColumns<T>(this Matrix<T> matrix, IComparer<T> comparer)
            where T : unmanaged
        {
            for (int column = 0; column < matrix.Columns; column++)
            {
                for (int i = 0; i < matrix.Rows - 1; i++)
                {
                    for (int j = i + 1; j > 0; j--)
                    {
                        if (comparer.Compare(matrix[j - 1, column], matrix[j, column]) > 0)
                        {
                            // swap
                            (matrix[j - 1, column], matrix[j, column]) =
                                (matrix[j, column], matrix[j - 1, column]);
                        }
                    }
                }
            }
        }
    }
}