using System.Linq;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using NUnit.Framework;
#if DEBUG
using BenchmarkDotNet.Exporters.Csv;
#endif

namespace MatrixDotNet.PerformanceTesting
{
    public class PerformanceTest
    {
        [Test]
        public void StartTest()
        {
            if (GetType() == typeof(PerformanceTest))
                Assert.Pass();


#if DEBUG
            Assert.Warn("Debug mode is on\n");
            var summary = BenchmarkRunner.Run(GetType(),
                new DebugInProcessConfig()
                    .AddExporter(new CsvExporter(CsvSeparator.Comma))
                    .WithOptions(ConfigOptions.StopOnFirstError | ConfigOptions.DisableLogFile)
            );
#else
            var summary = BenchmarkRunner.Run(this.GetType(),
                ManualConfig
                    .Create(DefaultConfig.Instance)
                    .WithOptions(ConfigOptions.StopOnFirstError | ConfigOptions.DisableLogFile)
            );
#endif

            Assert.IsFalse(summary.HasCriticalValidationErrors, "Critical validation error");
            Assert.IsTrue(summary.ValidationErrors.IsEmpty, "Not critical validation errors");
            Assert.IsFalse(summary.Reports.IsEmpty, "Here is no test results... Are you alright?");
            Assert.IsTrue(summary.Reports.All(r => r.Success), "One of benchmarks has failed");
        }
    }
}