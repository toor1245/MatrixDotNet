# MatrixDotNet

### Sample
```C#
public sealed class Program
{
    static void Main(string[] args)
    {
        // initialize matrix.
        double[,] arr =
        {
            {5,56,7},
            {3,6,3},
            {5,9,15}
        };


        Matrix<double> matrix = new Matrix<double>(arr);

        double[] right = { 1,23,5};

        // KramerSolve works only with floating type.
        double[] res = matrix.KramerSolve(right);
        for(var i = 0; i < res.Length; i++)
        {
            Console.Write($"x{i}: {res[i]}\n");
        }
}
 
```
### Result
```
x0: 12,393939393939394
x1: -0,6637806637806638
x2: -3,3997113997114
```
