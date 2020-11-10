using System;
using System.Collections.Generic;
using MatrixDotNet;
using MatrixDotNet.Exceptions;
using Xunit;

namespace MatrixDotNetTests.MatrixTests
{
    public class MatrixTest
    {
        [Fact]
        public void CtorMatrix_AssignPrimitiveMatrixViaCtor_AssertMustBeTrue()
        {
            // Arrange
            int m = 5;
            int n = 10;
            int[,] a = new int[m,n];

            // Act
            var matrix = new Matrix<int>(a);

            // Assert
            Assert.Equal(m,matrix.Rows);
            Assert.Equal(n,matrix.Columns);
        }

        [Fact]
        public void CtorMatrix_ImplicitAssignMatrix_AssertMustBeTrue()
        {
            // Arrange
            const int m = 3;
            const int n = 3;
            int[,] a = new int[m, n] {
                {1, 2, 3},
                {3, 4, 5},
                {6, 7, 8},
            };

            // Act
            Matrix<int> matrix = a;

            // Assert
            Assert.Equal(a[1,1],matrix[1,1]);
            Assert.Equal(a,matrix);
        }
        
        [Fact]
        public void CtorMatrix_InitMatrixWithConstantValue_AssertMustBeTrue()
        {
            // Arrange
            const int m = 3;
            const int n = 3;
            
            // Act
            Matrix<int> matrix = new Matrix<int>(m,n,10);

            // Assert
            Assert.Contains(matrix, x => x == 10);
        }
        
        [Fact]
        public void CtorMatrix_InitMatrixWithConstantValue_AssertMustDoesNotContainZero()
        {
            // Arrange
            const int m = 3;
            const int n = 3;
            
            // Act
            Matrix<int> matrix = new Matrix<int>(m,n,5);

            // Assert
            Assert.DoesNotContain(matrix,x => x == 0);
        }

        [Fact]
        public void EnumeratorMoveNext_ChecksTraversalOfMatrix_AssertMustBeEqual()
        {
            Matrix<int> matrix = new[,]
            {
                {3, 5, 4},
                {3, 2, 1}
            };
            
            var array = matrix.GetArray();
            
            int index = 0;

            using IEnumerator<int> enumerator = matrix.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Assert.Equal(array[index], enumerator.Current);
                ++index;
            }
            Assert.Equal(array.Length, index);
        }
        
        [Fact]
        public void EnumeratorReset_ChecksTraversalOfMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrix = new[,]
            {
                {3, 5, 4},
                {3, 2, 1}
            };
            int[] result = new int[matrix.Length];
            int index = 0;
            using var e = matrix.GetEnumerator();


            // Act
            foreach (var i in matrix)
            {
                result[index] = i;
                index++;
            }
            e.Reset();
            e.MoveNext();
            // Assert
            Assert.Equal(3,e.Current);
        }
        
        [Fact]
        public void Equals_ChecksOnEqualsElements_AssertMustBeTrue()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {3, 5, 4},
                {3, 2, 1}
            };
            
            Matrix<int> matrixB = new[,]
            {
                {3, 5, 4},
                {3, 2, 1}
            };
            
            
            // Act
            bool isEqual = matrixA.Equals(matrixB);
            
            // Assert
            Assert.True(isEqual);
        }
        
        [Fact]
        public void Equals_ChecksOnEqualsElements_AssertMustBeFalse()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {3, 5, 4},
                {3, 2, 1}
            };
            
            Matrix<int> matrixB = new[,]
            {
                {3, 5, 4},
                {3, 2, 6}
            };
            
            
            // Act
            bool isEqual = matrixA.Equals(matrixB);
            
            // Assert
            Assert.False(isEqual);
        }

        [Fact]
        public void GetByIndex_GetArrayOfMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6}
            };
            int[] expected = {1, 5, 8};

            // Act
            int[] actual = matrixA[0];

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void GetByIndex_GetElementOfMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6}
            };
            int expected = 8;

            // Act
            int actual = matrixA[0,2];

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void GetByIndex_GetElementOfMatrix_AssertMustBeNotEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6}
            };
            int expected = 6;

            // Act
            int actual = matrixA[0,0];

            // Assert
            Assert.NotEqual(expected,actual);
        }
        
        [Fact]
        public void SetMatrix_SetElementOfMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6}
            };
            
            int expected = 6;

            // Act
            var actual = matrixA[0,0] = 6;

            // Assert
            Assert.Equal(expected,actual);
        }

        [Fact]
        public void GetElement_ExpectedIndexOutOfRange_AssertMustBeThrowsIndexOutOfRange()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6}
            };
            
            // Assert Act
            Assert.Throws<IndexOutOfRangeException>(() => matrixA[5, 5]);
        }
        
        [Fact]
        public void SetElement_ExpectedIndexOutOfRange_AssertMustBeThrowsIndexOutOfRange()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6}
            };
            
            // Assert Act
            Assert.Throws<IndexOutOfRangeException>(() => matrixA[5, 5] = 10);
        }
        
        [Fact]
        public void SetArray_ExpectedIndexOutOfRange_AssertMustBeThrowsIndexOutOfRange()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6}
            };
            
            // Assert Act
            Assert.Throws<IndexOutOfRangeException>(() => matrixA[5] = new []{1,2,3});
        }
        
        [Fact]
        public void SetArray_ReAssignArrayOfMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6}
            };
            int[] expected = {1, 2, 3};
            
            // Act
            matrixA[0] = new[] {1, 2, 3};
            
            // Assert
            Assert.Equal(expected,matrixA[0]);
        }
        
        [Fact]
        public void GetAndSetArray_GetAndSetArrayOfMatrixByColumn_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6}
            };
            int[] expected = {1,3};
            
            // Act
            matrixA[0,State.Column] = new[] {1, 3};
            
            // Assert
            Assert.Equal(expected,matrixA[0,State.Column]);
        }
        
        [Fact]
        public void GetArray_GetAndSetArrayOfMatrixByRow_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6}
            };
            int[] expected = {1,2,3};
            
            // Act
            matrixA[0,State.Row] = new[] {1,2,3};
            
            // Assert
            Assert.Equal(expected,matrixA[0,State.Row]);
        }
        
        [Fact]
        public void GetArray_GetAndSetArrayOfMatrixByRow_AssertMustBeThrowsIndexOutOfRange()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6}
            };
  
            // Assert Act
            Assert.Throws<IndexOutOfRangeException>(() => matrixA[10,State.Row] = new []{1,2,3});
        }
        
        [Fact]
        public void IsSquare_ChecksMatrixSquare_AssertMustBeTrue()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
                {1, 5, 8}
            };
  
            // Assert Act
            Assert.True(matrixA.IsSquare);
        }
        
        [Fact]
        public void IsSquare_ChecksMatrixSquare_AssertMustBeNotTrue()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
            };
  
            // Assert Act
            Assert.True(!matrixA.IsSquare);
        }

        [Fact]
        public void CloneTest_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> expected = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
            };
            
            // Act
            var actual = expected.Clone() as Matrix<int>;

            // Assert
            Assert.Equal(expected,actual);
        }
        

        [Fact]
        public void MatrixMultiplyTest_MultiplyTwoMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
            };

            Matrix<int> matrixB = new[,]
            {
                {1, 2, 4},
                {3, 4, 6},
                {4, 8, 9},
            };
            
            Matrix<int> expected = new [,]
            {
                {48,86,106},
                {42,74,96}
            };
            
            // Act
            Matrix<int> actual = matrixA * matrixB;

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MatrixMultiplyTest_MultiplyTwoMatrix_AssertMustBeThrowsMatrixDotNetException()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
                {4, 8, 9},
            };

            Matrix<int> matrixB = new[,]
            {
                {1, 2, 4},
                {3, 4, 6},
            };
            
            // Assert, Act
            Assert.Throws<MatrixDotNetException>(() => matrixA * matrixB);
        }
        
        [Fact]
        public void SumOpMatrixTest_SumTwoMatrix_AssertMustBeThrowsMatrixDotNetException()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
                {4, 8, 9},
            };

            Matrix<int> matrixB = new[,]
            {
                {1, 2, 4},
                {3, 4, 6},
            };
            
            // Assert, Act
            Assert.Throws<MatrixDotNetException>(() => matrixA + matrixB);
        }
        
        [Fact]
        public void SumOpMatrixTest_SumTwoMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
                {4, 8, 9},
            };

            Matrix<int> matrixB = new[,]
            {
                {1, 2, 4},
                {3, 4, 6},
                {4, 8, 9},
            };
            
            Matrix<int> expected = new [,]
            {
                { 2, 7, 12 },
                { 6, 9, 12 },
                { 8, 16,18 }
            };
            
            // Act
            var actual = matrixA + matrixB;
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void SubtractOpMatrixTest_SumTwoMatrix_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
                {4, 8, 9},
            };

            Matrix<int> matrixB = new[,]
            {
                {1, 2, 4},
                {3, 4, 6},
                {4, 8, 9},
            };
            
            Matrix<int> expected = new [,]
            {
                { 0, 3, 4 },
                { 0, 1, 0 },
                { 0, 0, 0 }
            };
            
            // Act
            var actual = matrixA - matrixB;
            
            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void SubtractOpMatrixTest_SumTwoMatrix_AssertMustBeThrowsMatrixDotNetException()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
            };

            Matrix<int> matrixB = new[,]
            {
                {1, 2, 4},
                {3, 4, 6},
                {4, 8, 9},
            };
            
            // Assert, Act
            Assert.Throws<MatrixDotNetException>(() => matrixA - matrixB);
        }
        
        [Fact]
        public void MultiplyOpMatrixOnConstTest_MatrixMultiplyToConstantRightSide_AssertMustBeTrue()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
            };
            
            Matrix<int> expected = new[,]
            {
                {2, 10, 16},
                {6, 10, 12},
            };
            
            // Act
            var actual = matrixA * 2; 
            

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MultiplyOpMatrixOnConstTest_MatrixMultiplyToConstantLeftSide_AssertMustBeTrue()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {3, 5, 6},
            };
            
            Matrix<int> expected = new[,]
            {
                {2, 10, 16},
                {6, 10, 12},
            };
            
            // Act
            var actual = 2 * matrixA; 
            

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void DivideOpMatrixOnConstTest_MatrixDivideToConstantLeftSide_AssertMustBeTrue()
        {
            // Arrange
            Matrix<double> matrixA = new double[,]
            {
                {1, 5, 8},
                {4, 5, 10},
            };
            
            const int k = 20;
            Matrix<double> expected = new[,]
            {
                {20, 4, 2.5},
                {5,  4, 2},
            };
            
            // Act
            var actual = k / matrixA; 
            

            // Assert
            Assert.Equal(expected,actual);
        }
        
        [Fact]
        public void MultiplyMatrixOnVector_AssertMustBeThrowsMatrixDotNetException()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 5, 8},
                {4, 5, 10},
            };

            int[] vector = { 1, 2, 4, 6 };
            
            // Assert, Assert
            Assert.Throws<MatrixDotNetException>(() => vector * matrixA);
            Assert.Throws<MatrixDotNetException>(() => matrixA * vector);
        }
        
        [Fact]
        public void DivideOpMatrixOnConstTest_MatrixDivideToConstantRightSide_AssertMustBeTrue()
        {
            // Arrange
            Matrix<double> matrixA = new[,]
            {
                {2, 4, 8.0},
                {6, 3, 1},
            };
            
            const int k = 2;
            Matrix<double> expected = new[,]
            {
                {1,  2,  4  },
                {3, 1.5, 0.5},
            };
            
            // Act
            var actual = matrixA / k; 
            

            // Assert
            Assert.Equal(expected,actual);
        }
    }
}