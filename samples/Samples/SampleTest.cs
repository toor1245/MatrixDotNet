using NUnit.Framework;

namespace Samples
{
    public class SampleTest
    {
        [Test]
        public void StartTest()
        {
            if (GetType() == typeof(SampleTest))
            {
                Assert.Pass();
            }
            
            SampleRunner.Run(GetType(),DefineProject.MatrixSamples);
        }
    }
}