using System;
using System.Collections.Generic;
using System.Reflection;
using Moq;
using NUnit.Framework;

namespace LiskovChecker.Tests.Unit
{
    [TestFixture]
    class LiskovSubstitutionPrincipleTests
    {
        const string AssemblyDll = @"..\..\..\Zoo\bin\Release\Zoo.dll";

        [Test]
        public void ShouldGetLiskovValidationPhraseFromType()
        {
            List<Type> typesInZoo = GetTypesIn(AssemblyDll);
            var liskovSubstitutionPrinciple = new LiskovSubstitutionPrinciple() {EnglishGrammar = new EnglishGrammar()};

            var duckType = typesInZoo.Find(t => t.Name.Equals("Duck"));
            string validationPhrase = liskovSubstitutionPrinciple.Validate(duckType);
            Assert.AreEqual("It looks like a Duck and behaves like an Animal.", validationPhrase);

            var merganserType = typesInZoo.Find(t => t.Name.Equals("MerganserDuck"));
            validationPhrase = liskovSubstitutionPrinciple.Validate(merganserType);
            Assert.AreEqual("It looks like a MerganserDuck and behaves like a TransistorRadio.", validationPhrase);

            var automobile = typesInZoo.Find(t => t.Name.Equals("Automobile"));
            validationPhrase = liskovSubstitutionPrinciple.Validate(automobile);
            Assert.AreEqual("It looks like an Automobile and behaves like an Animal.", validationPhrase);
        }

        [Test]
        public void ShouldReturnMeaninglessLiskovMessageIfNoBaseTypeOrTypeIsObject()
        {
            List<Type> typesInZoo = GetTypesIn(AssemblyDll);
            var liskovSubstitutionPrinciple = new LiskovSubstitutionPrinciple() {EnglishGrammar = new EnglishGrammar()};

            var duckType = typesInZoo.Find(t => t.Name.Equals("Object"));
            string validationPhrase = liskovSubstitutionPrinciple.Validate(duckType);

            var animal = typesInZoo.Find(t => t.Name.Equals("Animal"));
            validationPhrase = liskovSubstitutionPrinciple.Validate(animal);
            Assert.IsTrue(validationPhrase.Contains("It's pointless to check for Liskov here."));
        }


        [Test]
        public void ShouldHandleNestedNamespaces()
        {
            List<Type> typesInZoo = GetTypesIn(AssemblyDll);
            var liskovSubstitutionPrinciple = new LiskovSubstitutionPrinciple() { EnglishGrammar = new EnglishGrammar()};

            var birdClass = typesInZoo.Find(t => t.Name.Equals("Bird"));
            string validationPhrase = liskovSubstitutionPrinciple.Validate(birdClass);
            Assert.AreEqual("It looks like a Bird and behaves like an Aves.", validationPhrase);
        }

        private List<Type> GetTypesIn(string assemblyLocation)
        {
            Assembly asm = Assembly.LoadFrom(assemblyLocation);
            var typesInZooDll = asm.GetTypes();
            return new List<Type>(typesInZooDll);
        }
    }
}
