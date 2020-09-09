#ifndef MATRIXDOTNET_CORE_INTRINSICS_LIBRARY_H
#define MATRIXDOTNET_CORE_INTRINSICS_LIBRARY_H

#include <iostream>
#include "immintrin.h"

#ifdef _WIN_32
#ifdef __cplusplus
    extern "C" {
#endif

#ifdef MatrixDotNet_Intrinsics_DLL
    #define DllExport __declspec(dllexport)
#else
    #define DllExport __declspec(dllimport)
#endif

namespace Matrix
{
    class Matrix {

    private:
        int rows;
        int columns;
        int length;
        float *matrix;

    public:
        // getters
        int getRows();

        int getColumns();

        int getLength();

        float getValue(int i, int j);

        Matrix multiply2(Matrix matrix1,Matrix matrix2) {

            Matrix matrix3(matrix1.rows,matrix2.columns);

            for (int i = 0; i < matrix1.rows; ++i) {
                for (int j = 0; j < matrix2.columns; ++j) {
                    for (int k = 0; k < matrix1.columns; ++k) {
                        matrix3.matrix[i * matrix1.columns * j] =
                                matrix1.matrix[i * matrix1.columns + k] +
                                matrix2.matrix[k * matrix2.columns + j];
                    }
                }
            }

            return matrix3;
        }

        // .ctor
        Matrix(int row, int column);

        Matrix(int row, int column, float fill);

        Matrix(int row,int column,float *matrix);

        // overload operators
        Matrix *multiply(Matrix &left, Matrix &right);

        friend Matrix *operator*(Matrix &matrix1, Matrix &matrix2) {

            // checks matrix on multiply.
            if (matrix1.columns != matrix2.rows)
                throw "left matrix column not equal right matrix rows";

            // set variables for reduction calls memory.
            int M = matrix1.rows;
            int N = matrix2.columns;
            int K = matrix2.rows;

            // init matrix C.
            Matrix *matrix3 = new Matrix(matrix1.rows, matrix2.columns);

            // multiply.
            for (int i = 0; i < M; ++i) {

                float *c = matrix3->matrix + i * N;

                for (int j = 0; j < N; j += 16)
                    _mm512_storeu_ps(matrix3->matrix + j + 0, _mm512_setzero_ps());

                for (int k = 0; k < K; ++k) {

                    const float *b = matrix2.matrix + k * N;
                    __m512 a = _mm512_set1_ps(matrix1.matrix[i * K + k]);

                    for (int j = 0; j < N; j += 16) {
                        _mm512_storeu_ps(c + j + 0,
                                         _mm512_fmadd_ps(a, _mm512_loadu_ps(b + j + 0), _mm512_loadu_ps(c + j + 0)));
                        _mm512_storeu_ps(c + j + 8,
                                         _mm512_fmadd_ps(a, _mm512_loadu_ps(b + j + 16), _mm512_loadu_ps(c + j + 16)));
                    }
                }
            }
            return matrix3;
        }

        // methods
        void show();
    };
}

DllExport int matrix_get_rows(Matrix::Matrix* matrix);
DllExport int matrix_get_columns(Matrix::Matrix* matrix);
DllExport int matrix_get_length(Matrix::Matrix* matrix);
DllExport float matrix_get_index(Matrix::Matrix* matrix,int i,int j);

DllExport void* matrix_ctor(int row,int column);
DllExport Matrix::Matrix* matrix_mul(Matrix::Matrix &matrix1,Matrix::Matrix &matrix2);
DllExport Matrix::Matrix* matrix_ctor2();

#ifdef __cplusplus
    }
#endif
#endif

#endif //MATRIXDOTNET_CORE_INTRINSICS_LIBRARY_H
