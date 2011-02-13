using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace LiskovChecker.Tests.Unit
{
    [TestFixture]
    class EnglishGrammarTests
    {
        private EnglishGrammar _grammar;

        public EnglishGrammarTests()
        {
            _grammar = new EnglishGrammar();
        }

        [Test]
        public void ShouldPutCorrectPrefixBeforeWordIfVovelOrConsonant()
        {
           
            //Vovels:
            Assert.AreEqual("an", _grammar.DetermineWhetherToUseAOrAnInFrontOf("automobile"));
            Assert.AreEqual("an", _grammar.DetermineWhetherToUseAOrAnInFrontOf("easel"));
            Assert.AreEqual("an", _grammar.DetermineWhetherToUseAOrAnInFrontOf("Iguana"));
            Assert.AreEqual("an", _grammar.DetermineWhetherToUseAOrAnInFrontOf("ontology"));
            Assert.AreEqual("an", _grammar.DetermineWhetherToUseAOrAnInFrontOf("Understatement"));

            //A consonant:
            Assert.AreEqual("a", _grammar.DetermineWhetherToUseAOrAnInFrontOf("balloon"));

            //A letter:
            Assert.AreEqual("a", _grammar.DetermineWhetherToUseAOrAnInFrontOf("c"));
            Assert.AreEqual("an", _grammar.DetermineWhetherToUseAOrAnInFrontOf("u"));

            //A number:
            Assert.AreEqual("a", _grammar.DetermineWhetherToUseAOrAnInFrontOf("1"));

            //Something non-alpha:
            Assert.AreEqual("a", _grammar.DetermineWhetherToUseAOrAnInFrontOf("_"));

        }

        [Test, ExpectedException(typeof(EnglishGrammar.EnglishGrammarException), ExpectedMessage = "No empty words. Or nulls.")]
        public void ShouldBlowUpIfNoWordGiven()
        {
            Assert.AreEqual("a", _grammar.DetermineWhetherToUseAOrAnInFrontOf(""));

        }
    }
}
