using MatrixDotNet.FunctionalTesting.ThreadingTests;
using NUnit.Framework;
using System.Threading;

namespace MatrixDotNet.FunctionalTesting
{
    public static class FunctionalRunner
    {
        [Test]
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

            Assert.Pass();
        }
    }
}