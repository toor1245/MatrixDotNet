using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Performance.Operations;
using MatrixDotNet.NetCore.Simd;

namespace MatrixDotNet.PerformanceTesting.Matrix.MathOperations
{
    [MemoryDiagnoser]
    public class MultTest : PerformanceTest
    {
        [Params(512)]
        public int Size;

        private Matrix<double> _matrix1;
        private Matrix<double> _matrix2;
        private Matrix<float> _matrix3;
        private Matrix<float> _matrix4;

        [GlobalSetup]
        public void Setup()
        {
            _matrix1 = BuildMatrix.RandomDouble(Size, Size, 0, 100);
            _matrix2 = BuildMatrix.RandomDouble(Size, Size, 0, 100);
            _matrix3 = BuildMatrix.RandomFloat(Size, Size, 0, 100);
            _matrix4 = BuildMatrix.RandomFloat(Size, Size, 0, 100);
        }
        
        [Benchmark]
        public Matrix<double> Default()
        {
            return _matrix1 * _matrix2;
        }

        [Benchmark]
        public Matrix<double> OptimizationDefault()
        {
            return Optimization.Multiply(_matrix1, _matrix2);
        }
        
        [Benchmark]
        public Matrix<double> Strassen()
        {
            return Optimization.MultiplyStrassen(_matrix1, _matrix2);
        }

        [Benchmark]
        public Matrix<float> BlockX16()
        {
            return Simd.BlockMultiply(_matrix3,_matrix4);
        }
    }
}