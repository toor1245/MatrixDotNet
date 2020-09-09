namespace MatrixDotNet.Extensions.Core.Optimization
{
    internal struct Data
    {
#if OS_WINDOWS
        internal const string Path = "Imports\\MatrixDotNet_Core_Intrinsics.dll";
#endif
#if OS_LINUX
        internal const string Path = "Imports/MatrixDotNet_Core_Intrinsics.dll";
#endif
    }
}