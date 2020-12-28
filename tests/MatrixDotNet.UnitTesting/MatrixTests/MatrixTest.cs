using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MatrixDotNet;
using MatrixDotNet.Exceptions;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Conversion;
using MatrixDotNet.Extensions.Performance;
using MatrixDotNet.NotStableFeatures;
using Xunit;

#if NETCOREAPP3_1 || NET5_0
using MatrixDotNet.Extensions.Performance.Simd;
#endif

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
        public void Equals_ChecksOnEqualsForAvxElements_AssertMustBeTrue()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {3, 5, 4},
                {3, 2, 1},
                {3, 5, 4},
                {3, 2, 1},
                {3, 5, 4},
                {3, 2, 1}
            };
            
            Matrix<int> matrixB = new[,]
            {
                {3, 5, 4},
                {3, 2, 1},
                {3, 5, 4},
                {3, 2, 1},
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
        public void Equalsnrolled_ChecksOnEqualsForElements_AssertMustBeTrue()
        {
            // Arrange
            Matrix<byte> matrixA = new byte[,]
            {
                {3, 5, 4},
                {3, 2, 1},
                {3, 5, 4},
                {3, 2, 1},
                {3, 5, 4},
                {3, 2, 1},
                {3, 2, 1}
            };
            
            Matrix<byte> matrixB = new Byte[,]
            {
                {3, 5, 4},
                {3, 2, 1},
                {3, 5, 4},
                {3, 2, 1},
                {3, 5, 4},
                {3, 2, 1},
                {3, 2, 1}
            };
            
            
            // Act
            bool isEqual = UnsafeEqualsUnrolled.EqualBytesLongUnrolled(matrixA.GetArray(),matrixB.GetArray());
            
            // Assert
            Assert.True(isEqual);
        }
        
        [Fact]
        public void EqualsUnrolled_ChecksOnEqualsElements_AssertMustBeFalse()
        {
            // Arrange
            Matrix<byte> matrixA = new byte[,]
            {
                {3, 5, 4},
                {3, 2, 1},
                {3, 5, 4},
                {3, 2, 1},
                {3, 3, 4},
                {3, 1, 1},
                {3, 2, 4}
            };
            
            Matrix<byte> matrixB = new Byte[,]
            {
                {3, 5, 4},
                {3, 2, 1},
                {3, 5, 4},
                {3, 2, 1},
                {3, 5, 4},
                {3, 2, 1},
                {3, 2, 1}
            };
            
            
            // Act
            bool isEqual = UnsafeEqualsUnrolled.EqualBytesLongUnrolled(matrixA.GetArray(),matrixB.GetArray());
            
            // Assert
            Assert.False(isEqual);
        }
        
        [Fact]
        public void Equals_ChecksOnEqualsElementsForAvx_AssertMustBeFalse()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {3, 5, 4},
                {3, 2, 1},
                {3, 2, 1},
                {3, 2, 1}
            };
            
            Matrix<int> matrixB = new[,]
            {
                {3, 5, 4},
                {3, 2, 6},
                {3, 2, 6},
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

#if NETCOREAPP3_1 || NET5_0
        [Theory]
        [InlineData(16)]
        [InlineData(32)]
        [InlineData(48)]
        [InlineData(64)]
        [InlineData(128)]
        public void MatrixMulTest16_MultiplyTwoMatrix_AssertMustBeEqual(int n)
        {
            // Arrange
            Matrix<float> matrixA = BuildMatrix.RandomFloat(n, n, -10, 10);
            Matrix<float> matrixB = BuildMatrix.RandomFloat(n, n, -10, 10);
            Matrix<float> expected = matrixA * matrixB;

            // Act
            Matrix<float> actual = Simd.BlockMultiply(matrixA, matrixB);

            // Assert
            Assert.Equal(expected, actual);
        }
#endif
        
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
        
        [Fact]
        public void IsSymmetricTest_AssertMustBeTrue()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 3, 0},
                {3, 2, 6},
                {0, 6, 5},
            };
            
            // Act
            var actual = matrixA.IsSymmetric;
            

            // Assert
            Assert.True(actual);
        }
        
        [Fact]
        public void IsSymmetricTest_AssertMustBeFalse()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 3, 0},
                {3, 2, 6},
                {0, 4, 3},
            };
            
            // Act
            var actual = matrixA.IsSymmetric;
            

            // Assert
            Assert.False(actual);
        }
        
        [Fact]
        public void IsSymmetricTest_IsNotSquareMatrix_ThrowsMatrixDotNetException()
        {
            // Arrange
            Matrix<int> matrixA = new[,]
            {
                {1, 3, 0},
                {3, 2, 6},
                {0, 4, 3},
                {0, 1, 2},
            };
            
            // Act Assert
            Assert.Throws<MatrixDotNetException>(() => matrixA.IsSymmetric);
        }

        [Fact]
        public void StrassenTest_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = BuildMatrix.RandomInt(1024,1024,1, 2);
            Matrix<int> matrixB = BuildMatrix.RandomInt(1024,1024,1, 2);
            Matrix<int> expected = matrixA * matrixB;

            // Act
            var actual = Optimization.MultiplyStrassen(matrixA, matrixB);
            
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact] 
        public async Task StrassenParallelTest_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = BuildMatrix.RandomInt(1024,1024,1, 2);
            Matrix<int> matrixB = BuildMatrix.RandomInt(1024,1024,1, 2);
            Matrix<int> expected = matrixA * matrixB;

            // Act
            var actual = await Optimization.MultiplyStrassenAsync(matrixA, matrixB);
            
            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Fact] 
        public void ReverseTest_AssertMustBeEqual()
        {
            // Arrange
            Matrix<int> matrixA = new [,]
            {
                { 1,  2,  3,  4,  5,  6  },
                { 7,  8,  9,  10, 11, 12 },
                { 13, 14, 15, 16, 17, 18 },
                { 19, 20, 21, 22, 23, 24 },
                { 25, 26, 27, 28, 29, 30 },
                { 31, 32, 33, 34, 35, 36 }
            };
            Matrix<int> expected = new [,]
            {
                { 36, 35, 34, 33, 32, 31 },
                { 30, 29, 28, 27, 26, 25 },
                { 24, 23, 22, 21, 20, 19 },
                { 18, 17, 16, 15, 14, 13 },
                { 12, 11, 10, 9,  8,  7  },
                { 6,  5,  4,  3,  2,  1  }
            };

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 34)]
        [InlineData(16, 34)]
        [InlineData(7, 103)]
        [InlineData(9, 8)]
        public void ReverseInt32Test_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<int> matrixA = new Matrix<int>(m, n);
            Matrix<int> expected = new Matrix<int>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr2.Length - i - 1;
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        public void ReverseSingleTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<float> matrixA = new Matrix<float>(m, n);
            Matrix<float> expected = new Matrix<float>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr2.Length - i - 1;
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
        }
        
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        public void ReverseDoubleTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<double> matrixA = new Matrix<double>(m, n);
            Matrix<double> expected = new Matrix<double>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr2.Length - i - 1;
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        public void ReverseByteTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<byte> matrixA = new Matrix<byte>(m, n);
            Matrix<byte> expected = new Matrix<byte>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (byte i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (byte i = 0; i < arr2.Length; i++)
            {
                arr2[i] = (byte) (arr2.Length - i - 1);
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        public void ReverseByteTest_CheckMask_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<byte> actual = BuildMatrix.RandomByte(m, n);
            Matrix<byte> matrixB = (Matrix<byte>) actual.Clone();
            var expected = matrixB.GetArray().Reverse();
            
            // Act
            MatrixConverter.Reverse(actual.GetArray());

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        [InlineData(7, 103)]
        [InlineData(2, 103)]
        public void ReverseInt64Test_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<long> matrixA = new Matrix<long>(m, n);
            Matrix<long> expected = new Matrix<long>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = arr2.Length - i - 1;
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        public void ReverseSByteTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<sbyte> matrixA = new Matrix<sbyte>(m, n);
            Matrix<sbyte> expected = new Matrix<sbyte>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (byte i = 0; i < arr1.Length; i++)
            {
                arr1[i] = (sbyte) i;
            }
            
            for (byte i = 0; i < arr2.Length; i++)
            {
                arr2[i] = (sbyte) (arr2.Length - i - 1);
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        public void ReverseSByteTest_CheckMask_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<sbyte> actual = BuildMatrix.RandomSByte(m, n);
            Matrix<sbyte> matrixB = (Matrix<sbyte>) actual.Clone();
            var expected = matrixB.GetArray().Reverse();
            
            // Act
            MatrixConverter.Reverse(actual.GetArray());

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 34)]
        [InlineData(16, 34)]
        [InlineData(7, 103)]
        [InlineData(9, 8)]
        public void ReverseUInt32Test_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<uint> matrixA = new Matrix<uint>(m, n);
            Matrix<uint> expected = new Matrix<uint>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (uint i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (uint i = 0; i < arr2.Length; i++)
            {
                arr2[i] = (uint) (arr2.Length - i - 1);
            }
            
            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        [InlineData(7, 103)]
        [InlineData(2, 103)]
        public void ReverseUInt64Test_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<ulong> matrixA = new Matrix<ulong>(m, n);
            Matrix<ulong> expected = new Matrix<ulong>(m, n);
            var arr1 = matrixA.GetArray();
            var arr2 = expected.GetArray();
            
            for (uint i = 0; i < arr1.Length; i++)
            {
                arr1[i] = i;
            }
            
            for (uint i = 0; i < arr2.Length; i++)
            {
                arr2[i] = (ulong) (arr2.Length - i - 1);
            }

            // Act
            MatrixConverter.Reverse(matrixA.GetArray());

            // Assert
            Assert.Equal(expected, matrixA);
        }

#if NET5_0 || NETCOREAPP3_1
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        [InlineData(7, 103)]
        [InlineData(2, 103)]
        public void NegateMatrixTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<int> matrixA = new Matrix<int>(m, n, 1);
            Matrix<int> expected = new Matrix<int>(m, n, -1);

            // Act
            var actual = Matrix<int>.Negate(matrixA);

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        [InlineData(7, 14)]
        [InlineData(2, 32)]
        public void NegateMatrixSByteTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<sbyte> matrixA = new Matrix<sbyte>(m, n, 1);
            Matrix<sbyte> expected = new Matrix<sbyte>(m, n, -1);

            // Act
            var actual = Matrix<sbyte>.Negate(matrixA);

            // Assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 1)]
        [InlineData(5, 5)]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 5)]
        [InlineData(5, 4)]
        [InlineData(8, 7)]
        [InlineData(4, 9)]
        [InlineData(15, 10)]
        [InlineData(16, 4)]
        [InlineData(7, 11)]
        [InlineData(9, 8)]
        [InlineData(7, 14)]
        [InlineData(2, 32)]
        public void NegateMatrixShortTest_AssertMustBeEqual(int m, int n)
        {
            // Arrange
            Matrix<short> matrixA = new Matrix<short>(m, n, 1);
            Matrix<short> expected = new Matrix<short>(m, n, -1);

            // Act
            var actual = Matrix<short>.Negate(matrixA);

            // Assert
            Assert.Equal(expected, actual);
        }
#endif
    }
}