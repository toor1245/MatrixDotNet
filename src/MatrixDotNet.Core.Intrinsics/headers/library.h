#ifndef MATRIXDOTNET_CORE_INTRINSICS_LIBRARY_H
#define MATRIXDOTNET_CORE_INTRINSICS_LIBRARY_H

#include <iostream>

#ifdef __cplusplus
    extern "C" {
#endif

#ifdef MatrixDotNet_Intrinsics_DLL
    #define DllExport __declspec(dllexport)
#else
    #define DllExport __declspec(dllimport)
#endif

#include "immintrin.h"

class Matrix {

public:
    int rows;
    int columns;
    int length;
    float* matrix;

    // getters
    int getRows();
    int getColumns();
    int getLength();

    // .ctor
    Matrix(int row,int column);

    // overload operators
    Matrix* multiply(Matrix &left,Matrix &right);
    friend Matrix* operator *(Matrix &matrix1,Matrix &matrix2) {

        // checks matrix on multiply.
        if(matrix1.columns != matrix2.rows)
            throw "left matrix column not equal right matrix rows";

        // set variables for reduction calls memory.
        int M = matrix1.rows;
        int N = matrix2.columns;
        int K = matrix2.rows;

        // init matrix C.
        Matrix* matrix3 = new Matrix(matrix1.rows,matrix2.columns);

        // multiply.
        for (int i = 0; i < M; ++i) {

            float* c = matrix3->matrix + i * N;

            for (int j = 0; j < N; j += 16)
                _mm512_storeu_ps(matrix3->matrix + j + 0, _mm512_setzero_ps());

            for (int k = 0; k < K; ++k) {

                const float* b = matrix2.matrix + k * N;
                __m512 a = _mm512_set1_ps(matrix1.matrix[i*K + k]);

                for (int j = 0; j < N; j += 16)
                {
                    _mm512_storeu_ps(c + j + 0, _mm512_fmadd_ps(a,_mm512_loadu_ps(b + j + 0), _mm512_loadu_ps(c + j + 0)));
                    _mm512_storeu_ps(c + j + 8, _mm512_fmadd_ps(a,_mm512_loadu_ps(b + j + 16), _mm512_loadu_ps(c + j + 16)));
                }
            }
        }
        return matrix3;
    }
};

DllExport int matrix_get_rows(Matrix* matrix);
DllExport int matrix_get_columns(Matrix* matrix);
DllExport int matrix_get_length(Matrix* matrix);

DllExport void* matrix_ctor(int row,int column);
DllExport void* matrix_mul(Matrix &matrix1,Matrix &matrix2);

#ifdef __cplusplus
    }
#endif

#endif //MATRIXDOTNET_CORE_INTRINSICS_LIBRARY_H
