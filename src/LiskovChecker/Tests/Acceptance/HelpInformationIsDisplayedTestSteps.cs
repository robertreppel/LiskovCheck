using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace LiskovChecker.Tests.Acceptance
{
    [Binding]
    public class HelpInformationIsDisplayedTestSteps
    {
        private string _assemblyToCheck;
        private List<string> _consoleOutput = null;

        [Given(@"no command line arguments")]
        public void GivenNoCommandLineArguments()
        {
            _assemblyToCheck = "";
        }

        [When(@"I run ""liskovcheck""")]
        public void WhenIRunLiskovCheck()
        {
            _consoleOutput = ConsoleRunner.Run("liskovcheck", _assemblyToCheck);

        }

        [Then(@"I should see ""Usage example: 'liskovcheck 'c:\\path\\to\\assembly\\MyAssembly\.dll'""")]
        public void ThenIShouldSeeUsageExampleLiskovCheckCPathToAssemblyMyAssembly_Dll()
        {
            Assert.IsNotNull(_consoleOutput.Find(line => line.Contains(@"Usage example: 'liskovcheck 'c:\path\to\assembly\MyAssembly.dll'")));
        }

        [Then(@"""Semi-automatic check for adherence to the Liskov Substitution Principle""")]
        public void ThenSemi_AutomaticCheckForAdherenceToTheLiskovSubstitutionPrinciple()
        {
            Assert.IsNotNull(_consoleOutput.Find(line => line.Contains(@"Liskovcheck semi-automates checking for adherence")));
            Assert.IsNotNull(_consoleOutput.Find(line => line.Contains(@"to the Liskov Substitution Principle.")));
        }
    }
}
