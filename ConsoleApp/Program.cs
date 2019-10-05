using System;

namespace ConsoleApp
{
    using CrossMultiTargetFrameworksLibrary;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Microshaoft;

    class Program
    {
        static void Main(string[] args)
        {

            OSPlatform OSPlatform
                   = EnumerableHelper
                           .Range
                               (
                                   OSPlatform.Linux
                                   , OSPlatform.OSX
                                   , OSPlatform.Windows
                               )
                           .First
                               (
                                   (x) =>
                                   {
                                       return
                                           RuntimeInformation
                                                   .IsOSPlatform(x);
                                   }
                               );
            var s = $"{nameof(RuntimeInformation.FrameworkDescription)}:{RuntimeInformation.FrameworkDescription}";
            s += "\n";
            s += $"{nameof(RuntimeInformation.OSArchitecture)}:{RuntimeInformation.OSArchitecture.ToString()}";
            s += "\n";
            s += $"{nameof(RuntimeInformation.OSDescription)}:{RuntimeInformation.OSDescription}";
            s += "\n";
            s += $"{nameof(RuntimeInformation.ProcessArchitecture)}:{RuntimeInformation.ProcessArchitecture.ToString()}";
            s += "\n";
            s += $"{nameof(OSPlatform)}:{OSPlatform}";
            Console.WriteLine(s);
            var os = Environment.OSVersion;
            Console.WriteLine("Current OS Information:\n");
            Console.WriteLine("Platform: {0:G}", os.Platform);
            Console.WriteLine("Version String: {0}", os.VersionString);
            Console.WriteLine("Version Information:");
            Console.WriteLine("   Major: {0}", os.Version.Major);
            Console.WriteLine("   Minor: {0}", os.Version.Minor);
            Console.WriteLine("Service Pack: '{0}'", os.ServicePack);

            Console.WriteLine("Hello World!");

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
            Console.ReadLine();
        }
    }
}
