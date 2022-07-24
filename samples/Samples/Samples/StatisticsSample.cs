using MatrixDotNet;
using MatrixDotNet.Extensions.Statistics;
using MatrixDotNet.Extensions.Statistics.TableSetup;

namespace Samples.Samples
{
    public class StatisticsSample
    {
        public static void Run()
        {
            // initialize matrix.
            Matrix<double> matrix = new[,]
            {
                {5.7, 6.7, 6.2, 7},
                {6.7, 7.7, 7.2, 11},
                {7.7, 8.7, 8.2, 6},
                {8.7, 9.7, 9.2, 4},
                {9.7, 10.7, 10.2, 2}
            };

            // choose column which will be mark.
            TableIntervals[] table = { TableIntervals.Xi, TableIntervals.Ni };

            // sets configuration for intervals
            var configIntervals = new ConfigIntervals<double>(matrix, table);

            // calculate matrix with intervals.
            var intervals = new Intervals<double>(configIntervals);

            // gets interval row mean
            intervals.GetIntervalRowMean();

            // gets modal interval
            double modal = intervals.ModalInterval;

            // gets median interval
            double median = intervals.MedianInterval;
        }
    }
}