#include "../headers/library.h"
#include <iostream>

namespace Matrix {

//#region DllExport implementations.

    //#region ctor.

    void *matrix_ctor(int row, int column) {
        return new Matrix(row, column);
    }

    //#endregion

    //#region getters

    int matrix_get_rows(Matrix *matrix) {
        return matrix->getRows();
    }

    int matrix_get_columns(Matrix *matrix) {
        return matrix->getColumns();
    }

    int matrix_get_length(Matrix *matrix) {
        return matrix->getLength();
    }


    //#endregion

    Matrix *matrix_mul(Matrix &matrix1, Matrix &matrix2) {
        return matrix1 * matrix2;
    }

//#endregion

//#region Matrix implementations

    //#region .ctor
    Matrix::Matrix(int row, int column) {
        rows = row;
        columns = column;
        length = row * column;

        matrix = new float[length];
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < columns; ++j) {
                matrix[i * columns + j] = 0;
            }
        }
    }

    Matrix::Matrix(int row, int column, float fill) {
        rows = row;
        columns = column;
        length = row * column;

        matrix = new float[length];

        for (int i = 0; i < row; ++i) {
            for (int j = 0; j < column; ++j) {
                matrix[i * column + j] = fill;
            }
        }

    }

    Matrix::Matrix(int row,int column,float *matrix) {
        rows = row;
        columns = column;
        length = row * column;

        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < columns; ++j) {
                this->matrix[i * column + j] = matrix[i * column + j];
            }
        }
    }
    //#endregion

    //#region getters.

    int Matrix::getRows() {
        return this->rows;
    }

    int Matrix::getColumns() {
        return this->columns;
    }

    int Matrix::getLength() {
        return this->length;
    }

    float Matrix::getValue(int i, int j) {
        this->matrix[i * columns + j];
    }

    //#endregion

    Matrix *Matrix::multiply(Matrix &left, Matrix &right) {
        return left * right;
    }

    void Matrix::show() {
        std::string str;
        for (int i = 0; i < rows; ++i) {
            for (int j = 0; j < columns; ++j) {
                str += matrix[i * columns + j];
                std::cout << " ";
            }
            std::cout << "\n";
        }
    }
}


//#endregion
