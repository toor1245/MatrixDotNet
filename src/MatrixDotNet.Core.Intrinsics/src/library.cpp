#include "../headers/library.h"
#include <iostream>

//#region DllExport implementations.

    //#region ctor.

    void* matrix_ctor(int row,int column){
        return new Matrix(row,column);
    }

    //#endregion

    //#region getters

    int matrix_get_rows(Matrix* matrix) {
        return matrix->getRows();
    }

    int matrix_get_columns(Matrix* matrix) {
        return matrix->getColumns();
    }

    int matrix_get_length(Matrix* matrix) {
        return matrix->getLength();
    }


    //#endregion

    void* matrix_mul(Matrix &matrix1,Matrix &matrix2){
        return matrix1 * matrix2;
    }

//#endregion

//#region Matrix implementations

    //#region .ctor
    Matrix::Matrix(int row,int column) {
        rows = row;
        columns = column;
        length = row * column;
        std::cout << rows << std::endl;
        std::cout << columns << std::endl;
        std::cout << length << std::endl;
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

    //#endregion

    Matrix* Matrix::multiply(Matrix &left, Matrix &right) {
        return left * right;
    }

//#endregion
