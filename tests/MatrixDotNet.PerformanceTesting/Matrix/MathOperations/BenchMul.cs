using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using MatrixDotNet.Extensions.Builder;
using MatrixDotNet.Extensions.Performance;

namespace MatrixDotNet.PerformanceTesting.Matrix.MathOperations
{
    public class BenchMul : PerformanceTest
    {
        [Params(1024)]
        public int Size;
        private Matrix<float> _matrix1;
        private Matrix<float> _matrix2;
        private MathNet.Numerics.LinearAlgebra.Matrix<float> _matrix3;
        private MathNet.Numerics.LinearAlgebra.Matrix<float> _matrix4;
        
        [GlobalSetup]
        public void Setup()
        {
            _matrix1 = BuildMatrix.RandomFloat(Size, Size, 0, 100);
            _matrix2 = BuildMatrix.RandomFloat(Size, Size, 0, 100);
            _matrix3 = MathNet.Numerics.LinearAlgebra.Matrix<float>.Build.Dense(Size, Size);
            _matrix4 = MathNet.Numerics.LinearAlgebra.Matrix<float>.Build.Dense(Size, Size);
        }

        [Benchmark]
        public async ValueTask<Matrix<float>> StrassenParallel()
        {
            return await Optimization.MultiplyStrassenAsync(_matrix1, _matrix2);
        }

        [Benchmark]
        public Matrix<float> Strassen()
        {
            return Optimization.MultiplyStrassen(_matrix1, _matrix2);
        }

        [Benchmark]
        public Matrix<float> Default()
        {
            return _matrix1 * _matrix2;
        }
        
        [Benchmark]
        public MathNet.Numerics.LinearAlgebra.Matrix<float> MathNumerics()
        {
            return _matrix3 * _matrix4;
        }
    }
}