# Getting started

In this Basic section of Guides we will cover all general operations with matrix.

### Install

Create new console application and install the [MatrixDotNet](https://www.nuget.org/packages/MatrixDotNet/) NuGet package. We support:

* Projects: classic and modern with PackageReferences
* Runtimes: .NETStandard 2.1 .NET Core 3.1+ 
* OS: Windows, Linux, MacOS
* Languages: C#

### Matrix

There are many ways creates of matrix so let's consider the following sample how to create Matrix.   

[!code-csharp[CreateMatrixSample.cs](../../../../samples/Samples/logs/MatrixCreateSample/MatrixCreateSampleDocs.cs)]

As you can see class `Matrix` can assign any type implicit such as one, two dimensional and jugged array.
Matrix stores your data in one dimensional array, you can get with happen method `GetArray()`.

> [MatrixCreateSample.cs](https://github.com/toor1245/MatrixDotNet/blob/master/samples/Samples/Samples/MatrixSamples/MatrixCreateSample.cs)

##### Output 

[!code-csharp[Run](../../../../samples/Samples/logs/MatrixCreateSample/Run.txt)]

#### Simple operations of matrix.

So, as we know how to create matrix let's to try make routine operations with matrix.  

[!code-csharp[CreateMatrixSampleDocs.cs](../../../../samples/Samples/logs/SimpleOperations/SimpleOperationsDocs.cs)]

> [CreateMatrixSample.cs](https://github.com/toor1245/MatrixDotNet/blob/master/samples/Samples/Samples/MatrixSamples/SimpleOperations.cs)

As you can see thanks overload operators, it's possible write less code and becomes more readable.   

##### Output

[!code-csharp[Run](../../../../samples/Samples/logs/SimpleOperations/Run.txt)]
