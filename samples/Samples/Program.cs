
using MatrixDotNet;
using MatrixDotNet.Extensions;

namespace Samples
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Matrix<double> matrix = new double[3,3]
            {
                { 4.43345, 12323.2423,-16243},
                {1242, -3723423423423443,-4332423},
                {-16234,-43432,98234234}
            };

            matrix.Pretty();
            matrix.SaveAndOpenAsync("test");
        }
    }
}