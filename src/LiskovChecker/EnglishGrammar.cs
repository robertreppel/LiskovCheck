using System;

namespace LiskovChecker
{
    internal class EnglishGrammar   
    {
        /// <summary>
        /// Foreign languages are the bane of my existence.
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public string DetermineWhetherToUseAOrAnInFrontOf(string word)
        {
            if(String.IsNullOrEmpty(word))
            {
                throw new EnglishGrammarException("No empty words. Or nulls.");
            }
            string putBeforeWord;
            string firstLetter = GetFirstLetterOf(word);

            if (IsVovel(firstLetter))
            {
                putBeforeWord = "an";
            }
            else
            {
                putBeforeWord = "a";
            }
            return putBeforeWord;
        }

        private string GetFirstLetterOf(string word)
        {
            return word
                .Substring(0, 1)
                .ToLower();
        }

        private bool IsVovel(string firstLetter)
        {
            bool baseTypeNameStartsWithVovel = false;
            if (firstLetter.Equals("a")
                || firstLetter.Equals("e")
                || firstLetter.Equals("i")
                || firstLetter.Equals("o")
                || firstLetter.Equals("u")
                )
            {
                baseTypeNameStartsWithVovel = true;
            }
            return baseTypeNameStartsWithVovel;
        }

        internal class EnglishGrammarException : Exception
        {
            public EnglishGrammarException(string message) : base(message)
            {
            }
        }
    }
}