namespace MatrixDotNet.Exceptions
{
    internal static class ExceptionArgument
    {
        internal const string MatricesLengthAreNotEqual = "Size of rows or columns of matrix A is not equal matrix B.";
        internal const string MatricesMultiplySize = "Matrix A columns length must be equal matrix B rows length.";
        internal const string VectorLength = "Length of vector A is not equal vector B.";
        internal const string ColumnOfMatrixIsNotEqualSizeOfVector = "Column size of matrix is not equal size of vector.";
        internal const string RowsOfMatricesAreNotEqual = "Rows of matrices are not equal.";
        internal const string RowSizeOfMatrixIsNotEqualSizeOfVector = "Row size of matrix is not equal size of vector.";
        internal const string NotSupportedTypeFloatType = "Unsupported type, floating type expected.";
    }
}