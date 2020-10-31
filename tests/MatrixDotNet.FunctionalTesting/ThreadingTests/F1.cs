namespace MatrixDotNet.FunctionalTesting.ThreadingTests
{
    public class F1
    {
        public int N { get; }

        public F1(int n)
        {
            N = n;
        }
        

        // E = A + B + C + D * (MA * MD)
        public void RunF1()
        {
            var A = new Vector<int>(N);
            var B = new Vector<int>(N);
            var C = new Vector<int>(N);
            var D = new Vector<int>(N);
            var MA = new Matrix<int>(N,N,1); 
            var MD = new Matrix<int>(N,N,1);

            var E = A + B + C + D.Array * (MA * MD);
        }
    }
}