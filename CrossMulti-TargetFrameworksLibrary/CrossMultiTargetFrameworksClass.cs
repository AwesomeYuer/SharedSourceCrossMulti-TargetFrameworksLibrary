namespace CrossMultiTargetFrameworksLibrary
{
    using System;
    using System.Runtime.InteropServices;
    public class CrossMultiTargetFrameworksClass
    {
        public void Test()
        {
#if NETFRAMEWORK
#if NET4X
            Console.WriteLine($"Target framework: {RuntimeInformation.FrameworkDescription}");
#endif
#elif NETCOREAPP
#if NETCOREAPP2_X
            Console.WriteLine($"Target framework: {RuntimeInformation.FrameworkDescription}");
#elif NETCOREAPP3_X
            Console.WriteLine($"Target framework: {RuntimeInformation.FrameworkDescription}");
#endif
#endif
        }
    }
}
