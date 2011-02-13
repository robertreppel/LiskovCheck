using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace LiskovChecker.Tests.Acceptance
{
     [Binding]
    public class AdheresToLiskovSubstitutionTestSteps
    {
        private string _assemblyToCheck;
        private List<string> _consoleOutput = null;

        [Given(@"a DLL named Zoo\.dll with a ""Duck"" class which inherits from ""Animal""")]
        public void GivenADLLNamedZoo_DllWithADuckClassWhichInheritsFromAnimal()
        {
            _assemblyToCheck = @"..\..\..\Zoo\bin\Release\Zoo.dll";
        }

        [When(@"I run ""liskovcheck Zoo\.dll""")]
        public void WhenIRunLiskovCheckerZoo_Dll()
        {
            _consoleOutput = ConsoleRunner.Run("liskovcheck", _assemblyToCheck);
        }

        [Then(@"the words ""It looks like a Duck and behaves like an Animal"" should be on the screen\.")]
        public void ThenTheWordsItLooksLikeADuckQuacksLikeADuckAndIsAnAnimalShouldBeOnTheScreen_()
        {
            Assert.IsNotNull(_consoleOutput.Find(line => line.Contains("It looks like a Duck and behaves like an Animal")));
        }

    }

}
