# Vector overview

Vector is just serial data which have many math operations under primitive array.

Let's consider the following sample which demonstrates how to create vector.

[!code-csharp[VectorSimpleOperationsDocs.cs](../../../../samples/Samples/logs/CreateVectorSample/CreateVectorSampleDocs.cs)]

> [SimpleOperations.cs](https://github.com/toor1245/MatrixDotNet/blob/master/samples/Samples/Samples/VectorSamples/CreateVectorSample.cs)

##### Output

[!code-csharp[Run](../../../../samples/Samples/logs/CreateVectorSample/Run.txt)]

#### Routine operations with `Vector`

So let's consider `Vector` operations as well as in `Matrix`

[!code-csharp[VectorSimpleOperationsDocs.cs](../../../../samples/Samples/logs/VectorSimpleOperations/VectorSimpleOperationsDocs.cs)]

> [SimpleOperations.cs](https://github.com/toor1245/MatrixDotNet/blob/master/samples/Samples/Samples/VectorSamples/VectorSimpleOperations.cs)

##### Output
[!code-csharp[Run](../../../../samples/Samples/logs/CreateVectorSample/Run.txt)]

### Tensor product

In mathematics, the tensor product V xor W of two vector spaces V and W is a vector space, endowed with a bilinear map from the Cartesian product.

`TensorProduct` method computes the Tensor product, which located in `VectorExtension`

```c#
Vector<int> a = new int[] { 1, 2, 3 };
Vector<int> b = new int[] { 2, 3, 4 };
VectorExtension.TensorProduct(a, b);
```

> [!NOTE]
> [Tensor Product](https://en.wikipedia.org/wiki/Tensor_product#Evaluation_map_and_tensor_contraction)

### Distance coordinate of vector between of two points

for calculating coordinate of vector between of two points uses method `VectorExtension.GetDistancePoint`

```c#
Vector<int> a = new int[] { 1, 2, 3 };
Vector<int> b = new int[] { 2, 3, 4 };
VectorExtension.GetDistancePoint(a, b);
```

for calculating direct of vector intended method `GetDirectCos`:

```c#
Vector<int> a = new int[] { 1, 2, 3 };
VectorExtension.GetDirectCos(a);
```
and by coordinates

```c#
Vector<int> a = new int[] { 1, 2, 3 };
Vector<int> b = new int[] { 2, 3, 4 };
VectorExtension.GetDirectCos(a);
```

> [!NOTE]
> If you didn't find answer for your question on this page, [ask it on gitter](https://gitter.im/MatrixDotNet/community?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge).






