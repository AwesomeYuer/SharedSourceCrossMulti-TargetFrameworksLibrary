namespace CrossMultiTargetFrameworksLibrary.Shared
{
    using System;
    using System.Runtime.InteropServices;

    public class SharedClass
    {
        public void Test()
        {
            #if NETFRAMEWORK
            #if NET48
                        Console.WriteLine($"Target framework: {RuntimeInformation.FrameworkDescription}");
            #endif
            #elif NETCOREAPP
            #if NETCOREAPP2_2
                        Console.WriteLine($"Target framework: {RuntimeInformation.FrameworkDescription}");
            #elif NETCOREAPP3_0
                        Console.WriteLine($"Target framework: {RuntimeInformation.FrameworkDescription}");
            #endif
            #endif
        }
    }
}
