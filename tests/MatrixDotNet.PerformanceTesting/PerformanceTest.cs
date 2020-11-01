using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Running;
using NUnit.Framework;
using System.Linq;

namespace MatrixDotNet.PerformanceTesting
{
    public class PerformanceTest
    {
        [Test]
        public void StartTest()
        {
            if (this.GetType() == typeof(PerformanceTest))
                Assert.Pass();
            

#if DEBUG
            Assert.Warn("Debug mode is on\n");
            var summary = BenchmarkRunner.Run(this.GetType(),
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
            Assert.IsEmpty(summary.ValidationErrors, "Not critical validation errors");
            Assert.IsTrue(summary.Reports.All(r => r.Success), "One of benchmarks has failed");
        }
    }
}