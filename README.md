# MatrixDotNet
![MatrixDotNet](https://github.com/toor1245/MatrixDotNet/blob/master/docs/MatrixDotNet.png)

MatrixDotNet is a lightweight .NET library for calculate matrix. You can install MatrixDotNet via [NuGet package](https://www.nuget.org/packages/MatrixDotNet/).

### Features
* Matrix DotNet is made in priority on speed and accuracy of calculations

##### Example 
[!code-csharp[StrassenSample.cs](../../../samples/Samples.Samples/StrassenSample.cs)]


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


        Matrix<float> matrix = new Matrix<float>(arr);

        double[] right = { 1,23,5};

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

#### See more information [docs](https://github.com/toor1245/MatrixDotNet/tree/master/docs/articles) 
