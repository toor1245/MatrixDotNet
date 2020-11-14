# Access to matrix elements overview.
 
There are many approaches in Matrix DotNet receive element or vector of matrix.
 
#### Lets see the following sample.
 
```c#
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Sample
{
    public class AccessElementsSample
    {
        public static void Run()
        {
            // Initialize matrix.
            Matrix<int> matrix = new int[3,3]
            {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };

            int[] arr = {10, 9, 11};
            
            // Approaches for obtaining elements.
            int first = matrix[1,2]; // simple way for receive element
            
            int second = matrix[1, 1, State.Row]; // gets element by State.Row
            
            int third = matrix[0, 2, State.Column]; // gets element by State.Column

            int[] arr1 = matrix[0, State.Row]; // gets array of matrix by row index.

            int[] arr2 = matrix[0, State.Column]; // gets array of matrix by column index
            
            int[] arr3 = matrix.GetColumn(1);

            int[] arr4 = matrix.GetRow(2);
            
            // also we can reassign element or vector of matrix.
            matrix[1, 0, State.Column] = 5;
            matrix[0, State.Column] = arr; // deep copy!!!
            matrix[1, State.Row] = arr; // deep copy!!!
        }
    }
}
```

If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).
 