using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Ninject;

namespace LiskovChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Count() == 0)
            {
                ShowHelpInformation();
            }
            else if (!File.Exists(args[0]))
            {
                Console.WriteLine("File '{0}' not found.", args[0]);
                ShowHelpInformation();
            }
            else
            {
                string assemblyDll = args[0];

                List<string> validationResults = CheckForLiskovSubstitutionPrincipleCompliance(assemblyDll);
                OutputToConsole(validationResults);
            }
        }

        private static void ShowHelpInformation()
        {
            Console.WriteLine();
            Console.WriteLine("Liskovcheck semi-automates checking for adherence");
            Console.WriteLine("to the Liskov Substitution Principle.");
            Console.WriteLine();
            Console.WriteLine(@"Usage example: 'liskovcheck 'c:\path\to\assembly\MyAssembly.dll'");
            Console.WriteLine();
            Console.WriteLine("Read the output and see if it makes sense:");
            Console.WriteLine();
            Console.WriteLine(@"  'It looks like a Duck and behaves like an Animal'");
            Console.WriteLine(@"   makes it look more likely that there is strong behavioral subtyping.");
            Console.WriteLine();
            Console.WriteLine(@"  'It looks like a Duck and behaves like a TransistorRadio.'");
            Console.WriteLine(@"   makes it a lot less likely.");
        }

        private static void OutputToConsole(List<string> validationResults)
        {
            foreach (var validationResult in validationResults)
            {
                Console.WriteLine(validationResult);
            }
        }

        private static List<string> CheckForLiskovSubstitutionPrincipleCompliance(string assemblyDll)
        {
            StandardKernel ninjectKernel = new StandardKernel();
            var liskovChecker = ninjectKernel.Get<LiskovChecker>();
            List<Type> typesInAssembly = GetTypesInAssemblyAt(assemblyDll);

            return liskovChecker.Check(typesInAssembly);
        }

        private static List<Type> GetTypesInAssemblyAt(string assemblyLocation)
        {
            Assembly asm = Assembly.LoadFrom(assemblyLocation);
            var typesInZooDll = asm.GetTypes();
            return new List<Type>(typesInZooDll);
        }
    }
}
