using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace LiskovChecker.Tests.Acceptance
{
    [Binding]
    class DoesntAdhereToLiskovSubstitutionTestSteps
    {
        private string _assemblyToCheck;
        private List<string> _consoleOutput = null;

        [Given(@"a DLL named Zoo\.dll with a ""MerganserDuck"" class which inherits from ""TransistorRadio""")]
        public void GivenADLLNamedZoo_DllWithADuckClassWhichInheritsFromTransistorRadio()
        {
            _assemblyToCheck = @"..\..\..\Zoo\bin\Release\Zoo.dll";
        }

        [When(@"I run liskovcheck\.exe with the argument 'Zoo\.dll'""")]
        public void WhenIRunLiskovChecker_ExeWithTheArgumentZoo_Dll()
        {
            _consoleOutput = ConsoleRunner.Run("liskovcheck", _assemblyToCheck);
        }

        [Then(@"the words ""It looks like a MerganserDuck and behaves like a TransistorRadio"" should be on the screen\.")]
        public void ThenTheWordsItLooksLikeADuckQuacksLikeADuckAndIsATransistorRadio_ShouldBeOnTheScreen_()
        {
            Assert.IsNotNull(_consoleOutput.Find(line => line.Contains("It looks like a MerganserDuck and behaves like a TransistorRadio")));
        }
    }
}
