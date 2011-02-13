using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Ninject;
using NUnit.Framework;

namespace LiskovChecker.Tests.Unit
{
    [TestFixture]
    class LiskovCheckTests
    {
        private static StandardKernel _kernel;

        static LiskovCheckTests()
        {
            _kernel = new StandardKernel();
        }

        const string ZooAssemblyDll = @"..\..\..\Zoo\bin\Release\Zoo.dll";

        [Test]
        public void ShouldProduceLiskovValidationPhrases()
        {
            var liskovChecker = _kernel.Get<LiskovChecker>();

            List<Type> typesInAssembly = GetTypesIn(ZooAssemblyDll);

            List<string> validationResults = liskovChecker.Check(typesInAssembly);

            Assert.IsNotNull(validationResults);
            Assert.AreEqual(4, validationResults.Count);
            Assert.IsFalse(validationResults.Contains("It's pointless to check for Liskov here. Basetype is Object or null."), 
                "Don't check any types which have base type 'Object'");
        }

        [Test]
        public void ShouldReturnCorrectPhraseIfNoSubclassingWasDone()
        {
            var liskovChecker = _kernel.Get<LiskovChecker>();

            //String and ValueType both have Object as base class. We aren't checking against base class Object.
            List<Type> typesInAssembly = new List<Type>()
                                             {
                                                 typeof (String),
                                                 typeof (ValueType)
                                             };


            List<string> validationResults = liskovChecker.Check(typesInAssembly);

            Assert.IsNotNull(validationResults);
            Assert.AreEqual(1, validationResults.Count);
            Assert.IsTrue(validationResults.Contains("No subclassing was used in this assembly."));
        }

        [Test]
        public void ShouldReturnCorrectPhraseIfNoTypes()
        {
            var liskovChecker = _kernel.Get<LiskovChecker>();

            //String and ValueType both have Object as base class. We aren't checking against base class Object.
            List<Type> typesInAssembly = new List<Type>();

            List<string> validationResults = liskovChecker.Check(typesInAssembly);

            Assert.IsNotNull(validationResults);
            Assert.AreEqual(1, validationResults.Count);
            Assert.IsTrue(validationResults.Contains("No subclassing was used in this assembly."));
        }

        [Test]
        public void ShouldValidateNinjectDll()
        {
            var liskovChecker = _kernel.Get<LiskovChecker>();

            List<Type> typesInAssembly = GetTypesIn("Ninject.dll");

            List<string> validationResults = liskovChecker.Check(typesInAssembly);

            Assert.IsNotNull(validationResults);
            Assert.AreEqual(49, validationResults.Count);
            Assert.IsFalse(validationResults.Contains("It's pointless to check for Liskov here. Basetype is Object or null."),
                "Don't check any types which have base type 'Object'");
        }
        
        private List<Type> GetTypesIn(string assemblyLocation)
        {
            Assembly asm = Assembly.LoadFrom(assemblyLocation);
            var typesInZooDll = asm.GetTypes();
            return new List<Type>(typesInZooDll);
        }
    }
}
