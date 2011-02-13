using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace LiskovChecker.Tests.Acceptance
{

   [Binding]
    public class FileContainingAssemblyNotFoundTestSteps
    {
       private string _assemblyToCheck;
       private List<string> _consoleOutput = null;

       [Given(@"a DLL named SomeAssembly\.dll which doesn't exist")]
        public void GivenADLLNamedSomeAssembly_DllWhichDoesnTExist()
        {
           _assemblyToCheck = "SomeAssembly.dll";
        }

        [When(@"I run ""liskovcheck SomeAssembly\.dll""")]
        public void WhenIRunLiskovCheckerSomeAssembly_Dll()
        {
            _consoleOutput = ConsoleRunner.Run("liskovcheck", _assemblyToCheck);
        }

        [Then(@"the message ""No file found at 'SomeAssembly\.dll'""")]
        public void ThenTheMessageNoFileFoundAtSomeAssembly_Dll()
        {
            Assert.IsNotNull(_consoleOutput.Find(line => line.Contains("File 'SomeAssembly.dll' not found.")));
        }
    }

}
