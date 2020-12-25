# Decomposition overview

In many applications, it is useful to decompose a matrix using other representations.
All decompositions available in namespace `MatrixDotNet.Extensions.Decomposition`.
So lets consider decompositions which supported by MatrixDotNet.

### LU decomposition

The LU decomposition finds a representation for the square matrix A as:

<h5> A = LU</h5>

*  where L is lower-triangular matrix `n x n`
*  where U is upper-triangular matrix `n x n`

for example lets take matrix size `3 x 3`

![example](https://wikimedia.org/api/rest_v1/media/math/render/svg/d536704df4f1374607bef1519ce452a28ea4a03a)

Without a proper ordering or permutations in the matrix, the factorization may fail to materialize. For example, it is easy to verify (by expanding the matrix multiplication) that a<sub>11</sub> = l<sub>11</sub> * u<sub>11</sub>
If a<sub>11</sub> = 0, then at least one of  l<sub>11</sub> and u<sub>11</sub>  has to be zero, which implies that either `L` or `U` is singular.  This is impossible if A is nonsingular (invertible). This is a procedural problem. It can be removed by simply reordering the rows of `A` so that the first element of the permuted matrix is nonzero.
The same problem in subsequent factorization steps can be removed the same way.

Lets see LU factorization in `MatrixDotNet`

```c#
public class LUSample
{
    public static void Run()
    {
        // initialize matrix with random values.
        Matrix<double> matrix = BuildMatrix.Random(5, 5, -10, 10);
        
        // display matrix.
        matrix.Pretty();
        
        // LU decomposition.
        matrix.GetLowerUpper(out var lower,out var upper);
        
        // display lower-triangular matrix.
        Console.WriteLine("lower-triangular matrix");
        lower.Pretty();
        
        // display upper-triangular matrix.
        Console.WriteLine("upper-triangular matrix");
        upper.Pretty();
        
        // A = LU
        Console.WriteLine("A = LU");
        Console.WriteLine(lower * upper);
    }
}
```
###### Output
```ini

Number of rows: 5
Number of columns: 5


  -8,00  |  9,00   |  -3,00  |  2,00   |  6,00   |
  7,00   |  -6,00  |  -5,00  |  -9,00  |  6,00   |
  5,00   |  4,00   |  6,00   |  -1,00  |  -9,00  |
  -4,00  |  3,00   |  -8,00  |  -9,00  |  5,00   |
  6,00   |  0,00   |  3,00   |  3,00   |  8,00   |

lower-triangular matrix
Number of rows: 5
Number of columns: 5


  1,00   |  0,00   |  0,00   |  0,00   |  0,00  |
  -0,88  |  1,00   |  0,00   |  0,00   |  0,00  |
  -0,62  |  5,13   |  1,00   |  0,00   |  0,00  |
  0,50   |  -0,80  |  -0,29  |  1,00   |  0,00  |
  -0,75  |  3,60   |  0,65   |  -1,26  |  1,00  |

upper-triangular matrix
Number of rows: 5
Number of columns: 5


  -8,00  |  9,00  |  -3,00  |  2,00   |  6,00    |
  0,00   |  1,88  |  -7,62  |  -7,25  |  11,25   |
  0,00   |  0,00  |  43,27  |  37,47  |  -63,00  |
  0,00   |  0,00  |  0,00   |  -4,89  |  -7,35   |
  0,00   |  0,00  |  0,00   |  0,00   |  3,77    |

A = LU

  -8,00  |  9,00   |  -3,00  |  2,00   |  6,00   |
  7,00   |  -6,00  |  -5,00  |  -9,00  |  6,00   |
  5,00   |  4,00   |  6,00   |  -1,00  |  -9,00  |
  -4,00  |  3,00   |  -8,00  |  -9,00  |  5,00   |
  6,00   |  0,00   |  3,00   |  3,00   |  8,00   |

```

### LUP decomposition
It turns out that a proper permutation in rows (or columns) is sufficient for LU factorization. LU factorization with partial pivoting (LUP) refers often to LU factorization with row permutations only:

<h5>PA = LU</h5>

where L and U are again lower and upper triangular matrices, and P is a permutation matrix, which, when left-multiplied to A, reorders the rows of A. It turns out that all square matrices can be factorized in this form, and the factorization is numerically stable in practice.This makes LUP decomposition a useful technique in practice.

```c#
public class LUPSample
{
    public static void Run()
    {
        // initialize matrix with random values.
        Matrix<double> matrix = BuildMatrix.Random(3, 3, -10, 10);
        
        // display matrix.
        matrix.Pretty();
        
        // LU decomposition.
        matrix.GetLowerUpperPermutation(out var lower,out var upper,out var perm);
        
        // Gets permutation matrix and C = L + U - E.
        matrix.GetLowerUpperPermutation(out var matrixC,out var matrixP);
        
        // display lower-triangular matrix.
        Console.WriteLine("lower-triangular matrix");
        lower.Pretty();
        
        // display upper-triangular matrix.
        Console.WriteLine("upper-triangular matrix");
        upper.Pretty();
        
        // display permutation matrix.
        Console.WriteLine("permutation matrix");
        perm.Pretty();
        
        // display matrix C
        Console.WriteLine("matrix C = L + U - E");
        matrixC.Pretty();
    }
}

```
###### Output
```ini 
Number of rows: 3
Number of columns: 3

  -8,00  |  -6,00  |  -1,00  |
  5,00   |  6,00   |  6,00   |
  -1,00  |  -2,00  |  -5,00  |

lower-triangular matrix
Number of rows: 3
Number of columns: 3

  1,00   |  0,00   |  0,00  |
  -0,62  |  1,00   |  0,00  |
  0,12   |  -1,80  |  1,00  |

upper-triangular matrix
Number of rows: 3
Number of columns: 3

  -8,00  |  -6,00  |  -1,00  |
  0,00   |  -1,25  |  -4,88  |
  0,00   |  0,00   |  -3,40  |

permutation matrix
Number of rows: 3
Number of columns: 3

  1,00  |  0,00  |  0,00  |
  0,00  |  0,00  |  1,00  |
  0,00  |  1,00  |  0,00  |

matrix C = L + U - E
Number of rows: 3
Number of columns: 3


  -8,00  |  -6,00  |  -1,00  |
  5,00   |  6,00   |  6,00   |
  -1,00  |  -0,33  |  -3,00  |
```

### Cholesky decomposition
Cholesky decomposition is a special case of LU decomposition applicable to Hermitian positive definite matrices. When
A = A<sup>H</sup> and x<sup>H</sup>Ax >= 0 for all x, then decompositions of A can be found:

A = U<sup>H</sup> U

A = L L<sup>H</sup>

where L is lower triangular and U is upper triangular.
`GetCholesky` method computes the Cholesky factorization.

```c#
Matrix<double> a = new Matrix<double>(8, 8);
a.GetCholesky(out var lower, out var transpose);
```
Or

```c#
Matrix<double> a = new Matrix<double>(8, 8);
Decomposition.GetCholesky(a, out var lower, out var transpose);
```

### QR decomposition
The QR decomposition (sometimes called a polar decomposition) works for any M x N array and finds M x M unitary(orthogonal) matrix
Q and M x N upper-trapezoidal matrix R

A = QR
Notice that if the QR of  is known, then the SVD decomposition can be found.

A = QR = SVD<sup>H</sup>

`QrDecomposition` method computes the QR factorization.

```c#
Matrix<double> a = new Matrix<double>(8, 8);
a.QrDecomposition(out var q, out var r);
```
Or

```c#
Matrix<double> a = new Matrix<double>(8, 8);
Decomposition.QrDecomposition(a, out var q, out var r);
```

### LQ decomposition
By analog QR you can find LQ factorization:

A = LQ

`LqDecomposition` method computes the LQ factorization.

```c#
Matrix<double> a = new Matrix<double>(8, 8);
a.LqDecomposition(out var l, out var q);
```
Or

```c#
Matrix<double> a = new Matrix<double>(8, 8);
Decomposition.LqDecomposition(a, out var l, out var q);
```

### Schur Decomposition
For a square N x N matrix A, the Schur decomposition finds matrices T and Z:

A = ZTZ<sup>H<sup>

where Z is a unitary(orthogonal) matrix and T is either upper triangular or quasi upper triangular,
depending on whether or not a real Schur form or complex Schur form is requested. For a real Schur form both T and Z
are real-valued when A is real-valued.

`SchurDecomposition` method computes the Schur factorization.

```c#
Matrix<double> a = new Matrix<double>(8, 8);
a.SchurDecomposition(out var ort, out var upper, ortTranspose);
```
Or

```c#
Matrix<double> a = new Matrix<double>(8, 8);
Decomposition.LqDecomposition(a, out var ort, out var upper, ortTranspose);
```

> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).
