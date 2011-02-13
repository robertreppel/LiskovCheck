using System;
using Ninject;

namespace LiskovChecker
{
    internal class LiskovSubstitutionPrinciple
    {
        private EnglishGrammar _englishGrammar = null;

        [Inject]
        public EnglishGrammar EnglishGrammar
        {
            set { _englishGrammar = value; }
        }

        public string Validate(Type type)
        {
            if (DoesntInheritFromAnythingInteresting(type))
            {
                return "It's pointless to check for Liskov here. (The basetype is Object)";
            }

            if (_englishGrammar != null)
            {
                return SentenceWhichExplainsBehaviorOf(type);
            }
            throw new LiskovSubstitutionException("No grammar specified. We need that to build the sentence which talks about behavior. Please initialize the 'EnglishGrammar' property.");
        }

        private bool DoesntInheritFromAnythingInteresting(Type type)
        {
            string baseType = BaseTypeOf(type);   
            return type == null || baseType.Equals("Object") || String.IsNullOrEmpty(baseType);
        }

        private string SentenceWhichExplainsBehaviorOf(Type type)
        {
            string typeName = type.Name;
            string vowelPrefixForType = _englishGrammar.DetermineWhetherToUseAOrAnInFrontOf(typeName);

            string baseTypeName = BaseTypeOf(type);
            string vovelPrefixForBaseType = _englishGrammar.DetermineWhetherToUseAOrAnInFrontOf(baseTypeName);

            const string validationPhraseTemplate = "It looks like {0} {1} and behaves like {2} {3}.";
            return String.Format(validationPhraseTemplate, vowelPrefixForType, typeName, vovelPrefixForBaseType, baseTypeName);
        }

        private string BaseTypeOf(Type type)
        {
            if(type == null)
            {
                return "";
            }
            if (type.BaseType != null)
            {
                if (type.BaseType.Name != null)
                {
                    string baseTypeName = type.BaseType.Name;
                    return baseTypeName;
                }
            }
            return "";
        }

        internal class LiskovSubstitutionException : Exception
        {
            public LiskovSubstitutionException(string message) : base(message)
            {
            }
        }
    
    }

}