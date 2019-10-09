using System;

namespace ConsoleApp
{
    using CrossMultiTargetFrameworksLibrary;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Microshaoft;
    using System.Reflection;
    //using Microsoft.AspNetCore.Mvc.ModelBinding;

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

            AppDomain currentDomain = AppDomain.CurrentDomain;

            // This call will fail to create an instance of MyType since the
            // assembly resolver is not set
            InstantiateMyTypeFail(currentDomain);

            //currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);

            currentDomain.AssemblyResolve += (sender, resolveEventArgs) =>
            {
                Console.WriteLine("Resolving...");
                return typeof(MyType).Assembly;
            };

            // This call will succeed in creating an instance of MyType since the
            // assembly resolver is now set.
            InstantiateMyTypeFail(currentDomain);

            // This call will succeed in creating an instance of MyType since the
            // assembly name is valid.
            InstantiateMyTypeSucceed(currentDomain);


            Console.ReadLine();
        }

 

        private static void InstantiateMyTypeFail(AppDomain domain)
        {
            // Calling InstantiateMyType will always fail since the assembly info
            // given to CreateInstance is invalid.
            try
            {
                // You must supply a valid fully qualified assembly name here.
#if NETCOREAPP2_X
                throw new NotSupportedException();
#else
                domain
                    .CreateInstance
                        (
                            "Assembly text name, Version, Culture, PublicKeyToken"
                            , "MyType"
                        );
#endif



            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
        }

        private static void InstantiateMyTypeSucceed(AppDomain domain)
        {
            try
            {
                Assembly assembly = Assembly.GetCallingAssembly();
                string asmname = assembly.FullName;
#if NETCOREAPP2_X
                throw new NotSupportedException();
#else
                domain.CreateInstance(asmname, "MyType");
#endif

            }
            catch (Exception e)
            {
                Console.WriteLine();
                Console.WriteLine(e.Message);
            }
        }

        //private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        //{
        //    Console.WriteLine("Resolving...");
        //    return typeof(MyType).Assembly;
        //}
    }


    public class MyType
    {
        public MyType()
        {
            Console.WriteLine();
            Console.WriteLine("MyType instantiated!");
        }
    }



}
