using System;
using System.Runtime.InteropServices;

namespace MatrixDotNet.Extensions.Core.Optimization
{
    public class Matrix
    {
        [DllImport("Imports\\MatrixDotNet_Core_Intrinsics.dll",
            CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr matrix_ctor(int row,int column);
        
        [DllImport("Imports\\MatrixDotNet_Core_Intrinsics.dll",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int matrix_get_rows(IntPtr matrix);
        
        [DllImport("Imports\\MatrixDotNet_Core_Intrinsics.dll",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int matrix_get_columns(IntPtr matrix);
        
        [DllImport("Imports\\MatrixDotNet_Core_Intrinsics.dll",
            CallingConvention = CallingConvention.Cdecl)]
        static extern int matrix_get_length(IntPtr matrix);
        
        [DllImport("Imports\\MatrixDotNet_Core_Intrinsics.dll",
            CallingConvention = CallingConvention.Cdecl)]
        static extern Matrix matrix_mul(IntPtr matrixA,IntPtr matrixB);
        
        public IntPtr Handle { get; internal set; }
        
        public int Rows => matrix_get_rows(Handle);
        public int Columns => matrix_get_columns(Handle);
        public int Length => matrix_get_length(Handle);

        public Matrix(int row,int column)
        {
            Handle = matrix_ctor(row,column);
        }
        
        public static Matrix operator *(Matrix matrixA,Matrix matrixB)
        {
            return matrix_mul(matrixA.Handle, matrixB.Handle);
        }
    }
}