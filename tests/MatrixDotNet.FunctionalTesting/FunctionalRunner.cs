using System.Threading;
using MatrixDotNet.FunctionalTesting.ThreadingTests;

namespace MatrixDotNet.FunctionalTesting
{
    public static class FunctionalRunner
    {
        public static void RunConcurrentTest()
        {
            DataTest data = new DataTest(4);
            var t1 = new Thread(data.RunF1);
            var t2 = new Thread(data.RunF2);
            var t3 = new Thread(data.RunF3);
            
            t1.Start();
            t2.Start();
            t3.Start();
            
            t1.Join();
            t2.Join();
            t3.Join();
        }
    }
}