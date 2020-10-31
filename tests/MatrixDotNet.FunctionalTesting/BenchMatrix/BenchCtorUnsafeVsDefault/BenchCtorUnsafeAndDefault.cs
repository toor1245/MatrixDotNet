using BenchmarkDotNet.Attributes;

namespace MatrixDotNet.FunctionalTesting.BenchMatrix.BenchCtorUnsafeVsDefault
{
    public class BenchCtorUnsafeAndDefault
    {
        private int n = 1000;


        [Benchmark]
        public void UnsafeCtorTest()
        {
            int[,] temp = {
                {2, 3, 4, 5},
                {2, 3, 4, 5},
                {2, 3, 4, 5},
                {2, 3, 4, 5},
            };
            for (int i = 0; i < n; i++)
            {
                Matrix<int> m = new Matrix<int>(0,0);
                // method not exists m.TestCtorUnsafe(temp);
            }
        }
        
        [Benchmark]
        public void DefaultCtorTest()
        {
            int[,] temp = {
                {2, 3, 4, 5},
                {2, 3, 4, 5},
                {2, 3, 4, 5},
                {2, 3, 4, 5},
            };
            for (int i = 0; i < n; i++)
            {
                Matrix<int> m = new Matrix<int>(0,0);
                // method not exists m.TestCtorDefault(temp);
            }
        }
    }
}