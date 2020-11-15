using System.Reflection;
using NUnit.Framework;
using Samples.Samples;

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

            var attrs = GetType().GetCustomAttribute<Output>();
            if (attrs != null)
            {
                SampleRunner.Run(GetType(), attrs.Project);
            }
        }
    }
}